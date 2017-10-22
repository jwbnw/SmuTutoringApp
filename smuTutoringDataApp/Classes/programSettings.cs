using System;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace smuTutoringDataApp
{
    class programSettings
    {

        //NOTE: Hard code first dir lookup

       // private static string GLOBAL_STATIC_FIRST_LOOKUP = "H:\\somePath";
          private static string GLOBAL_STATIC_FIRST_LOOKUP = "C:\\SomePath"; //    -- testing

        // Properties for settings
        public static string masterEmailAdress { get; set; }
        public static string studentInfoCSVPath { get; set; }
        public static string masterSaveLocationPath { get; set; }
        public static string masterTutorListPath { get; set; }
        public static string masterCourseCheckListPath { get; set; }

        // Properties for email-settings
        public static string senderEmail { get; set; }
        public static string senderPassword { get; set; }
        public static string recieverEmail { get; set; }
        public static string smptHostName { get; set; }
        public static int smptPortNumber { get; set; }

        public static void loadMainSettings()
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(GLOBAL_STATIC_FIRST_LOOKUP + "\\MainSettings.txt");

                masterSaveLocationPath = lines[0];
                studentInfoCSVPath = lines[1];
                masterTutorListPath = lines[2];
                masterCourseCheckListPath = lines[3];
            }
            catch
            {
                MessageBox.Show("There are no settings to load! A blank settings file will be created at the location :" + GLOBAL_STATIC_FIRST_LOOKUP + " Please set the settings from the settings menu");

                string[] lines = { GLOBAL_STATIC_FIRST_LOOKUP, "", "", "" };
                programSettings.masterSaveLocationPath = GLOBAL_STATIC_FIRST_LOOKUP;

                System.IO.File.WriteAllLines(GLOBAL_STATIC_FIRST_LOOKUP+"\\MainSettings.txt", lines);

            }
        }

        //************************************************
        //Method Name: getMasterStudentList
        //Description: This method will open up a dialogue
        //box for the user to select the file location
        //for the master student list. This list contains
        //student information such as course lists and 
        //name. That path is then saved to the 
        //studentInfoCSVPath property.
        //************************************************
        public static void getMasterStudentList()
        {
            // Open file dialogue object
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set extensions
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";

            // Open dialog
            Nullable<bool> result = dlg.ShowDialog();

            // Set the path if we get a result from the dialogue
            if (result == true)
            {
                string[] lines = System.IO.File.ReadAllLines(GLOBAL_STATIC_FIRST_LOOKUP + "\\MainSettings.txt");

                lines[1] = dlg.FileName;

                System.IO.File.WriteAllLines(GLOBAL_STATIC_FIRST_LOOKUP + "\\MainSettings.txt", lines);
                studentInfoCSVPath = dlg.FileName;
            }
            else
            {
                studentInfoCSVPath = null;
            }
        }

        //**************************************************
        //Method Name: getMasterTutorList
        //Description: This method will open up a dialogue
        //box for the user to select the file location for
        //the master tutor list  This list contains
        //tutor names. That path is then saved to the 
        //masterTutorListPath property.
        //**************************************************
        public static void getMasterTutorList()
        {

            // Open file dialogue obj
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set extentions
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";

            //Open dialogue
            Nullable<bool> result = dlg.ShowDialog();

            //set the path
            if (result == true)
            {
                string[] lines = System.IO.File.ReadAllLines(GLOBAL_STATIC_FIRST_LOOKUP + "\\MainSettings.txt");
                lines[2] = dlg.FileName;
                System.IO.File.WriteAllLines(GLOBAL_STATIC_FIRST_LOOKUP + "\\MainSettings.txt", lines);
                masterTutorListPath = dlg.FileName;
            }

            else
            {
                masterTutorListPath = null;
            }
        }

        //**************************************************
        //Method Name: getMasterStemCourseList
        //Description: This method will open up a dialogue
        //box for the user to select the file location for
        //the stem course list  This list contains
        //stem courses. That path is then saved to the 
        //masterCourseCheckListPath property.
        //**************************************************
        public static void getMasterStemCourseCheckList()
        {
            // Open file dialogue obj
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set extentions
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";

            //Open dialogue
            Nullable<bool> result = dlg.ShowDialog();

            //set the path
            if (result == true)
            {

                string[] lines = System.IO.File.ReadAllLines(GLOBAL_STATIC_FIRST_LOOKUP + "\\MainSettings.txt");
                lines[3] = dlg.FileName;
                System.IO.File.WriteAllLines(GLOBAL_STATIC_FIRST_LOOKUP + "\\MainSettings.txt", lines);
                masterCourseCheckListPath = dlg.FileName;
            }
            else
            {
                masterCourseCheckListPath = null;
            }
        }

        //NOTE: Leaving for later builds
        //**************************************************
        //Method Name: getMasterSaveLocation
        //Description: This method will open up a dialogue
        //box for the user to select the file location for
        //the save loaction. That path is then saved to the 
        //masterSaveLocationPath property.
        //**************************************************
        //public static void getMasterSaveLocation()
        //{

        //    //Using the Windows Forms dll
        //    System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();

        //    //Open Folder dialogue
        //    dlg.ShowDialog();

        //    //set path to temp string
        //    string tempPath = dlg.SelectedPath;

        //    //run quick test then assign property
        //    if (tempPath != null)
        //    {
        //        masterSaveLocationPath = tempPath;
        //        using (System.IO.StreamWriter file = new System.IO.StreamWriter(masterSaveLocationPath + "MainSetting.txt"
        //                {
        //            foreach (string lines in lines)

        //            }
        //        }
        //    }

        //    else
        //    {
        //        masterSaveLocationPath = null;
        //    }
        //}

        //************************************************************
        //Method Name: saveReport
        //Description: This method takes two arguments, the data table
        //to save and the file path as the where the table will be 
        //saved. The datatable is then saved to that path using 
        // the stream writer.
        //************************************************************
        public static void saveReport(DataTable dt, string filePath)
        {
            try
            {
                //Create new stream write object and assign it a path
                StreamWriter sw = new StreamWriter(filePath + "\\FinalDataTable.csv", false); //Note false = new file, True = append pre-existing doc

                //Get the column count from the data table
                int columnCount = dt.Columns.Count;

                //Write the column names to the object
                for (int i = 0; i < columnCount; i++)
                {
                    sw.Write(dt.Columns[i]);

                    //Place comma in appropriate place
                    if (i < columnCount - 1)
                    {
                        sw.Write(",");
                    }
                }

                sw.Write(sw.NewLine);

                //Write all the data to the stream
                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < columnCount; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            sw.Write(dr[i].ToString());
                        }

                        if (i < columnCount - 1)
                        {
                            sw.Write(",");
                        }
                    }

                    sw.Write(sw.NewLine);
                }

                //close the stream
                sw.Close();
            }

            //In the event of on error, should not throw one but just in case so the program does not crash.
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
     
        
        //***********************************************
        //Method Name: sendMasterReport
        //Description: This method will send the report
        //in the form of an email to the address that 
        //has been given via the admin. The method takes
        //advantage of the MailMessage and SMPTClient
        //classes.  
        //***********************************************
        public static void sendMasterReport()
        {
            try
            {
                //Create new mailMessage and Smpt Client
                MailMessage mail = new MailMessage();
                SmtpClient smptServer = new SmtpClient(smptHostName);

                //Consturct mail object
                mail.From = new MailAddress(senderEmail); // "your mail@gmail.com"
                mail.To.Add(recieverEmail); // "to_mail@gmail.com"
                mail.Subject = "Report Email!";
                mail.Body = "Report is attached";

                //Test for path to add attachment
                if (masterSaveLocationPath != null)
                {
                    //add master data table
                    System.Net.Mail.Attachment attachement;
                    attachement = new System.Net.Mail.Attachment(masterSaveLocationPath + "\\FinalDataTable.csv");
                    mail.Attachments.Add(attachement);

                    //enter smpt information
                    smptServer.Port = smptPortNumber;
                    smptServer.Credentials = new System.Net.NetworkCredential(senderEmail, senderPassword);
                    smptServer.EnableSsl = true;

                    //send email
                    try
                    {
                        smptServer.Send(mail);
                        MessageBox.Show("Sucess! Email Sent to: " + senderEmail);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Opps looks like something went wrong! Make sure credentials are correct");

                        //For error checking in event we have to debug 
                        string excep = ex.ToString();
                    }

                }
                else
                {
                    MessageBox.Show("Error Please select the master save location path");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void backUp()
        {

            try
            {
                //Delete prior backup
                File.Delete(masterSaveLocationPath + "\\backup.csv");

                //Copy latest report to a backup
                File.Copy(masterSaveLocationPath + "\\FinalDataTable.csv", masterSaveLocationPath + "\\backup.csv");
            }

            catch
            {
                MessageBox.Show("Error please close any files associated with this program.");
            }
        }

    }
}
