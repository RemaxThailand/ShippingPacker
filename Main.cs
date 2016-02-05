using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShippingChecker
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Util.mainPanel = pnlMain;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;

            //UcSearchOrder uc = new UcSearchOrder();
            /*UcChecking uc = new UcChecking();
            uc.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(uc);*/

            Util.LoadScreen("search");
        }
    }
}
