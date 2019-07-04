using System;
using Newtonsoft.Json;

namespace ShopIt.Infrastructure
{
    class JsonParser : IJsonParser
    {
        public T ParseJson<T>(string value)
        {
            try
            {
                var settings = new JsonSerializerSettings();
                settings.MissingMemberHandling = MissingMemberHandling.Ignore;

                return JsonConvert.DeserializeObject<T>(value, settings);
            }
            catch (Exception ex)
            {
                //TODO: log error
                return default(T);
            }
        }
    }
}