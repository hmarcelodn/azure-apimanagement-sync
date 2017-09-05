using System.Net.Http;
using System.Threading.Tasks;

namespace ApiMgmSynchronizer.Service.Interfaces
{
    public interface IHttpClientFactory
    {
        Task<HttpClient> Create(string uri);
    }
}