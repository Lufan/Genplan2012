/*
 * Created by Луферов Александр Николаевич
 * 
 * 
 * 
 * Лицензия GNU Lesser General Public License : http://www.gnu.org/copyleft/lesser.html.
 */
namespace RoadPavenment
{
    partial class RoadPavenment
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
            this.cbAlignment = new System.Windows.Forms.ComboBox();
            this.btGetAlign = new System.Windows.Forms.Button();
            this.btGetSurfEx = new System.Windows.Forms.Button();
            this.cbSurfEx = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btGetSurfPr = new System.Windows.Forms.Button();
            this.cbSurfPr = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbFillM = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbCutM = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbMinWidth = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbMaxWidth = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.BtSolut = new System.Windows.Forms.Button();
            this.dataGridResult = new System.Windows.Forms.DataGridView();
            this.btToExcel = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.tbEndStation = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbStartStation = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tbStep = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.RoadPavenmentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pKDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pKPlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leftBDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leftDeltaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.centrDeltaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightDeltaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightBDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.areaFDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.areaCDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leftPtExDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightPtExDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.centrPtExDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.leftPtPrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rightPtPrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.centrPtPrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.areaCutDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.areaFillDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoadPavenmentBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выберите проектную ось трассы:";
            // 
            // cbAlignment
            // 
            this.cbAlignment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAlignment.FormattingEnabled = true;
            this.cbAlignment.Location = new System.Drawing.Point(217, 8);
            this.cbAlignment.Name = "cbAlignment";
            this.cbAlignment.Size = new System.Drawing.Size(311, 21);
            this.cbAlignment.TabIndex = 1;
            this.cbAlignment.SelectedIndexChanged += new System.EventHandler(this.cbAlignment_SelectedIndexChanged);
            this.cbAlignment.TextChanged += new System.EventHandler(this.cbAlignment_TextChanged);
            // 
            // btGetAlign
            // 
            this.btGetAlign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btGetAlign.Location = new System.Drawing.Point(534, 6);
            this.btGetAlign.Name = "btGetAlign";
            this.btGetAlign.Size = new System.Drawing.Size(75, 23);
            this.btGetAlign.TabIndex = 2;
            this.btGetAlign.Text = "Указать";
            this.btGetAlign.UseVisualStyleBackColor = true;
            this.btGetAlign.Click += new System.EventHandler(this.btGetAlign_Click);
            // 
            // btGetSurfEx
            // 
            this.btGetSurfEx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btGetSurfEx.Location = new System.Drawing.Point(534, 33);
            this.btGetSurfEx.Name = "btGetSurfEx";
            this.btGetSurfEx.Size = new System.Drawing.Size(75, 23);
            this.btGetSurfEx.TabIndex = 5;
            this.btGetSurfEx.Text = "Указать";
            this.btGetSurfEx.UseVisualStyleBackColor = true;
            this.btGetSurfEx.Click += new System.EventHandler(this.btGetSurfEx_Click);
            // 
            // cbSurfEx
            // 
            this.cbSurfEx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSurfEx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSurfEx.FormattingEnabled = true;
            this.cbSurfEx.Location = new System.Drawing.Point(217, 35);
            this.cbSurfEx.Name = "cbSurfEx";
            this.cbSurfEx.Size = new System.Drawing.Size(311, 21);
            this.cbSurfEx.TabIndex = 4;
            this.cbSurfEx.TextChanged += new System.EventHandler(this.cbSurfEx_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Выберите существующую поверхность:";
            // 
            // btGetSurfPr
            // 
            this.btGetSurfPr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btGetSurfPr.Location = new System.Drawing.Point(534, 60);
            this.btGetSurfPr.Name = "btGetSurfPr";
            this.btGetSurfPr.Size = new System.Drawing.Size(75, 23);
            this.btGetSurfPr.TabIndex = 8;
            this.btGetSurfPr.Text = "Указать";
            this.btGetSurfPr.UseVisualStyleBackColor = true;
            this.btGetSurfPr.Click += new System.EventHandler(this.btGetSurfPr_Click);
            // 
            // cbSurfPr
            // 
            this.cbSurfPr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSurfPr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSurfPr.FormattingEnabled = true;
            this.cbSurfPr.Location = new System.Drawing.Point(217, 62);
            this.cbSurfPr.Name = "cbSurfPr";
            this.cbSurfPr.Size = new System.Drawing.Size(311, 21);
            this.cbSurfPr.TabIndex = 7;
            this.cbSurfPr.TextChanged += new System.EventHandler(this.cbSurfPr_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Выберите проектную поверхность:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Объемный вес выравнивающего слоя:";
            // 
            // tbFillM
            // 
            this.tbFillM.Location = new System.Drawing.Point(217, 89);
            this.tbFillM.MaxLength = 8;
            this.tbFillM.Name = "tbFillM";
            this.tbFillM.Size = new System.Drawing.Size(49, 20);
            this.tbFillM.TabIndex = 10;
            this.tbFillM.Text = "2.47";
            this.tbFillM.TextChanged += new System.EventHandler(this.tbFillM_TextChanged);
            this.tbFillM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFillM_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(272, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "т/м.куб.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(272, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "т/м.куб.";
            // 
            // tbCutM
            // 
            this.tbCutM.Location = new System.Drawing.Point(217, 115);
            this.tbCutM.MaxLength = 8;
            this.tbCutM.Name = "tbCutM";
            this.tbCutM.Size = new System.Drawing.Size(49, 20);
            this.tbCutM.TabIndex = 13;
            this.tbCutM.Text = "2.38";
            this.tbCutM.TextChanged += new System.EventHandler(this.tbCutM_TextChanged);
            this.tbCutM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCutM_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(190, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Объемный вес фрезеруемого слоя:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(272, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "м.";
            // 
            // tbMinWidth
            // 
            this.tbMinWidth.Location = new System.Drawing.Point(217, 141);
            this.tbMinWidth.MaxLength = 8;
            this.tbMinWidth.Name = "tbMinWidth";
            this.tbMinWidth.Size = new System.Drawing.Size(49, 20);
            this.tbMinWidth.TabIndex = 16;
            this.tbMinWidth.Text = "0.00";
            this.tbMinWidth.TextChanged += new System.EventHandler(this.tbMinWidth_TextChanged);
            this.tbMinWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMinWidth_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(190, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Минимальная ширина поперечника:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(272, 170);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(18, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "м.";
            // 
            // tbMaxWidth
            // 
            this.tbMaxWidth.Location = new System.Drawing.Point(217, 167);
            this.tbMaxWidth.MaxLength = 8;
            this.tbMaxWidth.Name = "tbMaxWidth";
            this.tbMaxWidth.Size = new System.Drawing.Size(49, 20);
            this.tbMaxWidth.TabIndex = 19;
            this.tbMaxWidth.Text = "10.00";
            this.tbMaxWidth.TextChanged += new System.EventHandler(this.tbMaxWidth_TextChanged);
            this.tbMaxWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMaxWidth_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 170);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(196, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Максимальная ширина поперечника:";
            // 
            // BtSolut
            // 
            this.BtSolut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BtSolut.Location = new System.Drawing.Point(347, 164);
            this.BtSolut.Name = "BtSolut";
            this.BtSolut.Size = new System.Drawing.Size(262, 23);
            this.BtSolut.TabIndex = 22;
            this.BtSolut.Text = "Запустить расчет";
            this.BtSolut.UseVisualStyleBackColor = true;
            this.BtSolut.Click += new System.EventHandler(this.BtSolut_Click);
            // 
            // dataGridResult
            // 
            this.dataGridResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridResult.AutoGenerateColumns = false;
            this.dataGridResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pKDataGridViewTextBoxColumn,
            this.pKPlDataGridViewTextBoxColumn,
            this.leftBDataGridViewTextBoxColumn,
            this.leftDeltaDataGridViewTextBoxColumn,
            this.centrDeltaDataGridViewTextBoxColumn,
            this.rightDeltaDataGridViewTextBoxColumn,
            this.rightBDataGridViewTextBoxColumn,
            this.areaFDataGridViewTextBoxColumn,
            this.areaCDataGridViewTextBoxColumn,
            this.leftPtExDataGridViewTextBoxColumn,
            this.rightPtExDataGridViewTextBoxColumn,
            this.centrPtExDataGridViewTextBoxColumn,
            this.leftPtPrDataGridViewTextBoxColumn,
            this.rightPtPrDataGridViewTextBoxColumn,
            this.centrPtPrDataGridViewTextBoxColumn,
            this.areaCutDataGridViewTextBoxColumn,
            this.areaFillDataGridViewTextBoxColumn});
            this.dataGridResult.DataSource = this.RoadPavenmentBindingSource;
            this.dataGridResult.Location = new System.Drawing.Point(6, 193);
            this.dataGridResult.Name = "dataGridResult";
            this.dataGridResult.RowHeadersVisible = false;
            this.dataGridResult.Size = new System.Drawing.Size(603, 175);
            this.dataGridResult.TabIndex = 23;
            // 
            // btToExcel
            // 
            this.btToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btToExcel.Enabled = false;
            this.btToExcel.Location = new System.Drawing.Point(486, 374);
            this.btToExcel.Name = "btToExcel";
            this.btToExcel.Size = new System.Drawing.Size(123, 23);
            this.btToExcel.TabIndex = 25;
            this.btToExcel.Text = "Сохранить на диск";
            this.btToExcel.UseVisualStyleBackColor = true;
            this.btToExcel.Click += new System.EventHandler(this.btToExcel_Click);
            // 
            // btClear
            // 
            this.btClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btClear.Enabled = false;
            this.btClear.Location = new System.Drawing.Point(6, 374);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(75, 23);
            this.btClear.TabIndex = 24;
            this.btClear.Text = "Очистить";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // tbEndStation
            // 
            this.tbEndStation.Location = new System.Drawing.Point(464, 115);
            this.tbEndStation.MaxLength = 8;
            this.tbEndStation.Name = "tbEndStation";
            this.tbEndStation.Size = new System.Drawing.Size(49, 20);
            this.tbEndStation.TabIndex = 30;
            this.tbEndStation.Text = "100.00";
            this.tbEndStation.TextChanged += new System.EventHandler(this.tbEndStation_TextChanged);
            this.tbEndStation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbEndStation_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(375, 118);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "Конец участка:";
            // 
            // tbStartStation
            // 
            this.tbStartStation.Location = new System.Drawing.Point(464, 89);
            this.tbStartStation.MaxLength = 8;
            this.tbStartStation.Name = "tbStartStation";
            this.tbStartStation.Size = new System.Drawing.Size(49, 20);
            this.tbStartStation.TabIndex = 27;
            this.tbStartStation.Text = "0.00";
            this.tbStartStation.TextChanged += new System.EventHandler(this.tbStartStation_TextChanged);
            this.tbStartStation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbStartStation_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(375, 92);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(89, 13);
            this.label15.TabIndex = 26;
            this.label15.Text = "Начало участка:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(534, 87);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "Указать";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(534, 113);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 32;
            this.button2.Text = "Указать";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tbStep
            // 
            this.tbStep.Location = new System.Drawing.Point(464, 141);
            this.tbStep.MaxLength = 8;
            this.tbStep.Name = "tbStep";
            this.tbStep.Size = new System.Drawing.Size(49, 20);
            this.tbStep.TabIndex = 34;
            this.tbStep.Text = "10.00";
            this.tbStep.TextChanged += new System.EventHandler(this.tbStep_TextChanged);
            this.tbStep.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbStep_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(375, 144);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 33;
            this.label12.Text = "Шаг расчета:";
            // 
            // RoadPavenmentBindingSource
            // 
            this.RoadPavenmentBindingSource.DataSource = typeof(LufsGenplan.ResultData);
            // 
            // pKDataGridViewTextBoxColumn
            // 
            this.pKDataGridViewTextBoxColumn.DataPropertyName = "PK";
            this.pKDataGridViewTextBoxColumn.HeaderText = "PK";
            this.pKDataGridViewTextBoxColumn.Name = "pKDataGridViewTextBoxColumn";
            this.pKDataGridViewTextBoxColumn.ReadOnly = true;
            this.pKDataGridViewTextBoxColumn.Visible = false;
            // 
            // pKPlDataGridViewTextBoxColumn
            // 
            this.pKPlDataGridViewTextBoxColumn.DataPropertyName = "PKPl";
            this.pKPlDataGridViewTextBoxColumn.HeaderText = "Пикет+, м";
            this.pKPlDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.pKPlDataGridViewTextBoxColumn.Name = "pKPlDataGridViewTextBoxColumn";
            this.pKPlDataGridViewTextBoxColumn.ReadOnly = true;
            this.pKPlDataGridViewTextBoxColumn.Width = 75;
            // 
            // leftBDataGridViewTextBoxColumn
            // 
            this.leftBDataGridViewTextBoxColumn.DataPropertyName = "LeftB";
            this.leftBDataGridViewTextBoxColumn.HeaderText = "Левая граница, м";
            this.leftBDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.leftBDataGridViewTextBoxColumn.Name = "leftBDataGridViewTextBoxColumn";
            this.leftBDataGridViewTextBoxColumn.ReadOnly = true;
            this.leftBDataGridViewTextBoxColumn.Width = 75;
            // 
            // leftDeltaDataGridViewTextBoxColumn
            // 
            this.leftDeltaDataGridViewTextBoxColumn.DataPropertyName = "LeftDelta";
            this.leftDeltaDataGridViewTextBoxColumn.HeaderText = "Левая раб.отм., м";
            this.leftDeltaDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.leftDeltaDataGridViewTextBoxColumn.Name = "leftDeltaDataGridViewTextBoxColumn";
            this.leftDeltaDataGridViewTextBoxColumn.ReadOnly = true;
            this.leftDeltaDataGridViewTextBoxColumn.Width = 75;
            // 
            // centrDeltaDataGridViewTextBoxColumn
            // 
            this.centrDeltaDataGridViewTextBoxColumn.DataPropertyName = "CentrDelta";
            this.centrDeltaDataGridViewTextBoxColumn.HeaderText = "По оси раб.отм., м";
            this.centrDeltaDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.centrDeltaDataGridViewTextBoxColumn.Name = "centrDeltaDataGridViewTextBoxColumn";
            this.centrDeltaDataGridViewTextBoxColumn.ReadOnly = true;
            this.centrDeltaDataGridViewTextBoxColumn.Width = 75;
            // 
            // rightDeltaDataGridViewTextBoxColumn
            // 
            this.rightDeltaDataGridViewTextBoxColumn.DataPropertyName = "RightDelta";
            this.rightDeltaDataGridViewTextBoxColumn.HeaderText = "Правая раб.отм., м";
            this.rightDeltaDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.rightDeltaDataGridViewTextBoxColumn.Name = "rightDeltaDataGridViewTextBoxColumn";
            this.rightDeltaDataGridViewTextBoxColumn.ReadOnly = true;
            this.rightDeltaDataGridViewTextBoxColumn.Width = 75;
            // 
            // rightBDataGridViewTextBoxColumn
            // 
            this.rightBDataGridViewTextBoxColumn.DataPropertyName = "RightB";
            this.rightBDataGridViewTextBoxColumn.HeaderText = "Правая граница, м";
            this.rightBDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.rightBDataGridViewTextBoxColumn.Name = "rightBDataGridViewTextBoxColumn";
            this.rightBDataGridViewTextBoxColumn.ReadOnly = true;
            this.rightBDataGridViewTextBoxColumn.Width = 75;
            // 
            // areaFDataGridViewTextBoxColumn
            // 
            this.areaFDataGridViewTextBoxColumn.DataPropertyName = "AreaF";
            this.areaFDataGridViewTextBoxColumn.HeaderText = "Площадь выравнивания, м.кв.";
            this.areaFDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.areaFDataGridViewTextBoxColumn.Name = "areaFDataGridViewTextBoxColumn";
            this.areaFDataGridViewTextBoxColumn.ReadOnly = true;
            this.areaFDataGridViewTextBoxColumn.Width = 75;
            // 
            // areaCDataGridViewTextBoxColumn
            // 
            this.areaCDataGridViewTextBoxColumn.DataPropertyName = "AreaC";
            this.areaCDataGridViewTextBoxColumn.HeaderText = "Площадь срезки, м.кв.";
            this.areaCDataGridViewTextBoxColumn.MinimumWidth = 50;
            this.areaCDataGridViewTextBoxColumn.Name = "areaCDataGridViewTextBoxColumn";
            this.areaCDataGridViewTextBoxColumn.ReadOnly = true;
            this.areaCDataGridViewTextBoxColumn.Width = 75;
            // 
            // leftPtExDataGridViewTextBoxColumn
            // 
            this.leftPtExDataGridViewTextBoxColumn.DataPropertyName = "LeftPtEx";
            this.leftPtExDataGridViewTextBoxColumn.HeaderText = "LeftPtEx";
            this.leftPtExDataGridViewTextBoxColumn.Name = "leftPtExDataGridViewTextBoxColumn";
            this.leftPtExDataGridViewTextBoxColumn.ReadOnly = true;
            this.leftPtExDataGridViewTextBoxColumn.Visible = false;
            // 
            // rightPtExDataGridViewTextBoxColumn
            // 
            this.rightPtExDataGridViewTextBoxColumn.DataPropertyName = "RightPtEx";
            this.rightPtExDataGridViewTextBoxColumn.HeaderText = "RightPtEx";
            this.rightPtExDataGridViewTextBoxColumn.Name = "rightPtExDataGridViewTextBoxColumn";
            this.rightPtExDataGridViewTextBoxColumn.ReadOnly = true;
            this.rightPtExDataGridViewTextBoxColumn.Visible = false;
            // 
            // centrPtExDataGridViewTextBoxColumn
            // 
            this.centrPtExDataGridViewTextBoxColumn.DataPropertyName = "CentrPtEx";
            this.centrPtExDataGridViewTextBoxColumn.HeaderText = "CentrPtEx";
            this.centrPtExDataGridViewTextBoxColumn.Name = "centrPtExDataGridViewTextBoxColumn";
            this.centrPtExDataGridViewTextBoxColumn.ReadOnly = true;
            this.centrPtExDataGridViewTextBoxColumn.Visible = false;
            // 
            // leftPtPrDataGridViewTextBoxColumn
            // 
            this.leftPtPrDataGridViewTextBoxColumn.DataPropertyName = "LeftPtPr";
            this.leftPtPrDataGridViewTextBoxColumn.HeaderText = "LeftPtPr";
            this.leftPtPrDataGridViewTextBoxColumn.Name = "leftPtPrDataGridViewTextBoxColumn";
            this.leftPtPrDataGridViewTextBoxColumn.ReadOnly = true;
            this.leftPtPrDataGridViewTextBoxColumn.Visible = false;
            // 
            // rightPtPrDataGridViewTextBoxColumn
            // 
            this.rightPtPrDataGridViewTextBoxColumn.DataPropertyName = "RightPtPr";
            this.rightPtPrDataGridViewTextBoxColumn.HeaderText = "RightPtPr";
            this.rightPtPrDataGridViewTextBoxColumn.Name = "rightPtPrDataGridViewTextBoxColumn";
            this.rightPtPrDataGridViewTextBoxColumn.ReadOnly = true;
            this.rightPtPrDataGridViewTextBoxColumn.Visible = false;
            // 
            // centrPtPrDataGridViewTextBoxColumn
            // 
            this.centrPtPrDataGridViewTextBoxColumn.DataPropertyName = "CentrPtPr";
            this.centrPtPrDataGridViewTextBoxColumn.HeaderText = "CentrPtPr";
            this.centrPtPrDataGridViewTextBoxColumn.Name = "centrPtPrDataGridViewTextBoxColumn";
            this.centrPtPrDataGridViewTextBoxColumn.ReadOnly = true;
            this.centrPtPrDataGridViewTextBoxColumn.Visible = false;
            // 
            // areaCutDataGridViewTextBoxColumn
            // 
            this.areaCutDataGridViewTextBoxColumn.DataPropertyName = "AreaCut";
            this.areaCutDataGridViewTextBoxColumn.HeaderText = "AreaCut";
            this.areaCutDataGridViewTextBoxColumn.Name = "areaCutDataGridViewTextBoxColumn";
            this.areaCutDataGridViewTextBoxColumn.ReadOnly = true;
            this.areaCutDataGridViewTextBoxColumn.Visible = false;
            // 
            // areaFillDataGridViewTextBoxColumn
            // 
            this.areaFillDataGridViewTextBoxColumn.DataPropertyName = "AreaFill";
            this.areaFillDataGridViewTextBoxColumn.HeaderText = "AreaFill";
            this.areaFillDataGridViewTextBoxColumn.Name = "areaFillDataGridViewTextBoxColumn";
            this.areaFillDataGridViewTextBoxColumn.ReadOnly = true;
            this.areaFillDataGridViewTextBoxColumn.Visible = false;
            // 
            // RoadPavenment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbStep);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbEndStation);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.tbStartStation);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btToExcel);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.dataGridResult);
            this.Controls.Add(this.BtSolut);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbMaxWidth);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbMinWidth);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbCutM);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbFillM);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btGetSurfPr);
            this.Controls.Add(this.cbSurfPr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btGetSurfEx);
            this.Controls.Add(this.cbSurfEx);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btGetAlign);
            this.Controls.Add(this.cbAlignment);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "RoadPavenment";
            this.Size = new System.Drawing.Size(612, 400);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RoadPavenmentBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAlignment;
        private System.Windows.Forms.Button btGetAlign;
        private System.Windows.Forms.Button btGetSurfEx;
        private System.Windows.Forms.ComboBox cbSurfEx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btGetSurfPr;
        private System.Windows.Forms.ComboBox cbSurfPr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbFillM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbCutM;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbMinWidth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbMaxWidth;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button BtSolut;
        private System.Windows.Forms.DataGridView dataGridResult;
        private System.Windows.Forms.Button btToExcel;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.TextBox tbEndStation;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbStartStation;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbStep;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.BindingSource RoadPavenmentBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn pKDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pKPlDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leftBDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leftDeltaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn centrDeltaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rightDeltaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rightBDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn areaFDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn areaCDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leftPtExDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rightPtExDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn centrPtExDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn leftPtPrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn rightPtPrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn centrPtPrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn areaCutDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn areaFillDataGridViewTextBoxColumn;
    }
}
