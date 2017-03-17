namespace Crypto
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.GreyDecodeButton = new System.Windows.Forms.Button();
            this.textBoxCodeGray = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GreyEncodeButton = new System.Windows.Forms.Button();
            this.listBoxCodeGray = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.comboBoxDdcKey = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDdcData = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BCDDecodeButton = new System.Windows.Forms.Button();
            this.BCDEncodeButton = new System.Windows.Forms.Button();
            this.listBoxDdc = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.BerderDecodeButton = new System.Windows.Forms.Button();
            this.textBoxBerger = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BerderEncodeButton = new System.Windows.Forms.Button();
            this.listBoxBerger = new System.Windows.Forms.ListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.richTextBoxEllays = new System.Windows.Forms.RichTextBox();
            this.listBoxEllays = new System.Windows.Forms.ListBox();
            this.EllaysDecodeButton = new System.Windows.Forms.Button();
            this.EllaysEncodeButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(-1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(504, 356);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.GreyDecodeButton);
            this.tabPage1.Controls.Add(this.textBoxCodeGray);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.GreyEncodeButton);
            this.tabPage1.Controls.Add(this.listBoxCodeGray);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(496, 330);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Код Грея";
            // 
            // GreyDecodeButton
            // 
            this.GreyDecodeButton.Location = new System.Drawing.Point(400, 38);
            this.GreyDecodeButton.Name = "GreyDecodeButton";
            this.GreyDecodeButton.Size = new System.Drawing.Size(89, 23);
            this.GreyDecodeButton.TabIndex = 4;
            this.GreyDecodeButton.Text = "Дешифровать";
            this.GreyDecodeButton.UseVisualStyleBackColor = true;
            this.GreyDecodeButton.Click += new System.EventHandler(this.GreyDecodeButton_Click);
            // 
            // textBoxCodeGray
            // 
            this.textBoxCodeGray.Location = new System.Drawing.Point(63, 12);
            this.textBoxCodeGray.Name = "textBoxCodeGray";
            this.textBoxCodeGray.Size = new System.Drawing.Size(331, 20);
            this.textBoxCodeGray.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Данные";
            // 
            // GreyEncodeButton
            // 
            this.GreyEncodeButton.Location = new System.Drawing.Point(400, 10);
            this.GreyEncodeButton.Name = "GreyEncodeButton";
            this.GreyEncodeButton.Size = new System.Drawing.Size(89, 23);
            this.GreyEncodeButton.TabIndex = 1;
            this.GreyEncodeButton.Text = "Шифровать";
            this.GreyEncodeButton.UseVisualStyleBackColor = true;
            this.GreyEncodeButton.Click += new System.EventHandler(this.GreyEncodeButton_Click);
            // 
            // listBoxCodeGray
            // 
            this.listBoxCodeGray.FormattingEnabled = true;
            this.listBoxCodeGray.Location = new System.Drawing.Point(6, 34);
            this.listBoxCodeGray.Name = "listBoxCodeGray";
            this.listBoxCodeGray.Size = new System.Drawing.Size(388, 290);
            this.listBoxCodeGray.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.comboBoxDdcKey);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.textBoxDdcData);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.BCDDecodeButton);
            this.tabPage2.Controls.Add(this.BCDEncodeButton);
            this.tabPage2.Controls.Add(this.listBoxDdc);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(496, 330);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Двоично-Десятичная";
            // 
            // comboBoxDdcKey
            // 
            this.comboBoxDdcKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDdcKey.FormattingEnabled = true;
            this.comboBoxDdcKey.Items.AddRange(new object[] {
            "3-3-2-1",
            "4-2-2-1",
            "4-3-1-1",
            "4-3-2-1",
            "4-4-2-1",
            "5-2-1-1",
            "5-2-2-1",
            "5-3-1-1",
            "5-3-2-1",
            "5-4-2-1",
            "6-2-2-1",
            "6-3-1-1",
            "6-3-2-1",
            "6-4-2-1",
            "7-3-2-1",
            "7-4-2-1",
            "8-4-2-1"});
            this.comboBoxDdcKey.Location = new System.Drawing.Point(64, 36);
            this.comboBoxDdcKey.Name = "comboBoxDdcKey";
            this.comboBoxDdcKey.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDdcKey.Sorted = true;
            this.comboBoxDdcKey.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Ключ";
            // 
            // textBoxDdcData
            // 
            this.textBoxDdcData.Location = new System.Drawing.Point(64, 8);
            this.textBoxDdcData.Name = "textBoxDdcData";
            this.textBoxDdcData.Size = new System.Drawing.Size(331, 20);
            this.textBoxDdcData.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Данные";
            // 
            // BCDDecodeButton
            // 
            this.BCDDecodeButton.Location = new System.Drawing.Point(401, 34);
            this.BCDDecodeButton.Name = "BCDDecodeButton";
            this.BCDDecodeButton.Size = new System.Drawing.Size(89, 23);
            this.BCDDecodeButton.TabIndex = 12;
            this.BCDDecodeButton.Text = "Дешифровать";
            this.BCDDecodeButton.UseVisualStyleBackColor = true;
            this.BCDDecodeButton.Click += new System.EventHandler(this.BCDDecodeButton_Click);
            // 
            // BCDEncodeButton
            // 
            this.BCDEncodeButton.Location = new System.Drawing.Point(401, 6);
            this.BCDEncodeButton.Name = "BCDEncodeButton";
            this.BCDEncodeButton.Size = new System.Drawing.Size(89, 23);
            this.BCDEncodeButton.TabIndex = 11;
            this.BCDEncodeButton.Text = "Шифровать";
            this.BCDEncodeButton.UseVisualStyleBackColor = true;
            this.BCDEncodeButton.Click += new System.EventHandler(this.BCDEncodeButton_Click);
            // 
            // listBoxDdc
            // 
            this.listBoxDdc.FormattingEnabled = true;
            this.listBoxDdc.Location = new System.Drawing.Point(7, 71);
            this.listBoxDdc.Name = "listBoxDdc";
            this.listBoxDdc.Size = new System.Drawing.Size(388, 251);
            this.listBoxDdc.TabIndex = 10;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.BerderDecodeButton);
            this.tabPage3.Controls.Add(this.textBoxBerger);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.BerderEncodeButton);
            this.tabPage3.Controls.Add(this.listBoxBerger);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(496, 330);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Код Бергера";
            // 
            // BerderDecodeButton
            // 
            this.BerderDecodeButton.Location = new System.Drawing.Point(401, 36);
            this.BerderDecodeButton.Name = "BerderDecodeButton";
            this.BerderDecodeButton.Size = new System.Drawing.Size(89, 23);
            this.BerderDecodeButton.TabIndex = 9;
            this.BerderDecodeButton.Text = "Дешифровать";
            this.BerderDecodeButton.UseVisualStyleBackColor = true;
            this.BerderDecodeButton.Click += new System.EventHandler(this.BerderDecodeButton_Click);
            // 
            // textBoxBerger
            // 
            this.textBoxBerger.Location = new System.Drawing.Point(64, 10);
            this.textBoxBerger.Name = "textBoxBerger";
            this.textBoxBerger.Size = new System.Drawing.Size(331, 20);
            this.textBoxBerger.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Данные";
            // 
            // BerderEncodeButton
            // 
            this.BerderEncodeButton.Location = new System.Drawing.Point(401, 8);
            this.BerderEncodeButton.Name = "BerderEncodeButton";
            this.BerderEncodeButton.Size = new System.Drawing.Size(89, 23);
            this.BerderEncodeButton.TabIndex = 6;
            this.BerderEncodeButton.Text = "Шифровать";
            this.BerderEncodeButton.UseVisualStyleBackColor = true;
            this.BerderEncodeButton.Click += new System.EventHandler(this.BerderEncodeButton_Click);
            // 
            // listBoxBerger
            // 
            this.listBoxBerger.FormattingEnabled = true;
            this.listBoxBerger.Location = new System.Drawing.Point(7, 32);
            this.listBoxBerger.Name = "listBoxBerger";
            this.listBoxBerger.Size = new System.Drawing.Size(388, 290);
            this.listBoxBerger.TabIndex = 5;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.richTextBoxEllays);
            this.tabPage4.Controls.Add(this.listBoxEllays);
            this.tabPage4.Controls.Add(this.EllaysDecodeButton);
            this.tabPage4.Controls.Add(this.EllaysEncodeButton);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(496, 330);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Код Еллайса";
            // 
            // richTextBoxEllays
            // 
            this.richTextBoxEllays.Location = new System.Drawing.Point(6, 6);
            this.richTextBoxEllays.Name = "richTextBoxEllays";
            this.richTextBoxEllays.Size = new System.Drawing.Size(388, 163);
            this.richTextBoxEllays.TabIndex = 51;
            this.richTextBoxEllays.Text = "";
            // 
            // listBoxEllays
            // 
            this.listBoxEllays.FormattingEnabled = true;
            this.listBoxEllays.Location = new System.Drawing.Point(6, 175);
            this.listBoxEllays.Name = "listBoxEllays";
            this.listBoxEllays.Size = new System.Drawing.Size(483, 147);
            this.listBoxEllays.TabIndex = 50;
            // 
            // EllaysDecodeButton
            // 
            this.EllaysDecodeButton.Location = new System.Drawing.Point(400, 34);
            this.EllaysDecodeButton.Name = "EllaysDecodeButton";
            this.EllaysDecodeButton.Size = new System.Drawing.Size(89, 45);
            this.EllaysDecodeButton.TabIndex = 49;
            this.EllaysDecodeButton.Text = "Исправить ошибки";
            this.EllaysDecodeButton.UseVisualStyleBackColor = true;
            this.EllaysDecodeButton.Click += new System.EventHandler(this.EllaysDecodeButton_Click);
            // 
            // EllaysEncodeButton
            // 
            this.EllaysEncodeButton.Location = new System.Drawing.Point(400, 6);
            this.EllaysEncodeButton.Name = "EllaysEncodeButton";
            this.EllaysEncodeButton.Size = new System.Drawing.Size(89, 23);
            this.EllaysEncodeButton.TabIndex = 48;
            this.EllaysEncodeButton.Text = "Шифровать";
            this.EllaysEncodeButton.UseVisualStyleBackColor = true;
            this.EllaysEncodeButton.Click += new System.EventHandler(this.EllaysEncodeButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 357);
            this.Controls.Add(this.tabControl1);
            this.Name = "Main";
            this.Text = "Code Tool";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button GreyDecodeButton;
        private System.Windows.Forms.TextBox textBoxCodeGray;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button GreyEncodeButton;
        private System.Windows.Forms.ListBox listBoxCodeGray;
        private System.Windows.Forms.Button BerderDecodeButton;
        private System.Windows.Forms.TextBox textBoxBerger;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BerderEncodeButton;
        private System.Windows.Forms.ListBox listBoxBerger;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDdcData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BCDDecodeButton;
        private System.Windows.Forms.Button BCDEncodeButton;
        private System.Windows.Forms.ListBox listBoxDdc;
        private System.Windows.Forms.ComboBox comboBoxDdcKey;
        private System.Windows.Forms.Button EllaysDecodeButton;
        private System.Windows.Forms.Button EllaysEncodeButton;
        private System.Windows.Forms.ListBox listBoxEllays;
        private System.Windows.Forms.RichTextBox richTextBoxEllays;
    }
}