using Ordinacija.Features.Common;
using Ordinacija.Features.Patients;
using System.Windows;

namespace Ordinacija
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PageNavigationService _navigationService;
        private readonly Func<PatientsView> _patientsViewFactory; // Lazy loading

        public MainWindow(MainViewModel mainViewModel, PageNavigationService navigationService, Func<PatientsView> patientsViewFactory)
        {
            InitializeComponent();
            DataContext = mainViewModel;

            _navigationService = navigationService;
            _patientsViewFactory = patientsViewFactory;
        }

        private void PatientsButton_Click(object sender, RoutedEventArgs e)
        {
            var patientsView = _patientsViewFactory(); // Create a new instance when needed
            _navigationService.ShowWindow(patientsView);
        }
    }
}