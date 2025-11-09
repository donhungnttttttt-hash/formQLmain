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
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            // Open the login form
            FrmLogin f = new FrmLogin("USER");
            f.Show();
            this.Hide();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            // Open the login form
            FrmLogin f = new FrmLogin("ADMIN");
            f.Show();
            this.Hide();
        }
    }
}
