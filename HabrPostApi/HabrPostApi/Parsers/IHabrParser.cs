﻿using AngleSharp.Html.Dom;
using HabrPostApi.Models;
using HabrPostApi.Settings;
using HabrPostApi.TryParsers.Collections;
using System.Collections.Generic;

namespace HabrPostApi.Parsers
{
    public interface IHabrParser : IParser<IHtmlDocument, List<HabrPost>>
    {
        IHabrSelector Selectors { get; }
        TryParserCollection<int> TryParsers { get; }
    }
}