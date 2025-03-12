using Ordinacija.Features.MedicalReports.Service;
using Ordinacija.Features.Patients.Models;
using System.Windows;

namespace Ordinacija.Features.MedicalReports
{
    /// <summary>
    /// Interaction logic for MedicalReportsView.xaml
    /// </summary>
    public partial class MedicalReportsView : Window
    {
        private readonly Patient _patient;
        private readonly IMedicalReportService _medicalReportService;

        public MedicalReportsView(
            IMedicalReportService medicalReportService,
            Patient? patient = null)
        {
            InitializeComponent();

            _patient = patient;
            _medicalReportService = medicalReportService ?? throw new ArgumentNullException(nameof(medicalReportService));

            DataContext = new MedicalReportViewModel(_patient, _medicalReportService);
        }

        private void AddNewMedicalReportButton_Click(object sender, RoutedEventArgs e)
        {
            var addNewMedicalReportView = new AddNewMedicalReportView(_medicalReportService);
            addNewMedicalReportView.ShowDialog();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
