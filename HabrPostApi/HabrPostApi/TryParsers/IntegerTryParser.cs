namespace HabrPostApi.TryParsers
{
    public class IntegerTryParser : ITryParser<int>
    {
        public IntegerTryParser()
        { }

        public bool TryParse(string str, out int result)
            => int.TryParse(str.Replace('–', '-'), out result);
    }
}