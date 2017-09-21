using System;
using System.Data;
using System.Windows;

namespace smuTutoringDataApp
{
    public class dataTables
    {

        // The master DataTable property and the Waiver Table property
        public static DataTable masterDataTable { get; set; }
        //public static DataTable waiverTable { get; set; }


        //*************************************************
        //Method Name: CreateTable
        //Description: This method creates the
        // data table that will be used to save
        // the final report. The reason we create
        // a separate datatable as opposed to editing
        // the main datatable is an extra safety measure.
        // That way we can check for data.
        //*************************************************
        public static void CreateTable()
        {
            // Create new Data Table
            DataTable UserDataTable = new DataTable();

            // Assign columns with their names and data types
            UserDataTable.Columns.Add("Appointment", typeof(string));
            UserDataTable.Columns.Add("StudentID", typeof(string));
            UserDataTable.Columns.Add("Last name", typeof(string));
            UserDataTable.Columns.Add("First name", typeof(string));
            UserDataTable.Columns.Add("Semester", typeof(string));
            UserDataTable.Columns.Add("Academic year", typeof(string));
            UserDataTable.Columns.Add("Subject area", typeof(string));
            UserDataTable.Columns.Add("Course Code", typeof(string));
            UserDataTable.Columns.Add("Tutor Name", typeof(string));
            UserDataTable.Columns.Add("Sign In Time", typeof(string));
            UserDataTable.Columns.Add("Sign Out Time", typeof(string));
            UserDataTable.Columns.Add("Total Time", typeof(string));
            UserDataTable.Columns.Add("Activity", typeof(string));
            UserDataTable.Columns.Add("Waiver", typeof(string));

            // Check for data in the master data table, if null then assign columns
            if (masterDataTable == null)
            {
                masterDataTable = UserDataTable;
            }
        }

        //******************************************
        //Method Name: WriteInData
        //Description: This method take in the
        //necessary data from a student signing
        //in and writes it to the master data table
        //This method does no editing of the data
        //it take as parameters, that must be done 
        //elsewhere
        //*******************************************
        public static void WriteInData(string date, string studentId, string studentLastName, string studentFirstName, string semester, string academicYear, string subjectArea, string courseCode, string signInTime,string waiver)
        {
            masterDataTable.Rows.Add(date, studentId, studentLastName, studentFirstName, semester, academicYear, subjectArea, courseCode, "", signInTime ,"","","",waiver);
        }

        //******************************************************
        //Method Name: CheckForOutEntery
        //Description: This method will check to see if
        //there is an entery within the sign out time column.
        //If there is no data there aka null, it will fill that
        //null space with an empty space. It then returns the 
        //master table.
        //******************************************************
        public static void checkForOutEntery()
        {
            //Check for null data
            DataRow[] result = masterDataTable.Select("[Sign Out Time] is null");

            //Fill each entery in the data row array with the empty string
            for (int i = 0; i < result.Length; i++)
            {
                DataRow tempToOperate = result[i];

                if (tempToOperate != null)
                {
                    try
                    {
                        tempToOperate[10] = "N/A";
                        tempToOperate[8] = "N/A";
                        tempToOperate[11] = "N/A";
                        tempToOperate[12] = "N/A";
                    }
                    catch
                    {
                    }

                    if (i == result.Length - 1 && tempToOperate[12].ToString() == "N/A")
                    {
                        programSettings.saveReport(masterDataTable, programSettings.masterSaveLocationPath);
                    }
                }
            }

        }

