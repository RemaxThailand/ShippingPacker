using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace ShippingChecker
{
    public partial class UcSearchOrder : UserControl
    {
        public UcSearchOrder()
        {
            InitializeComponent();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                Util.orderNo = txtBarcode.Text;
                if (Util.LoadOrderDetail(Util.orderNo))
                {
                    Util.LoadScreen("detail");
                }
                else
                {
                    SoundPlayer simpleSound = new SoundPlayer(@"Source/lowspin.wav");
                    simpleSound.Play();
                    lblNoData.Visible = true;
                }
            }
        }

        private void txtBarcode_Enter(object sender, EventArgs e)
        {
            txtBarcode.Text = "";
            lblNoData.Visible = false;
        }

        private void UcSearchOrder_Load(object sender, EventArgs e)
        {
            txtBarcode.Focus();
        }
    }
}
