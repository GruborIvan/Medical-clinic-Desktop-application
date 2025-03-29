using Ordinacija.Features.Login.Models;
using Ordinacija.Features.Login.Repository;
using Ordinacija.Features.Patients;
using System.Windows;

namespace Ordinacija.Features.Login
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly ILoginRepository _loginRepository;
        private readonly PatientsView _patientsView;

        public LoginView(
            ILoginRepository loginRepository,
            PatientsView mainWindow)
        {
            InitializeComponent();
            _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
            _patientsView = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            bool isAuthenticated = await _loginRepository.AuthenticateUser(new LoginCredentials
            {
                Username = username,
                Password = password
            });

            if (isAuthenticated)
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
