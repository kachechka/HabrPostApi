using AngleSharp.Html.Dom;
using HabrPostApi.DataLoaders;
using HabrPostApi.Extensions;
using HabrPostApi.Models;
using HabrPostApi.Parsers;
using HabrPostApi.Settings;
using System;
using System.Collections.Generic;

namespace HabrPostApi.ParserWorkers
{
    public class HabrParserWorker : IHabrParserWorker
    {
        public IPageParserSettings Settings { get; set; }
        public IHabrParser Parser { get; set; }
        public IHabrDataLoader DataLoader { get; set; }

        public HabrParserWorker(
            IPageParserSettings settings,
            IHabrParser parser,
            IHabrDataLoader dataLoader)
        {
            Settings = settings;
            Parser = parser;
            DataLoader = dataLoader;
            Parser.InitializeTryParsers();
        }

        public List<HabrPost> Parse()
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
                document = DataLoader.LoadAsync(Settings.Url).GetAwaiter().GetResult();

                pagePosts = Parser.Parse(document);

                posts.AddRange(pagePosts);

                ++Settings.CurrentPageNumber;
            }

            return posts;
        }
    }
}