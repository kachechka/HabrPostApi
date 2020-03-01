using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.Net.Http;
using System.Threading.Tasks;

namespace HabrPostApi.DataLoaders
{
    public class HtmlDataLoader : IHabrDataLoader
    {
        private readonly HttpClient _client;
        private readonly HtmlParser _parser;

        public HtmlDataLoader()
        {
            _client = new HttpClient();
            _parser = new HtmlParser();
        }

        public async Task<IHtmlDocument> LoadAsync(string url)
        {
            using (var page = await _client.GetAsync(url))
            {
                page.EnsureSuccessStatusCode();

                using (var stream = await page.Content.ReadAsStreamAsync())
                {
                    return await _parser.ParseDocumentAsync(stream);
                }
            }
        }

        public void Dispose() => _client.Dispose();
    }
}