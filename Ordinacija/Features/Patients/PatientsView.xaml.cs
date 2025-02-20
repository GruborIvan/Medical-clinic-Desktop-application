using System.Windows;

namespace Ordinacija.Features.Patients
{
    /// <summary>
    /// Interaction logic for PatientsView.xaml
    /// </summary>
    public partial class PatientsView : Window
    {
        private readonly PatientViewModel _patientViewModel;

        public PatientsView(PatientViewModel patientViewModel)
        {
            InitializeComponent();
            _patientViewModel = patientViewModel;
            DataContext = _patientViewModel; // Set the DataContext here
        }
    }
}
