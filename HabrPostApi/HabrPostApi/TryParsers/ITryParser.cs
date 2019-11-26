namespace HabrPostApi.TryParsers
{
    public interface ITryParser<T>
    {
        bool TryParse(string str, out T result);
    }
}