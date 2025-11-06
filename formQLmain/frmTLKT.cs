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
    public partial class frmTLKT : Form
    {
        
        public frmTLKT()
        {
            InitializeComponent();
        }
        public bool Expand = false; // khai báo biến Expand 
        public bool Expand2 = false; // khai báo biến Expand2 
        public bool Expandmenu = false; // khai báo biến Expand2 
        private void btndropQLDL_Click(object sender, EventArgs e)
        {

            QLDLdrop.Start();
            Console.WriteLine(Expand);
            Console.ReadLine();
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

        private void btnDrop2_Click(object sender, EventArgs e)
        {
            QLDAdrop.Start();
            Console.WriteLine(Expand2);
            Console.ReadLine();
        }

        private void btnDSDA_Click(object sender, EventArgs e)
        {
            frmDSDA f = new frmDSDA();
            f.Show();
        }

        private void btnTLKT_Click(object sender, EventArgs e)
        {
           
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
        private ModifyTaiLieuKemTheo repo;
        private string maTLCu = ""; // lưu mã TLBC gốc để cho phép đổi khóa khi sửa
        private void frmTLKT_Load(object sender, EventArgs e)
        {
            repo = new ModifyTaiLieuKemTheo();
            try
            {
                grdTLKT.AutoGenerateColumns = true; // nếu bạn không tự add cột
                grdTLKT.DataSource = repo.getAllTaiLieu();
                grdTLKT.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grdTLKT.MultiSelect = false;

                grdTLKT.CellClick += grdTLKT_CellClick;
                grdTLKT.SelectionChanged += grdTLKT_SelectionChanged;

                if (grdTLKT.Rows.Count > 0)
                {
                    grdTLKT.Rows[0].Selected = true;
                    SyncFromGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
       

        // ================== ĐỒNG BỘ GRID → TEXTBOX ==================
        private void grdTLKT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) SyncFromGrid();
        }

        private void grdTLKT_SelectionChanged(object sender, EventArgs e)
        {
            SyncFromGrid();
        }

        private void SyncFromGrid()
        {
            if (grdTLKT.CurrentRow == null || grdTLKT.CurrentRow.IsNewRow) return;

            var row = grdTLKT.CurrentRow;

            txtbox_MaTLBC.Text = row.Cells["MATAILIEUBC"].Value?.ToString() ?? "";
            txtbox_FileBC.Text = row.Cells["FILEBC"].Value?.ToString() ?? "";
            txtbox_Slide.Text = row.Cells["SLIDE"].Value?.ToString() ?? "";
            txtbox_LyLich.Text = row.Cells["LY_LICH"].Value?.ToString() ?? "";

            // lưu mã gốc
            maTLCu = txtbox_MaTLBC.Text;
        }

        // ================== TIỆN ÍCH ==================
        private void ClearInputs()
        {
            txtbox_MaTLBC.Clear();
            txtbox_FileBC.Clear();
            txtbox_Slide.Clear();
            txtbox_LyLich.Clear();
            maTLCu = "";
        }
        // ================== THÊM ==================
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            var tl = new TaiLieuKemTheo(
                txtbox_MaTLBC.Text.Trim(),
                txtbox_FileBC.Text.Trim(),
                txtbox_Slide.Text.Trim(),
                txtbox_LyLich.Text.Trim()
            );

            string err;
            if (repo.insert(tl, out err))
            {
                MessageBox.Show("Thêm tài liệu kèm theo thành công.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                grdTLKT.DataSource = repo.getAllTaiLieu();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Thêm thất bại.\nLỗi: " + err, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // ================== SỬA (có thể đổi MATAILIEUBC) ==================
        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maTLCu) && grdTLKT.CurrentRow != null)
                maTLCu = grdTLKT.CurrentRow.Cells["MATAILIEUBC"].Value?.ToString();

            if (string.IsNullOrWhiteSpace(maTLCu))
            {
                MessageBox.Show("Vui lòng chọn tài liệu cần sửa từ bảng.",
                                "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tl = new TaiLieuKemTheo(
                txtbox_MaTLBC.Text.Trim(),
                txtbox_FileBC.Text.Trim(),
                txtbox_Slide.Text.Trim(),
                txtbox_LyLich.Text.Trim()
            );

            string err;
            if (repo.update(tl, maTLCu, out err))
            {
                MessageBox.Show("Cập nhật thành công.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                grdTLKT.DataSource = repo.getAllTaiLieu();
                maTLCu = tl.MaTLBC; // cập nhật lại mã gốc nếu tiếp tục sửa
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.\n" + err, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // ================== XÓA ==================
        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string ma = txtbox_MaTLBC.Text.Trim();
            if (string.IsNullOrWhiteSpace(ma) && grdTLKT.CurrentRow != null)
                ma = grdTLKT.CurrentRow.Cells["MATAILIEUBC"].Value?.ToString();

            if (string.IsNullOrWhiteSpace(ma))
            {
                MessageBox.Show("Vui lòng chọn tài liệu để xóa.", "Thiếu thông tin",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa: {ma}?",
                                          "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            string err;
            if (repo.delete(ma, out err))
            {
                MessageBox.Show("Xóa thành công.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                grdTLKT.DataSource = repo.getAllTaiLieu();
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Xóa thất bại.\nLỗi: " + err, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string kw = textbox_TimKiem.Text.Trim();
            if (string.IsNullOrEmpty(kw))
            {
                grdTLKT.DataSource = repo.getAllTaiLieu();
                return;
            }
            grdTLKT.DataSource = repo.searchTaiLieu(kw);
        }

        private void textbox_TimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // tránh tiếng 'ding'
                btnTimKiem_Click(sender, e);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            textbox_TimKiem.Clear();
            grdTLKT.DataSource = repo.getAllTaiLieu();
        }
    }
}
