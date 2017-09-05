using System.Collections.Generic;
using Newtonsoft.Json;
namespace ApiMgmSynchronizer.Service.Dto
{
    public class Value
    {
        public string id { get; set; }
        public string name { get; set; }
        public string apiRevision { get; set; }
        public object description { get; set; }
        public string serviceUrl { get; set; }
        public string path { get; set; }
        public List<string> protocols { get; set; }
        public object authenticationSettings { get; set; }
        public object subscriptionKeyParameterNames { get; set; }
        public bool? isCurrent { get; set; }
    }

    public class ApisRootObject
    {
        public List<Value> value { get; set; }
        public int count { get; set; }
        public object nextLink { get; set; }
    }
}
