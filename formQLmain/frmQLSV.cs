using DevExpress.CodeParser;
using DevExpress.XtraCharts.Native;
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
    public partial class frmQLSV : Form
    {
        
        public frmQLSV()
        {
            InitializeComponent();
        }
        public bool Expand = false; // khai báo biến Expand 
        public bool Expand2 = false; // khai báo biến Expand2 
        public bool Expandmenu = false; // khai báo biến Expand2 

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        Modify modify;
        SinhVien sinhVien;
        private string maSVCu = ""; //////////// cẩn thận lỗi 
        private void frmQLDL_Load(object sender, EventArgs e) // đổi tên thành frmQLSV_Load rồi 
        {
            modify = new Modify();

            try
            {
                grdSinhVien.DataSource = modify.getAllSinhVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // GẮN SỰ KIỆN SAU KHI ĐÃ LOAD DỮ LIỆU + KHỞI TẠO 'modify'
            comboBox_ChuyenNganh.SelectedIndexChanged += ComboBox_FilterChanged;
            comboBox_Lop.SelectedIndexChanged += ComboBox_FilterChanged;
            comboBox_Khoa.SelectedIndexChanged += ComboBox_FilterChanged;
        }
        private void ComboBox_FilterChanged(object sender, EventArgs e)
        {
            if (modify == null) return; // tránh null khi form chưa khởi xong

            string chuyenNganh = comboBox_ChuyenNganh.Text?.Trim() ?? "";
            string lop = comboBox_Lop.Text?.Trim() ?? "";
            string khoa = comboBox_Khoa.Text?.Trim() ?? "";

            grdSinhVien.DataSource = modify.filterSinhVien(chuyenNganh, lop, khoa);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            frmQLTK f = new frmQLTK();
            f.Show();
        }

        private void btnDSDA_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDSDA f = new frmDSDA();
            f.Show();
        }

        private void btnTLKT_Click(object sender, EventArgs e)
        {
            this.Close();
            frmTLKT f = new frmTLKT();
            f.Show();
        }

        private void btndropQLDL_Click(object sender, EventArgs e)
        {
            QLDLdrop.Start();
            Console.WriteLine(Expand);
            Console.ReadLine();
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

        private void btnDrop2_Click(object sender, EventArgs e)
        {
            QLDAdrop.Start();
            Console.WriteLine(Expand2);
            Console.ReadLine();
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlgrid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
       
        private void btnThem_Click(object sender, EventArgs e)
        {// nút thêm thật 

            string MaSV = txtbox_MaSV.Text;
            string MaTK = txtbox_MaTK.Text;
            string HoTen = txtbox_Hovaten.Text;
            string ChuyenNganh = txtbox_Chuyennganh.Text;
            string Lop = txtbox_Lop.Text;
            string Khoa = txtbox_Khoa.Text;
            sinhVien = new SinhVien(MaSV, MaTK, HoTen, ChuyenNganh, Lop, Khoa);
            string err;
            if (modify.insert(sinhVien, out err))
            {
                MessageBox.Show("Thêm sinh viên thành công", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                grdSinhVien.DataSource = modify.getAllSinhVien();
            }
            else
            {
                MessageBox.Show("Thêm sinh viên thất bại\nLỗi: " + err, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // đang bị lỗi rất to là khi thêm sinh viên thì phải thêm mã TK ?? mà mã TK chưa có trong bảng tài khoản thì không thêm được nên bị xung đột
            // nên là phải thêm tài khoản trước rồi mới thêm sinh viên được
            // rất là kỳ kì , hoặc nên tạo dạng nhập khác để ko bị nhầm 
        }

        private void btnSua_Click(object sender, EventArgs e)
        {// đây là nút sửa thật 
            // Kiểm tra có chọn dòng chưa
            if (string.IsNullOrWhiteSpace(maSVCu))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa từ bảng.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SinhVien sv = new SinhVien(
                txtbox_MaSV.Text.Trim(),
                txtbox_MaTK.Text.Trim(),
                txtbox_Hovaten.Text.Trim(),
                txtbox_Chuyennganh.Text.Trim(),
                txtbox_Lop.Text.Trim(),
                txtbox_Khoa.Text.Trim()
            );

            string err;
            if (modify.update(sv, maSVCu, out err)) // hàm hợp nhất (SET MASV=@maSV WHERE MASV=@maSVCu)
            {
                MessageBox.Show("Cập nhật sinh viên thành công.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                grdSinhVien.DataSource = modify.getAllSinhVien();

                // Cập nhật lại mã cũ thành mã mới (phục vụ cho lần sửa kế tiếp)
                maSVCu = sv.MaSV;
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại:\n" + err,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) SyncFromGrid();
        }

        private void grdSinhVien_SelectionChanged(object sender, EventArgs e)
        {
            SyncFromGrid();
        }

        // Hàm đồng bộ TextBox với dòng hiện tại
        private void SyncFromGrid()
        {
            if (grdSinhVien.CurrentRow == null || grdSinhVien.CurrentRow.IsNewRow) return;

            DataGridViewRow row = grdSinhVien.CurrentRow;
            txtbox_MaSV.Text = row.Cells["MASINHVIEN"].Value?.ToString();
            txtbox_MaTK.Text = row.Cells["MATK"].Value?.ToString();
            txtbox_Hovaten.Text = row.Cells["HOTEN"].Value?.ToString();
            txtbox_Chuyennganh.Text = row.Cells["CHUYENNGANH"].Value?.ToString();
            txtbox_Lop.Text = row.Cells["LOP"].Value?.ToString();
            txtbox_Khoa.Text = row.Cells["KHOA"].Value?.ToString();

            maSVCu = txtbox_MaSV.Text; // lưu mã gốc khi chọn
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Lấy MaSV từ textbox (hoặc từ dòng đang chọn)
            string maSV = txtbox_MaSV.Text.Trim();

            if (string.IsNullOrWhiteSpace(maSV))
            {
                // Nếu người dùng chưa điền textbox, thử lấy từ row đang chọn
                if (grdSinhVien.CurrentRow != null && grdSinhVien.CurrentRow.Index >= 0)
                {
                    maSV = grdSinhVien.CurrentRow.Cells["MASINHVIEN"].Value?.ToString()?.Trim();
                }
            }

            if (string.IsNullOrWhiteSpace(maSV))
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa.", "Thiếu thông tin",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hỏi xác nhận
            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa sinh viên có mã: {maSV}?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            string err;
            if (modify.delete(maSV, out err))
            {
                MessageBox.Show("Xóa sinh viên thành công.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Load lại dữ liệu
                grdSinhVien.DataSource = modify.getAllSinhVien();

                // Xóa nội dung các ô nhập cho sạch sẽ (tùy chọn)
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Xóa sinh viên thất bại.\nLỗi: " + err, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm tiện ích làm sạch textbox (tùy chọn)///////
        private void ClearInputs()
        {
            txtbox_MaSV.Text = "";
            txtbox_MaTK.Text = "";
            txtbox_Hovaten.Text = "";
            txtbox_Chuyennganh.Text = "";
            txtbox_Lop.Text = "";
            txtbox_Khoa.Text = "";
        }








        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$4 
        // vừa thêm quả NapCT giống thầy sau khi học ## giống ở dòng 246 về mục đích  
        public void NapCT()
        {
            int i = grdSinhVien.CurrentRow.Index;
            // lệnh để lấy dữ liệu từ lưới đổ vào các textbox
            txtbox_MaSV.Text = grdSinhVien.Rows[i].Cells[0].Value.ToString(); // cột 0, dòng i 
            txtbox_MaTK.Text = grdSinhVien.Rows[i].Cells[1].Value.ToString();   // cell 1 tức là cột 2 
            txtbox_Hovaten.Text = grdSinhVien.Rows[i].Cells[2].Value.ToString();
            txtbox_Chuyennganh.Text = grdSinhVien.Rows[i].Cells[3].Value.ToString();
            txtbox_Lop.Text = grdSinhVien.Rows[i].Cells[4].Value.ToString();
            txtbox_Khoa.Text = grdSinhVien.Rows[i].Cells[5].Value.ToString();
        }
        // $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$4

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string kw = textbox_TimKiem.Text.Trim();

            if (string.IsNullOrEmpty(kw))
            {
                // Không nhập gì → hiện tất cả
                grdSinhVien.DataSource = modify.getAllSinhVien();
                return;
            }

            grdSinhVien.DataSource = modify.searchSinhVien(kw);
        }

        private void textbox_TimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // khỏi kêu 'ding'
                btnTimKiem_Click(sender, e);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            textbox_TimKiem.Clear();
            grdSinhVien.DataSource = modify.getAllSinhVien();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void frmQLSV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");  // đây là lệnh gửi phím tab
                e.Handled = true;
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {//####################################################3
            int i = grdSinhVien.Rows.Count - 1; //#########
            grdSinhVien.CurrentCell = grdSinhVien[0, i]; // nhảy đến dòng i (tức là dòng cuối  dòng cuối 
            MessageBox.Show("Bạn hãy nhập thông tin  vào các ô thông tin, sau đó nhấn nút Lưu");
            txtbox_MaSV.Focus();    // chuyển con trỏ đến textbox mã nhóm 
            ClearInputs();
            //#########################################################3
            btnLuu.Visible = true; // hiện nút Lưu lên 
            // nút lưu ở đây là nút thêm thật 
            // thêm mới là thêm giả 

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SyncFromGrid();
            MessageBox.Show("Bạn hãy nhập thông tin cần thay đổi vào các ô thông tin, sau đó nhấn nút Cập nhật");
            txtbox_MaSV.Focus();    // chuyển con trỏ đến textbox mã nhóm 
            btnCapNhat.Visible = true; // hiện nút cập nhật lên
        }

        private void button6_Click(object sender, EventArgs e)
        {
            rptDoann rpt = new rptDoann();
            string sql = "SELECT DA.TENDETAI, SV.HOTEN, SV.LOP, DA.GVHD, YEAR(DA.NAMBAOVE) AS N'NĂM' FROM DOAN DA JOIN SINHVIEN SV ON DA.MASINHVIEN = SV.MASINHVIEN" +
                "where " + comboBox_ChuyenNganh.Text + " = N'" + comboBox_Khoa.Text + " '";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable rdt = new DataTable();
            da.Fill(rdt);
            rpt.rptNgayIn.Text = string.Format("MIS, ngày {0} tháng {1} năm {2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
            rpt.DataSource = rdt;
            rpt.ShowPreview();
        }

        private void comboBox_ChuyenNganh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        // Cách làm bây giờ là đẩy nút thêm cũ  sang phải và đổi tên thành cập nhật 
        // tạo một nút thêm giả: nút này chỉ để nó trắng xóa và nó nhảy về cuối 
        //Xem hét việc thêm nút hủy thêm, kiểu nhắn vào thêm xong ko muốn thêm nữa thì bấm hủy

    }
}


