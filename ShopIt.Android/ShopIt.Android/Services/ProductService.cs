using ShopIt.Models;
using System.Threading.Tasks;
using System.Net;
using System;
using ShopIt.Infrastructure;

namespace ShopIt.Services
{
    public class AdvertService
    {
        private const string URL = "{0}/api/ad/{1}";

        private readonly IJsonParser _parser;
        private readonly Func<string> _getServerUrl;

        public AdvertService(Func<string> getServerUrl)
        {
            //TODO dependency injection
            _parser = new JsonParser();
            _getServerUrl = getServerUrl;
        }

        public async Task<Advert> FetchAdvertAsync(string id)
        {
            var server = _getServerUrl();
            var requestUrl = string.Format(URL, server, id);
            
            try
            { 
                // Create an HTTP web request using the URL:
                var request = (HttpWebRequest)WebRequest.Create(new Uri(requestUrl));
                request.ContentType = "application/json";
                request.Method = "GET";

                // Send the request to the server and wait for the response:
                using (WebResponse response = await request.GetResponseAsync())
                {
                    // Get a stream representation of the HTTP web response:
                    using (System.IO.Stream stream = response.GetResponseStream())
                    {
                        using (var reader = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                        {
                            string responseText = reader.ReadToEnd();
                            return _parser.ParseJson<Advert>(responseText);
                        }                    
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}