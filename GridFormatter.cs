/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
*/
using System.Data;

namespace AssetStatusInfo
{
    internal class GridFormatter
    {
        public Dictionary<string, string> nameMap = new Dictionary<string, string>();
        public Dictionary<string, string> inverseNameMap = new Dictionary<string, string>();
        public GridFormatter() 
        {
            string[] names = AllSettings.Default.ColumnFriendlyNameMapping.Split("|");
            foreach(string name in names)
            {
                nameMap[name.Split(",")[0]] = name.Split(",")[1];
            }
            foreach (KeyValuePair<string, string> pair in nameMap)
            {
                inverseNameMap.Add(pair.Key, pair.Key);
            }
            foreach (KeyValuePair<string, string> pair in nameMap)
            {
                if (!inverseNameMap.ContainsKey(pair.Value))
                {
                    inverseNameMap.Add(pair.Value, pair.Key);
                }
            }
        }

        public static int width(DataGridView grid, bool visible = true)
        {
            int width = 0;

            if (visible)
            {
                var visibleColumns = from DataGridViewColumn c in grid.Columns
                                     where c.Visible
                                     select c;
                foreach (DataGridViewColumn column in visibleColumns)
                {
                    width += column.Width;
                }
            }
            else
            {
                foreach (DataGridViewColumn column in grid.Columns)
                {
                    width += column.Width;
                }

            }
            return width + grid.Margin.Left + grid.Margin.Right + 25;
        }
        public static InputConfigurations GridColumnDataConfiguration(string formatName)
        {
            InputConfigurations configs = new InputConfigurations();
            InputConfigurationBuilder builder = new InputConfigurationBuilder(typeof(ushort));

            if (formatName == "manifestGrid")
            {
                builder.HasLengthMinMax(0, 30);
                configs.AddConfiguration("Amount", builder.Build());
                //
                builder.Reset(typeof(ulong));
                builder.HasLengthMinMax(0, 7);
                configs.AddConfiguration("TicketNumber", builder.Build());
                //
                builder.Reset(typeof(string));
                configs.AddConfiguration("Type", builder.Build());
                configs.AddConfiguration("Found", builder.Build());
                configs.AddConfiguration("SerialNumber", builder.Build());
                //
                builder.HasLengthMinMax(0, 30);
                configs.AddConfiguration("ticketTech", builder.Build());
                configs.AddConfiguration("CustomerPurchaseOrder", builder.Build());
                //
                builder.HasLengthMinMax(0, 100);
                configs.AddConfiguration("ItemDescription", builder.Build());
                //
                builder.HasLengthMinMax(0, 6);
                builder.HasCodeWords(["N/A"]);
                configs.AddConfiguration("AssetTag", builder.Build());
            }
            else if (formatName == "historySearchGrid")
            {
                builder.Reset(typeof(string));
                configs.AddConfiguration("Found", builder.Build());
                configs.AddConfiguration("SerialNumber", builder.Build());
                configs.AddConfiguration("IMEI", builder.Build());
                //
                builder.HasLengthMinMax(0, 100);
                configs.AddConfiguration("ItemDescription", builder.Build());
                //
                builder.HasLengthMinMax(0, 30);
                configs.AddConfiguration("CustomerPurchaseOrder", builder.Build());
                configs.AddConfiguration("uname", builder.Build());
                configs.AddConfiguration("Amount", builder.Build());
                //
                builder.Reset(typeof(ulong));
                builder.HasLength(6);
                builder.HasCodeWords(["N/A"]);
                configs.AddConfiguration("AssetTag", builder.Build());
                //
                builder.Reset(typeof(string));
                builder.HasCodeWords(DatabaseQueries.GetLocations());
                configs.AddConfiguration("location", builder.Build());
                //
                builder.Reset(typeof(ulong));
                builder.HasLength(7);
                configs.AddConfiguration("TicketNumber", builder.Build());
                //
                builder.Reset(typeof(DateTime));
                builder.HasLengthMinMax(0, 30);
                configs.AddConfiguration("TimeReceived", builder.Build());
                //
                builder.Reset(typeof(ushort));
                builder.HasLengthMinMax(0, 30);
                builder.HasValueMinMax(0, null);
                configs.AddConfiguration("AmountAtADC", builder.Build());
            }
            
            return configs;
        }
        public DataTable DataTableColumnHeaderFormatting(DataTable grid)
        {
            foreach (DataColumn column in grid.Columns)
            {
                if (nameMap.ContainsKey(column.ColumnName))
                {
                    column.ColumnName = nameMap[column.ColumnName];
                }
            }
            return grid;
        }
        public void DataGridViewColumnHeaderFormatting(DataGridView grid)
        {
            grid.RowHeadersVisible = false; //removes extra column on the left
            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (nameMap.ContainsKey(column.HeaderText))
                {
                    column.HeaderText = nameMap[column.HeaderText];
                }
                switch (column.Name)
                {
                    //hide primary keys
                    case "uname":
                        column.Visible = false; break;
                    case "timeReceived":
                        column.Visible = false; break;
                    //hide IMEI 
                    case "IMEI":
                        column.Visible = false; break;
                }
                //column.Name = column.HeaderText;
            }
            if (grid.Columns["TimeReceived"] != null)
            {
                grid.Sort(grid.Columns["timeReceived"], System.ComponentModel.ListSortDirection.Descending);
            }
        }
        public void DataGridViewCommonFormating(DataGridView grid)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (!column.Name.Contains("Checkbox"))
                {
                    column.ReadOnly = true;
                }
            }
            DataGridViewColumnHeaderFormatting(grid);
        }
    }
}
