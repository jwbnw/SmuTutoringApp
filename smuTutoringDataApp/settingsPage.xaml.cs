using System.Windows;
using System.Windows.Controls;

namespace smuTutoringDataApp
{
    /// <summary>
    /// Interaction logic for settingsPage.xaml
    /// </summary>
    public partial class settingsPage : Page
    {
        public settingsPage()
        {
            InitializeComponent();
        }
        //******************************************
        //Event Name: LogInBtn_Click
        //Description: This event allows the user
        //to enter a username and password to access
        //the settings. If the username and password
        //are correct the setting options are shown
        //otherwise the user to told he/she has
        //entered the wrong credentials.
        //******************************************
        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {

            //Make sure the fields are not empty
            if (!usernameTxtBox.Text.Equals("") && !passwordBox.Password.Equals(""))
            {
                //Check to see if credentials are correct
                if (usernameTxtBox.Text.Equals("admin") && passwordBox.Password.Equals("smusaints"))
                {
                    //Change visibility
                    optionsLablesStkPnl.Visibility = Visibility.Visible;
                    optionsChoiceStkPnl.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Wrong Password/Username");
                }
            }
            else
            {
                MessageBox.Show("Field left blank");
            }
        }

        //****************************************
        //Event Name: masterStudentListBtn_Click
        //Description: This method will call the
        //getMasterStudentList() method from the
        //program Settings class. It will then 
        //check to see if the user has selected
        //a file.
        //****************************************
        private void masterStudentListBtn_Click(object sender, RoutedEventArgs e)
        {
            //Call method
            programSettings.getMasterStudentList();

            //Run check
            if (programSettings.studentInfoCSVPath == null)
            {
                MessageBox.Show("Error please Select a File");
            }
            else
            {
                MessageBox.Show("Sucess!");
            }
        }

        //****************************************
        //Event Name: masterSaveLocation_Click
        //Description: This method will call the
        //getMasterSaveLocation() method from the
        //program Settings class. It will then 
        //check to see if the user has selected
        //a path.

        //****************************************
        //private void masterSaveLocation_Click(object sender, RoutedEventArgs e)
        //{
        //    //Run method
        //    programSettings.getMasterSaveLocation();

        //    //Run check
        //    if (programSettings.masterSaveLocationPath == null)
        //    {
        //        MessageBox.Show("Error please Select a File");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Sucess!");
        //    }
        //}

        //**********************************************
        //Event Name: LogoutBtn_Click
        //Description: This event will re-hide the 
        //stack panels containing the settings elements
        //it will then route the user to the start page.
        //***********************************************
        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            //Hide stack panels
            optionsLablesStkPnl.Visibility = Visibility.Hidden;
            optionsChoiceStkPnl.Visibility = Visibility.Hidden;

            //Navigate to start page
            startPage sp = new startPage();
            this.NavigationService.Navigate(sp);

        }

        //********************************************
        //Event Name: selectMasterTutorListBtn_Click
        //Description: This method will call the
        //getMatsterTutorList method from the
        //program Settings class. It will then 
        //check to see if the user has selected
        //a file.
        //****************************************
        private void selectMasterTutorListBtn_Click(object sender, RoutedEventArgs e)
        {
            //Call method
            programSettings.getMasterTutorList();

            //Run check
            if (programSettings.masterTutorListPath == null)
            {
                MessageBox.Show("Error please Select a File");
            }
            else
            {
                MessageBox.Show("Sucess!");
            }
        }

        //*******************************************
        //Event Name: editEmailSettingsBtn_Click
        //Description: Route the user to the email
        //settings page.
        //*******************************************
        private void editEmailSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            //Navigate to email settings page
            emailSettings es = new emailSettings();
            this.NavigationService.Navigate(es);
        }

        //********************************************
        //Event Name: selectStemCourseListBtn_Click
        //Description: This method will call the
        //getMasterStemCourseCheckList() method from the
        //program Settings class. It will then 
        //check to see if the user has selected
        //a file.
        //********************************************
        private void selectStemCourseListBtn_Click(object sender, RoutedEventArgs e)
        {
            //Call method
            programSettings.getMasterStemCourseCheckList();

            //Run check
            if (programSettings.masterCourseCheckListPath == null)
            {
                MessageBox.Show("Error please Select a File");
            }
            else
            {
                MessageBox.Show("Sucess!");
            }
        }

        //********************************************
        //Event Name: emailReportBtn_Click
        //Description: This method will call the
        //sendMasterReport() method from the
        //program Settings class. Checks are done 
        //within sendMasterReport()
        //********************************************
        private void emailReportBtn_Click(object sender, RoutedEventArgs e)
        {
            programSettings.sendMasterReport(); 
        }
    }



}