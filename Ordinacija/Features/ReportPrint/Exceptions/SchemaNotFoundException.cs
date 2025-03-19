namespace Ordinacija.Features.ReportPrint.Exceptions
{
    public class SchemaNotFoundException : Exception
    {
        public SchemaNotFoundException(string schemaKey)
            : base($"Schema with key '{schemaKey}' was not found.") { }
    }
}
