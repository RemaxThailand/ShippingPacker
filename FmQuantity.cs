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
    public partial class FmQuantity : Form
    {
        public int Qty = 0;
        public int MaxQty = 0;

        public FmQuantity()
        {
            InitializeComponent();
        }

        public void SetTitle(string message)
        {
            this.Text = message;
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (lblNumber.Text == "0") lblNumber.Text = "";
            if (lblNumber.Text.Length < 4)
                lblNumber.Text += btn.Name.Substring(3,1);
            Qty = int.Parse(lblNumber.Text);
            if(Qty > MaxQty)
            {
                lblNumber.ForeColor = Color.OrangeRed;
            }
            else if (Qty < MaxQty)
            {
                lblNumber.ForeColor = Color.OrangeRed;
            }
            else
            {
                lblNumber.ForeColor = Color.Chartreuse;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lblNumber.ForeColor = Color.DarkOrange;
            lblNumber.Text = "0";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void FmQuantity_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
