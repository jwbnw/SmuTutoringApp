using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
namespace smuTutoringDataApp
{
    /// <summary>
    /// Interaction logic for signIn.xaml
    /// </summary>
    public partial class signIn : Page
    {
        public signIn()
        {
            InitializeComponent();

            //Focus input on idTxTbox for scanner
            idTxtBox.Focus();
        }

        //*****************************************************
        //Event Name: idTxtBox_TextChanged
        //Description: This event will run everytime input
        //is detected in the idTxtBox. If the input length is
        //10 (length of id) the conditions are met and the
        //code inside runs. This method will fill the empty
        //text boxes up with data related to the current user
        //****************************************************
        private void idTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            //State condition
            if (idTxtBox.Text.Length == 10)
            {
                //Create new student obj
                studentInfo studentObj = new studentInfo();

                //Assign ID property
                studentObj.userID = idTxtBox.Text;

                //Run AssignData method
                studentObj.AssignData();

                //Run check on the master course check list since we need it
                if (programSettings.masterCourseCheckListPath != null)
                {
                    //Set student name
                    nameTxtBox.Text = studentObj.userFirstName + " " + studentObj.userLastName;
                    //Set the users course list w/ some conditions. First when list is empty
                    if (studentObj.userCourseList != null && ClassListBox.Items.IsEmpty == true)
                    {
                        ClassListBox.Items.Add("Here To Study");
                        foreach (string item in studentObj.userCourseList)
                        {
                            ClassListBox.Items.Add(item);
                        }

                    }

                    //Second if the list is not empty
                    else if (studentObj.userCourseList != null && ClassListBox.Items.IsEmpty == false)
                    {

                        //Clear prior data
                        ClassListBox.Items.Clear();

                        ClassListBox.Items.Add("Here To Study"); // NEED TO TEST(view finsl fsts tbslr)

                        foreach (string itemAdd in studentObj.userCourseList)
                        {
                            ClassListBox.Items.Add(itemAdd);
                        }
                    }
                }

                //Otherwise show error that relates to condition
                else
                {
                    MessageBox.Show("Error please make sure a STEM Course comparison spreadsheet is seleted from the settings menu", "No STEM List Error");
                }
            }
        }

        //**************************************************
        //Event Name: signInGoBtn_Click
        //Description: This event will send the information
        //given by the user to the master data table. It 
        //will also save the master data table.
        //*************************************************

        private void signInGoBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                // Build studentInfo Object to pull and assign data from CSV using csvInputClass
                studentInfo studentObj = new studentInfo();
                studentObj.userID = idTxtBox.Text;
                studentObj.AssignData();

                //Assign time, date and year to strings
                string inTimeStamp = DateTime.Now.ToString("h:mm:ss tt");
                string dateStamp = DateTime.Now.ToString("MM-dd-yy");
                string currentYear = DateTime.Now.Year.ToString();

                //get semester using my getSemester method 
                string semester = getSemester(DateTime.Today);

                studentObj.userWaiver = dataTables.checkForWaiver(studentObj.userID);


                //Check to userWaiver object, if false hit this conditional block
                if (studentObj.userWaiver == false)
                {

                    //Show tutor waiver form
                    MessageBoxResult result = MessageBox.Show("As a user of the tutoring program in the CSLWA, I will come prepared by\n 1) Signing in and out when I recieve tutoring\n 2) Attempting assignments in advance of tutoring\n 3) Bringing assignments, questions, notes, book, syllabus, and/or other course           materials\n 4) Attending classes regularly (tutoring is not a substitute for class attendance)\n 5) Expecting the tutor to only serve as a guide & not as someone who will do my       homework for me\n\n   By pressing Yes, I " + studentObj.userFirstName + ", studentID: " + studentObj.userID + " understand that by following\n   these five simple tips, I can  maximize my tutoring experience\n\n   If you choose No, tutors have the right to refuse service", "Student Waiver", MessageBoxButton.YesNo);

                    //switch block for results NOTE: In the event we get rid of the Waiver tabel we will use the materDataTable to write this info
                    switch (result)
                    {
                        //Write Yes
                        case MessageBoxResult.Yes:
                            studentObj.userWaiver = true;                       
                            break;

                        //Write No
                        case MessageBoxResult.No:
                            studentObj.userWaiver = false;
                            break;

                    }
                }

                if (ClassListBox.SelectedItem == null)
                {

                    //Show error
                    MessageBox.Show("Error please select the course you need tutoring help with or seletect here to study", "No Course Error");
                    return;
                }
                else
                {
                    //split couseSelection into course and code
                    string courseSelection = ClassListBox.SelectedItem.ToString();
                    string subjectArea = "N/A";

                    //In event student is not here for a certain class
                    if (courseSelection != "Here To Study")
                    {

                        subjectArea = courseSelection.Substring(0, 3);

                    }

                    string waiverResult;

                    if (studentObj.userWaiver == true)
                    {
                        waiverResult = "Yes";
                    }
                    else
                    {
                        waiverResult = "No";
                    }
                    //Write Data
                    dataTables.WriteInData(dateStamp, studentObj.userID, studentObj.userLastName, studentObj.userFirstName, semester, currentYear, subjectArea, courseSelection, inTimeStamp, waiverResult);

                    //Clear Data
                    idTxtBox.Text = "";
                    nameTxtBox.Text = "";

                    foreach (string item in studentObj.userCourseList)
                    {
                        ClassListBox.Items.Remove(item);
                    }

                    // Save report with latest data
                    programSettings.saveReport(dataTables.masterDataTable, programSettings.masterSaveLocationPath);

                    //Save backUp
                    programSettings.backUp();

                    //Route to start page
                    startPage sp = new startPage();
                    this.NavigationService.Navigate(sp);
                }

            }
            //   Catch exception and show error message
            catch
            {
                MessageBox.Show("Please enter your student ID and press GO\nIf you see an additional error message after entering your ID please tell the front desk");

                //Route back to start page
                startPage sp1 = new startPage();
                this.NavigationService.Navigate(sp1);
            }


        }

        //****************************************
        //Method Name: getSemester
        //Description: This method takes in the
        //current date. It then runs a quick
        //calculation to see if it fall or spring.
        //It returns the result of fall or spring
        //depending on the date.
        //****************************************
        private string getSemester(DateTime date)
        {
            float value = (float)date.Month + date.Day / 100; // month.day (2 digits)

            // if value is less than 7 (July) return spring
            if (value > 7.1)
            {
                return "FALL";
            }
            // else return fall
            else
            {
                return "SPR";
            }
        }

        //****************************************************
        //Event Name: backHomeBtn_Click 
        //Description: This method routes the user back to the
        //start page.
        //****************************************************
        private void backHomeBtn_Click(object sender, RoutedEventArgs e)
        {
            startPage sp = new startPage();
            this.NavigationService.Navigate(sp);
        }
    }
}
