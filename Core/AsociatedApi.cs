namespace ApiMgmSynchronizer.Service.Core
{
    public class AsociatedApi
    {
        private string _apiAid;

        public AsociatedApi(string apiAid)
        {
            this._apiAid = apiAid;
        }

        public bool ApiExists()
        {
            return !(string.IsNullOrEmpty(this._apiAid));
        }

        public string ApiAid
        {
            get{ return this._apiAid; }
        }
    }
}