/*
using Microsoft.Data.SqlClient;
using System.Data;
using static System.ComponentModel.Design.ObjectSelectorEditor;
*/
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Windows.Forms;
namespace AssetStatusInfo
{
    public partial class Form1 : Form
    {
        public Form ManifestValidation;
        private static double dataGridLocationX = .5;
        private static double dataGridLocationY = .20;
        private static double dataGridSizeX = .95;
        private static double dataGridSizeY = 1 - dataGridLocationY;
        private bool maximized;
        private string historyLabelDefault = "History";
        private string historyLabelMod = "";
        private string historyLabelSite = "";
        private DataTable historyGridDataTable = new DataTable();
        InputConfigurations searchGridConfig = new InputConfigurations();
        SearchGridDecorator historySearchGrid;
        GridFormatter gridFormatter = new GridFormatter();
        public Form1()
        {
            InitializeComponent();
            historySearchGrid = new SearchGridDecorator(historyGrid, searchGrid, searchButton, searchClearButton, () => { DataTable r = DatabaseQueries.GetHistory(comboBoxLocation(), fullHistory.Checked); AdjustElements(); return r; });
            historySearchGrid.AddSearchAction(()=>AdjustElements());
            historySearchGrid.AddSearchClearAction(()=>AdjustElements());
            historySearchGrid.nonSearchableFields = AllSettings.Default.NonSearchableFields.Split("|");
                //new List<string>() { "TimeReceived", "Total", "Needed", "Found", "Site", "Amount", "AmountAtADC" };
            //historyGrid.ColumnWidthChanged += AdjustElements;// HistoryGrid_ColumnWidthChanged; ;
            historyGrid.DataContextChanged += AdjustElements; //HistoryGrid_DataContextChanged;
            locationSelector.SelectedIndexChanged += LocationSelector_SelectedIndexChanged;
            //dataGridView1.DataSource = GetHistory();
            locationSelector.DataSource = DatabaseQueries.GetLocations();
            locationSelector.SelectedItem = "All Sites";

            //get validation parameters for the ManifestGridFields
            searchGridConfig = GridFormatter.GridColumnDataConfiguration("historySearchGrid");
            historySearchGrid.GridDataSetter(null, searchGridConfig, "search");
            gridFormatter.DataGridViewColumnHeaderFormatting(searchGrid);
            historySearchGrid.FormatNonSearchableFields();
            searchGrid.DataError += SearchGrid_DataError;
            searchGrid.CellEndEdit += SearchGrid_CellEndEdit;
            //add a new empty row to the data grid
            ((DataTable)searchGrid.DataSource).Rows.Add(((DataTable)searchGrid.DataSource).NewRow());
        }

        private void updateHistoryLabel()
        {
            historyLabel.Text = $"{historyLabelSite} {historyLabelDefault} {historyLabelMod}";
        }
        private void LocationSelector_SelectedIndexChanged(object? sender, EventArgs e)
        {
            historyLabelSite = locationSelector.Text;
            updateHistoryLabel();
            button1_Click(sender, e);
        }

