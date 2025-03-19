using Microsoft.Win32;
using Ordinacija.Features.MedicalReports.Models;
using Ordinacija.Features.Patients.Models;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.IO;

namespace Ordinacija.Features.MedicalReports.Service.Implementation
{
    public class PdfService
    {
        private readonly string _outputPath = Path.Combine(Path.GetTempPath(), "MedicalReport.pdf");
        private string _pageTitle;

        public string GenerateMedicalReport(MedicalReport medicalReport, Patient patient)
        {
            // Open SaveFileDialog to let the user choose save location
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files (*.pdf)|*.pdf",
                Title = "Save Medical Report",
                FileName = "MedicalReport.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                // Create a new PDF document
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Medical Report";

                // Create a new page
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont titleFont = new XFont("Arial", 20);
                XFont textFont = new XFont("Arial", 12);

                // Add content
                gfx.DrawString("Medical Report", titleFont, XBrushes.Black, new XPoint(50, 50));
                gfx.DrawString($"Doctor: {medicalReport.DoctorId}", textFont, XBrushes.Black, new XPoint(50, 100));
                gfx.DrawString($"Diagnosis: {medicalReport.DG}", textFont, XBrushes.Black, new XPoint(50, 130));
                gfx.DrawString($"Treatment: {medicalReport.TH}", textFont, XBrushes.Black, new XPoint(50, 160));
                gfx.DrawString($"Notes: {medicalReport.Description}", textFont, XBrushes.Black, new XPoint(50, 190));

                // Save the document
                document.Save(filePath);

                return filePath; // Return the path so it can be printed
            }

            return null; // Return null if user cancels
        }
        public void PrintMedicalReport(string filePath)
        {
            Process printProcess = new Process();
            printProcess.StartInfo = new ProcessStartInfo
            {
                FileName = filePath,
                Verb = "print",
                CreateNoWindow = true,
                UseShellExecute = true
            };
            printProcess.Start();
        }

    }
}
