using AngleSharp.Html.Dom;

namespace HabrPostApi.DataLoaders
{
    public interface IAsyncHabrDataLoader 
        : IAsyncDataLoader<string, IHtmlDocument>
    {
    }
}