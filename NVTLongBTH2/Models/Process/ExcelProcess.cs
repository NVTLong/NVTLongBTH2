using System.Data;
using OfficeOpenXml;
namespace NVTLongBTH2.Models.Process
{
    public class ExcelProcess
    {
        public DataTable ExcelToDataTable(string strPath)
        {
            FileInfo fi = new FileInfo(strPath);
            ExcelPackage excelPackage = new ExcelPackage(fi);
            DataTable dt = new DataTable();
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
            //check if the worksheet is completely empty
            if(worksheet.Dimension == null)
            {
                return dt;
            }
            //create a list to hold the column names
            List<string> columnNames = new List<string>();
            //needed to keep track of empty column headers
            int currentColumn = 1;
            //loop all columns in the sheet and add them to the database
            foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                string columnName = cell.Text.Trim();
                //check if the previous header was empty and add it if it was
                if(cell.Start.Column != currentColumn)
                {
                    columnNames.Add("Header_" + currentColumn);
                    dt.Columns.Add("Header_" + currentColumn);
                    currentColumn ++;
                }
                //add the column name to the list to count the duplicates
                columnNames.Add(columnName);
                //
                //
                int occurrences = columnNames.Count(x => x.Equals(columnName));
                if(occurrences > 1)
                {
                    columnName = columnName + "_" + occurrences;
                }
                // add the column to the database
                dt.Columns.Add(columnName);
                currentColumn++;
            }
            //
            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
            {
                var row = worksheet.Cells[i, 1, i, worksheet.Dimension.End.Column];
                DataRow newRow = dt.NewRow();
                foreach (var cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                }
                dt.Rows.Add(newRow);
            }
            return dt;
        }
    }
}