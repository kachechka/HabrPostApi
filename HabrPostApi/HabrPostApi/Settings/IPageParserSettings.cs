namespace HabrPostApi.Settings
{
    public interface IPageParserSettings : IParserSettings
    {
        int StartPageNumber { get; set; }
        int CurrentPageNumber { get; set; }
        int EndPageNumber { get; set; }
    }
}