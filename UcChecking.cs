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
            for (int i = 0; i < Util.orderId.Count; i++)
            {
                Cell cellProgress = new Cell();
                cellProgress.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                cellProgress.Data = 0;

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
            progressBar.Value = 0;
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                if (Util.serialDict.ContainsKey(txtBarcode.Text))
                {
                    var id = Util.serialDict[txtBarcode.Text];
                    var row = Util.orderDict[id].RowIndex;

                    if (Util.orderDict[id].Barcode.IndexOf("|" + txtBarcode.Text + "|") == -1)
                    {
                        Util.orderDict[id].Count++;
                        table1.BeginUpdate();
                        table1.TableModel[row, 2].Data = (int)((float)Util.orderDict[id].Count / (float)Util.orderDict[id].Qty * 100.0);
                        table1.EndUpdate();

                        tableModel1.Selections.SelectCells(row, 0, row, 2);
                        table1.EnsureVisible(row, 0);

                        progressBar.Value++;

                        Util.orderDict[id].Barcode += txtBarcode.Text + "|";

                        if (progressBar.Value == progressBar.Maximum)
                            RenderSuccess();
                    }
                    else
                    {
                        SoundPlayer simpleSound = new SoundPlayer(@"Source/ah.wav");
                        simpleSound.Play();
                        MessageBox.Show("มีข้อมูล Barcode นี้ ในระบบแล้วครับ", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (Util.skuDict.ContainsKey(txtBarcode.Text))
                    {
                        var id = Util.skuDict[txtBarcode.Text];
                        SkuSelect(id, Util.orderDict[id].RowIndex);

                    }
                    else
                    {

                        if (Util.barcodeDict.ContainsKey(txtBarcode.Text))
                        {
                            string[] sp = Util.barcodeDict[txtBarcode.Text].Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
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
            table1.Enabled = false;
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
    }
}
