using System.Threading.Tasks;
using ApiMgmSynchronizer.Service.Core;
using ApiMgmSynchronizer.Service.Dto;

namespace ApiMgmSynchronizer.Service.Interfaces
{
    public interface IApiManagementClient
    {
        Task<AsociatedApi> GetAsociatedApi(ApiMetadataDTO metadata);

        Task UpgradeSwaggerApi(string aid, ApiMetadataDTO metadata);

        Task<bool> GetPolicy(string aid, ApiMetadataDTO metadata);
    }
}
