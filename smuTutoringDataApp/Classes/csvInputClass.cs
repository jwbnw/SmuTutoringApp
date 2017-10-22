using System.Data;
using System.Windows;

namespace smuTutoringDataApp
{
    class csvInputClass
    {
        // Datatable used for comparison 
        public DataTable compTable { get; set; } 

        //******************************************
        //Method Name: loadSetFromCSC
        // Description: This method takes a file path
        // for a csv file and creates a 
        // datatable from said csv file.
        //******************************************
        public DataTable loadSetFromCSV(string filePath)
        {
            DataTable csvDt = new DataTable();
            try
            {
                //read .csv into string[]
                string[] csvRows = System.IO.File.ReadAllLines(filePath); 
                string[] fields = null;

                //take the header row and split it up
                string colHeaders = csvRows[0];
                string[] headerFeilds = colHeaders.Split(',');

                //Assign columns based on header names
                foreach (string column in headerFeilds)
                {
                    DataColumn headers = new DataColumn(column);
                    csvDt.Columns.Add(headers);
                }

                //Note: These are extra columns in the event there is some extra data in the .csv
                DataColumn spacer = new DataColumn("Spacer");
                DataColumn spacer1 = new DataColumn("Spacer1");

                csvDt.Columns.Add(spacer);
                csvDt.Columns.Add(spacer1);

                //take each row of data, read it into data row then add to data table
                for (int i = 1; i < csvRows.Length; i++)
                {
                    fields = csvRows[i].Split(',');
                    DataRow dataRow = csvDt.NewRow();
                    dataRow.ItemArray = fields;
                    csvDt.Rows.Add(dataRow);
                }
                return compTable = csvDt;
            }
            catch
            {
                MessageBox.Show("Error, please close all files associated with this program");
            }
            return null;
        }
    }
}
