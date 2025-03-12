using Ordinacija.Features.MedicalReports.Service;
using System.Windows;

namespace Ordinacija.Features.MedicalReports
{
    /// <summary>
    /// Interaction logic for AddNewMedicalReportView.xaml
    /// </summary>
    public partial class AddNewMedicalReportView : Window
    {
        private readonly IMedicalReportService _medicalReportService;

        public AddNewMedicalReportView(IMedicalReportService medicalReportService)
        {
            InitializeComponent();

            _medicalReportService = medicalReportService;
        }

        private async void ConfirmCreateMedicalReportButton_Click(object sender, RoutedEventArgs e)
        {
            await _medicalReportService.InsertMedicalReport(null);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
