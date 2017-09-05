using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApiMgmSynchronizer.Service.Clients;
using ApiMgmSynchronizer.Service.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiMgmSynchronizer.Service.Controllers
{
    [Route("api/[controller]")]
    public class ManagementController : Controller
    {
        [HttpPost]
        public void Post([FromBody]ApiMetadataDTO apiMetadata)
        {            
            if(ModelState.IsValid)
            {
                Console.WriteLine("API Management Synchronizer");
                Console.WriteLine("===========================");

                Console.WriteLine(string.Format("API Name: {0}", apiMetadata.ApiName));
                Console.WriteLine(string.Format("Azure Api Version: {0}", apiMetadata.AzureApiVersion));
                Console.WriteLine(string.Format("Swagger Url: {0}", apiMetadata.SwaggerUrl));
                Console.WriteLine(string.Format("Tenant Name: {0}", apiMetadata.TenantName));            

                // Apis
                var apiManagementImportClient = new ApiManagementClient();

                // Actions
                Console.Write("Getting Existing Apis...");
                var apiInfo = apiManagementImportClient.GetAsociatedApi(apiMetadata).Result;            

                if(apiInfo.ApiExists())
                {
                    // Upgrade Api
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("OK");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Yellow;                
                    Console.WriteLine(" - Api Found: {0}", apiInfo.ApiAid);              
                    Console.ResetColor();
                    
                    Console.Write("Deploying API: {0} to Management API...", apiInfo.ApiAid);
                    apiManagementImportClient.UpgradeSwaggerApi(apiInfo.ApiAid, apiMetadata).Wait();
                    Console.ForegroundColor = ConsoleColor.Green;                
                    Console.WriteLine("Sync");   
                    Console.ResetColor();

                    // Checking Policies
                    Console.Write("Getting API Policies...");
                    var policy = apiManagementImportClient.GetPolicy(apiInfo.ApiAid, apiMetadata).Result;

                    Console.ForegroundColor = ConsoleColor.Green;  
                    Console.Write("OK");
                    Console.ResetColor(); 

                    if(!policy)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue; 
                        Console.WriteLine("Policy does not exists for API.");
                        Console.ResetColor();                                        
                    }         
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;  
                        Console.WriteLine(" - Policy already set.");
                        Console.ResetColor();                    
                    }   
                }  
            }     
            else
            {
                Console.WriteLine("Incomming Request is invalid.");
            }       
        }
    }
}
