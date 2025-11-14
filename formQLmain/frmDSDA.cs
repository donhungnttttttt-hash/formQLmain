using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.Expando;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace formQLmain
{
    public partial class frmDSDA : Form
    {
       
        public frmDSDA()
        {
            InitializeComponent();
        }
        public bool Expand = false; // khai báo biến Expand 
        public bool Expand2 = false; // khai báo biến Expand2 
        public bool Expandmenu = false; // khai báo biến Expand2 
        private void btnDrop2_Click(object sender, EventArgs e)
        {
            QLDAdrop.Start();
            Console.WriteLine(Expand2);
            Console.ReadLine();
        }

        private void btnDSDA_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTLKT_Click(object sender, EventArgs e)
        {
            frmTLKT f = new frmTLKT();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmQLSV f = new frmQLSV();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmQLTK f = new frmQLTK();
            f.Show();
        }

        private void QLDAdrop_Tick(object sender, EventArgs e)
        {
            if (Expand2 == false)
            {
                dropdown2.Height += 15;
                if (dropdown2.Height >= dropdown2.MaximumSize.Height)
                {

                    QLDAdrop.Stop();
                    Expand2 = true;
                }
            }
            else
            {
                dropdown2.Height -= 15;
                if (dropdown2.Height <= dropdown2.MinimumSize.Height)
                {

                    QLDAdrop.Stop();
                    Expand2 = false;
                }
            }
        }

        private void QLDLdrop_Tick(object sender, EventArgs e)
        {

            if (Expand == false)
            {
                dropdown.Height += 15;
                if (dropdown.Height >= dropdown2.MaximumSize.Height)
                {

                    QLDLdrop.Stop();
                    Expand = true;
                }
            }
            else
            {
                dropdown.Height -= 15;
                if (dropdown.Height <= dropdown2.MinimumSize.Height)
                {

                    QLDLdrop.Stop();
                    Expand = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btndropQLDL_Click(object sender, EventArgs e)
        {
            QLDLdrop.Start();
            Console.WriteLine(Expand);
            Console.ReadLine();
        }
        private ModifyDanhSachDoAn repo;
        private string maDoAnCu = "";   // lưu MADOAN gốc để cho phép đổi khóa khi sửa
        private string maTLCu = ""; // lưu mã TLBC gốc để cho phép đổi khóa khi sửa
        // ================ LOAD FORM ================
        // ================ LOAD FORM ================
        private void frmDSDA_Load(object sender, EventArgs e) /// from load 
        {
            repo = new ModifyDanhSachDoAn();

            try
            {
                grdDoAn.AutoGenerateColumns = true; // nếu không tự add cột
                grdDoAn.DataSource = repo.getAllDoAn();
                grdDoAn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grdDoAn.MultiSelect = false;

                grdDoAn.CellClick += grdDoAn_CellClick;
                grdDoAn.SelectionChanged += grdDoAn_SelectionChanged;

                // DateTimePicker cho phép null bằng checkbox
                dtpick_Nambaove.ShowCheckBox = true;

                if (grdDoAn.Rows.Count > 0)
                {
                    grdDoAn.Rows[0].Selected = true;
                    SyncFromGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 
        // ================ THÊM ================
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            var da = new DanhSachDoAn(
                txtbox_MaDoAn.Text.Trim(),
                txtbox_TenDeTai.Text.Trim(),
                txtbox_MaSV.Text.Trim(),
                txtbox_GVHD.Text.Trim(),
                dtpick_Nambaove.Checked ? dtpick_Nambaove.Value.Date : (DateTime?)null,
                txtbox_MaTLBC.Text.Trim(),
                txtbox_TomTat.Text.Trim(),
                txtbox_MaTLBC2.Text.Trim(),
                txtbox_FileBC.Text.Trim(),
                txtbox_Slide.Text.Trim(),
                txtbox_LyLich.Text.Trim(),
                txtbox_MaTK.Text.Trim(),
                txtbox_TK.Text.Trim()

            );

            string err;
            if (repo.insert(da, out err))
            {
                MessageBox.Show("Thêm đồ án thành công.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                grdDoAn.DataSource = repo.getAllDoAn();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Thêm thất bại.\nLỗi: " + err,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // ================ XÓA ================
        private void btnXoa_Click_1(object sender, EventArgs e)
        {
        }

        // ================ SỬA (có thể đổi MADOAN) ================
        private void btnSua_Click_1(object sender, EventArgs e) // Sửa cũ 
        {
            
        }
        // ================== ĐỒNG BỘ GRID → CONTROL ==================
        private void grdDoAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) SyncFromGrid();
        }

        private void grdDoAn_SelectionChanged(object sender, EventArgs e)
        {
            SyncFromGrid();
        }

        private void SyncFromGrid()
        {
            if (grdDoAn.CurrentRow == null || grdDoAn.CurrentRow.IsNewRow) return;

            var row = grdDoAn.CurrentRow;

            txtbox_MaDoAn.Text = row.Cells["MADOAN"].Value?.ToString() ?? "";
            txtbox_TenDeTai.Text = row.Cells["TENDETAI"].Value?.ToString() ?? "";
            txtbox_MaSV.Text = row.Cells["MASINHVIEN"].Value?.ToString() ?? "";
            txtbox_GVHD.Text = row.Cells["GVHD"].Value?.ToString() ?? "";
            txtbox_MaTLBC.Text = row.Cells["MATAILIEUBC"].Value?.ToString() ?? "";
            txtbox_TomTat.Text = row.Cells["TOMTAT"].Value?.ToString() ?? "";
            //
            txtbox_MaTLBC2.Text = row.Cells["MATAILIEUBC"].Value?.ToString() ?? "";
            txtbox_FileBC.Text = row.Cells["FILEBC"].Value?.ToString() ?? "";
            txtbox_Slide.Text = row.Cells["SLIDE"].Value?.ToString() ?? "";
            txtbox_LyLich.Text = row.Cells["LY_LICH"].Value?.ToString() ?? "";
            txtbox_MaTK.Text = row.Cells["MATUKHOA"].Value?.ToString() ?? "";
            txtbox_TK.Text = row.Cells["TUKHOA"].Value?.ToString() ?? "";

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

        // ================== TIỆN ÍCH ==================
        private void ClearInputs()
        {
            txtbox_MaDoAn.Clear();
            txtbox_TenDeTai.Clear();
            txtbox_MaSV.Clear();
            txtbox_GVHD.Clear();
            txtbox_MaTLBC.Clear();
            txtbox_MaTLBC2.Clear();
            txtbox_TomTat.Clear();
            dtpick_Nambaove.Checked = false;
            txtbox_MaTLBC.Clear();
            txtbox_FileBC.Clear();
            txtbox_Slide.Clear();
            txtbox_LyLich.Clear();
            txtbox_MaTK.Clear();
            txtbox_TK.Clear();

            maDoAnCu = "";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string kw = textbox_TimKiem.Text.Trim();
            if (string.IsNullOrEmpty(kw))
            {
                grdDoAn.DataSource = repo.getAllDoAn();
                return;
            }
            grdDoAn.DataSource = repo.searchDoAn(kw);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            textbox_TimKiem.Clear();
            grdDoAn.DataSource = repo.getAllDoAn();
        }

        private void textbox_TimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // tránh tiếng 'ding'
                btnTimKiem_Click(sender, e);
            }
        }

        private void grdDoAn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e) // nút thêm giả 
        {
            int i = grdDoAn.Rows.Count - 1; //#########
            grdDoAn.CurrentCell = grdDoAn[0, i]; // nhảy đến dòng i (tức là dòng cuối  dòng cuối 
            MessageBox.Show("Bạn hãy nhập thông tin  vào các ô thông tin, sau đó nhấn nút Lưu");
            txtbox_MaDoAn.Focus();    // chuyển con trỏ đến textbox mã nhóm 
            ClearInputs();
            //#########################################################3
            btnLuu.Visible = true; // hiện nút Lưu lên 
            // nút lưu ở đây là nút thêm thật 
            // thêm mới là thêm giả 
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //
            string maSV = txtbox_MaSV.Text.Trim();

            // Nếu để trống
            if (string.IsNullOrEmpty(maSV))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên.", "Cảnh báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var da = new DanhSachDoAn(
                null,                               // MADOAN -> để null
                txtbox_TenDeTai.Text.Trim(),        // Tên đề tài
                txtbox_MaSV.Text.Trim(),            // Mã sinh viên
                txtbox_GVHD.Text.Trim(),            // GVHD
                dtpick_Nambaove.Checked ? dtpick_Nambaove.Value.Date : (DateTime?)null,

                null,                               // MATAILIEUBC (1) -> để null
                txtbox_TomTat.Text.Trim(),          // Tóm tắt

                null,                               // MATAILIEUBC (2) -> bỏ
                txtbox_FileBC.Text.Trim(),          // FILE BC
                txtbox_Slide.Text.Trim(),           // Slide
                txtbox_LyLich.Text.Trim(),          // Lý lịch

                null,                               // MATUKHOA -> để null
                txtbox_TK.Text.Trim()               // Từ khóa
            );

            string err;
            if (repo.insert(da, out err))
            {
                MessageBox.Show("Thêm đồ án thành công.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                grdDoAn.DataSource = repo.getAllDoAn();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Thêm thất bại.\nLỗi: " + err,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maDoAnCu) && grdDoAn.CurrentRow != null)
                maDoAnCu = grdDoAn.CurrentRow.Cells["MADOAN"].Value?.ToString();

            if (string.IsNullOrWhiteSpace(maDoAnCu))
            {
                MessageBox.Show("Vui lòng chọn đồ án cần sửa từ bảng.",
                                "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var da = new DanhSachDoAn(
                txtbox_MaDoAn.Text.Trim(),
                txtbox_TenDeTai.Text.Trim(),
                txtbox_MaSV.Text.Trim(),
                txtbox_GVHD.Text.Trim(),
                dtpick_Nambaove.Checked ? dtpick_Nambaove.Value.Date : (DateTime?)null,
                txtbox_MaTLBC.Text.Trim(),
                txtbox_TomTat.Text.Trim(),
                txtbox_MaTLBC2.Text.Trim(),
                txtbox_FileBC.Text.Trim(),
                txtbox_Slide.Text.Trim(),
                txtbox_LyLich.Text.Trim(),
                txtbox_MaTK.Text.Trim(),
                txtbox_TK.Text.Trim()
            );

            string err;
            if (repo.update(da, maDoAnCu, out err))
            {
                MessageBox.Show("Cập nhật đồ án thành công.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                grdDoAn.DataSource = repo.getAllDoAn();
                maDoAnCu = da.MaDoAn; // cập nhật lại mã gốc nếu tiếp tục sửa
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.\n" + err,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbox_MaDoAn.Text))
            {
                MessageBox.Show("Bạn chưa chọn đồ án để xóa!",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa đồ án {txtbox_MaDoAn.Text.Trim()} không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.No)
                return;

            // Tạo object DanhSachDoAn chỉ chứa thông tin cần thiết cho xóa toàn bộ
            DanhSachDoAn da = new DanhSachDoAn()
            {
                MaDoAn = txtbox_MaDoAn.Text.Trim(),
                MaTaiLieuBC = txtbox_MaTLBC.Text.Trim(),
                MaTuKhoa = txtbox_MaTK.Text.Trim()
            };

            string err;
            if (repo.delete(da, out err))
            {
                MessageBox.Show("Xóa thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                grdDoAn.DataSource = repo.getAllDoAn();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Xóa thất bại.\nLỗi: " + err,
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
//ghi chú