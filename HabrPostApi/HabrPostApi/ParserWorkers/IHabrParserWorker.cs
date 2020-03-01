using HabrPostApi.DataLoaders;
using HabrPostApi.Models;
using HabrPostApi.Parsers;
using HabrPostApi.Settings;
using System.Collections.Generic;

namespace HabrPostApi.ParserWorkers
{
    public interface IHabrParserWorker
    {
        IPageParserSettings Settings { get; set; }
        IHabrParser Parser { get; set; }
        IHabrDataLoader DataLoader { get; set; }

        List<HabrPost> Parse();
    }
}