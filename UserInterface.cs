using System;
using System.Windows.Forms;
using VO_FileHandle;
using RC_Resources;

namespace UI_UserInterFace
{
    public class ui_Controls
    {
        public static void AddColumnInGrid(DataGridView grd_userData)
        {
            grd_userData.Columns.Clear();

            foreach (ColumnNamesEnum column in Enum.GetValues(typeof(ColumnNamesEnum)))
            {
                grd_userData.Columns.Add(column.ToString(), column.ToString());
            }
            grd_userData.RowHeadersWidth = 25;
        }

        public static void ExtractDataIntoGrid(DataGridView grd_userData, ValueLayerObject vlo)
        {
            grd_userData.Rows.Clear();
            grd_userData.Rows.Add();
            vlo.userData = new string[ValueLayerObject.records.Count, 10];
            for (int i = 0; i < ValueLayerObject.records.Count; i++)
            {
                string[] columnData = new string[10];
                columnData[0] = ValueLayerObject.records[i].Substring(0, 18).Trim();
                columnData[1] = ValueLayerObject.records[i].Substring(18, 7).Trim();
                columnData[2] = ValueLayerObject.records[i].Substring(25, 50).Trim();
                columnData[3] = ValueLayerObject.records[i].Substring(75, 25).Trim();
                columnData[4] = ValueLayerObject.records[i].Substring(100, 50).Trim();
                columnData[5] = ValueLayerObject.records[i].Substring(150, 10).Trim();
                int index = Convert.ToInt32(ValueLayerObject.records[i].Substring(160, 2).Trim());
                columnData[6] = ((Qualification)index).ToString();
                columnData[7] = ValueLayerObject.records[i].Substring(162, 255).Trim();
                columnData[8] = ValueLayerObject.records[i].Substring(417, 10).Trim();
                columnData[9] = ValueLayerObject.records[i].Substring(427, 500).Trim();
                grd_userData.Rows.Add(columnData);
                vlo.serialNumber = Convert.ToInt32(columnData[0]) + 1;
                for (int column = 0; column < 10; column++)
                {
                    vlo.userData[i, column] = columnData[column];
                }
            }
            grd_userData.Sort(grd_userData.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
        }

        public static void HideRowsWithEmptyCells(int indexOfColumn, object data, DataGridView grd_userData)
        {
            if (data != null)
            {
                for (int index = 1; index < grd_userData.Rows.Count - 1; index++)
                {
                    DataGridViewRow selectedRow = grd_userData.Rows[index];
                    if (string.IsNullOrEmpty(selectedRow.Cells[indexOfColumn].Value?.ToString()) || !(selectedRow.Cells[indexOfColumn].Value?.ToString().ToUpper()).Contains(data?.ToString().ToUpper()))
                    {
                        selectedRow.Visible = false;
                    }
                }
            }
        }
    }
}