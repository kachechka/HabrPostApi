using HabrPostApi.Parsers;
using HabrPostApi.TryParsers;

namespace HabrPostApi.Extensions
{
    internal static class HabrParserExtensions
    {
        internal static void InitializeTryParsers(this IHabrParser self)
        {
            self.TryParsers.Add(new IntegerTryParser());
            self.TryParsers.Add(new ThousandTryParser());
        }
    }
}