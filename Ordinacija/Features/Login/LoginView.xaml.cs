using Ordinacija.Features.Patients;
using System.Windows;

namespace Ordinacija.Features.Login
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly PatientsView _patientsView;

        public LoginView(PatientsView mainWindow)
        {
            InitializeComponent();
            _patientsView = mainWindow;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (username == "" && password == "")
            {
                _patientsView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials, try again!", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
