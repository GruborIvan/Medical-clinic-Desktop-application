using Ordinacija.Features.Doctors.Models;
using Ordinacija.Features.Doctors.Service;
using Ordinacija.Features.Patients.Service;
using System.Windows;
using System.Windows.Controls;

namespace Ordinacija.Features.Doctors
{
    /// <summary>
    /// Interaction logic for DoctorsView.xaml
    /// </summary>
    public partial class DoctorsView : Window
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public DoctorViewModel DoctorViewModel;


        public DoctorsView(
            IDoctorService doctorService,
            IPatientService patientService)
        {
            InitializeComponent();

            _doctorService = doctorService;
            _patientService = patientService;

            DoctorViewModel = new DoctorViewModel(doctorService);
            DataContext = DoctorViewModel;
        }

        private void AddDoctor_Click(object sender, RoutedEventArgs e)
        {
            var addNewDoctorView = new AddNewDoctorView( _doctorService, this);
            addNewDoctorView.Show();
        }

        private void EditDoctorButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Doctor selectedDoctor)
            {
                var editDoctorView = new AddNewDoctorView(_doctorService, this, selectedDoctor);
                editDoctorView.ShowDialog();
            }
        }
    }
}
