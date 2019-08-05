using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace kSoft.Core.Contracts.Repository
{
    public interface IKSoftRepository
    {
        Task<T> GetAsync<T>(string uri, string authToken = null);
        Task<T> PostAsync<T>(string uri, T data, string authToken = null);
        Task<T> PutAsync<T>(string uri, T data, string authToken = null);
        Task DeleteAsync(string uri, string authToken = null);
        Task<R> PostAsync<T, R>(string uri, T data, string authToken = null);
    }
}
