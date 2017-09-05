using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ApiMgmSynchronizer.Service.Core;
using ApiMgmSynchronizer.Service.Interfaces;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Net;
using ApiMgmSynchronizer.Service.Dto;

namespace ApiMgmSynchronizer.Service.Clients
{
    public class ApiManagementClient : IApiManagementClient
    {
        private IHttpClientFactory _httpClientFactory;

        public ApiManagementClient(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }

        public async Task<AsociatedApi> GetAsociatedApi(ApiMetadataDTO metadata)
        {
            using(var httpClient = await this._httpClientFactory.Create(UrisTemplate.GetBaseApiManagementUri(metadata.TenantName)))
            {
                var httpResponse = await httpClient.GetAsync(UrisTemplate.GetAllApisUri(metadata.AzureApiVersion)).ConfigureAwait(false);
                httpResponse.EnsureSuccessStatusCode();
                
                var apis = JsonConvert.DeserializeObject<ApisRootObject>(
                        await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false)
                );

                var apiObj = apis.value.Where(api => api.name.ToLower() == metadata.ApiName.ToLower()).FirstOrDefault();

                return new AsociatedApi(apiObj.id);
            }                        
        }

        public async Task UpgradeSwaggerApi(string aid, ApiMetadataDTO metadata)
        {
            using(var httpClient = await this._httpClientFactory.Create(UrisTemplate.GetBaseApiManagementUri(metadata.TenantName)))
            {
                httpClient.DefaultRequestHeaders.Add("If-Match", "*");

                var requestParams = new Dictionary<string, string>()
                {
                    { "link", metadata.SwaggerUrl }
                };
                
                var content = new StringContent(JsonConvert.SerializeObject(requestParams), Encoding.UTF8, "application/vnd.swagger.link+json");                
                var httpResponse = await httpClient.PutAsync(UrisTemplate.GetSwaggerImportUri(aid, metadata.AzureApiVersion), content);                
                httpResponse.EnsureSuccessStatusCode();                
            }
        }

        public async Task<bool> GetPolicy(string aid, ApiMetadataDTO metadata)
        {
            using(var httpClient = await this._httpClientFactory.Create(UrisTemplate.GetBaseApiManagementUri(metadata.TenantName)))
            {
                var httpResponse = await httpClient.GetAsync(UrisTemplate.GetPolicyTemplate(aid, metadata.AzureApiVersion));
                
                try
                {
                    httpResponse.EnsureSuccessStatusCode();
                }
                catch(WebException)
                {
                    if(httpResponse.StatusCode == HttpStatusCode.NotFound)
                    {
                        return false;
                    }
                }
                
            }

            return true;
        }
    }
}
