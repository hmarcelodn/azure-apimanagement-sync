namespace ApiMgmSynchronizer.Service.Core
{
    public class UrisTemplate
    {
        private static string ApiManagementUriTemplate { get; } = "https://{0}.management.azure-api.net/";
        private static string ImportSwaggerUriTemplate { get; } = "{0}?api-version={1}&import=true";   
        private static string AllApisUriTemplate { get; } = "apis?api-version={0}";          

        private static string GetApiPolicyTemplate { get; } = "{0}/policy?api-version={1}&import=true";

        public static string GetBaseApiManagementUri(string tenantName)
        {
            return string.Format(ApiManagementUriTemplate, tenantName);
        }

        public static string GetSwaggerImportUri(string apiId, string apiVersion)
        {
            return string.Format(ImportSwaggerUriTemplate, apiId, apiVersion);
        }

        public static string GetAllApisUri(string apiVersion)
        {
            return string.Format(AllApisUriTemplate, apiVersion);
        }        

        public static string GetPolicyTemplate(string appId, string apiVersion)
        {
            return string.Format(GetApiPolicyTemplate, appId, apiVersion);
        }
    }
}