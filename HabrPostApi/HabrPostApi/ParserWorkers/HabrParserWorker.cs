using AngleSharp.Html.Dom;
using HabrPostApi.DataLoaders;
using HabrPostApi.Models;
using HabrPostApi.Parsers;
using HabrPostApi.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HabrPostApi.ParserWorkers
{
    public class HabrParserWorker : IAsyncHabrParserWorker
    {
        public IPageParserSettings Settings { get; set; }
        public IHabrParser Parser { get; set; }
        public IAsyncHabrDataLoader DataLoader { get; set; }

        public HabrParserWorker(
            IPageParserSettings settings,
            IHabrParser parser,
            IAsyncHabrDataLoader dataLoader)
        {
            Settings = settings;
            Parser = parser;
            DataLoader = dataLoader;
        }

        public async Task<List<HabrPost>> ParseAsync()
        {
            if (Settings is null)
            {
                throw new ArgumentNullException(nameof(Settings));
            }
            if (Parser is null)
            {
                throw new ArgumentNullException(nameof(Parser));
            }

            var posts = new List<HabrPost>();

            IHtmlDocument document;
            List<HabrPost> pagePosts;

            while (Settings.CanParse)
            {
                document = await DataLoader.LoadAsync(Settings.Url);

                pagePosts = Parser.Parse(document);

                posts.AddRange(pagePosts);

                ++Settings.CurrentPageNumber;
            }

            return posts;
        }
    }
}