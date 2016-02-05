namespace ShippingChecker
{
    partial class UcChecking
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            XPTable.Models.DataSourceColumnBinder dataSourceColumnBinder1 = new XPTable.Models.DataSourceColumnBinder();
            XPTable.Renderers.DragDropRenderer dragDropRenderer1 = new XPTable.Renderers.DragDropRenderer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbbQty = new System.Windows.Forms.ComboBox();
            this.gbxCash = new System.Windows.Forms.GroupBox();
            this.ckbCash = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbbCompany = new System.Windows.Forms.ComboBox();
            this.rbCompany = new System.Windows.Forms.RadioButton();
            this.rbSelf = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.lblBarcode = new System.Windows.Forms.Label();
            this.lblOrderNo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panel5 = new System.Windows.Forms.Panel();
            this.table1 = new XPTable.Models.Table();
            this.columnModel1 = new XPTable.Models.ColumnModel();
            this.textColumn1 = new XPTable.Models.TextColumn();
            this.textColumn2 = new XPTable.Models.TextColumn();
            this.progressBarColumn1 = new XPTable.Models.ProgressBarColumn();
            this.tableModel1 = new XPTable.Models.TableModel();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbxCash.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.gbxCash);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1023, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 5);
            this.panel1.Size = new System.Drawing.Size(257, 648);
            this.panel1.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnSave);
            this.panel6.Controls.Add(this.btnCancel);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(3, 591);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(254, 52);
            this.panel6.TabIndex = 17;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.DimGray;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("DilleniaUPC", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(115, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 52);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "บันทึกข้อมูล";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.OrangeRed;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("DilleniaUPC", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 52);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "ยกเลิก";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.DimGray;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrint.Enabled = false;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("DilleniaUPC", 20.25F, System.Drawing.FontStyle.Bold);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(3, 519);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(254, 46);
            this.btnPrint.TabIndex = 16;
            this.btnPrint.Text = "พิมพ์ใบปะหน้า";
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 508);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(254, 11);
            this.panel3.TabIndex = 15;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbbQty);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Font = new System.Drawing.Font("DilleniaUPC", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(3, 405);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(20, 0, 3, 3);
            this.groupBox4.Size = new System.Drawing.Size(254, 103);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "จำนวนกล่อง";
            // 
            // cbbQty
            // 
            this.cbbQty.BackColor = System.Drawing.Color.White;
            this.cbbQty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbQty.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbQty.FormattingEnabled = true;
            this.cbbQty.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cbbQty.Location = new System.Drawing.Point(14, 38);
            this.cbbQty.Name = "cbbQty";
            this.cbbQty.Size = new System.Drawing.Size(226, 44);
            this.cbbQty.TabIndex = 12;
            this.cbbQty.SelectedIndexChanged += new System.EventHandler(this.cbbQty_SelectedIndexChanged);
            // 
            // gbxCash
            // 
            this.gbxCash.Controls.Add(this.ckbCash);
            this.gbxCash.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbxCash.Font = new System.Drawing.Font("DilleniaUPC", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.gbxCash.ForeColor = System.Drawing.Color.White;
            this.gbxCash.Location = new System.Drawing.Point(3, 308);
            this.gbxCash.Name = "gbxCash";
            this.gbxCash.Padding = new System.Windows.Forms.Padding(20, 0, 3, 3);
            this.gbxCash.Size = new System.Drawing.Size(254, 97);
            this.gbxCash.TabIndex = 11;
            this.gbxCash.TabStop = false;
            this.gbxCash.Text = "การชำระเงิน";
            // 
            // ckbCash
            // 
            this.ckbCash.AutoSize = true;
            this.ckbCash.Font = new System.Drawing.Font("DilleniaUPC", 27.75F, System.Drawing.FontStyle.Bold);
            this.ckbCash.Location = new System.Drawing.Point(20, 31);
            this.ckbCash.Name = "ckbCash";
            this.ckbCash.Size = new System.Drawing.Size(218, 53);
            this.ckbCash.TabIndex = 12;
            this.ckbCash.Text = "เก็บเงินสดปลายทาง";
            this.ckbCash.UseVisualStyleBackColor = true;
            this.ckbCash.CheckedChanged += new System.EventHandler(this.ckbCash_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbbCompany);
            this.groupBox2.Controls.Add(this.rbCompany);
            this.groupBox2.Controls.Add(this.rbSelf);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("DilleniaUPC", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(3, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(20, 0, 3, 3);
            this.groupBox2.Size = new System.Drawing.Size(254, 158);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "การขนส่ง";
            // 
            // cbbCompany
            // 
            this.cbbCompany.BackColor = System.Drawing.Color.White;
            this.cbbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCompany.Font = new System.Drawing.Font("DilleniaUPC", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.cbbCompany.FormattingEnabled = true;
            this.cbbCompany.Items.AddRange(new object[] {
            "เคอรี่ เอ็กซ์เพรส",
            "ไปรษณีย์ EMS",
            "ไปรษณีย์ลงทะเบียน",
            "ขนส่งเอกชน"});
            this.cbbCompany.Location = new System.Drawing.Point(14, 69);
            this.cbbCompany.Name = "cbbCompany";
            this.cbbCompany.Size = new System.Drawing.Size(226, 39);
            this.cbbCompany.TabIndex = 11;
            // 
            // rbCompany
            // 
            this.rbCompany.AutoSize = true;
            this.rbCompany.BackColor = System.Drawing.Color.Transparent;
            this.rbCompany.Font = new System.Drawing.Font("DilleniaUPC", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCompany.ForeColor = System.Drawing.Color.Azure;
            this.rbCompany.Location = new System.Drawing.Point(20, 24);
            this.rbCompany.Name = "rbCompany";
            this.rbCompany.Size = new System.Drawing.Size(162, 50);
            this.rbCompany.TabIndex = 10;
            this.rbCompany.Text = "ส่งบริษัทขนส่ง";
            this.rbCompany.UseVisualStyleBackColor = false;
            this.rbCompany.CheckedChanged += new System.EventHandler(this.rbSelf_CheckedChanged);
            // 
            // rbSelf
            // 
            this.rbSelf.BackColor = System.Drawing.Color.Transparent;
            this.rbSelf.Checked = true;
            this.rbSelf.Font = new System.Drawing.Font("DilleniaUPC", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSelf.ForeColor = System.Drawing.Color.DarkOrange;
            this.rbSelf.Location = new System.Drawing.Point(23, 106);
            this.rbSelf.Name = "rbSelf";
            this.rbSelf.Size = new System.Drawing.Size(231, 53);
            this.rbSelf.TabIndex = 9;
            this.rbSelf.TabStop = true;
            this.rbSelf.Text = "ส่งเอง";
            this.rbSelf.UseVisualStyleBackColor = false;
            this.rbSelf.CheckedChanged += new System.EventHandler(this.rbSelf_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBarcode);
            this.groupBox1.Controls.Add(this.lblBarcode);
            this.groupBox1.Controls.Add(this.lblOrderNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("DilleniaUPC", 20.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(254, 150);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "หมายเลขคำสั่งซื้อ";
            // 
            // txtBarcode
            // 
            this.txtBarcode.BackColor = System.Drawing.Color.Bisque;
            this.txtBarcode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBarcode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtBarcode.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarcode.Location = new System.Drawing.Point(3, 110);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(248, 36);
            this.txtBarcode.TabIndex = 10;
            this.txtBarcode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // lblBarcode
            // 
            this.lblBarcode.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarcode.ForeColor = System.Drawing.Color.White;
            this.lblBarcode.Location = new System.Drawing.Point(3, 71);
            this.lblBarcode.Name = "lblBarcode";
            this.lblBarcode.Size = new System.Drawing.Size(249, 36);
            this.lblBarcode.TabIndex = 9;
            this.lblBarcode.Text = "Barcode";
            this.lblBarcode.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.BackColor = System.Drawing.Color.Transparent;
            this.lblOrderNo.Font = new System.Drawing.Font("Calibri", 32F, System.Drawing.FontStyle.Bold);
            this.lblOrderNo.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblOrderNo.Location = new System.Drawing.Point(3, 30);
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.Size = new System.Drawing.Size(249, 52);
            this.lblOrderNo.TabIndex = 8;
            this.lblOrderNo.Text = "1512B00316";
            this.lblOrderNo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1023, 648);
            this.panel2.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.Controls.Add(this.progressBar);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.table1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1023, 648);
            this.panel4.TabIndex = 0;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar.Location = new System.Drawing.Point(0, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1023, 23);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1194, 28);
            this.panel5.TabIndex = 4;
            // 
            // table1
            // 
            this.table1.BackColor = System.Drawing.Color.Black;
            this.table1.BorderColor = System.Drawing.Color.Transparent;
            this.table1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.table1.ColumnModel = this.columnModel1;
            this.table1.DataMember = null;
            this.table1.DataSourceColumnBinder = dataSourceColumnBinder1;
            dragDropRenderer1.ForeColor = System.Drawing.Color.Red;
            this.table1.DragDropRenderer = dragDropRenderer1;
            this.table1.Font = new System.Drawing.Font("DilleniaUPC", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.table1.ForeColor = System.Drawing.Color.White;
            this.table1.FullRowSelect = true;
            this.table1.GridColor = System.Drawing.Color.DimGray;
            this.table1.GridLinesContrainedToData = false;
            this.table1.GridLineStyle = XPTable.Models.GridLineStyle.Dash;
            this.table1.HeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.table1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.table1.Location = new System.Drawing.Point(5, 2);
            this.table1.Name = "table1";
            this.table1.SelectionBackColor = System.Drawing.Color.Black;
            this.table1.SelectionForeColor = System.Drawing.Color.DarkOrange;
            this.table1.SelectionStyle = XPTable.Models.SelectionStyle.Grid;
            this.table1.Size = new System.Drawing.Size(1006, 638);
            this.table1.TabIndex = 3;
            this.table1.TableModel = this.tableModel1;
            this.table1.Text = "table1";
            this.table1.UnfocusedBorderColor = System.Drawing.Color.Black;
            this.table1.UnfocusedSelectionBackColor = System.Drawing.Color.Black;
            this.table1.UnfocusedSelectionForeColor = System.Drawing.Color.DarkOrange;
            // 
            // columnModel1
            // 
            this.columnModel1.Columns.AddRange(new XPTable.Models.Column[] {
            this.textColumn1,
            this.textColumn2,
            this.progressBarColumn1});
            this.columnModel1.HeaderHeight = 25;
            // 
            // textColumn1
            // 
            this.textColumn1.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.textColumn1.Editable = false;
            this.textColumn1.IsTextTrimmed = false;
            this.textColumn1.Resizable = false;
            this.textColumn1.Sortable = false;
            this.textColumn1.Text = "ที่";
            this.textColumn1.Width = 40;
            // 
            // textColumn2
            // 
            this.textColumn2.Editable = false;
            this.textColumn2.IsTextTrimmed = false;
            this.textColumn2.Resizable = false;
            this.textColumn2.Sortable = false;
            this.textColumn2.Text = "สินค้า";
            this.textColumn2.Width = 523;
            // 
            // progressBarColumn1
            // 
            this.progressBarColumn1.Alignment = XPTable.Models.ColumnAlignment.Center;
            this.progressBarColumn1.Editable = true;
            this.progressBarColumn1.IsTextTrimmed = false;
            this.progressBarColumn1.Resizable = false;
            this.progressBarColumn1.Sortable = false;
            this.progressBarColumn1.Text = "ความคืบหน้า";
            this.progressBarColumn1.Width = 200;
            // 
            // tableModel1
            // 
            this.tableModel1.RowHeight = 60;
            // 
            // UcChecking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UcChecking";
            this.Size = new System.Drawing.Size(1280, 648);
            this.Load += new System.EventHandler(this.UcChecking_Load);
            this.panel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.gbxCash.ResumeLayout(false);
            this.gbxCash.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label lblBarcode;
        private System.Windows.Forms.Label lblOrderNo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbCompany;
        private System.Windows.Forms.RadioButton rbSelf;
        private System.Windows.Forms.GroupBox gbxCash;
        private System.Windows.Forms.ComboBox cbbCompany;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbbQty;
        private System.Windows.Forms.CheckBox ckbCash;
        private System.Windows.Forms.Panel panel4;
        private XPTable.Models.Table table1;
        private XPTable.Models.ColumnModel columnModel1;
        private XPTable.Models.TextColumn textColumn1;
        private XPTable.Models.TextColumn textColumn2;
        private XPTable.Models.ProgressBarColumn progressBarColumn1;
        private XPTable.Models.TableModel tableModel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
