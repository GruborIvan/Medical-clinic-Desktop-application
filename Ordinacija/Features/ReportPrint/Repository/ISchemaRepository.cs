namespace Ordinacija.Features.ReportPrint.Repository
{
    public interface ISchemaRepository
    {
        Task<string> GetTemplateSchemaByKey(string schemaKey);
    }
}
