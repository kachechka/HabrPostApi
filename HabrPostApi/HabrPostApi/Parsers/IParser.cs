namespace HabrPostApi.Parsers
{
    public interface IParser<T, TResult>
    {
        TResult Parse(T input);
    }
}