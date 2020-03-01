using AngleSharp.Html.Dom;

namespace HabrPostApi.DataLoaders
{
    public interface IHabrDataLoader 
        : IDataLoader<string, IHtmlDocument>
    {
    }
}