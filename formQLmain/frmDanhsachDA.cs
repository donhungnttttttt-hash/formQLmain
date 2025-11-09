using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace formQLmain
{
    public partial class frmDanhsachDA : Form
    {
        public frmDanhsachDA()
        {
            InitializeComponent();
        }

        private void grdDoAn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            frmChitie f = new frmChitie();  
            f.ShowDialog();
        }
    }
}
