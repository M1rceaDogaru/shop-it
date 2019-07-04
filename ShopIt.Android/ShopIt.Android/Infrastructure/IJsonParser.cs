namespace ShopIt.Infrastructure
{
    interface IJsonParser
    {
        T ParseJson<T>(string value);
    }
}