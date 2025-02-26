using System.Windows;

namespace Ordinacija.Features.Login
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly MainWindow _mainWindow;

        public LoginView(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (username == "" && password == "")
            {
                _mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials, try again!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
