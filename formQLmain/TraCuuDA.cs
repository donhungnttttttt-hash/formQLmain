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
        SqlConnection conn=new SqlConnection("Data Source=LAPTOP-D4IEITM3\\SQLEXPRESS02;Initial Catalog=DOAN;User ID=sa;Password=Sa@12345;TrustServerCertificate=True");
        SqlDataAdapter da = new SqlDataAdapter();
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        string sql, constr;

        public frmTracuu()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmTracuu_Load(object sender, EventArgs e)
        {
            sql = "SELECT  DA.TENDETAI, SV.HOTEN, DA.GVHD, DA.NAMBAOVE, DA.TOMTAT FROM DOAN DA JOIN SINHVIEN SV ON DA.MASINHVIEN = SV.MASINHVIEN ";
            conn.Open();
            da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            grdTracuu.DataSource = dt;
            grdTracuu.Refresh();
            LoadData();

        }

        private void LoadData()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void grdDoAn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

           

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        private void pnlgrid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
