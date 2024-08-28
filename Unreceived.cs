using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssetStatusInfo
{
    public partial class Unreceived : Form
    {
        private static double dataGridLocationX = .5;
        private static double dataGridLocationY = .20;
        private static double dataGridSizeX = .95;
        private static double dataGridSizeY = 1 - dataGridLocationY;
        private bool maximized;
        InputConfigurations searchGridConfig = new InputConfigurations();
        SearchGridDecorator historySearchGrid;
        GridFormatter gridFormatter = new GridFormatter();
        public Unreceived()
        {
            InitializeComponent();
            historySearchGrid = new SearchGridDecorator(unrecdGrid, searchGrid, uSearchButton, uSearchClearButton, () => { DataTable r = DatabaseQueries.GetUnreceived(); AdjustElements(); return r; });
            historySearchGrid.AddSearchAction(() => AdjustElements());
            historySearchGrid.AddSearchClearAction(() => AdjustElements());
            historySearchGrid.nonSearchableFields = AllSettings.Default.NonSearchableFields.Split("|");
            //new List<string>() { "TimeReceived", "Total", "Needed", "Found", "Site", "Amount", "AmountAtADC" };
            //unrecdGrid.ColumnWidthChanged += AdjustElements;// unrecdGrid_ColumnWidthChanged; ;
            unrecdGrid.DataContextChanged += AdjustElements; //unrecdGrid_DataContextChanged;
            Load += Form1_Load;
            //get validation parameters for the ManifestGridFields
            searchGridConfig = GridFormatter.GridColumnDataConfiguration("unreceivedSearchGrid");
            historySearchGrid.GridDataSetter(null, searchGridConfig, "unreceivedSearchGrid");
            gridFormatter.DataGridViewColumnHeaderFormatting(searchGrid);
            historySearchGrid.FormatNonSearchableFields();
            searchGrid.DataError += SearchGrid_DataError;
            searchGrid.CellEndEdit += SearchGrid_CellEndEdit;
            unrecdGrid.ColumnWidthChanged += AdjustElements;
            //add a new empty row to the data grid
            //((DataTable)searchGrid.DataSource).Rows.Add(((DataTable)searchGrid.DataSource).NewRow());
            WindowState = FormWindowState.Maximized;
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
            unrecdGrid.Size = new Size(((int)(ClientSize.Width * dataGridSizeX) < GridFormatter.width(unrecdGrid) ? (int)(ClientSize.Width * dataGridSizeX) : GridFormatter.width(unrecdGrid)), (int)(ClientSize.Height * dataGridSizeY));
            unrecdGrid.Location = new Point((int)(ClientSize.Width * dataGridLocationX - .5 * unrecdGrid.Width), (int)(ClientSize.Height * .9 * dataGridLocationY));
            searchGrid.Location = new Point(unrecdGrid.Location.X, unrecdGrid.Location.Y - (int)(1.1 * searchGrid.Height));
            unrecdLabel.Location = new Point(unrecdGrid.Location.X + (int)(.23*unrecdLabel.Width), searchGrid.Location.Y - unrecdLabel.Height);
            //deletePanel.Location = delete.Location;
            historySearchGrid.MatchColumnWidth();
            historySearchGrid.MatchColumnOrder();
            searchGrid.Size = new Size(((int)(ClientSize.Width * dataGridSizeX) < GridFormatter.width(searchGrid) ? (int)(ClientSize.Width * dataGridSizeX) : GridFormatter.width(searchGrid)), searchGrid.Height);
            uSearchButton.Location = new Point(searchGrid.Location.X + searchGrid.Width + (int)(.2 * uSearchButton.Width), searchGrid.Location.Y);
            uSearchClearButton.Location = new Point(uSearchButton.Location.X, uSearchButton.Location.Y + (int)(1.2 * uSearchButton.Height));
        }

        private void SearchGrid_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            DataGridView? s = null;
            if (sender != null)
            {
                s = (DataGridView)sender;
                DataGridViewCell cell = s.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (!searchGridConfig.Validate(cell))
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
                string userInput = e.Exception.Message.Replace("The input string '", "").Replace("' was not in a correct format.", "");
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
            //why was I modifying the DataTable and not the grid itself?
            unrecdGrid.DataSource = DatabaseQueries.GetUnreceived();
            //historyGridDataTable = gridFormatter.DataTableColumnHeaderFormatting(historyGridDataTable);
            gridFormatter.DataGridViewCommonFormating(unrecdGrid);
            AdjustElements();
            ResizeEnd += AdjustElements;
            Resize += Form1_Resize;
            //searchClearButton_Click(new object(), new EventArgs());
        }
    }
}
