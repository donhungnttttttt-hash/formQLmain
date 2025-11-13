using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.Expando;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace formQLmain
{
    public partial class frmCTDA : Form
    {
        public bool Expand = false; // khai báo biến Expand 
        public bool Expand2 = false; // khai báo biến Expand2 
        public bool Expandmenu = false; // khai báo biến Expand2 
        public frmCTDA()
        {
            InitializeComponent();
        }
        private ModifyChiTietDoAn repo;
        Modify ModifyChiTietDoAn;
        ChiTietDoAn ChiTietDoAn ;
        private string maDoAnCu = "";   // lưu MADOAN gốc để cho phép đổi khóa khi sửa
        private void frmCTDA_Load(object sender, EventArgs e)
        {
            repo = new ModifyChiTietDoAn();

            try
            {
                grdDoAn.AutoGenerateColumns = true; // nếu không tự add cột
                grdDoAn.DataSource = repo.getAllDoAn();
                grdDoAn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grdDoAn.MultiSelect = false;

                //grdDoAn.CellClick += grdDoAn_CellClick;
                //grdDoAn.SelectionChanged += grdDoAn_SelectionChanged;

                // DateTimePicker cho phép null bằng checkbox
                dtpick_Nambaove.ShowCheckBox = true;

                if (grdDoAn.Rows.Count > 0)
                {
                    grdDoAn.Rows[0].Selected = true;
                   // SyncFromGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================ THÊM ================
        
        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtbox_Slide_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtbox_FileBC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTuKhoa_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtbox_LyLich_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btndropQLDL_Click(object sender, EventArgs e)
        {
            QLDLdrop.Start();
            Console.WriteLine(Expand);
            Console.ReadLine();
        }

        private void QLDLdrop_Tick(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            frmQLSV f = new frmQLSV();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            frmQLTK f = new frmQLTK();
            f.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            menutimer.Start();
            Console.WriteLine(Expandmenu);
            Console.ReadLine();
            panel11.Visible = true;
            pnlgrid.Left = 200; //
        }

        private void menutimer_Tick(object sender, EventArgs e)
        {
            // đã làm được 
            if (Expandmenu == false)
            {
                panelMenu.Width += 25;
                if (panelMenu.Width >= panelMenu.MaximumSize.Width)
                {

                    menutimer.Stop();
                    Expandmenu = true;
                    panel11.Visible = true;
                    pnlgrid.Left = 245; //

                }
            }
            else
            {
                panelMenu.Width -= 25;
                if (panelMenu.Width <= panelMenu.MinimumSize.Width)
                {
                    menutimer.Stop();
                    Expandmenu = false;
                    panel11.Visible = false;
                    pnlgrid.Left = 73;
                    //141

                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            menutimer.Start();
            Console.WriteLine(Expandmenu);
            Console.ReadLine();
            panel11.Visible = true;
            pnlgrid.Left = 245;
        }

        private void txtbox_TenDeTai_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            int i = grdDoAn.Rows.Count - 1; //#########
            grdDoAn.CurrentCell = grdDoAn[0, i]; // nhảy đến dòng i (tức là dòng cuối  dòng cuối 
            MessageBox.Show("Bạn hãy nhập thông tin  vào các ô thông tin, sau đó nhấn nút Lưu");
            txtbox_TenDeTai.Focus();    // chuyển con trỏ đến textbox mã nhóm 
            ClearInputs();
            //#########################################################3
            btnLuu.Visible = true; // hiện nút Lưu lên 
            // nút lưu ở đây là nút thêm thật 
            // thêm mới là thêm giả 
        }
        private void ClearInputs()
        {
            txtbox_MaDoAn.Clear();
            txtbox_TenDeTai.Clear();
            txtbox_MaSV.Clear();
            txtbox_GVHD.Clear();
            txtbox_MaTLBC.Clear();
            txtbox_TomTat.Clear();
            dtpick_Nambaove.Checked = false;
            txtbox_FileBC.Clear();
            txtbox_Slide.Clear();
            txtbox_LyLich.Clear();
            txtbox_TuKhoa.Clear();
           // maDoANCu = "";
            
        }
        // ================== ĐỒNG BỘ GRID → CONTROL ==================
        

        
        
        

        private void grdDoAn_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) SyncFromGrid();
        }

        private void grdDoAn_SelectionChanged_1(object sender, EventArgs e)
        {
            SyncFromGrid();
        }
        // hàm lấy giá trị từ text box 
        private void SyncFromGrid()
        {

            if (grdDoAn.CurrentRow == null || grdDoAn.CurrentRow.IsNewRow) return;

            var row = grdDoAn.CurrentRow;

            txtbox_MaDoAn.Text = row.Cells["MADOAN"].Value?.ToString() ?? "";
            txtbox_TenDeTai.Text = row.Cells["TENDETAI"].Value?.ToString() ?? "";
            txtbox_MaSV.Text = row.Cells["MASINHVIEN"].Value?.ToString() ?? "";
            txtbox_TenSV.Text = row.Cells["HOTEN"].Value?.ToString() ?? "";
            txtbox_GVHD.Text = row.Cells["GVHD"].Value?.ToString() ?? "";
            txtbox_MaTLBC.Text = row.Cells["MATAILIEUBC"].Value?.ToString() ?? "";
            txtbox_TomTat.Text = row.Cells["TOMTAT"].Value?.ToString() ?? "";
            txtbox_FileBC.Text = row.Cells["FILEBC"].Value?.ToString() ?? "";
            txtbox_Slide.Text = row.Cells["SLIDE"].Value?.ToString() ?? "";
            txtbox_LyLich.Text = row.Cells["LY_LICH"].Value?.ToString() ?? "";
            txtbox_TuKhoa.Text = row.Cells["TUKHOA"].Value?.ToString() ?? "";

            // DateTimePicker: set null/giá trị
            var cell = row.Cells["NAMBAOVE"].Value;
            if (cell == null || cell == DBNull.Value)
            {
                dtpick_Nambaove.Checked = false;
            }
            else
            {
                dtpick_Nambaove.Checked = true;
                dtpick_Nambaove.Value = Convert.ToDateTime(cell);
            }

            maDoAnCu = txtbox_MaDoAn.Text;
        }

        private void btnLuu_Click(object sender, EventArgs e) // nút thêm real 
        {
            var da = new ChiTietDoAn(
        txtbox_MaDoAn.Text.Trim(),
        txtbox_TenDeTai.Text.Trim(),
        txtbox_MaSV.Text.Trim(),
        txtbox_GVHD.Text.Trim(),
        dtpick_Nambaove.Checked ? dtpick_Nambaove.Value.Date : (DateTime?)null,
        txtbox_MaTLBC.Text.Trim(),
        txtbox_TomTat.Text.Trim(),
        txtbox_FileBC.Text.Trim(),
        txtbox_Slide.Text.Trim(),
        txtbox_LyLich.Text.Trim(),
        Guid.NewGuid().ToString(), // Sinh mã từ khóa tự động, tránh trùng (hoặc bạn có thể nhập thủ công)
        txtbox_TuKhoa.Text.Trim());

            // Gọi hàm insert  thêm dữ liệu trong lớp ModifyChiTietDoAn
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtbox_GVHD_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
