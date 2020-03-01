using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using HabrPostApi.Models;
using HabrPostApi.Settings;
using HabrPostApi.TryParsers.Collections;
using System;
using System.Collections.Generic;

namespace HabrPostApi.Parsers
{
    public class HabrParser : IHabrParser
    {
        public IHabrSelector Selectors { get; }
        public TryParserCollection<int> TryParsers { get; private set; }

        public HabrParser(IHabrSelector selectors)
        {
            TryParsers = new TryParserCollection<int>();
            Selectors = selectors;
        }

        public List<HabrPost> Parse(IHtmlDocument input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var postElements = input.QuerySelectorAll(Selectors.Post);

            if (postElements.Length == 0)
            {
                return new List<HabrPost>();
            }

            var posts = new List<HabrPost>(postElements.Length);

            foreach (var postElement in postElements)
            {
                posts.Add(ParsePost(postElement));
            }

            return posts;
        }

        private HabrPost ParsePost(IElement postElement)
        {
            var title = postElement.QuerySelector(Selectors.Title);
            var footer = postElement.QuerySelector(Selectors.Footer);

            return new HabrPost
            {
                Title = title.InnerHtml,
                Link = title.GetAttribute("href"),
                Hubs = ParseHubs(postElement),
                Details = ParsePostDetails(footer)
            };
        }

        private List<HabrHub> ParseHubs(IElement post)
        {
            var hubElements = post.QuerySelectorAll(Selectors.Hub);

            if (hubElements.Length == 0)
            {
                return new List<HabrHub>();
            }

            var hubs = new List<HabrHub>(hubElements.Length);

            foreach (var hubElement in hubElements)
            {
                hubs.Add(new HabrHub
                {
                    Link = hubElement.GetAttribute("href"),
                    Name = hubElement.InnerHtml
                });
            }

            return hubs;
        }

        private HabrPostDetails ParsePostDetails(IElement footer)
        {
            var mark = ParseNumber(footer, Selectors.Mark);
            var countBookmarks = ParseNumber(footer, Selectors.Bookmark);
            var countViews = ParseNumber(footer, Selectors.View);
            var countComments = ParseNumber(footer, Selectors.Comment);

            return new HabrPostDetails
            {
                Mark = mark,
                CountBookmarks = countBookmarks,
                CountViews = countViews,
                CountComments = countComments
            };
        }

        private int ParseNumber(IElement footer, string selector)
        {
            var html = footer.QuerySelector(selector)?.InnerHtml;

            var result = 0;

            if (!string.IsNullOrWhiteSpace(html))
            {
                foreach (var tryParser in TryParsers)
                {
                    if (tryParser.TryParse(html, out result))
                    {
                        break;
                    }
                }
            }

            return result;
        }
    }
}