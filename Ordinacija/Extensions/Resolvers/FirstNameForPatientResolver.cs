using AutoMapper;

namespace Ordinacija.Extensions.Resolvers
{
    public class FirstNameResolver<TSource> : IValueResolver<TSource, object, string>
    where TSource : class
    {
        public string Resolve(TSource source, object destination, string member, ResolutionContext context)
        {
            var name = typeof(TSource).GetProperty("AcName2")?.GetValue(source) as string;
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            var parts = name.Split(' ');
            return parts.First(); // First word as first name
        }
    }

    public class LastNameResolver<TSource> : IValueResolver<TSource, object, string>
        where TSource : class
    {
        public string Resolve(TSource source, object destination, string member, ResolutionContext context)
        {
            var name = typeof(TSource).GetProperty("AcName2")?.GetValue(source) as string;
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            var parts = name.Split(' ');
            return parts.Length > 1 ? string.Join(" ", parts.Skip(1)) : string.Empty; // All remaining words as last name
        }
    }
}
