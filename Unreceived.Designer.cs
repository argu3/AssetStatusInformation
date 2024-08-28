namespace AssetStatusInfo
{
    partial class Unreceived
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
            unrecdGrid = new DataGridView();
            searchGrid = new DataGridView();
            unrecdLabel = new Label();
            uSearchButton = new Button();
            uSearchClearButton = new Button();
            ((System.ComponentModel.ISupportInitialize)unrecdGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)searchGrid).BeginInit();
            SuspendLayout();
            // 
            // unrecdGrid
            // 
            unrecdGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            unrecdGrid.Location = new Point(130, 250);
            unrecdGrid.Name = "unrecdGrid";
            unrecdGrid.RowHeadersWidth = 51;
            unrecdGrid.Size = new Size(300, 188);
            unrecdGrid.TabIndex = 0;
            // 
            // searchGrid
            // 
            searchGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            searchGrid.Location = new Point(130, 38);
            searchGrid.Name = "searchGrid";
            searchGrid.RowHeadersWidth = 51;
            searchGrid.Size = new Size(300, 70);
            searchGrid.TabIndex = 1;
            // 
            // unrecdLabel
            // 
            unrecdLabel.AutoSize = true;
            unrecdLabel.Font = new Font("Segoe UI", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            unrecdLabel.Location = new Point(60, 33);
            unrecdLabel.Name = "unrecdLabel";
            unrecdLabel.Size = new Size(655, 106);
            unrecdLabel.TabIndex = 2;
            unrecdLabel.Text = "Unreceived Items";
            // 
            // uSearchButton
            // 
            uSearchButton.Location = new Point(499, 64);
            uSearchButton.Name = "uSearchButton";
            uSearchButton.Size = new Size(94, 29);
            uSearchButton.TabIndex = 3;
            uSearchButton.Text = "Search";
            uSearchButton.UseVisualStyleBackColor = true;
            // 
            // uSearchClearButton
            // 
            uSearchClearButton.Location = new Point(516, 162);
            uSearchClearButton.Name = "uSearchClearButton";
            uSearchClearButton.Size = new Size(94, 29);
            uSearchClearButton.TabIndex = 4;
            uSearchClearButton.Text = "Clear";
            uSearchClearButton.UseVisualStyleBackColor = true;
            // 
            // Unreceived
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(uSearchClearButton);
            Controls.Add(uSearchButton);
            Controls.Add(unrecdLabel);
            Controls.Add(searchGrid);
            Controls.Add(unrecdGrid);
            Name = "Unreceived";
            Text = "Unreceived";
            ((System.ComponentModel.ISupportInitialize)unrecdGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)searchGrid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView unrecdGrid;
        private DataGridView searchGrid;
        private Label unrecdLabel;
        private Button uSearchButton;
        private Button uSearchClearButton;
    }
}