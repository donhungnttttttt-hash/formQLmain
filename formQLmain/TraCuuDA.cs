using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace formQLmain
{
    public partial class frmTracuu : Form
    {
        SqlConnection conn=new SqlConnection("Data Source=DESKTOP-QQ88INT\\SQLEXPRESS;Initial Catalog=DOAN1211;Integrated Security=True;TrustServerCertificate=True;Encrypt=False");
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sql, constr;

       
        public frmTracuu()
        {
            InitializeComponent();
        }

        public void NapCT()
        {
            int i = grdTracuu.CurrentRow.Index;
            txtTenDeTai.Text = grdTracuu.Rows[i].Cells[0].Value.ToString();
            txtTenSinhVien.Text = grdTracuu.Rows[i].Cells[1].Value.ToString();
            txtChuyenNganh.Text = grdTracuu.Rows[i].Cells[2].Value.ToString();
            txtKhoa.Text = grdTracuu.Rows[i].Cells[3].Value.ToString();
            txtGVHD.Text = grdTracuu.Rows[i].Cells[4].Value.ToString();
            dtpickNambaove.Text = grdTracuu.Rows[i].Cells[5].Value.ToString();
            txtTomTat.Text = grdTracuu.Rows[i].Cells[6].Value.ToString();
            


        }




        private void TimKiemTheoTuKhoa(string keyword)
        {
           
          //  string baseSql = @"
          //SELECT  DA.TENDETAI, 
          //SV.HOTEN, 
          //SV.CHUYENNGANH, 
          //SV.KHOA, 
          //DA.GVHD, 
          //DA.NAMBAOVE, 
          //DA.TOMTAT,
          //TLBC.FILEBC,
          //TLBC.SLIDE,
          //TLBC.LY_LICH,
          //SV.LOP AS LOP
          //FROM DOAN DA 
          // JOIN SINHVIEN SV ON DA.MASINHVIEN = SV.MASINHVIEN
          //  JOIN TAILIEUBC TLBC ON DA.MATAILIEUBC = TLBC.MATAILIEUBC";
          string baseSql = @" SELECT DA.TENDETAI, SV.HOTEN, SV.CHUYENNGANH, SV.KHOA, DA.GVHD, DA.NAMBAOVE, DA.TOMTAT,TLBC.FILEBC, TLBC.SLIDE,TLBC.LY_LICH,SV.LOP, TK.TUKHOA  FROM DOAN DA 
                  JOIN SINHVIEN SV ON DA.MASINHVIEN = SV.MASINHVIEN JOIN TAILIEUBC TLBC ON DA.MATAILIEUBC=TLBC.MATAILIEUBC JOIN  TUKHOA_DOAN TKDA ON TKDA.MADOAN=DA.MADOAN JOIN TUKHOA TK ON TK.MATUKHOA=TKDA.MATUKHOA  ";


            if (string.IsNullOrWhiteSpace(keyword))
            {
                // Không có từ khóa -> trả về toàn bộ
                sql = baseSql;
                da = new SqlDataAdapter(sql, conn);
            }
            else
            {
                sql = baseSql + @"
            WHERE DA.TENDETAI    LIKE @kw
               OR SV.HOTEN       LIKE @kw
               OR SV.CHUYENNGANH LIKE @kw
               OR SV.KHOA        LIKE @kw
               OR DA.GVHD        LIKE @kw";

                da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@kw", "%" + keyword.Trim() + "%"); //keyword.Trim() cắt bỏ khoảng trắng ở đầu và cuối.
            }

            dt = new DataTable();
            da.Fill(dt);
            grdTracuu.DataSource = dt;
            grdTracuu.Refresh();

            if (dt.Rows.Count > 0)
                NapCT();
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmTracuu_Load(object sender, EventArgs e)
        {
            sql = @" SELECT DA.TENDETAI, SV.HOTEN, SV.CHUYENNGANH, SV.KHOA, DA.GVHD, DA.NAMBAOVE, DA.TOMTAT,TLBC.FILEBC, TLBC.SLIDE,TLBC.LY_LICH,SV.LOP, TK.TUKHOA  FROM DOAN DA 
                  JOIN SINHVIEN SV ON DA.MASINHVIEN = SV.MASINHVIEN JOIN TAILIEUBC TLBC ON DA.MATAILIEUBC=TLBC.MATAILIEUBC JOIN  TUKHOA_DOAN TKDA ON TKDA.MADOAN=DA.MADOAN JOIN TUKHOA TK ON TK.MATUKHOA=TKDA.MATUKHOA  ";
            conn.Open();
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdTracuu.DataSource = dt;
            grdTracuu.Refresh();
            NapCT();
            TimKiemTheoTuKhoa(""); // load toàn bộ dữ liệu lúc ban đầu

            

            

         
            if (grdTracuu.Columns.Contains("FILEBC"))
                grdTracuu.Columns["FILEBC"].Visible = false;
            if (grdTracuu.Columns.Contains("SLIDE"))
                grdTracuu.Columns["SLIDE"].Visible = false;
            if (grdTracuu.Columns.Contains("LY_LICH"))
                grdTracuu.Columns["LY_LICH"].Visible = false;
            if (grdTracuu.Columns.Contains("LOP"))
                grdTracuu.Columns["LOP"].Visible = false;
            if (grdTracuu.Columns.Contains("TUKHOA"))
                grdTracuu.Columns["TUKHOA"].Visible = false;


        }

        

      
   

        private void grdDoAn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NapCT();// khi click vào ô nào đó thì NapCT() sẽ được gọi


        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                //sql = "  SELECT DA.TENDETAI, SV.HOTEN, SV.CHUYENNGANH, SV.KHOA, DA.GVHD, DA.NAMBAOVE, DA.TOMTAT, ,SV.LOP, TLBC.FILEBC, TLBC.SLIDE,TLBC.LY_LICH FROM DOAN DA JOIN SINHVIEN SV ON DA.MASINHVIEN = SV.MASINHVIEN JOIN TAILIEUBC TLBC ON DA.MATAILIEUBC=TLBC.MATAILIEUBC ";
                sql = @" SELECT DA.TENDETAI, SV.HOTEN, SV.CHUYENNGANH, SV.KHOA, DA.GVHD, DA.NAMBAOVE, DA.TOMTAT,TLBC.FILEBC, TLBC.SLIDE,TLBC.LY_LICH,SV.LOP, TK.TUKHOA  FROM DOAN DA 
                  JOIN SINHVIEN SV ON DA.MASINHVIEN = SV.MASINHVIEN JOIN TAILIEUBC TLBC ON DA.MATAILIEUBC=TLBC.MATAILIEUBC JOIN  TUKHOA_DOAN TKDA ON TKDA.MADOAN=DA.MADOAN JOIN TUKHOA TK ON TK.MATUKHOA=TKDA.MATUKHOA  ";
                da = new SqlDataAdapter(sql, conn);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            grdTracuu.DataSource = dt;
            grdTracuu.Refresh();
            NapCT(); }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi làm mới dữ liệu: " + ex.Message);
            }
            comTruong.SelectedIndex = -1;
            comGT.SelectedIndex = -1;
            // 2️⃣ Reset toàn bộ phần lọc và tìm kiếm
            comTruong.SelectedIndex = -1;  // Bỏ chọn tên trường
            comGT.DataSource = null;       // Xóa dữ liệu trong combo giá trị
            comGT.Text = "";               // Làm trống text hiển thị

            txtTukhoa.Clear();             //  Xóa ô tìm kiếm về rỗng






        }

        private void pnlgrid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDrop2_Click(object sender, EventArgs e)
        {
            frmTracuu f = new frmTracuu();
            f.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            

            if (grdTracuu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một đồ án trong danh sách!");
                return;
            }

            int i = grdTracuu.CurrentRow.Index;

            // Lấy dữ liệu đầy đủ từ dòng đang chọn
            string tenDeTai = grdTracuu.Rows[i].Cells["TENDETAI"].Value.ToString();
            string tenSinhVien = grdTracuu.Rows[i].Cells["HOTEN"].Value.ToString();
            string chuyenNganh = grdTracuu.Rows[i].Cells["CHUYENNGANH"].Value.ToString();
            string khoa = grdTracuu.Rows[i].Cells["KHOA"].Value.ToString();
            string gvhd = grdTracuu.Rows[i].Cells["GVHD"].Value.ToString();
            string namBaoVe = grdTracuu.Rows[i].Cells["NAMBAOVE"].Value.ToString();
            string tomTat = grdTracuu.Rows[i].Cells["TOMTAT"].Value.ToString();

            // Các cột bị ẩn vẫn có thể lấy được dữ liệu
            string fileBaoCao = grdTracuu.Rows[i].Cells["FILEBC"].Value.ToString();
            string slide = grdTracuu.Rows[i].Cells["SLIDE"].Value.ToString();
            string lyLich = grdTracuu.Rows[i].Cells["LY_LICH"].Value.ToString();
            string lop = grdTracuu.Rows[i].Cells["LOP"].Value.ToString();
            string tuKhoa = grdTracuu.Rows[i].Cells["TUKHOA"].Value.ToString();


            

            // Mở form chi tiết và truyền dữ liệu qua
            frmChitiet f = new frmChitiet(
                tenDeTai,
                tenSinhVien,
                lop,
                gvhd,
                tuKhoa,
                namBaoVe,
                tomTat,
                fileBaoCao,
                slide,
                lyLich
            );

            f.Show();
            this.Hide();
        }

        


        

        private void comTruong_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                sql = "Select distinct " + comTruong.Text + " FROM DOAN DA JOIN SINHVIEN SV ON DA.MASINHVIEN = SV.MASINHVIEN ";
                da = new SqlDataAdapter(sql, conn);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                //comGT.Items.Clear();
                comGT.DataSource = dt1;
                comGT.DisplayMember = comTruong.Text; //trường hiện ra
                comGT.ValueMember = comTruong.Text;// trường để lấy 

            }
            catch
            {

            }



        }
        

        private void btnFillter_Click(object sender, EventArgs e)
        {
            sql = " SELECT  DA.TENDETAI, SV.HOTEN, SV.CHUYENNGANH, SV.KHOA, DA.GVHD, DA.NAMBAOVE, DA.TOMTAT FROM DOAN DA JOIN SINHVIEN SV ON DA.MASINHVIEN = SV.MASINHVIEN WHERE " + comTruong.Text + "= N'" + comGT.Text + "'";// Đảm bảo mọi dữ liệu có tiếng việt vẫn lọc được
            da = new SqlDataAdapter(sql, conn);
            dt = new DataTable();
            da.Fill(dt);
            grdTracuu.DataSource = dt;
            grdTracuu.Refresh();
            NapCT();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            grdTracuu.ClearSelection();
            grdTracuu.CurrentCell = grdTracuu[0, 0];
            NapCT();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            int i = grdTracuu.Rows.Count - 1;
            grdTracuu.CurrentCell = grdTracuu[0, i];
            NapCT();
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            int i = grdTracuu.CurrentRow.Index;
            if (i > 0)
            {
                grdTracuu.CurrentCell = grdTracuu[0, i - 1];
                NapCT();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int i = grdTracuu.CurrentRow.Index;
            if (i < grdTracuu.Rows.Count - 1)
            {
                grdTracuu.CurrentCell = grdTracuu[0, i + 1];
                NapCT();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemTheoTuKhoa(txtTukhoa.Text);



        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmHome f = new frmHome();
            f.Show();
            this.Hide();
        }

        private void txtTenSinhVien_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtKhoa_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void panelMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
