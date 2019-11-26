namespace HabrPostApi.Settings
{
    public interface IParserSettings
    {
        string BaseUrl { get; }
        string Url { get; }
        bool CanParse { get; }
    }
}