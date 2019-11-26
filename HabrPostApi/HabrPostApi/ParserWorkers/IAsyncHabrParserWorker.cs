using HabrPostApi.DataLoaders;
using HabrPostApi.Models;
using HabrPostApi.Parsers;
using HabrPostApi.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HabrPostApi.ParserWorkers
{
    public interface IAsyncHabrParserWorker
    {
        IPageParserSettings Settings { get; set; }
        IHabrParser Parser { get; set; }
        IAsyncHabrDataLoader DataLoader { get; set; }

        Task<List<HabrPost>> ParseAsync();
    }
}