namespace AssetStatusInfo
{
    partial class ManifestValidation
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManifestValidation));
            historyGrid = new DataGridView();
            historyCheckbox = new DataGridViewCheckBoxColumn();
            manifestGrid = new DataGridView();
            manifestCheckbox = new DataGridViewCheckBoxColumn();
            inputBox = new RichTextBox();
            textBox1 = new TextBox();
            historyLabel = new Label();
            manifestLabel = new Label();
            label3 = new Label();
            linkButton = new Button();
            label4 = new Label();
            submitButton = new Button();
            copyNontaggedButton = new Button();
            deleteButton = new Button();
            DeleteToolTip = new ToolTip(components);
            copyTaggedButton = new Button();
            copySerialButton = new Button();
            siteLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)historyGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)manifestGrid).BeginInit();
            SuspendLayout();
            // 
            // historyGrid
            // 
            historyGrid.AllowUserToAddRows = false;
            historyGrid.AllowUserToDeleteRows = false;
            historyGrid.AllowUserToOrderColumns = true;
            historyGrid.AllowUserToResizeRows = false;
            historyGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            historyGrid.ColumnHeadersHeight = 29;
            historyGrid.Columns.AddRange(new DataGridViewColumn[] { historyCheckbox });
            historyGrid.Location = new Point(96, 209);
            historyGrid.Margin = new Padding(1);
            historyGrid.Name = "historyGrid";
            historyGrid.RowHeadersWidth = 51;
            historyGrid.Size = new Size(299, 188);
            historyGrid.TabIndex = 0;
            // 
            // historyCheckbox
            // 
            historyCheckbox.HeaderText = "";
            historyCheckbox.MinimumWidth = 6;
            historyCheckbox.Name = "historyCheckbox";
            historyCheckbox.Width = 6;
            // 
            // manifestGrid
            // 
            manifestGrid.AllowUserToAddRows = false;
            manifestGrid.AllowUserToDeleteRows = false;
            manifestGrid.AllowUserToOrderColumns = true;
            manifestGrid.AllowUserToResizeRows = false;
            manifestGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            manifestGrid.ColumnHeadersHeight = 29;
            manifestGrid.Columns.AddRange(new DataGridViewColumn[] { manifestCheckbox });
            manifestGrid.Location = new Point(538, 209);
            manifestGrid.Margin = new Padding(1);
            manifestGrid.Name = "manifestGrid";
            manifestGrid.RowHeadersWidth = 51;
            manifestGrid.Size = new Size(299, 188);
            manifestGrid.TabIndex = 1;
            // 
            // manifestCheckbox
            // 
            manifestCheckbox.HeaderText = "";
            manifestCheckbox.MinimumWidth = 6;
            manifestCheckbox.Name = "manifestCheckbox";
            manifestCheckbox.Width = 6;
            // 
            // inputBox
            // 
            inputBox.Location = new Point(96, 135);
            inputBox.Multiline = false;
            inputBox.Name = "inputBox";
            inputBox.Size = new Size(473, 35);
            inputBox.TabIndex = 4;
            inputBox.Text = "";
            inputBox.WordWrap = false;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new Point(636, 10);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(437, 85);
            textBox1.TabIndex = 5;
            textBox1.Text = "To manually link a scanned item:\r\n    -select the checkbox next to the scanned item\r\n    -select the checkbox next to the item from the history\r\n    -click the \"link\" button";
            // 
            // historyLabel
            // 
            historyLabel.AutoSize = true;
            historyLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            historyLabel.Location = new Point(96, 167);
            historyLabel.Name = "historyLabel";
            historyLabel.Size = new Size(122, 41);
            historyLabel.TabIndex = 6;
            historyLabel.Text = "History";
            // 
            // manifestLabel
            // 
            manifestLabel.AutoSize = true;
            manifestLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            manifestLabel.Location = new Point(538, 167);
            manifestLabel.Name = "manifestLabel";
            manifestLabel.Size = new Size(143, 41);
            manifestLabel.TabIndex = 7;
            manifestLabel.Text = "Manifest";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(96, 112);
            label3.Name = "label3";
            label3.Size = new Size(76, 20);
            label3.TabIndex = 8;
            label3.Text = "Scan Here";
            // 
            // linkButton
            // 
            linkButton.AutoSize = true;
            linkButton.Location = new Point(677, 176);
            linkButton.Name = "linkButton";
            linkButton.Size = new Size(94, 30);
            linkButton.TabIndex = 9;
            linkButton.Text = "Link";
            linkButton.UseVisualStyleBackColor = true;
            linkButton.Click += linkButton_click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(535, 13);
            label4.Name = "label4";
            label4.Size = new Size(13, 20);
            label4.TabIndex = 10;
            label4.Text = " ";
            label4.Click += label4_Click;
            // 
            // submitButton
            // 
            submitButton.AutoSize = true;
            submitButton.Location = new Point(213, 176);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(94, 30);
            submitButton.TabIndex = 11;
            submitButton.Text = "Submit";
            submitButton.UseVisualStyleBackColor = true;
            submitButton.Click += submitButton_click;
            // 
            // copyNontaggedButton
            // 
            copyNontaggedButton.AutoSize = true;
            copyNontaggedButton.Location = new Point(946, 176);
            copyNontaggedButton.Name = "copyNontaggedButton";
            copyNontaggedButton.Size = new Size(140, 30);
            copyNontaggedButton.TabIndex = 12;
            copyNontaggedButton.Text = "Copy Non-Tagged";
            copyNontaggedButton.UseVisualStyleBackColor = true;
            copyNontaggedButton.Click += copyNonTagged_Click;
            // 
            // deleteButton
            // 
            deleteButton.AutoSize = true;
            deleteButton.Location = new Point(313, 176);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(94, 30);
            deleteButton.TabIndex = 13;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += delete_Click;
            // 
            // DeleteToolTip
            // 
            DeleteToolTip.AutomaticDelay = 300;
            DeleteToolTip.Tag = "";
            // 
            // copyTaggedButton
            // 
            copyTaggedButton.AutoSize = true;
            copyTaggedButton.Location = new Point(876, 176);
            copyTaggedButton.Name = "copyTaggedButton";
            copyTaggedButton.Size = new Size(86, 30);
            copyTaggedButton.TabIndex = 14;
            copyTaggedButton.Text = "Copy Tags";
            copyTaggedButton.UseVisualStyleBackColor = true;
            copyTaggedButton.Click += copyTaggedButton_Click;
            // 
            // copySerialButton
            // 
            copySerialButton.AutoSize = true;
            copySerialButton.Location = new Point(770, 175);
            copySerialButton.Name = "copySerialButton";
            copySerialButton.Size = new Size(100, 30);
            copySerialButton.TabIndex = 15;
            copySerialButton.Text = "Copy Serials";
            copySerialButton.UseVisualStyleBackColor = true;
            copySerialButton.Click += copySerialButton_Click;
            // 
            // siteLabel
            // 
            siteLabel.AutoSize = true;
            siteLabel.Font = new Font("Segoe UI", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            siteLabel.ImageAlign = ContentAlignment.TopLeft;
            siteLabel.Location = new Point(41, 6);
            siteLabel.Name = "siteLabel";
            siteLabel.Size = new Size(354, 106);
            siteLabel.TabIndex = 17;
            siteLabel.Text = "siteLabel";
            // 
            // ManifestValidation
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1314, 763);
            Controls.Add(siteLabel);
            Controls.Add(copySerialButton);
            Controls.Add(copyTaggedButton);
            Controls.Add(copyNontaggedButton);
            Controls.Add(submitButton);
            Controls.Add(label4);
            Controls.Add(linkButton);
            Controls.Add(label3);
            Controls.Add(manifestLabel);
            Controls.Add(historyLabel);
            Controls.Add(textBox1);
            Controls.Add(inputBox);
            Controls.Add(manifestGrid);
            Controls.Add(historyGrid);
            Controls.Add(deleteButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "ManifestValidation";
            Text = "History View";
            ((System.ComponentModel.ISupportInitialize)historyGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)manifestGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView historyGrid;
        private DataGridView manifestGrid;
        private RichTextBox inputBox;
        private TextBox textBox1;
        private Label historyLabel;
        private Label manifestLabel;
        private Label label3;
        private Button linkButton;
        private Label label4;
        private Button submitButton;
        private Button copyNontaggedButton;
        private Button deleteButton;
        private ToolTip DeleteToolTip;
        private DataGridViewCheckBoxColumn historyCheckbox;
        private DataGridViewCheckBoxColumn manifestCheckbox;
        private Button copyTaggedButton;
        private Button copySerialButton;
        private Label siteLabel;
    }
}