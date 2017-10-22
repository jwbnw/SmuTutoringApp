using System.Windows;


namespace smuTutoringDataApp
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
            InitializeComponent();

            //Navigate to start page
            startPage sp = new startPage();
            mainFrame.NavigationService.Navigate(sp);

        }
       
    }
}
