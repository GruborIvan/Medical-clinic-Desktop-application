using Ordinacija.Features.ReportPrint.Repository;
using System.Windows;

namespace Ordinacija.Features.ReportPrint
{
    /// <summary>
    /// Interaction logic for AddNewUZView.xaml
    /// </summary>
    public partial class AddNewUZView : Window
    {
        public string UZ_Results { get; set; }

        private readonly ISchemaRepository _schemaRepository;

        public AddNewUZView(ISchemaRepository schemaRepository)
        {
            InitializeComponent();

            _schemaRepository = schemaRepository;

            UZ_Results = _schemaRepository.GetTemplateSchemaByKey("UZ ABDOMENA I BUBREGA").Result;
            DataContext = this;
        }
    }
}
