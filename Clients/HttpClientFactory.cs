using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ApiMgmSynchronizer.Service.Interfaces;

namespace ApiMgmSynchronizer.Service.Clients
{
    public class HttpClientFactory: IHttpClientFactory
    {
        public async Task<HttpClient> Create(string uri)
        {
            var apiIdentifier = "integration";
            var primaryKey = "Fr8z3AUwKQ44q5m1CqmMCGE8R8jynJAvUPkpeVq5e21AaemJ+nk7gAMQmnnf3GT+n5sk+WWLmVzFvdE1XdTvhg==";
            var sharedSignature = await this.GenerateSharedSignature(apiIdentifier, primaryKey, DateTime.UtcNow.AddDays(1));                        
            var httpClient = new HttpClient() { BaseAddress = new Uri(uri) };
            httpClient.DefaultRequestHeaders.Add("Authorization", sharedSignature);

            return httpClient;
        }

        protected Task<string> GenerateSharedSignature(string id, string key, DateTime expiry)
        {
            using (var encoder = new HMACSHA512(Encoding.UTF8.GetBytes(key)))   
            {   
                var dataToSign = id + "\n" + expiry.ToString("O", CultureInfo.InvariantCulture);   
                var hash = encoder.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));   
                var signature = Convert.ToBase64String(hash);   
                var encodedToken = string.Format("SharedAccessSignature uid={0}&ex={1:o}&sn={2}", id, expiry, signature);

                return Task.FromResult(encodedToken);
            }   
        }
    }
}
