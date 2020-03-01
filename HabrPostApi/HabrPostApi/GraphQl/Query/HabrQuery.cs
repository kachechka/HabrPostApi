using GraphQL.Types;
using HabrPostApi.GraphQl.Types;
using HabrPostApi.Models;
using HabrPostApi.ParserWorkers;
using System;
using System.Collections.Generic;

namespace HabrPostApi.GraphQl.Query
{
    public class HabrQuery : ObjectGraphType
    {
        private static readonly string _posts;
        private static readonly string _start;
        private static readonly string _end;
        private static readonly string _page;

        static HabrQuery()
        {
            _posts = "posts";
            _start = "start";
            _end = "end";
            _page = "page";
        }

        private readonly IHabrParserWorker _worker;

        public HabrQuery(IHabrParserWorker worker)
        {
            _worker = worker
                ?? throw new ArgumentNullException(nameof(worker));

            Name = "query";

            InitalizeFields();
        }

        private void InitalizeFields()
        {
            Field<ListGraphType<HabrPostType>>(
                            _posts,
                            arguments: new QueryArguments
                            {
                    new QueryArgument<IntGraphType> { Name = _start },
                    new QueryArgument<IntGraphType> { Name = _end },
                    new QueryArgument<IntGraphType> { Name = _page }
                            },
                            resolve: PostsResolve);
        }

        private List<HabrPost> PostsResolve(
            ResolveFieldContext<object> context)
        {
            var page = context.GetArgument<int?>(_page);

            if (page.HasValue)
            {
                return GetPosts(page.Value, page.Value);
            }

            var start = context.GetArgument<int?>(_start);
            var end = context.GetArgument<int?>(_end);

            if (end is null)
            {
                return null;
            }

            return GetPosts(start ?? 1, end.Value);
        }

        private List<HabrPost> GetPosts(int start, int end)
        {
            var settings = _worker.Settings;

            settings.StartPageNumber = start;
            settings.EndPageNumber = end;

            return _worker.Parse();
        }
    }
}
