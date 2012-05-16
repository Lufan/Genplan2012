/*
 * Created by Луферов Александр Николаевич
 * 
 * 
 * 
 * Лицензия GNU Lesser General Public License : http://www.gnu.org/copyleft/lesser.html.
 */
namespace LufsGenplan
{
    partial class MarkingCalc
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.cbLayerUse = new System.Windows.Forms.ComboBox();
            this.btSample = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RbUser = new System.Windows.Forms.RadioButton();
            this.RbAll = new System.Windows.Forms.RadioButton();
            this.CheckbMasUse = new System.Windows.Forms.CheckBox();
            this.cbMasht = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.BtSolut = new System.Windows.Forms.Button();
            this.dataGridResult = new System.Windows.Forms.DataGridView();
            this.entityTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nPPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.razmDescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.razmTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.razmLenghtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.razmAreaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.razmDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btClear = new System.Windows.Forms.Button();
            this.btToExcel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.razmDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите слой с разметкой:";
            // 
            // cbLayerUse
            // 
            this.cbLayerUse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLayerUse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLayerUse.FormattingEnabled = true;
            this.cbLayerUse.Location = new System.Drawing.Point(163, 6);
            this.cbLayerUse.Name = "cbLayerUse";
            this.cbLayerUse.Size = new System.Drawing.Size(365, 21);
            this.cbLayerUse.Sorted = true;
            this.cbLayerUse.TabIndex = 1;
            this.cbLayerUse.SelectedIndexChanged += new System.EventHandler(this.cbLayerUse_SelectedIndexChanged);
            // 
            // btSample
            // 
            this.btSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSample.Location = new System.Drawing.Point(534, 4);
            this.btSample.Name = "btSample";
            this.btSample.Size = new System.Drawing.Size(75, 23);
            this.btSample.TabIndex = 2;
            this.btSample.Text = "Указать";
            this.btSample.UseVisualStyleBackColor = true;
            this.btSample.Click += new System.EventHandler(this.btSample_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RbUser);
            this.groupBox1.Controls.Add(this.RbAll);
            this.groupBox1.Location = new System.Drawing.Point(6, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 67);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Область выбора:";
            // 
            // RbUser
            // 
            this.RbUser.AutoSize = true;
            this.RbUser.Location = new System.Drawing.Point(6, 42);
            this.RbUser.Name = "RbUser";
            this.RbUser.Size = new System.Drawing.Size(125, 17);
            this.RbUser.TabIndex = 1;
            this.RbUser.Text = "Выбор выделением";
            this.RbUser.UseVisualStyleBackColor = true;
            // 
            // RbAll
            // 
            this.RbAll.AutoSize = true;
            this.RbAll.Checked = true;
            this.RbAll.Location = new System.Drawing.Point(6, 19);
            this.RbAll.Name = "RbAll";
            this.RbAll.Size = new System.Drawing.Size(151, 17);
            this.RbAll.TabIndex = 0;
            this.RbAll.TabStop = true;
            this.RbAll.Text = "Выбор по всему чертежу";
            this.RbAll.UseVisualStyleBackColor = true;
            // 
            // CheckbMasUse
            // 
            this.CheckbMasUse.AutoSize = true;
            this.CheckbMasUse.Checked = true;
            this.CheckbMasUse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckbMasUse.Location = new System.Drawing.Point(212, 53);
            this.CheckbMasUse.Name = "CheckbMasUse";
            this.CheckbMasUse.Size = new System.Drawing.Size(150, 17);
            this.CheckbMasUse.TabIndex = 4;
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
            this.cbMasht.Location = new System.Drawing.Point(357, 51);
            this.cbMasht.Name = "cbMasht";
            this.cbMasht.Size = new System.Drawing.Size(123, 21);
            this.cbMasht.TabIndex = 5;
            this.cbMasht.SelectedIndexChanged += new System.EventHandler(this.cbMasht_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(212, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Категория улицы/дороги:";
            // 
            // cbCategory
            // 
            this.cbCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Location = new System.Drawing.Point(357, 74);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(123, 21);
            this.cbCategory.TabIndex = 7;
            this.cbCategory.SelectedIndexChanged += new System.EventHandler(this.cbCategory_SelectedIndexChanged);
            // 
            // BtSolut
            // 
            this.BtSolut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtSolut.Location = new System.Drawing.Point(486, 74);
            this.BtSolut.Name = "BtSolut";
            this.BtSolut.Size = new System.Drawing.Size(123, 23);
            this.BtSolut.TabIndex = 8;
            this.BtSolut.Text = "Запустить расчет";
            this.BtSolut.UseVisualStyleBackColor = true;
            this.BtSolut.Click += new System.EventHandler(this.BtSolut_Click);
            // 
            // dataGridResult
            // 
            this.dataGridResult.AllowUserToAddRows = false;
            this.dataGridResult.AllowUserToDeleteRows = false;
            this.dataGridResult.AllowUserToResizeRows = false;
            this.dataGridResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridResult.AutoGenerateColumns = false;
            this.dataGridResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.entityTypeDataGridViewTextBoxColumn,
            this.nPPDataGridViewTextBoxColumn,
            this.razmDescriptionDataGridViewTextBoxColumn,
            this.razmTypeDataGridViewTextBoxColumn,
            this.razmLenghtDataGridViewTextBoxColumn,
            this.razmAreaDataGridViewTextBoxColumn});
            this.dataGridResult.DataBindings.Add(new System.Windows.Forms.Binding("Tag", this.razmDataBindingSource, "NPP", true));
            this.dataGridResult.DataSource = this.razmDataBindingSource;
            this.dataGridResult.Location = new System.Drawing.Point(6, 106);
            this.dataGridResult.Name = "dataGridResult";
            this.dataGridResult.ReadOnly = true;
            this.dataGridResult.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridResult.RowHeadersVisible = false;
            this.dataGridResult.Size = new System.Drawing.Size(603, 255);
            this.dataGridResult.TabIndex = 9;
            this.dataGridResult.Tag = "";
            // 
            // entityTypeDataGridViewTextBoxColumn
            // 
            this.entityTypeDataGridViewTextBoxColumn.DataPropertyName = "EntityType";
            this.entityTypeDataGridViewTextBoxColumn.HeaderText = "EntityType";
            this.entityTypeDataGridViewTextBoxColumn.Name = "entityTypeDataGridViewTextBoxColumn";
            this.entityTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.entityTypeDataGridViewTextBoxColumn.Visible = false;
            // 
            // nPPDataGridViewTextBoxColumn
            // 
            this.nPPDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nPPDataGridViewTextBoxColumn.DataPropertyName = "NPP";
            this.nPPDataGridViewTextBoxColumn.HeaderText = "№ п/п";
            this.nPPDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.nPPDataGridViewTextBoxColumn.Name = "nPPDataGridViewTextBoxColumn";
            this.nPPDataGridViewTextBoxColumn.ReadOnly = true;
            this.nPPDataGridViewTextBoxColumn.Width = 50;
            // 
            // razmDescriptionDataGridViewTextBoxColumn
            // 
            this.razmDescriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.razmDescriptionDataGridViewTextBoxColumn.DataPropertyName = "RazmDescription";
            this.razmDescriptionDataGridViewTextBoxColumn.HeaderText = "Наименование разметки";
            this.razmDescriptionDataGridViewTextBoxColumn.MinimumWidth = 200;
            this.razmDescriptionDataGridViewTextBoxColumn.Name = "razmDescriptionDataGridViewTextBoxColumn";
            this.razmDescriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // razmTypeDataGridViewTextBoxColumn
            // 
            this.razmTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.razmTypeDataGridViewTextBoxColumn.DataPropertyName = "RazmType";
            this.razmTypeDataGridViewTextBoxColumn.HeaderText = "Тип разметки";
            this.razmTypeDataGridViewTextBoxColumn.MinimumWidth = 70;
            this.razmTypeDataGridViewTextBoxColumn.Name = "razmTypeDataGridViewTextBoxColumn";
            this.razmTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.razmTypeDataGridViewTextBoxColumn.Width = 70;
            // 
            // razmLenghtDataGridViewTextBoxColumn
            // 
            this.razmLenghtDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.razmLenghtDataGridViewTextBoxColumn.DataPropertyName = "RazmLenght";
            this.razmLenghtDataGridViewTextBoxColumn.HeaderText = "Количество";
            this.razmLenghtDataGridViewTextBoxColumn.MinimumWidth = 80;
            this.razmLenghtDataGridViewTextBoxColumn.Name = "razmLenghtDataGridViewTextBoxColumn";
            this.razmLenghtDataGridViewTextBoxColumn.ReadOnly = true;
            this.razmLenghtDataGridViewTextBoxColumn.Width = 80;
            // 
            // razmAreaDataGridViewTextBoxColumn
            // 
            this.razmAreaDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.razmAreaDataGridViewTextBoxColumn.DataPropertyName = "RazmArea";
            this.razmAreaDataGridViewTextBoxColumn.HeaderText = "Площадь, м.кв.";
            this.razmAreaDataGridViewTextBoxColumn.MinimumWidth = 170;
            this.razmAreaDataGridViewTextBoxColumn.Name = "razmAreaDataGridViewTextBoxColumn";
            this.razmAreaDataGridViewTextBoxColumn.ReadOnly = true;
            this.razmAreaDataGridViewTextBoxColumn.Width = 170;
            // 
            // razmDataBindingSource
            // 
            this.razmDataBindingSource.DataSource = typeof(LufsGenplan.RazmData);
            // 
            // btClear
            // 
            this.btClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btClear.Location = new System.Drawing.Point(6, 367);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 10;
            this.btClear.Text = "Очистить";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btToExcel
            // 
            this.btToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btToExcel.Location = new System.Drawing.Point(486, 367);
            this.btToExcel.Name = "btToExcel";
            this.btToExcel.Size = new System.Drawing.Size(123, 23);
            this.btToExcel.TabIndex = 11;
            this.btToExcel.Text = "Сохранить на диск";
            this.btToExcel.UseVisualStyleBackColor = true;
            this.btToExcel.Click += new System.EventHandler(this.btToExcel_Click);
            // 
            // MarkingCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.btToExcel);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.dataGridResult);
            this.Controls.Add(this.BtSolut);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbMasht);
            this.Controls.Add(this.CheckbMasUse);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btSample);
            this.Controls.Add(this.cbLayerUse);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MarkingCalc";
            this.Size = new System.Drawing.Size(612, 400);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.razmDataBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbLayerUse;
        private System.Windows.Forms.Button btSample;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RbUser;
        private System.Windows.Forms.RadioButton RbAll;
        private System.Windows.Forms.CheckBox CheckbMasUse;
        private System.Windows.Forms.ComboBox cbMasht;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.Button BtSolut;
        private System.Windows.Forms.DataGridView dataGridResult;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btToExcel;
        private System.Windows.Forms.BindingSource razmDataBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn entityTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nPPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn razmDescriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn razmTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn razmLenghtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn razmAreaDataGridViewTextBoxColumn;
    }
}
