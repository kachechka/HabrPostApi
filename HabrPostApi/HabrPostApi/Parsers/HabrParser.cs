using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using HabrPostApi.Models;
using HabrPostApi.TryParsers.Collections;
using System;
using System.Collections.Generic;

namespace HabrPostApi.Parsers
{
    public class HabrParser : IHabrParser
    {
        private readonly Dictionary<string, string> _selectors;

        public TryParserCollection<int> TryParsers { get; private set; }

        public HabrParser()
        {
            _selectors = new Dictionary<string, string>
            {
                { "Post", ".post" },
                { "Title", ".post__title_link" },
                { "Footer", ".post__footer" },
                { "Hub", ".hub-link" },
                { "Mark", ".voting-wjt__counter" },
                { "Bookmark", ".bookmark__counter" },
                { "View", ".post-stats__views-count" },
                { "Comment", ".post-stats__comments-count" },
            };

            TryParsers = new TryParserCollection<int>();
        }

        public List<HabrPost> Parse(IHtmlDocument input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var postElements = input.QuerySelectorAll(_selectors["Post"]);

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
            var title = postElement.QuerySelector(_selectors["Title"]);
            var footer = postElement.QuerySelector(_selectors["Footer"]);

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
            var hubElements = post.QuerySelectorAll(_selectors["Hub"]);

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
            var mark = ParseNumber(footer, _selectors["Mark"]);
            var countBookmarks = ParseNumber(footer, _selectors["Bookmark"]);
            var countViews = ParseNumber(footer, _selectors["View"]);
            var countComments = ParseNumber(footer, _selectors["Comment"]);

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