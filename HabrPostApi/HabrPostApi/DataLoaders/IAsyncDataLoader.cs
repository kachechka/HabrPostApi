using System;
using System.Threading.Tasks;

namespace HabrPostApi.DataLoaders
{
    public interface IAsyncDataLoader<TInput, TResult> : IDisposable
    {
        Task<TResult> LoadAsync(TInput input);
    }
}