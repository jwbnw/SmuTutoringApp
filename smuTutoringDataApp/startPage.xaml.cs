using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace smuTutoringDataApp
{
    /// <summary>
    /// Interaction logic for startPage.xaml
    /// </summary>
    public partial class startPage : Page
    {
        public startPage()
        {
            InitializeComponent();
      
            //set auto save and backup time
            var dailyTimer = "19:00:00";
    
            //Seperate daily timer
            var timerParts = dailyTimer.Split(new char[1] { ':' });
            

            //get date time now and parse using timerParts into date
            var dateNow = DateTime.Now;

            var date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, int.Parse(timerParts[0]), int.Parse(timerParts[1]), int.Parse(timerParts[2]));
            //Declare new Timespan obj
            TimeSpan ts;
     
            //Set first tasktimespan depending on current time
            if (date > dateNow)
            {
                ts = date - dateNow;
            }
            else
            {
                date = date.AddDays(1);
                ts = date - dateNow;
            }

            //Run task on time met
            Task.Delay(ts).ContinueWith(x => dataTables.checkForOutEntery());
            
            // SomeMethod(); write backuptable as well, put in someMethod() then call both janajorial funcitons

            // check to load masterTable in event of new start-up/crash.
            if (dataTables.masterDataTable == null)
            {
                //Give usrs option to select folder where report is
                programSettings.loadMainSettings();

                //set master table with existing table. If none exists masterDataTable wil remain null
                dataTables.setMasterTable(programSettings.masterSaveLocationPath + "\\FinalDataTable.csv");

                // Second check, If no table is found then create base table. 
                if (dataTables.masterDataTable == null)
                {
                    dataTables.CreateTable();
                }
            }
       
        }
      
        //****************************************
        //Event Name: signInBtn_Click
        //Description: Route user to sign-in page.
        //****************************************
        private void signInBtn_Click(object sender, RoutedEventArgs e)
        {
            signIn si = new signIn();
            this.NavigationService.Navigate(si);
        }

        //*********************************************
        //Event Name: SignOutBtn_Click
        //Description: Route the user to sign-out page.
        //*********************************************
        private void signOutBtn_Click(object sender, RoutedEventArgs e)
        {
            signOut so = new signOut();
            this.NavigationService.Navigate(so);
        }

        //*************************************************
        //Event Name: settingBtn_Click
        //Description: Route the user to the settings page.
        //*************************************************
        private void settingBtn_Click(object sender, RoutedEventArgs e)
        {
            settingsPage sp = new settingsPage();
            this.NavigationService.Navigate(sp);
        }
    }
}
