using System.Collections;
using System.Collections.Generic;

namespace HabrPostApi.TryParsers.Collections
{
    public class TryParserCollection<T> : IEnumerable<ITryParser<T>>
    {
        private readonly List<ITryParser<T>> _tryParsers;

        public TryParserCollection()
            => _tryParsers = new List<ITryParser<T>>();

        public void Add(ITryParser<T> tryParser)
            => _tryParsers.Add(tryParser);

        public IEnumerator<ITryParser<T>> GetEnumerator()
            => _tryParsers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}