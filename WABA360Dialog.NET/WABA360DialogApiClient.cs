using System.Net.Http;
using WABA360Dialog.ApiClient;

namespace WABA360Dialog
{
    public class WABA360DialogApiClient : WABA360DialogApiClientBase
    {
        private const string BasePath =  "https://waba.360dialog.io/";
        
        public WABA360DialogApiClient(string apiKey) : base(apiKey, BasePath, new HttpClient())
        {
        }
        
        public WABA360DialogApiClient(string apiKey, HttpClient httpClient) : base(apiKey, BasePath, httpClient)
        {
        }
    }
}