        //Check 4 Waiver
        public static bool checkForWaiver(string userID)
        {
            DataRow[] rowArrayToMatch = masterDataTable.Select("studentID='" + userID + "'");
            int latestEntry = rowArrayToMatch.Length;

            try
            {
                //Set data row to last entery, we subtract one do the the lenght property starting its count at 1 as opposed to 0
                DataRow UserTest = rowArrayToMatch[latestEntry - 1];
            }
            catch
            {
                //return value as fals
                return false;
            }

            try
            {
                // Set the data row to edit
                DataRow studentIdMatch = rowArrayToMatch[latestEntry - 1];

                // set a check to see if the user signed in
                string Wcheck = studentIdMatch[13].ToString();

                if (Wcheck == "Yes")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch
            {
                return false;
            }
        }


        //***************************************************************
        //Method Name: WriteTheOutData
        //Description: This method takes in 4 arguments, the user ID,
        //the tutor name, the sign out time and the option for Peer
        //assisted learning. We take the user Id and user it to query 
        //the master data table and pull a data row array of rows 
        //related to the user ID. We then preform some checks described
        //below and add the final data to the master data table.
        //***************************************************************

        public static void WriteOutData(string userID, string tutorName, string outTime)
        {
            // A check in the event that we want to abort writing the data
            bool NoSendAlert = false;

            // Create data row array
            DataRow[] rowArrayToMatch = masterDataTable.Select("studentID='" + userID + "'");

            //Get length of data row array
            int latestEntry = rowArrayToMatch.Length;

            // Run a test to see if there is any data 
            try
            {
                //Set data row to last entery, we subtract one do the the lenght property starting its count at 1 as opposed to 0
                DataRow UserTest = rowArrayToMatch[latestEntry - 1];
            }
            catch
            {
                //Set the no send alert
                NoSendAlert = true;
            }

            // In the event of the No sent alert being true we prompt the user with the most likly error.
            if (NoSendAlert == true)
            {
                MessageBox.Show("Error No Match Found? Did you sign in? Please try again or ask for assistence.");
            }

            // If the No send alert is false we continue on
            else
            {
                try
                {
                    // Set the data row to edit
                    DataRow studentIdMatch = rowArrayToMatch[latestEntry - 1];

                    // set a check to see if the user signed in
                    string check = studentIdMatch[9].ToString();
                    string doubleCheck = studentIdMatch[10].ToString();

                    // Run check. 
                    if (check == "")
                    {
                        MessageBox.Show("Error Please sign in before trying to sign out!");
                    }

                    // If check clears
                    else
                    {
                        //Run check and doubleCheck to make sure we are not pulling the prior row that already has data
                        if (check != "" && doubleCheck != "")
                        {
                            MessageBox.Show("Error Please sign in before trying to sign out!");
                        }

                        else
                        {
                            string inTime = studentIdMatch[9].ToString();
                            //calculate total time
                            TimeSpan duration = DateTime.Parse(outTime).Subtract(DateTime.Parse(inTime));

                            string totalTime = duration.ToString();
                            //take in time set to string see wat looks like
                            //make int then subtract from out time made int
                            //add to table via adding new row


                            // Set the data(put in try catch block, kek)

                     
                            studentIdMatch[11] = totalTime;
                            studentIdMatch[10] = outTime;
                            studentIdMatch[8] = tutorName;

                            //if pal needed and param in this function as a bool to corispond to signOut.cs then if statment here it write it


                            //If student was here to study fill with study
                            if (studentIdMatch[7].ToString() == "Here To Study")
                            {
                                studentIdMatch[12] = "STUDY";
                            }
                            //Else fill with STEM
                            else
                            {
                                studentIdMatch[12] = "STEM";
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


            }

        }


        //****************************************************
        //Method Name: SetMasterTable
        //Description: This program will set the master table
        //with a table that it reads in from a .csv file that
        //is provided via a parameter in the form of a string.
        //The reason for not invoking the csvInput class here
        //is due to the fact that an exact copy of the datatable
        //is required this method does not use spacers due to 
        //that this table needs to be a specific size. One
        //spacer is left commented out to demonstrate 
        //what this description mentions. The method itself
        //is very similar to that used in the csvInout class.
        //*****************************************************
        public static void setMasterTable(string filePath)
        {
            //Run process in try-catch block
            try
            {

                //Check file path for data
                if (filePath != null)
                {
                    //Build data table
                    DataTable csvDt = new DataTable();

                    //Read .csv into string[]
                    string[] csvRows = System.IO.File.ReadAllLines(filePath);
                    string[] fields = null;

                    //Take the header row and split it up
                    string colHeaders = csvRows[0];
                    string[] headerFeilds = colHeaders.Split(',');

                    //Check for column names and then Asign columns based on header names                
                    foreach (string column in headerFeilds)
                    {
                        DataColumn headers = new DataColumn(column);
                        csvDt.Columns.Add(headers);
                    }

                    //Note: Spacer example mentioned in description
                    // DataColumn spacer = new DataColumn("Spacer"); 
                    //  csvDt.Columns.Add(spacer);

                    //Take each row of data, read it into data row then add to data table
                    for (int i = 1; i < csvRows.Length; i++)
                    {
                        fields = csvRows[i].Split(',');
                        DataRow dataRow = csvDt.NewRow();
                        dataRow.ItemArray = fields;
                        csvDt.Rows.Add(dataRow);
                    }

                    //Set master data table
                    masterDataTable = csvDt;
                }
            }
            //Catch error in event of exception.Most likly two addressed directly. 
            catch
            {
                MessageBox.Show("Error, No master found please continue. An open .csv file will also cause this error. If you keep seeing this message please contact the front desk");
            }
        }

    }


}

