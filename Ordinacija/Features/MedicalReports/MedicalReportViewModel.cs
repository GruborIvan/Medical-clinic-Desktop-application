using CommunityToolkit.Mvvm.Input;
using Ordinacija.Features.Common;
using Ordinacija.Features.MedicalReports.Models;
using Ordinacija.Features.MedicalReports.Service;
using Ordinacija.Features.Patients.Models;
using Ordinacija.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;
using RelayCommand = Ordinacija.Helpers.RelayCommand;

namespace Ordinacija.Features.MedicalReports
{
    public class MedicalReportViewModel : BaseViewModel
    {
        private readonly IMedicalReportService _medicalReportService;

        private Patient _patient;
        public Patient Patient
        {
            get => _patient;
            set => SetProperty(ref _patient, value);
        }

        private int _currentIndex;

        public ObservableCollection<MedicalReport> MedicalReports { get; } = new();

        public ICommand LoadMedicalReportsCommand { get; }
        public IAsyncRelayCommand NextReportCommand { get; }
        public IAsyncRelayCommand PreviousReportCommand { get; }

        private MedicalReport? _currentMedicalReport;
        public MedicalReport? CurrentMedicalReport
        {
            get => _currentMedicalReport;
            private set => SetProperty(ref _currentMedicalReport, value);
        }

        public int CurrentReportPage
        {
            get => _currentIndex + 1;
            private set
            {
                _currentIndex = value - 1;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalReports));
                OnPropertyChanged(nameof(CanMoveNext));
                OnPropertyChanged(nameof(CanMovePrevious));
            }
        }
        public int TotalReports => MedicalReports.Count;

        public MedicalReportViewModel(Patient patient, IMedicalReportService medicalReportService)
        {
            Patient = patient ?? throw new ArgumentNullException(nameof(patient));
            _patient = patient; // Sufficient, but for warning.

            _medicalReportService = medicalReportService ?? throw new ArgumentNullException(nameof(medicalReportService));

            LoadMedicalReportsCommand = new RelayCommand(LoadMedicalReports);
            NextReportCommand = new AsyncRelayCommand(NextReport, CanMoveNext);
            PreviousReportCommand = new AsyncRelayCommand(PreviousReport, CanMovePrevious);

            _ = LoadMedicalReports();
        }

        private async Task LoadMedicalReports()
        {
            OnPropertyChanged(nameof(Patient));
            var reports = await _medicalReportService.GetMedicalReportsForPatient(Patient.AcSubject);

            MedicalReports.Clear();
            foreach (var report in reports)
                MedicalReports.Add(report);

            _currentIndex = 0;
            CurrentMedicalReport = MedicalReports.FirstOrDefault();

            // Manually trigger updates for report data
            OnPropertyChanged(nameof(MedicalReports));
            OnPropertyChanged(nameof(CurrentReportPage));
            OnPropertyChanged(nameof(TotalReports));
            OnPropertyChanged(nameof(CanMoveNext));
            OnPropertyChanged(nameof(CanMovePrevious));

            NextReportCommand.NotifyCanExecuteChanged();
            PreviousReportCommand.NotifyCanExecuteChanged();
        }

        private async Task NextReport()
        {
            if (!CanMoveNext()) return;

            _currentIndex++;
            CurrentMedicalReport = MedicalReports[_currentIndex];

            RefreshCommandStates();
            await Task.CompletedTask; // Ensure it returns a Task
        }

        private async Task PreviousReport()
        {
            if (!CanMovePrevious()) return;

            _currentIndex--;
            CurrentMedicalReport = MedicalReports[_currentIndex];

            RefreshCommandStates();
            await Task.CompletedTask; // Ensure it returns a Task
        }

        private void RefreshCommandStates()
        {
            OnPropertyChanged(nameof(MedicalReports));
            OnPropertyChanged(nameof(CurrentReportPage));
            OnPropertyChanged(nameof(CanMoveNext));
            OnPropertyChanged(nameof(CanMovePrevious));

            NextReportCommand.NotifyCanExecuteChanged();
            PreviousReportCommand.NotifyCanExecuteChanged();

            CommandManager.InvalidateRequerySuggested();  // Forces WPF to reevaluate button state
        }

        private bool CanMoveNext() => _currentIndex < MedicalReports.Count - 1;
        private bool CanMovePrevious() => _currentIndex > 0;
    }
}
