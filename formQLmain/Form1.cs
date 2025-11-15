using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace formQLmain
{
   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



private void OpenHtmlLink()
{
            string url = "https://trinhyenli.github.io/tailieuhuongdan/";

            Process.Start(new ProcessStartInfo(url)
    {
        UseShellExecute = true
    });
}

        private void button1_Click(object sender, EventArgs e)
        {
            OpenHtmlLink();
        }
    }
}
