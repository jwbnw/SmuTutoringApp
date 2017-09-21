using System;
using System.Windows;
using System.Windows.Controls;

namespace smuTutoringDataApp
{
    /// <summary>
    /// Interaction logic for emailSettings.xaml
    /// </summary>
    public partial class emailSettings : Page
    {
        public emailSettings()
        {
            InitializeComponent();
        }

        //******************************************
        //Event Name: saveEmailSettingsBtn_Click
        //Description: This method sets the email
        //settings that the admin enters into the
        //email settings fields. 
        //******************************************

        private void saveEmailSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            //Set info
            programSettings.senderEmail = senderEmailTxtBox.Text;
            programSettings.senderPassword = senderPasswordTxtBox.Text;
            programSettings.smptHostName = smptNameTxtBox.Text;
            programSettings.recieverEmail = recieverEmailTxtBox.Text;

            //try catch for numerical input for the port #
            try
            {
                programSettings.smptPortNumber = Convert.ToInt32(smptPortNumberTxtBpx.Text);
                settingsPage sp = new settingsPage();
                this.NavigationService.Navigate(sp);
            }
            catch
            {
                MessageBox.Show("Port number must be a number, no letters or characters. e.g) 578 ");
                emailSettings ep = new emailSettings();
                this.NavigationService.Navigate(ep);
            }
        }

        //*******************************************
        //Event Name: backBtn_Click
        //Description: This event routes the user
        //back to the settings page.
        //********************************************
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            settingsPage sp = new settingsPage();
            this.NavigationService.Navigate(sp);
        }
    }
}
