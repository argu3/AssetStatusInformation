using System.Windows.Forms;

namespace AssetStatusInfo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            historyGrid = new DataGridView();
            locationSelector = new ComboBox();
            showHistory = new Button();
            prepareDelivery = new Button();
            fullHistory = new CheckBox();
            historyLabel = new Label();
            locationSelectorLabel = new Label();
            searchGrid = new DataGridView();
            searchClearButton = new Button();
            searchButton = new Button();
            unrecdItems = new Button();
            ((System.ComponentModel.ISupportInitialize)historyGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)searchGrid).BeginInit();
            SuspendLayout();
            // 
            // historyGrid
            // 
            historyGrid.AllowUserToAddRows = false;
            historyGrid.AllowUserToDeleteRows = false;
            historyGrid.AllowUserToOrderColumns = true;
            historyGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            historyGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            historyGrid.Location = new Point(27, 209);
            historyGrid.Name = "historyGrid";
            historyGrid.ReadOnly = true;
            historyGrid.RowHeadersWidth = 51;
            historyGrid.RowTemplate.ReadOnly = true;
            historyGrid.Size = new Size(1539, 638);
            historyGrid.TabIndex = 0;
            // 
            // locationSelector
            // 
            locationSelector.FormattingEnabled = true;
            locationSelector.Location = new Point(27, 32);
            locationSelector.Name = "locationSelector";
            locationSelector.Size = new Size(151, 28);
            locationSelector.TabIndex = 2;
            // 
            // showHistory
            // 
            showHistory.Location = new Point(204, 32);
            showHistory.Name = "showHistory";
            showHistory.Size = new Size(141, 29);
            showHistory.TabIndex = 3;
            showHistory.Text = "Show History";
            showHistory.UseVisualStyleBackColor = true;
            showHistory.Click += button1_Click;
            // 
            // prepareDelivery
            // 
            prepareDelivery.Location = new Point(351, 32);
            prepareDelivery.Name = "prepareDelivery";
            prepareDelivery.Size = new Size(141, 29);
            prepareDelivery.TabIndex = 4;
            prepareDelivery.Text = "Prepare Delivery";
            prepareDelivery.UseVisualStyleBackColor = true;
            prepareDelivery.Click += button2_Click;
            // 
            // fullHistory
            // 
            fullHistory.AutoSize = true;
            fullHistory.Checked = true;
            fullHistory.CheckState = CheckState.Checked;
            fullHistory.Location = new Point(678, 34);
            fullHistory.Name = "fullHistory";
            fullHistory.Size = new Size(150, 24);
            fullHistory.TabIndex = 5;
            fullHistory.Text = "Include sent items";
            fullHistory.UseVisualStyleBackColor = true;
            fullHistory.CheckedChanged += fullHistory_CheckedChanged;
            // 
            // historyLabel
            // 
            historyLabel.AutoSize = true;
            historyLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            historyLabel.Location = new Point(27, 165);
            historyLabel.Name = "historyLabel";
            historyLabel.Size = new Size(277, 41);
            historyLabel.TabIndex = 7;
            historyLabel.Text = "History (All Items)";
            // 
            // locationSelectorLabel
            // 
            locationSelectorLabel.AutoSize = true;
            locationSelectorLabel.Location = new Point(27, 9);
            locationSelectorLabel.Name = "locationSelectorLabel";
            locationSelectorLabel.Size = new Size(99, 20);
            locationSelectorLabel.TabIndex = 8;
            locationSelectorLabel.Text = "Site Selection";
            // 
            // searchGrid
            // 
            searchGrid.AllowUserToAddRows = false;
            searchGrid.AllowUserToDeleteRows = false;
            searchGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            searchGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            searchGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            searchGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            searchGrid.EditMode = DataGridViewEditMode.EditOnKeystroke;
            searchGrid.Location = new Point(27, 67);
            searchGrid.Name = "searchGrid";
            searchGrid.RowHeadersWidth = 51;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            searchGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            searchGrid.RowTemplate.Height = 35;
            searchGrid.Size = new Size(1539, 70);
            searchGrid.StandardTab = true;
            searchGrid.TabIndex = 9;
            searchGrid.TabStop = false;
            // 
            // searchClearButton
            // 
            searchClearButton.Location = new Point(174, 137);
            searchClearButton.Name = "searchClearButton";
            searchClearButton.Size = new Size(141, 29);
            searchClearButton.TabIndex = 10;
            searchClearButton.Text = "Clear";
            searchClearButton.UseVisualStyleBackColor = true;
            // 
            // searchButton
            // 
            searchButton.Location = new Point(27, 137);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(141, 29);
            searchButton.TabIndex = 11;
            searchButton.Text = "Search";
            searchButton.UseVisualStyleBackColor = true;
            // 
            // unrecdItems
            // 
            unrecdItems.Location = new Point(498, 31);
            unrecdItems.Name = "unrecdItems";
            unrecdItems.Size = new Size(174, 29);
            unrecdItems.TabIndex = 12;
            unrecdItems.Text = "Show Unreceived Items";
            unrecdItems.UseVisualStyleBackColor = true;
            unrecdItems.Click += unrecdItems_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            ClientSize = new Size(1925, 1181);
            Controls.Add(unrecdItems);
            Controls.Add(searchButton);
            Controls.Add(searchClearButton);
            Controls.Add(searchGrid);
            Controls.Add(locationSelectorLabel);
            Controls.Add(historyLabel);
            Controls.Add(fullHistory);
            Controls.Add(prepareDelivery);
            Controls.Add(showHistory);
            Controls.Add(locationSelector);
            Controls.Add(historyGrid);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "History View";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)historyGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)searchGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        /*
private void DataGridView1_DataSourceChanged(object sender, EventArgs e)
{
   throw new NotImplementedException();
}
*/
        #endregion
        public void TempDesignHolder()
        {
            historyGrid = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)historyGrid).BeginInit();
            SuspendLayout();
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size((int)(Screen.PrimaryScreen.Bounds.Width * .6), (int)(Screen.PrimaryScreen.Bounds.Height * .6));
            Controls.Add(historyGrid);
            //this.ClientSizeChanged += Form1_ClientSizeChanged;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)historyGrid).EndInit();
            ResumeLayout(false);
            // 
            // dataGridView1
            // 
            //https://stackoverflow.com/questions/37661303/how-to-fit-datagridview-Width-and-height-to-its-content
            historyGrid.AllowUserToAddRows = false;
            historyGrid.AllowUserToDeleteRows = false;
            historyGrid.AllowUserToOrderColumns = true;
            historyGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            historyGrid.Name = "dataGridView1";
            historyGrid.ReadOnly = true;
            //dataGridView1.RowHeadersWidth = 51;
            historyGrid.Size = new Size((int)(ClientSize.Width * .90), (int)(ClientSize.Height * .90));
            historyGrid.TabIndex = 0;
            historyGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            historyGrid.Location = new Point((int)(ClientSize.Width * .05), (int)(ClientSize.Height * .05));
            //dataGridView1.DataSourceChanged += DataGridView1_DataSourceChanged;
            //
         }

        private DataGridView historyGrid;
        private ComboBox locationSelector;
        private Button showHistory;
        private Button prepareDelivery;
        private CheckBox fullHistory;
        private Label historyLabel;
        private Label locationSelectorLabel;
        private DataGridView searchGrid;
        private Button searchClearButton;
        private Button searchButton;
        private Button unrecdItems;
    }
}
