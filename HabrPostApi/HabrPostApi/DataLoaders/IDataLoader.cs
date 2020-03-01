using System;
using System.Threading.Tasks;

namespace HabrPostApi.DataLoaders
{
    public interface IDataLoader<TInput, TResult> : IDisposable
    {
        Task<TResult> LoadAsync(TInput input);
    }
}