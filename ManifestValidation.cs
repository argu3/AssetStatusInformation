using System.Data;
using System.Text.RegularExpressions;
using System.Xml;

namespace AssetStatusInfo
{
    public partial class ManifestValidation : Form
    {
        private int count = 0;
        //header text and label
        private String location;
        private readonly string headerText = "Manifest For ";
        private readonly string tableName = "manifest";
        //multipliers based on client size
        private static double dataGridLocationX = .05;
        private static double dataGridLocationY = .20;
        private static double dataGridSizeX = .5 - dataGridLocationX;
        private static double dataGridSizeY = 1 - dataGridLocationY;
        //determines if the client was maximized and is now being minimized
        private static bool maximized = false;
        //radio button implementation
        private RadioButtonColumn radioButton = new RadioButtonColumn();
        //config object for text fields
        InputConfigurations manifestGridConfig = new InputConfigurations();
        //used to determine what fields need to match
        Dictionary<String, bool> matchValues = new Dictionary<String, bool>()
        {
            //{"Found", true},
            {"ItemDescription", false },
            {"CustomerPurchaseOrder", false },
            {"TicketNumber", true },
        };

        //the fields that go in the manifest
        SortedList<int, String> manifestOutputFields = new SortedList<int, String>()
        {
            {0, "TicketNumber"},
            {1, "Amount"},
            {2, "ItemDescription"},
            {3, "ticketTech"}
        };
        //the text used for different options of "Found" 
        private readonly struct Found
        {
            public Found()
            {
            }
            public const string fullyFound = "Yes";
            public const string partiallyFound = "Partial";
            public const string notFound = "No";
            public const string tooManyFound = "Dupe";
            public const string undefined = "n/a";
        }
        private bool ValidateManifestFields(Dictionary<string, string> manifestFields)
        {
            foreach (KeyValuePair<string, string> field in manifestFields)
            {
                if (manifestGridConfig.configurations.ContainsKey(field.Key))
                {
                    if (!manifestGridConfig.configurations[field.Key].Validate(field.Value))
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


        DataSet ds = new DataSet();

        public ManifestValidation(String loc)
        {
            InitializeComponent();
            location = loc;
            Text = headerText + location;
            //
            GridFormatter gridFormatter = new GridFormatter();
            //get validation parameters for the ManifestGridFields
            manifestGridConfig = GridFormatter.GridColumnDataConfiguration("manifestGrid");
            //set grid contents
            ds = SearchGridDecorator.GridDataSetter(manifestGrid, ds, manifestGridConfig, "manifest");
            historyGrid.DataSource = DatabaseQueries.GetHistory(location);
            //initial grid formatting
            radioButton.ConvertToRadioButtonColumn(historyGrid, historyCheckbox);
            radioButton.ConvertToRadioButtonColumn(manifestGrid, manifestCheckbox);
            gridFormatter.DataGridViewCommonFormating(historyGrid);
            gridFormatter.DataGridViewCommonFormating(manifestGrid);
            DataGridViewCommonLocalFormatting(historyGrid);
            DataGridViewCommonLocalFormatting(manifestGrid);
            //other intiial formatting steps
            AdjustElements();
            inputBox.KeyDown += RichTextBox1_KeyDown;
            ResizeEnd += ManifestValidation_ResizeEnd;
            Resize += ManifestValidation_Resize;
            historyGrid.CellMouseClick += Grid_CurrentCellDirtyStateChanged;
            linkButton.Enabled = false;
            deleteButton.Enabled = false;
            this.FormClosing += ManifestValidation_FormClosing;
            //***************
            //manifestGrid.Font = new System.Drawing.Font(manifestGrid.Font.FontFamily, 60);
            //***************
            siteLabel.Text = loc + " Delivery";
            siteLabel.BorderStyle = BorderStyle.FixedSingle;
            WindowState = FormWindowState.Maximized;
        }

        private void ManifestValidation_FormClosing(object? sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you actually want to exit?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void ManifestValidation_Resize(object? sender, EventArgs e)
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

        private void ManifestValidation_ResizeEnd(object? sender, EventArgs e)
        {
            AdjustElements();
        }
        private void AdjustElements()
        {
            Size GridSize = new Size((int)(ClientSize.Width * dataGridSizeX), (int)(ClientSize.Height * dataGridSizeY));
            historyGrid.Size = GridSize;
            manifestGrid.Size = GridSize;
            manifestGrid.Location = new Point((int)(ClientSize.Width * dataGridLocationX), (int)(ClientSize.Height * dataGridLocationY));
            historyGrid.Location = new Point((int)(ClientSize.Width * (dataGridLocationX + dataGridSizeX)), (int)(ClientSize.Height * dataGridLocationY));
            historyLabel.Location = new Point(historyGrid.Location.X, historyGrid.Location.Y - historyLabel.Height);
            manifestLabel.Location = new Point(manifestGrid.Location.X, manifestGrid.Location.Y - manifestLabel.Height);
            inputBox.Location = new Point(manifestLabel.Location.X, manifestLabel.Location.Y - inputBox.Height);
            label3.Location = new Point(inputBox.Location.X, inputBox.Location.Y - label3.Height);
            siteLabel.Location = new Point(label3.Location.X, siteLabel.Location.Y);
            //
            label3.Location = new Point(label3.Location.X + 10, label3.Location.Y);
            //
            textBox1.Font = new System.Drawing.Font(textBox1.Font.FontFamily, (3 + (ClientSize.Width + ClientSize.Height) / 300));
            textBox1.Location = new Point(historyLabel.Location.X, 15);
            textBox1.Height = historyLabel.Location.Y - textBox1.Location.Y;
            textBox1.Width = manifestGrid.Width;
            //historyGrid.InvalidateColumn(0);
            //manifestGrid.InvalidateColumn(0);
            linkButton.Location = new Point(historyLabel.Location.X + (int)(1.5 * linkButton.Width), historyGrid.Location.Y - (int)(1.1 * linkButton.Height));
            submitButton.Location = new Point(manifestLabel.Location.X + (int)(1.1 * manifestLabel.Width), historyGrid.Location.Y - (int)(1.1 * submitButton.Height));
            copyTaggedButton.Location = new Point(submitButton.Location.X + (int)(1.1 * submitButton.Width), historyGrid.Location.Y - (int)(1.1 * copyTaggedButton.Height));
            copySerialButton.Location = new Point(copyTaggedButton.Location.X + (int)(1.1 * copyTaggedButton.Width), historyGrid.Location.Y - (int)(1.1 * copyNontaggedButton.Height));
            copyNontaggedButton.Location = new Point(copySerialButton.Location.X + (int)(1.1 * copySerialButton.Width), historyGrid.Location.Y - (int)(1.1 * copySerialButton.Height));

            deleteButton.Location = new Point(copyNontaggedButton.Location.X + (int)(1.1 * copyNontaggedButton.Width), historyGrid.Location.Y - (int)(1.1 * deleteButton.Height));
            //deletePanel.Location = delete.Location;
        }

        private void DataGridViewCommonLocalFormatting(DataGridView grid)
        //sets the formatting and adds event handlers that is the common between data grids
        {
            grid.CurrentCellDirtyStateChanged += Grid_CurrentCellDirtyStateChanged;
            grid.Sorted += Grid_Sorted;
        }

        private void Grid_Sorted(object? sender, EventArgs e)
        {
            //fixes cell styles after the grid is sorted
            //https://stackoverflow.com/questions/6278576/windows-forms-datagridview-problem-with-backgroundcolor-after-sorting#:~:text=Sorting%20a%20databound%20grid%20causes%20all%20rows%20to,styling.%20Alternatively%20you%20can%20use%20the%20CellFormatting%20event.
            if (sender != null)
            {
                DataGridView s = (DataGridView)sender;
                foreach (DataGridViewRow row in s.Rows)
                {
                    UpdateRowStatus(row.Cells["Found"].Value.ToString(), row);
                    /*
                     * what was I thinking here
                    if (row.Cells["Found"].Value.ToString() != Found.notFound)
                    {
                        UpdateRowStatus(Found.notFound, row);
                    }
                    */
                }
            }
        }

        private void Grid_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        //turns the checkbox column into a radio button
        {
            if (sender != null)
            {
                DataGridView s = (DataGridView)sender;
                if (radioButton.SelectedCell.ContainsKey("manifestCheckbox"))
                {
                    if (radioButton.SelectedCell.ContainsKey("historyCheckbox"))
                    {
                        if (radioButton.SelectedCell["historyCheckbox"] > -1 && radioButton.SelectedCell["manifestCheckbox"] > -1)
                        {
                            linkButton.Enabled = true;
                        }
                    }
                    if (s.Name == "manifestGrid")
                    {
                        if (radioButton.SelectedCell["manifestCheckbox"] > -1 && s.CurrentRow.Cells["Found"].Value.ToString() != Found.fullyFound && s.CurrentRow.Cells["Found"].Value.ToString() != Found.partiallyFound)
                        {
                            deleteButton.Enabled = true;
                        }
                        else
                        {
                            deleteButton.Enabled = false;
                        }
                    }
                }
            }
        }
        private void UpdateRowStatus(string status, DataGridViewRow row)
        {
            Color rowColor = Color.White;
            switch (status)
            {
                case Found.fullyFound:
                    rowColor = Color.Green; break;
                case Found.partiallyFound:
                    rowColor = Color.Yellow;
                    break;
                case Found.notFound:
                    rowColor = Color.Red; break;
                case Found.tooManyFound:
                    rowColor = Color.Orange; break;
            }
            row.Cells["Found"].Value = status;
            row.DefaultCellStyle.BackColor = rowColor;
        }
        private void checkRowStatus(string status, DataGridViewRow row1, DataGridViewRow row2)
        {
            if (status != Found.notFound)
            {
                int oldValue = int.Parse(row2.Cells["AmountAtADC"].Value.ToString());
                row2.Cells["AmountAtADC"].Value = int.Parse(row2.Cells["AmountAtADC"].Value.ToString()) - int.Parse(row1.Cells["Amount"].Value.ToString());
                if (int.Parse(row2.Cells["AmountAtADC"].Value.ToString()) > 0)
                {
                    status = Found.partiallyFound;
                    UpdateRowStatus(Found.fullyFound, row1);
                    UpdateRowStatus(status, row2);
                }
                else if (int.Parse(row2.Cells["AmountAtADC"].Value.ToString()) == 0)
                {
                    status = Found.fullyFound;
                    UpdateRowStatus(status, row1);
                    UpdateRowStatus(status, row2);
                }
                else if (int.Parse(row2.Cells["AmountAtADC"].Value.ToString()) < 0)
                {
                    row2.Cells["AmountAtADC"].Value = 0;
                    status = Found.fullyFound;
                    UpdateRowStatus(Found.tooManyFound, row1);
                    UpdateRowStatus(status, row2);
                }
            }
            else
            {
                UpdateRowStatus(status, row1);
            }
        }
        public void SearchForTaggedItemsInHistory(string type)
        {
            string status = Found.notFound;
            foreach (DataGridViewRow row in manifestGrid.Rows) //can I just check the 0th row since that's the newest...?
            {
                status = Found.fullyFound;
                if (row.Cells["Found"].Value == Found.undefined) //if I check the 0th row can I skip this...?
                {
                    foreach (DataGridViewRow hrow in historyGrid.Rows)
                    {
                        status = Found.fullyFound;
                        if (row.Cells[type]?.Value.ToString() != "N/A")
                        {
                            if (hrow.Cells[type]?.Value.ToString() == row.Cells[type]?.Value.ToString())
                            {
                                checkRowStatus(Found.fullyFound, row, hrow);
                                break;
                                //return;
                            }
                            else
                            {
                                status = Found.notFound;
                            }
                        }
                    }
                    if (status == Found.notFound)
                    {
                        checkRowStatus(Found.notFound, row, row);
                    }
                }
            }
        }
        public void ManualMatchItems()
        {
            DataGridViewRow row = manifestGrid.Rows[radioButton.SelectedCell["manifestCheckbox"]];
            DataGridViewRow hrow = historyGrid.Rows[radioButton.SelectedCell["historyCheckbox"]];
            char status = 'y';
            if (hrow.Cells["Found"].Value?.ToString() == Found.undefined || hrow.Cells["Found"].Value?.ToString() == Found.partiallyFound || hrow.Cells["Found"].Value?.ToString() == Found.notFound)
            {
                foreach (KeyValuePair<String, bool> kvp in matchValues)
                {
                    if (hrow.Cells[kvp.Key].Value.ToString() != row.Cells[kvp.Key].Value.ToString())
                    {
                        if (!kvp.Value) //if it doesn't need to match
                        {
                            DialogResult result = MessageBox.Show($"The field {kvp.Key} doesn't match up!\nContinue anyway?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.No)
                            {
                                radioButton.Deselect(manifestGrid, "manifestCheckbox");
                                radioButton.Deselect(historyGrid, "historyCheckbox");
                                linkButton.Enabled = false;
                                return;
                            }
                        }
                        else
                        {
                            DialogResult result = MessageBox.Show($"Linking failed. The field {kvp.Key} needs to match up between manifest and history.", "Link Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            radioButton.Deselect(manifestGrid, "manifestCheckbox");
                            radioButton.Deselect(historyGrid, "historyCheckbox");
                            linkButton.Enabled = false;
                            return;
                        }
                    }
                }
                //the actual "found" status is corrected in checkRowStatus
                checkRowStatus(Found.fullyFound, row, hrow);
            }
            radioButton.Deselect(manifestGrid, "manifestCheckbox");
            radioButton.Deselect(historyGrid, "historyCheckbox");
            linkButton.Enabled = false;
        }
        public void SearchForNonTaggedItemsInHistory(bool forceMatch)
        {
            string status = Found.fullyFound;
            foreach (DataGridViewRow row in manifestGrid.Rows)
            {
                status = Found.fullyFound;
                if (row.Cells["Found"].Value == Found.undefined || (row.Cells["Found"].Value != Found.fullyFound && forceMatch))
                {
                    foreach (DataGridViewRow hrow in historyGrid.Rows)
                    {
                        status = Found.fullyFound;
                        if (hrow.Cells["Found"].Value?.ToString() == Found.undefined || hrow.Cells["Found"].Value?.ToString() == Found.partiallyFound)
                        {
                            foreach (KeyValuePair<String, bool> kvp in matchValues)
                            {
                                if (hrow.Cells[kvp.Key].Value.ToString() != row.Cells[kvp.Key].Value.ToString())
                                {
                                    String key = kvp.Key;
                                    //key = key + "|" + hrow.Cells[kvp.Key].Value.ToString() + "|" + row.Cells[kvp.Key].Value.ToString();
                                    //MessageBox.Show(key);
                                    status = Found.notFound;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            status = Found.notFound;
                        }
                        if (status == Found.fullyFound)
                        {
                            checkRowStatus(Found.fullyFound, row, hrow);
                            break;
                        }
                    }
                    if (status == Found.notFound)
                    {
                        checkRowStatus(Found.notFound, row, row); //lazy implementation
                    }
                }
            }

        }
        public void fillDataGrid(Dictionary<String, String> fields)
        {

            bool gotDefinition = false;
            if (ds.Tables.Count == 0)
            {
                ds.Tables.Add(tableName);
            }
            if (ds.Tables[tableName].Columns.Count == 0)
            {
                foreach (KeyValuePair<String, String> kvp in fields)
                {
                    ds.Tables[tableName].Columns.Add(kvp.Key, Type.GetType("System.String"));
                }
            }
            DataRow newRow = ds.Tables[tableName].NewRow();
            foreach (KeyValuePair<String, String> kvp in fields)
            {
                if (kvp.Value != null)
                {
                    newRow[kvp.Key] = kvp.Value;
                }
                else
                {
                    newRow[kvp.Key] = 0;
                }
            }
            ds.Tables[tableName].Rows.InsertAt(newRow, 0);
            manifestGrid.DataSource = ds.Tables[tableName];
        }
        private Dictionary<String, String> ParseText()
        {
            /*This input is in several different formats:
             * Asset tag: 6 digit number
             * Serial number: arbirtrary character string with no spaces
             * QR code from item label:
             *  legacy format example:
             *      1234567 1 DESCRIPTION OF ITEM PO: ###-1234567 Lastname, Firstname
             *          1234567: $TicketNumber (7 digit number)
             *          1: $Amount amount of this item (any #)
             *          DESCRIPTION OF ITEM: $ItemDescription arbitrary length space-seprated string
             *          PO: :just to visually separate the PO on the printed label
             *          ###-1234567: $CustomerPurchaseOrder ### is either AHS, AMG, or AVN followed by - and then a 7 digit number
             *          Lastname, FirstName: $TicketTech sometimes has extra space-separated last names
             *  first pipe format example:
             *      1234567|1 DESCRIPTION OF ITEM ###-1234567|Lastname, Firstname
             *          same fields, I just didn't think this all the way through. In use for maybe a week
             *  current pipe format example:
             *      1234567|1|DESCRIPTION OF ITEM|###-1234567|Lastname, Firstname
             *          same fields, self explanatory
             */
            Dictionary<String, String> ItemFields = new Dictionary<string, string>();
            //MessageBox.Show($"You are searching for {richTextBox1.Text}");
            string searchTerm = inputBox.Text;
            if (!searchTerm.Contains(" ") && !searchTerm.Contains("|"))
            {
                Regex tagMatch = new Regex("[0-9]{6}");
                if (searchTerm.Length == 6 && tagMatch.Match(searchTerm).Success)
                {
                    ItemFields["AssetTag"] = searchTerm;
                    ItemFields["Type"] = "AssetTag";
                }
                else
                {
                    if (searchTerm[0] == 's' || searchTerm[0] == 'S')
                    {
                        searchTerm = searchTerm.Substring(1, searchTerm.Length - 1);
                    }
                    ItemFields["SerialNumber"] = searchTerm;
                    ItemFields["Type"] = "SerialNumber";
                }
                ItemFields["Found"] = Found.undefined;
                ItemFields["Amount"] = "1";

            }
            else if (!searchTerm.Contains("|")) //legacy format
            {
                int firstSpace = searchTerm.IndexOf(" ");
                ItemFields["TicketNumber"] = searchTerm.Substring(0, firstSpace);

                searchTerm = searchTerm.Substring(firstSpace + 1, searchTerm.Length - firstSpace - 1);
                if (searchTerm.Contains("N/A"))
                {
                    firstSpace = searchTerm.IndexOf(" ");
                    ItemFields["AssetTag"] = searchTerm.Substring(0, firstSpace);
                    searchTerm = searchTerm.Substring(firstSpace + 1, searchTerm.Length - firstSpace - 1);
                }

                Regex poMatch = new Regex("[A-Z]{3}-[0-9]{7}");
                int beforeName = poMatch.Match(searchTerm).Index;
                string description;

                ItemFields["Found"] = Found.undefined;
                ItemFields["Type"] = "non-stockItem";
                ItemFields["SerialNumber"] = "N/A";
                ItemFields["AssetTag"] = "N/A";
                int tester;
                if (beforeName != 0)
                {
                    description = searchTerm.Substring(0, beforeName);
                    ItemFields["Amount"] = description.Substring(0, description.IndexOf(" "));
                    if (!int.TryParse(ItemFields["Amount"], out tester))
                    {
                        ItemFields["ItemDescription"] = searchTerm.Substring(0, beforeName - 1);
                        ItemFields["Amount"] = "1";
                    }
                    else
                    {
                        ItemFields["ItemDescription"] = searchTerm.Substring(description.IndexOf(" ") + 1, beforeName - 3);
                    }
                    ItemFields["CustomerPurchaseOrder"] = searchTerm.Substring(beforeName, 11);
                    int remainder = searchTerm.Length - beforeName - 12;
                    if (beforeName + 12 < searchTerm.Length)
                    {
                        ItemFields["ticketTech"] = searchTerm.Substring(beforeName + 12, remainder);
                    }
                    else
                    {
                        ItemFields["ticketTech"] = "?";
                    }
                }
                else
                {
                    //doesn't work with names with spaces in them :(
                    int lastSpace = 0;
                    beforeName = 0;
                    for (int i = 0; i < searchTerm.Length; i++)
                    {
                        if (searchTerm[i] == ' ')
                        {
                            beforeName = lastSpace;
                            lastSpace = i;
                        }
                    }
                    description = searchTerm.Substring(0, beforeName);
                    ItemFields["Amount"] = description.Substring(0, description.IndexOf(" "));
                    if (!int.TryParse(ItemFields["Amount"], out tester))
                    {
                        ItemFields["ItemDescription"] = searchTerm.Substring(0, beforeName);
                        ItemFields["Amount"] = "1";
                    }
                    else
                    {
                        ItemFields["ItemDescription"] = searchTerm.Substring(description.IndexOf(" ") + 1, beforeName - 2);
                    }
                    ItemFields["CustomerPurchaseOrder"] = "N/A";
                    ItemFields["ticketTech"] = searchTerm.Substring(beforeName + 1, searchTerm.Length - beforeName - 1);
                }
            }
            else //bar formats
            {
                String[] searchTermArray = searchTerm.Split("|");
                if (searchTermArray.Length == 5) //current pipe format
                {
                    ItemFields["TicketNumber"] = searchTermArray[0];
                    ItemFields["Amount"] = searchTermArray[1];
                    ItemFields["ItemDescription"] = searchTermArray[2];
                    ItemFields["CustomerPurchaseOrder"] = searchTermArray[3];
                    ItemFields["ticketTech"] = searchTermArray[4];
                    ItemFields["Type"] = "non-stockItem";
                }
                else if (searchTermArray.Length == 3) //temporary pipe format
                {
                    ItemFields["TicketNumber"] = searchTermArray[0];
                    ItemFields["Amount"] = searchTermArray[1].Substring(0, searchTermArray[1].IndexOf(" "));
                    searchTermArray[1] = searchTermArray[1].Substring(searchTermArray[1].IndexOf(" "), searchTermArray[1].Length - searchTermArray[1].IndexOf(" "));
                    Regex poMatch = new Regex("[A-Z]{3}-[0-9]{7}");
                    Match matched = poMatch.Match(searchTermArray[1]);
                    int beforeName = poMatch.Match(searchTermArray[1]).Index;
                    if (beforeName != 0)
                    {
                        ItemFields["ItemDescription"] = searchTermArray[1].Substring(1, beforeName - 2);
                        ItemFields["CustomerPurchaseOrder"] = searchTermArray[1].Substring(beforeName, 11);
                    }
                    ItemFields["ticketTech"] = searchTermArray[2];
                }
                ItemFields["Found"] = Found.undefined;
                ItemFields["Type"] = "non-stockItem";
                ItemFields["SerialNumber"] = "N/A";
                ItemFields["AssetTag"] = "N/A";
            }
            //validate entry
            return ItemFields;
        }
        private void RichTextBox1_KeyDown(object? sender, KeyEventArgs e)
        {
            //processes user input
            if (e.KeyData == Keys.Enter)
            {
                if (inputBox.Text.Length > 0)
                {
                    //breaks input into fields. returns null if there's an invalid value
                    Dictionary<String, String> ItemFields = ParseText();
                    if (ValidateManifestFields(ItemFields))
                    {
                        String type = ItemFields["Type"];
                        //inserts into data grid
                        fillDataGrid(ItemFields);
                        //checks for item in history
                        if (type != "non-stockItem")
                        {
                            SearchForTaggedItemsInHistory(type); //either set to SerialNumber or AssetTag
                        }
                        else
                        {
                            SearchForNonTaggedItemsInHistory(false);
                        }
                        //resets input box text
                        inputBox.Text = "";
                    }
                }
            }
        }

        private void linkButton_click(object sender, EventArgs e)
        {
            ManualMatchItems();
        }

        private void submitButton_click(object sender, EventArgs e)
        {
            string table;
            string query;
            DialogResult result = MessageBox.Show("If you didn't copy the scanned items into the manifest, click 'No' and do so now.", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                return;
            }
            //else
            foreach (DataGridViewRow row in historyGrid.Rows)
            {
                table = "";
                query = "";
                if (row.Cells["IMEI"].Value.ToString() != "n/a")
                {
                    table = "phoneHistory";
                    String sent = row.Cells["amountAtADC"].Value.ToString();
                    if (sent == "0")
                    {
                        sent = "1";
                    }
                    else
                    {
                        sent = "0";
                    }
                    query = $"UPDATE {table} SET sent='{sent}' WHERE uname='{row.Cells["uname"].Value.ToString()}' AND timeReceived='{row.Cells["timeReceived"].Value.ToString()}'";
                }
                else
                {
                    table = "history";
                    query = $"UPDATE {table} SET amountAtADC='{row.Cells["amountAtADC"].Value.ToString()}' WHERE uname='{row.Cells["uname"].Value.ToString()}' AND timeReceived='{row.Cells["timeReceived"].Value.ToString()}'";
                }
                DatabaseQueries.Upsert(query);
                //remove manifest rows
                //reset history
                //close window(?)

                /*
                query += " WHERE ";
                List<string> keys = DatabaseQueries.PrimaryKeys("History");
                foreach (String key in keys) 
                {

                }
                */
            }
            foreach (DataGridViewRow row in manifestGrid.Rows)
            {
                if (row.Cells["Found"].Value.ToString() == Found.undefined || row.Cells["Found"].Value.ToString() == Found.notFound)
                {
                    table = "unmatchedhistory";
                    List<string> exclusionList = ["Type", "Found"];
                    string fieldsList = "(";
                    string valuesList = "(";
                    foreach (string k in manifestGridConfig.configurations.Keys)
                    {
                        if (!exclusionList.Contains(k))
                        {
                            fieldsList += k + ", ";
                            valuesList += "'" + row.Cells[k].Value.ToString() + "', ";
                        }
                    }
                    fieldsList = fieldsList.Substring(0, fieldsList.Length - 2);
                    valuesList = valuesList.Substring(0, valuesList.Length - 2);
                    fieldsList += ")";
                    valuesList += ")";
                    query = $"INSERT INTO {table} {fieldsList} VALUES {valuesList}";
                    DatabaseQueries.Upsert(query);
                }
            }
            Close();
            //List<String> manifest = new List<String>();
            //popup.rich
        }

        private void CopyItems(bool assetTag, bool serialNumber)
        {
            String manifestRow = "";
            String manifestRowTagged = "";
            foreach (DataGridViewRow row in manifestGrid.Rows)
            {
                if (row.Cells["type"].Value.ToString() == "non-stockItem")
                {
                    if (!assetTag && !serialNumber)
                    {
                        foreach (KeyValuePair<int, String> kvp in manifestOutputFields)
                        {
                            manifestRow += row.Cells[kvp.Value].Value.ToString() + "\t";
                        }
                        manifestRow += "\n";
                        //String manifestRow = row.Cells["TicketNumber"].Value.ToString() + "\tN/A\t" + row.Cells["Amount"].Value.ToString() + " " + row.Cells["ItemDescription"].Value.ToString() + "\t" + row.Cells["ticketTech"].Value.ToString();
                        //manifest.Add(manifestRow);
                    }
                }
                else if (assetTag)
                {
                    //the type is either "AssetTag" or "SerialNumber" which is the only value we need to copy in this case
                    manifestRowTagged += row.Cells["AssetTag"].Value.ToString();
                    manifestRowTagged += "\n";
                }
                else if (serialNumber)
                {
                    //the type is either "AssetTag" or "SerialNumber" which is the only value we need to copy in this case
                    manifestRowTagged += row.Cells["SerialNumber"].Value.ToString();
                    manifestRowTagged += "\n";
                }
            }
            manifestRow += manifestRowTagged;
            Clipboard.SetData(DataFormats.Text, (Object)manifestRow);
        }
        private void copyNonTagged_Click(object sender, EventArgs e)
        {
            CopyItems(false, false);
        }
        private void copyTaggedButton_Click(object sender, EventArgs e)
        {
            CopyItems(true, false);
        }

        private void copySerialButton_Click(object sender, EventArgs e)
        {
            CopyItems(false, true);
        }
        private void delete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to delete this row?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                ds.Tables[tableName].Rows.RemoveAt(radioButton.SelectedCell["manifestCheckbox"]);
                manifestGrid.DataSource = ds.Tables[tableName];
                linkButton.Enabled = false;
                deleteButton.Enabled = false;
                //deletePanel.BringToFront();
                //deletePanel.Visible = false;
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < manifestGrid.Rows.Count; i++)
            {
                Rectangle cellBounds = manifestGrid.Rows[i].Cells[0].GetContentBounds(manifestGrid.Rows[i].Cells[0].RowIndex);
                //cellBounds.Height = cellBounds.Height - 1;
                //cellBounds.Width = cellBounds.Width - 1;
                Rectangle cellClip = cellBounds;
                //cellClip.Height = cellClip.Height - 1;
                //cellClip.Width = cellClip.Width - 1;

                //cellBounds.Width = 10;
                //cellBounds.Height = 10;
                //cellClip.Width = 10;
                //cellClip.Height = 10;
                if (cellBounds.Height > 0 && cellBounds.Width > 0)
                {
                    DataGridViewCellStyle s = manifestGrid.DefaultCellStyle;
                    //DataGridViewCellStyle s = manifestGrid.Rows[i].Cells[0].Style;
                    bool first = manifestGrid.Rows[i].Cells[0].RowIndex == 0 ? true : false;
                    //manifestGrid.Rows[i].Cells[0].PositionEditingControl(false, true, temp, temp, s, true, true, true, first) ;
                    manifestGrid.Rows[i].Cells[0].PositionEditingControl(false, true, cellBounds, cellClip, s, false, false, true, first);
                    //manifestGrid.Rows[i].Cells[0].PositionEditingPanel(cellBounds, cellClip, s, true, true, true, first);
                }
            }

                    private void copyDataGridView(DataGridView source, DataTable table)
        {
            foreach (PropertyInfo sourceProperty in source.GetType().GetProperties())
            {
                if (sourceProperty.Name == "DataSource")
                {
                    searchResults.DataSource = table;
                }
                else if(!sourceProperty.Name.Contains("Current") && !sourceProperty.Name.Contains("Displayed") && !sourceProperty.Name.Contains("Count") && !sourceProperty.Name.Contains("Item"))
                {
                    object newValue = sourceProperty.GetValue(source, null);
                    MethodInfo mi = sourceProperty.GetSetMethod(true);
                    if (mi != null)
                    {
                        sourceProperty.SetValue(searchResults, newValue, null);
                    }
                }
            }
        }
            */
        }



        /*
Dictionary<String, Type> manifestGridFields = new Dictionary<string, Type>()
{
{"TicketNumber", Type.GetType("System.UInt32")},
{"Amount", Type.GetType("System.UInt16")},
{"ItemDescription", Type.GetType("System.String")},
{"ticketTech", Type.GetType("System.String")},
{"AssetTag", Type.GetType("System.String")},
{"Type", Type.GetType("System.String")},
{"SerialNumber", Type.GetType("System.String")},
{"Found", Type.GetType("System.String")},
{"CustomerPurchaseOrder", Type.GetType("System.String")}
};
*/
    }
}
