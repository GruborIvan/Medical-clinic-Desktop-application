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

        public MedicalReportsView(Patient patient, IMedicalReportService medicalReportService)
        {
            InitializeComponent();
            _patient = patient ?? throw new ArgumentNullException(nameof(patient));
            _medicalReportService = medicalReportService ?? throw new ArgumentNullException(nameof(medicalReportService));

            DataContext = new MedicalReportViewModel(_patient, _medicalReportService);
        }
    }
}
