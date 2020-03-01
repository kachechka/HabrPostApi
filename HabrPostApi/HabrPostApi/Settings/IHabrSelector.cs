namespace HabrPostApi.Settings
{
    public interface IHabrSelector
    {
        string Post { get; }
        string Title { get; }
        string Footer { get; }
        string Hub { get; }
        string Mark { get; }
        string Bookmark { get; }
        string View { get; }
        string Comment { get; }
    }
}