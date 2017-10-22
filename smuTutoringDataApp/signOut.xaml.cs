using System;
using System.Windows;
using System.Windows.Controls;
namespace smuTutoringDataApp
{
    /// <summary>
    /// Interaction logic for signOut.xaml
    /// </summary>
    public partial class signOut : Page
    {
        public signOut()
        {
            InitializeComponent();

            //Set focus on idTxtBox
            outIdTxtBox.Focus();
        }

        //*****************************************************
        //Event Name: outiIdTxtBox_TextChanged
        //Description: This event will run every time input
        //is detected in the outIdTxtBox. If the input length is
        //10 (length of id) the conditions are met and the
        //code inside runs. This method will fill the empty
        //text boxes up with data related to the current user
        //****************************************************
        private void outIdTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Check input length
            if (outIdTxtBox.Text.Length == 10)
            {

                //Create new student obj and assign data
                studentInfo studentObj = new studentInfo();
                studentObj.userID = outIdTxtBox.Text;
                studentObj.AssignData();

                // Check for data and fill name txt box then assign the tutor list via the student obj
                if (studentObj.userFirstName != null)
                {
                    outNameTxtBox.Text = studentObj.userFirstName + " " + studentObj.userLastName;
                    studentObj.AssignTutorList();
                }

                //Add tutors to tutor list box
                if (studentObj.userTutorList != null && tutorListBox.Items.IsEmpty == true)
                {
                    foreach (string item in studentObj.userTutorList)
                    {
                        tutorListBox.Items.Add(item);
                    }

                }
            }
        }

        //**************************************************
        //Event Name: signOUtGoBtn_Click
        //Description: This event will send the information
        //given by the user to the master data table. It 
        //will also save the master data table.
        //*************************************************
        private void signOutGoBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //Create student obj and assign data
                studentInfo studentObj = new studentInfo();
                studentObj.userID = outIdTxtBox.Text;
                studentObj.AssignData();


                //Set out time stamp and Pal or not
                string outTimeStamp = DateTime.Now.ToString("h:mm:ss tt");

                //Peer Assisted learning alg, in-event needed

                //bool? palOrNah = palCheckBox.IsChecked;

                //if (palOrNah == true)
                //{
                //    MessageBoxResult result = MessageBox.Show("Looks like Peer Assisted Learning is selected. If you meant to select this press yes, if you did not mean to or are unsure on your tutor selection please press No. Thanks!", "PAL Selected", MessageBoxButton.YesNo);

                //    switch (result)
                //    {
                //        case MessageBoxResult.Yes:
                //            break;
                //        case MessageBoxResult.No:
                //            return;
                //    }
                //}

                //Quick double check to see if user selected PAL on purpose


                if (tutorListBox.SelectedItem == null)
                {

                    //Show error
                    MessageBox.Show("Error please select the tutor you saw","No Tutor Error");
                    return;
                }
                else
                {
                    
                    //Write the out data
                    dataTables.WriteOutData(studentObj.userID, tutorListBox.SelectedItem.ToString(), outTimeStamp);

                    //Save current Table
                    programSettings.saveReport(dataTables.masterDataTable, programSettings.masterSaveLocationPath);

                    //Save backUp
                    programSettings.backUp();

                    //Route back to start page
                    startPage sp = new startPage();
                    this.NavigationService.Navigate(sp);

                }
            }

            //Catch exception
            catch
            {
                MessageBox.Show("Please enter your student ID and press GO\nIf you see an additional error message after entering your ID please tell the front desk");

                //Route back to start page
                startPage sp = new startPage();
                this.NavigationService.Navigate(sp);
            }


        }
        //***********************************
        //Event Name: goBackHomeBtn_Click
        //Description: Routes the user back
        //to the home screen.
        //***********************************
        private void goBackHomeBtn_Click(object sender, RoutedEventArgs e)
        {
            startPage sp = new startPage();
            this.NavigationService.Navigate(sp);

        }
    }
}
