using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XPTable.Renderers;
using XPTable.Models;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Media;
using System.Threading;
using System.Drawing.Printing;
using System.Configuration;

namespace ShippingChecker
{
    public partial class UcChecking : UserControl
    {
        public UcChecking()
        {
            InitializeComponent();
        }

        private void UcChecking_Load(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(@"Source/hiscale.wav");
            simpleSound.Play();
            
            lblOrderNo.Text = Util.orderNo;
            cbbCompany.SelectedIndex = 0;
            //cbbQty.SelectedIndex = 0;

            if (Util.hasShippingCost)
            {
                rbCompany.Checked = true;
            }

            renderData();

            txtBarcode.Focus();
        }

        private void rbSelf_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSelf.Checked)
            {
                rbSelf.ForeColor = Color.DarkOrange;
                rbCompany.ForeColor = Color.White;
                cbbCompany.Enabled = false;
                //gbxCash.Visible = true;
            }
            else
            {
                rbSelf.ForeColor = Color.White;
                rbCompany.ForeColor = Color.DarkOrange;
                cbbCompany.Enabled = true;
                ckbCash.Checked = false;
                //gbxCash.Visible = false;
            }
        }

        private void ckbCash_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbCash.Checked)
            {
                ckbCash.ForeColor = Color.DarkOrange;
            }
            else
            {
                ckbCash.ForeColor = Color.White;
            }
            if (rbCompany.Checked) ckbCash.Checked = false;
        }

        private void renderData()
        {
            table1.BeginUpdate();
            tableModel1.Rows.Clear();
            tableModel1.RowHeight = 28;

            var sumQty = 0;
            var sumChecked = 0;
            for (int i = 0; i < Util.orderId.Count; i++)
            {

                DataTable dt = Util.DBQuery("SELECT serial, isChecked FROM SellDetail WHERE orderNo = \'" + Util.orderNo + "\' AND product = \'" + Util.orderDict[Util.orderId[i]].Id + "\'");

                int cnt = 0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    if ((bool)dt.Rows[x]["isChecked"])
                    {
                        Util.orderDict[Util.orderId[i]].Count++;
                        sumChecked++;
                        Util.orderDict[Util.orderId[i]].Barcode += dt.Rows[x]["serial"].ToString() + "|";
                    }
                }

                Cell cellProgress = new Cell();
                cellProgress.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                cellProgress.Data = (int)((float)Util.orderDict[Util.orderId[i]].Count / (float)Util.orderDict[Util.orderId[i]].Qty * 100.0);

                tableModel1.Rows.Add(new Row(
                    new Cell[] {
                            new Cell("" + (i+1)),
                            new Cell( Util.orderDict[Util.orderId[i]].Name ),
                            cellProgress
                }));

                Util.orderDict[Util.orderId[i]].Barcode = "|";
                sumQty += Util.orderDict[Util.orderId[i]].Qty;
            }
            table1.EndUpdate();

            progressBar.Maximum = sumQty;
            progressBar.Value = sumChecked;

            if (progressBar.Value == progressBar.Maximum)
                RenderSuccess();

        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                var barcode = txtBarcode.Text;
                DataTable dt = Util.DBQuery("SELECT * FROM SellDetail WHERE orderNo = \'" + Util.orderNo + "\' AND serial = \'" + barcode + "\'");
                //if (Util.serialDict.ContainsKey(txtBarcode.Text))
                if (dt.Rows.Count != 0)
                {
                    var id = Util.serialDict[barcode];
                    var row = Util.orderDict[id].RowIndex;

                    //if (Util.orderDict[id].Barcode.IndexOf("|" + txtBarcode.Text + "|") == -1)
                    if (!(bool)dt.Rows[0]["isChecked"])
                    {
                        Util.orderDict[id].Count++;
                        table1.BeginUpdate();
                        table1.TableModel[row, 2].Data = (int)((float)Util.orderDict[id].Count / (float)Util.orderDict[id].Qty * 100.0);
                        table1.EndUpdate();

                        progressBar.Value++;
                        Util.DBExecute("UPDATE SellDetail SET isChecked = 1 WHERE orderNo = \'" + Util.orderNo + "\' AND serial = \'" + barcode + "\'");

                        Thread thread = new Thread(() => Util.DownloadWeb("/order/barcode/checked", "token=" + Util.token + "&barcode=" + barcode));
                        thread.Start();

                        Util.orderDict[id].Barcode += barcode + "|";

                        if (progressBar.Value == progressBar.Maximum)
                            RenderSuccess();
                    }
                    else
                    {
                        SoundPlayer simpleSound = new SoundPlayer(@"Source/ah.wav");
                        simpleSound.Play();
                        MessageBox.Show("มีข้อมูล Barcode นี้ ในระบบแล้วครับ", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    tableModel1.Selections.SelectCells(row, 0, row, 2);
                    table1.EnsureVisible(row, 0);
                }
                else
                {
                    if (Util.skuDict.ContainsKey(barcode))
                    {
                        var id = Util.skuDict[barcode];
                        SkuSelect(id, Util.orderDict[id].RowIndex);

                    }
                    else
                    {

                        if (Util.barcodeDict.ContainsKey(barcode))
                        {
                            string[] sp = Util.barcodeDict[barcode].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                            if (sp.Length == 1)
                            {
                                var id = sp[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[1];
                                SkuSelect(id, Util.orderDict[id].RowIndex);
                            }
                            else
                            {
                                FmColor fm = new FmColor();
                                fm.cbbColor.Items.Clear();
                                Dictionary<string, string> color = new Dictionary<string, string>();
                                for (int i = 0; i < sp.Length; i++)
                                {
                                    string[] data = sp[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    if (Util.orderDict[data[1]].Count != Util.orderDict[data[1]].Qty)
                                        color.Add(data[0], data[1]);
                                }

                                fm.cbbColor.DataSource = new BindingSource(color, null);
                                fm.cbbColor.DisplayMember = "Key";
                                fm.cbbColor.ValueMember = "Value";

                                if (color.Count > 1)
                                {
                                    if (fm.ShowDialog() == DialogResult.OK)
                                    {
                                        var id = fm.cbbColor.SelectedValue.ToString();
                                        SkuSelect(id, Util.orderDict[id].RowIndex);
                                    }
                                }
                                else
                                {
                                    var id = fm.cbbColor.SelectedValue.ToString();
                                    SkuSelect(id, Util.orderDict[id].RowIndex);
                                }
                            }
                        }
                        else
                        {
                            SoundPlayer simpleSound = new SoundPlayer(@"Source/lowspin.wav");
                            simpleSound.Play();
                            MessageBox.Show("ไม่พบข้อมูล Barcode นี้ ในระบบครับ", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                txtBarcode.Text = "";
            }
        }

        private void InputNumber(string id, int row)
        {
            FmQuantity fm = new FmQuantity();
            fm.SetTitle(Util.orderDict[id].Name);
            fm.MaxQty = Util.orderDict[id].Qty;
            if (fm.ShowDialog() == DialogResult.OK)
            {
                if (fm.Qty > Util.orderDict[id].Qty)
                {
                    SoundPlayer simpleSound = new SoundPlayer(@"Source/ohno.wav");
                    simpleSound.Play();
                    MessageBox.Show("มีสินค้าเกินจำนวน " + (fm.Qty - Util.orderDict[id].Qty) + " ชิ้น\nกรุณาตรวจสอบสินค้าใหม่อีกรอบ", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    InputNumber(id, row);
                }
                else if (fm.Qty < Util.orderDict[id].Qty)
                {
                    SoundPlayer simpleSound = new SoundPlayer(@"Source/ohno.wav");
                    simpleSound.Play();
                    MessageBox.Show("มีสินค้าขาดจำนวน " + (Util.orderDict[id].Qty - fm.Qty) + " ชิ้น\nกรุณาตรวจสอบสินค้าใหม่อีกรอบ", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    InputNumber(id, row);
                }
                else
                {
                    Util.orderDict[id].Count = fm.Qty;
                    table1.BeginUpdate();
                    table1.TableModel[row, 2].Data = 100;
                    table1.EndUpdate();

                    tableModel1.Selections.SelectCells(row, 0, row, 2);
                    table1.EnsureVisible(row, 0);

                    progressBar.Value += fm.Qty;

                    if (progressBar.Value == progressBar.Maximum)
                        RenderSuccess();
                }
            }
        }

        private void SkuSelect(string id, int row)
        {
            tableModel1.Selections.SelectCells(row, 0, row, 2);
            table1.EnsureVisible(row, 0);

            if (Util.orderDict[id].Count != Util.orderDict[id].Qty)
            {
                InputNumber(id, row);
            }
            else
            {
                SoundPlayer simpleSound = new SoundPlayer(@"Source/ah.wav");
                simpleSound.Play();
                MessageBox.Show("มีข้อมูล Barcode นี้ ในระบบแล้วครับ", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RenderSuccess()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"Source/yahoo.wav");
            simpleSound.Play();
            //table1.Enabled = false;
            txtBarcode.Visible = false;
            lblBarcode.Visible = false;
            cbbQty.Focus();

        }

        private void cbbQty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbQty.SelectedIndex >= 0)
            {
                btnSave.Enabled = true;
                btnSave.BackColor = Color.Green;

                btnPrint.Enabled = true;
                btnPrint.BackColor = Color.SteelBlue;
            }
            else
            {
                btnSave.Enabled = false;
                btnSave.BackColor = Color.DimGray;

                btnPrint.Enabled = false;
                btnPrint.BackColor = Color.DimGray;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณแน่ใจหรือไม่ที่จะยกเลิก", "ยืนยันข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Util.LoadScreen("search");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Util.LoadOrderAddress(Util.orderNo);
            btnPrint.Enabled = false;

            PaperSize paperSize = new PaperSize();
            paperSize.RawKind = (int)PaperKind.A4;

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PaperSize = paperSize;
            pd.PrintController = new System.Drawing.Printing.StandardPrintController();
            pd.PrinterSettings.PrinterName = ConfigurationManager.AppSettings["PrinterName"]; //"Microsoft Print to PDF";
            //pd.PrinterSettings.PrinterName = "GP-80250 Series";
            //pd.PrinterSettings.PrinterName = "POS80";

            int count = int.Parse(cbbQty.SelectedItem.ToString());
            for (int i = 1; i <= Math.Ceiling((float)count / 2); i++)
            {
                pd.PrintPage += (_, g) =>
                {
                    Util.PrintAddress(g, Util.orderNo, (i - 1) * 2 + 1, int.Parse(cbbQty.SelectedItem.ToString()), 
                        (rbSelf.Checked) ? "จัดส่งเอง" : cbbCompany.SelectedItem.ToString(), 
                        ckbCash.Checked);
                };
                pd.Print();
            }

            btnPrint.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
