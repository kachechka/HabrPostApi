namespace HabrPostApi.TryParsers
{
    public class ThousandTryParser : ITryParser<int>
    {
        public ThousandTryParser()
        { }

        public bool TryParse(string str, out int result)
        {
            if (str.EndsWith('k'))
            {
                var thousandStr = str.Remove(str.Length - 1);

                if (decimal.TryParse(thousandStr, out var coefficient))
                {
                    result = (int)(coefficient * 1_000);

                    return true;
                }
            }

            result = 0;

            return false;
        }
    }
}