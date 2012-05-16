/*
 * Created by Луферов Александр Николаевич
 * 
 * 
 * 
 * Лицензия GNU Lesser General Public License : http://www.gnu.org/copyleft/lesser.html.
 */
namespace AreaLinearCalc
{
    partial class AreaLinearCalc
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btSample = new System.Windows.Forms.Button();
            this.CheckbOnlyModel = new System.Windows.Forms.CheckBox();
            this.cbLinetUse = new System.Windows.Forms.ComboBox();
            this.CheckbLinetUse = new System.Windows.Forms.CheckBox();
            this.BtSelectColor = new System.Windows.Forms.Button();
            this.LbColorName = new System.Windows.Forms.Label();
            this.pColor = new System.Windows.Forms.Panel();
            this.CheckbColorUse = new System.Windows.Forms.CheckBox();
            this.cbLayerUse = new System.Windows.Forms.ComboBox();
            this.CheckbLayerUse = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.RbUser = new System.Windows.Forms.RadioButton();
            this.RbAll = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.RbAllEnt = new System.Windows.Forms.RadioButton();
            this.RbPlineEnt = new System.Windows.Forms.RadioButton();
            this.CheckbMasUse = new System.Windows.Forms.CheckBox();
            this.cbMasht = new System.Windows.Forms.ComboBox();
            this.BtSolut = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.RichTextBox();
            this.btErase = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btSample);
            this.groupBox1.Controls.Add(this.CheckbOnlyModel);
            this.groupBox1.Controls.Add(this.cbLinetUse);
            this.groupBox1.Controls.Add(this.CheckbLinetUse);
            this.groupBox1.Controls.Add(this.BtSelectColor);
            this.groupBox1.Controls.Add(this.LbColorName);
            this.groupBox1.Controls.Add(this.pColor);
            this.groupBox1.Controls.Add(this.CheckbColorUse);
            this.groupBox1.Controls.Add(this.cbLayerUse);
            this.groupBox1.Controls.Add(this.CheckbLayerUse);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(606, 129);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выбор в соответствии с параметрами:";
            // 
            // btSample
            // 
            this.btSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSample.Location = new System.Drawing.Point(364, 98);
            this.btSample.Name = "btSample";
            this.btSample.Size = new System.Drawing.Size(230, 23);
            this.btSample.TabIndex = 10;
            this.btSample.Text = "Определить по образцу";
            this.btSample.UseVisualStyleBackColor = true;
            this.btSample.Click += new System.EventHandler(this.btSample_Click);
            // 
            // CheckbOnlyModel
            // 
            this.CheckbOnlyModel.AutoSize = true;
            this.CheckbOnlyModel.Checked = true;
            this.CheckbOnlyModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckbOnlyModel.Location = new System.Drawing.Point(12, 104);
            this.CheckbOnlyModel.Name = "CheckbOnlyModel";
            this.CheckbOnlyModel.Size = new System.Drawing.Size(289, 17);
            this.CheckbOnlyModel.TabIndex = 9;
            this.CheckbOnlyModel.Text = "Учитывать объекты только в пространстве модели";
            this.CheckbOnlyModel.UseVisualStyleBackColor = true;
            // 
            // cbLinetUse
            // 
            this.cbLinetUse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLinetUse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLinetUse.Enabled = false;
            this.cbLinetUse.FormattingEnabled = true;
            this.cbLinetUse.Location = new System.Drawing.Point(223, 72);
            this.cbLinetUse.Name = "cbLinetUse";
            this.cbLinetUse.Size = new System.Drawing.Size(371, 21);
            this.cbLinetUse.Sorted = true;
            this.cbLinetUse.TabIndex = 8;
            // 
            // CheckbLinetUse
            // 
            this.CheckbLinetUse.AutoSize = true;
            this.CheckbLinetUse.Location = new System.Drawing.Point(12, 74);
            this.CheckbLinetUse.Name = "CheckbLinetUse";
            this.CheckbLinetUse.Size = new System.Drawing.Size(202, 17);
            this.CheckbLinetUse.TabIndex = 7;
            this.CheckbLinetUse.Text = "Выбор соответсвия по типу линии:";
            this.CheckbLinetUse.UseVisualStyleBackColor = true;
            this.CheckbLinetUse.CheckedChanged += new System.EventHandler(this.CheckbLinetUse_CheckedChanged);
            // 
            // BtSelectColor
            // 
            this.BtSelectColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtSelectColor.Enabled = false;
            this.BtSelectColor.Location = new System.Drawing.Point(519, 44);
            this.BtSelectColor.Name = "BtSelectColor";
            this.BtSelectColor.Size = new System.Drawing.Size(75, 23);
            this.BtSelectColor.TabIndex = 6;
            this.BtSelectColor.Text = "Выбрать";
            this.BtSelectColor.UseVisualStyleBackColor = true;
            this.BtSelectColor.Click += new System.EventHandler(this.BtSelectColor_Click);
            // 
            // LbColorName
            // 
            this.LbColorName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LbColorName.AutoSize = true;
            this.LbColorName.Location = new System.Drawing.Point(429, 45);
            this.LbColorName.Name = "LbColorName";
            this.LbColorName.Size = new System.Drawing.Size(32, 13);
            this.LbColorName.TabIndex = 5;
            this.LbColorName.Text = "Цвет";
            // 
            // pColor
            // 
            this.pColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pColor.Location = new System.Drawing.Point(223, 45);
            this.pColor.Name = "pColor";
            this.pColor.Size = new System.Drawing.Size(200, 15);
            this.pColor.TabIndex = 4;
            // 
            // CheckbColorUse
            // 
            this.CheckbColorUse.AutoSize = true;
            this.CheckbColorUse.Location = new System.Drawing.Point(12, 45);
            this.CheckbColorUse.Name = "CheckbColorUse";
            this.CheckbColorUse.Size = new System.Drawing.Size(175, 17);
            this.CheckbColorUse.TabIndex = 3;
            this.CheckbColorUse.Text = "Выбор соответсвия по цвету:";
            this.CheckbColorUse.UseVisualStyleBackColor = true;
            this.CheckbColorUse.CheckedChanged += new System.EventHandler(this.CheckbColorUse_CheckedChanged);
            // 
            // cbLayerUse
            // 
            this.cbLayerUse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLayerUse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayerUse.Enabled = false;
            this.cbLayerUse.FormattingEnabled = true;
            this.cbLayerUse.Location = new System.Drawing.Point(223, 16);
            this.cbLayerUse.Name = "cbLayerUse";
            this.cbLayerUse.Size = new System.Drawing.Size(371, 21);
            this.cbLayerUse.Sorted = true;
            this.cbLayerUse.TabIndex = 1;
            this.cbLayerUse.SelectedIndexChanged += new System.EventHandler(this.cbLayerUse_SelectedIndexChanged);
            // 
            // CheckbLayerUse
            // 
            this.CheckbLayerUse.AutoSize = true;
            this.CheckbLayerUse.Location = new System.Drawing.Point(12, 18);
            this.CheckbLayerUse.Name = "CheckbLayerUse";
            this.CheckbLayerUse.Size = new System.Drawing.Size(173, 17);
            this.CheckbLayerUse.TabIndex = 0;
            this.CheckbLayerUse.Text = "Выбор соответсвия по слою:";
            this.CheckbLayerUse.UseVisualStyleBackColor = true;
            this.CheckbLayerUse.CheckedChanged += new System.EventHandler(this.CheckbLayerUse_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.RbUser);
            this.groupBox2.Controls.Add(this.RbAll);
            this.groupBox2.Location = new System.Drawing.Point(3, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(167, 65);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Область выбора:";
            // 
            // RbUser
            // 
            this.RbUser.AutoSize = true;
            this.RbUser.Checked = true;
            this.RbUser.Location = new System.Drawing.Point(12, 39);
            this.RbUser.Name = "RbUser";
            this.RbUser.Size = new System.Drawing.Size(125, 17);
            this.RbUser.TabIndex = 1;
            this.RbUser.TabStop = true;
            this.RbUser.Text = "Выбор выделением";
            this.RbUser.UseVisualStyleBackColor = true;
            // 
            // RbAll
            // 
            this.RbAll.AutoSize = true;
            this.RbAll.Location = new System.Drawing.Point(12, 16);
            this.RbAll.Name = "RbAll";
            this.RbAll.Size = new System.Drawing.Size(151, 17);
            this.RbAll.TabIndex = 0;
            this.RbAll.TabStop = true;
            this.RbAll.Text = "Выбор по всему чертежу";
            this.RbAll.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.RbAllEnt);
            this.groupBox3.Controls.Add(this.RbPlineEnt);
            this.groupBox3.Location = new System.Drawing.Point(176, 138);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(93, 65);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Примитивы:";
            // 
            // RbAllEnt
            // 
            this.RbAllEnt.AutoSize = true;
            this.RbAllEnt.Checked = true;
            this.RbAllEnt.Location = new System.Drawing.Point(6, 39);
            this.RbAllEnt.Name = "RbAllEnt";
            this.RbAllEnt.Size = new System.Drawing.Size(44, 17);
            this.RbAllEnt.TabIndex = 1;
            this.RbAllEnt.TabStop = true;
            this.RbAllEnt.Text = "Все";
            this.RbAllEnt.UseVisualStyleBackColor = true;
            // 
            // RbPlineEnt
            // 
            this.RbPlineEnt.AutoSize = true;
            this.RbPlineEnt.Location = new System.Drawing.Point(6, 16);
            this.RbPlineEnt.Name = "RbPlineEnt";
            this.RbPlineEnt.Size = new System.Drawing.Size(81, 17);
            this.RbPlineEnt.TabIndex = 0;
            this.RbPlineEnt.TabStop = true;
            this.RbPlineEnt.Text = "Полилинии";
            this.RbPlineEnt.UseVisualStyleBackColor = true;
            // 
            // CheckbMasUse
            // 
            this.CheckbMasUse.AutoSize = true;
            this.CheckbMasUse.Checked = true;
            this.CheckbMasUse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckbMasUse.Location = new System.Drawing.Point(275, 154);
            this.CheckbMasUse.Name = "CheckbMasUse";
            this.CheckbMasUse.Size = new System.Drawing.Size(150, 17);
            this.CheckbMasUse.TabIndex = 3;
            this.CheckbMasUse.Text = "Использовать масштаб:";
            this.CheckbMasUse.UseVisualStyleBackColor = true;
            this.CheckbMasUse.CheckedChanged += new System.EventHandler(this.CheckbMasUse_CheckedChanged);
            // 
            // cbMasht
            // 
            this.cbMasht.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMasht.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMasht.FormattingEnabled = true;
            this.cbMasht.Items.AddRange(new object[] {
            "1:1",
            "1:10",
            "1:20",
            "1:50",
            "1:100",
            "1:200",
            "1:250",
            "1:500",
            "1:1000",
            "1:2000",
            "1:2500",
            "1:5000"});
            this.cbMasht.Location = new System.Drawing.Point(431, 150);
            this.cbMasht.Name = "cbMasht";
            this.cbMasht.Size = new System.Drawing.Size(166, 21);
            this.cbMasht.TabIndex = 4;
            // 
            // BtSolut
            // 
            this.BtSolut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtSolut.Location = new System.Drawing.Point(275, 180);
            this.BtSolut.Name = "BtSolut";
            this.BtSolut.Size = new System.Drawing.Size(322, 23);
            this.BtSolut.TabIndex = 5;
            this.BtSolut.Text = "Запустить расчет";
            this.BtSolut.UseVisualStyleBackColor = true;
            this.BtSolut.Click += new System.EventHandler(this.BtSolut_Click);
            // 
            // tbResult
            // 
            this.tbResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbResult.Location = new System.Drawing.Point(3, 209);
            this.tbResult.Name = "tbResult";
            this.tbResult.ReadOnly = true;
            this.tbResult.Size = new System.Drawing.Size(513, 185);
            this.tbResult.TabIndex = 6;
            this.tbResult.Text = "";
            // 
            // btErase
            // 
            this.btErase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btErase.Location = new System.Drawing.Point(522, 371);
            this.btErase.Name = "btErase";
            this.btErase.Size = new System.Drawing.Size(75, 23);
            this.btErase.TabIndex = 7;
            this.btErase.Text = "Очистить";
            this.btErase.UseVisualStyleBackColor = true;
            this.btErase.Click += new System.EventHandler(this.btErase_Click);
            // 
            // AreaLinearCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.btErase);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.BtSolut);
            this.Controls.Add(this.cbMasht);
            this.Controls.Add(this.CheckbMasUse);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(600, 200);
            this.Name = "AreaLinearCalc";
            this.Size = new System.Drawing.Size(612, 400);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btSample;
        private System.Windows.Forms.CheckBox CheckbOnlyModel;
        private System.Windows.Forms.ComboBox cbLinetUse;
        private System.Windows.Forms.CheckBox CheckbLinetUse;
        private System.Windows.Forms.Button BtSelectColor;
        private System.Windows.Forms.Label LbColorName;
        private System.Windows.Forms.Panel pColor;
        private System.Windows.Forms.CheckBox CheckbColorUse;
        private System.Windows.Forms.ComboBox cbLayerUse;
        private System.Windows.Forms.CheckBox CheckbLayerUse;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton RbAll;
        private System.Windows.Forms.RadioButton RbUser;
        private System.Windows.Forms.RadioButton RbAllEnt;
        private System.Windows.Forms.RadioButton RbPlineEnt;
        private System.Windows.Forms.CheckBox CheckbMasUse;
        private System.Windows.Forms.ComboBox cbMasht;
        private System.Windows.Forms.Button BtSolut;
        private System.Windows.Forms.RichTextBox tbResult;
        private System.Windows.Forms.Button btErase;


    }
}