        private void SearchGrid_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            DataGridView? s = null;
            if (sender != null)
            {
                s = (DataGridView)sender;
                DataGridViewCell cell = s.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if(!searchGridConfig.Validate(cell))
                {
                    cell.Value = DBNull.Value;
                }
            }
        }
        private void SearchGrid_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            DataGridView? s = null;
            if (sender != null)
            {
                s = (DataGridView)sender;
                DataGridViewCell errorCell = s.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //searchGridConfig.configurations[gridFormatter.inverseNameMap[errorCell.OwningColumn.Name]].Validate(errorCell.Value.ToString());
                string userInput = e.Exception.Message.Replace("The input string '", "").Replace("' was not in a correct format.","");
                searchGridConfig.configurations[gridFormatter.inverseNameMap[errorCell.OwningColumn.Name]].Validate(userInput);
            }
            else
            {
                MessageBox.Show("Bad search", "Bad search format. If you see this, please screensot and send to app owner", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //e.Cancel = true;
            s.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button1_Click(new object(), new EventArgs());
            ResizeEnd += AdjustElements;
            Resize += Form1_Resize;
            //searchClearButton_Click(new object(), new EventArgs());
        }

        private void Form1_Resize(object? sender, EventArgs e)
        {
            AdjustElements();
            if (this.WindowState == FormWindowState.Maximized)
            {
                maximized = true;
            }
            else if (maximized)
            {
                maximized = false;
            }
        }

        private void AdjustElements(object? sender, EventArgs e)
        {
            AdjustElements();
        }
        private void AdjustElements()
        {
            historySearchGrid.FormatNonSearchableFields();
            historyGrid.Size = new Size(((int)(ClientSize.Width * dataGridSizeX) < GridFormatter.width(historyGrid) ? (int)(ClientSize.Width * dataGridSizeX) : GridFormatter.width(historyGrid)), (int)(ClientSize.Height * dataGridSizeY));
            historyGrid.Location = new Point((int)(ClientSize.Width * dataGridLocationX - .5 * historyGrid.Width), (int)(ClientSize.Height * .9 * dataGridLocationY));
            searchGrid.Location = new Point(historyGrid.Location.X, historyGrid.Location.Y - (int)(1.1 * searchGrid.Height));
            historyLabel.Location = new Point(historyGrid.Location.X, searchGrid.Location.Y - historyLabel.Height);
            locationSelector.Location = new Point(historyGrid.Location.X, historyLabel.Location.Y - (int)(1.3 * locationSelector.Height));
            locationSelectorLabel.Location = new Point(historyLabel.Location.X, locationSelector.Location.Y - (int)(1.3 * locationSelectorLabel.Height));
            prepareDelivery.Location = new Point(locationSelector.Location.X + (int)(1.2 * prepareDelivery.Width), locationSelector.Location.Y);
            showHistory.Location = new Point(prepareDelivery.Location.X + (int)(1.2 * showHistory.Width), locationSelector.Location.Y);
            fullHistory.Location = new Point(showHistory.Location.X + (int)(1.2 * fullHistory.Width), locationSelector.Location.Y);
            FormClosing += Form1_FormClosing;
            //deletePanel.Location = delete.Location;
            historySearchGrid.MatchColumnWidth();
            historySearchGrid.MatchColumnOrder();
            searchGrid.Size = new Size(((int)(ClientSize.Width * dataGridSizeX) < GridFormatter.width(searchGrid) ? (int)(ClientSize.Width * dataGridSizeX) : GridFormatter.width(searchGrid)), searchGrid.Height);
            searchButton.Location = new Point(searchGrid.Location.X + searchGrid.Width + (int)(.2 * searchButton.Width), searchGrid.Location.Y);
            searchClearButton.Location = new Point(searchButton.Location.X, searchButton.Location.Y + (int)(1.2 * searchButton.Height));
        }

        private void Form1_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (ManifestValidation != null)
            {
                if (ManifestValidation.Visible)
                {
                    DialogResult result = MessageBox.Show("Do you actually want to exit?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private String comboBoxLocation()
        {
            string currentLocation = DatabaseQueries.defaultLocation;
            if (locationSelector.SelectedValue != null)
            {
                currentLocation = locationSelector.SelectedValue.ToString();
            }
            if (currentLocation == "All Sites")
            {
                currentLocation = "%";
            }
            return currentLocation;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //why was I modifying the DataTable and not the grid itself?
            historyGridDataTable = DatabaseQueries.GetHistory(comboBoxLocation(), fullHistory.Checked);
            //historyGridDataTable = gridFormatter.DataTableColumnHeaderFormatting(historyGridDataTable);
            historyGrid.DataSource = historyGridDataTable;
            gridFormatter.DataGridViewCommonFormating(historyGrid);
            AdjustElements();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string location = comboBoxLocation();
            string noDelivery = AllSettings.Default.CantBeDeliveredTo;
            if (noDelivery.Length >= 1)
            {
                if(noDelivery.Contains(location))
                {
                    MessageBox.Show("We don't deliver there. Please pick a different site to prepare the delivery for.", "Pick a site", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    ManifestValidation = new ManifestValidation(location);
                    ManifestValidation.MaximizeBox = true;
                    ManifestValidation.Show();
                }
            }
            
        }

        private void fullHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                CheckBox s = (CheckBox)sender;
                //string labelMod = "";
                if (s.Checked == true)
                {
                    historyLabelMod = "";
                }
                else
                {
                    historyLabelMod = "(Items at Warehouse only)";
                }
                updateHistoryLabel();
                button1_Click(new object(), new EventArgs());//, s.Checked);
            }
        }
    }
}
