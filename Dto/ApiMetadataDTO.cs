using System.ComponentModel.DataAnnotations;

namespace ApiMgmSynchronizer.Service.Dto
{
    public class ApiMetadataDTO
    {
        [Required]
        public string TenantName { get; set; }

        [Required]
        public string AzureApiVersion { get; set; }

        [Required]
        public string ApiName { get; set; }

        [Required]
        public string SwaggerUrl { get; set; }
    }
}
