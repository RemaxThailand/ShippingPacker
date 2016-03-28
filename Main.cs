using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Drawing.Printing;
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
            /*foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                Console.WriteLine(printer);
            }*/

            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            //UcSearchOrder uc = new UcSearchOrder();
            /*UcChecking uc = new UcChecking();
            uc.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(uc);*/


            /*PaperSize paperSize = new PaperSize();
            paperSize.RawKind = (int)PaperKind.A4;

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize = paperSize;
            pd.PrintController = new System.Drawing.Printing.StandardPrintController();
            pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            //pd.PrinterSettings.PrinterName = "GP-80250 Series";
            //pd.PrinterSettings.PrinterName = "POS80";

            pd.PrintPage += (_, g) =>
            {
                Util.PrintAddress(g, "1603B00888", 2);
                //PrintOrder(g, orderNo);
            };
            pd.Print();

            this.Dispose();*/


            Util.LoadScreen("search");
            Util.ConnectSQLiteDatabase();

        }
    }
}
