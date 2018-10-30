using Microsoft.AspNetCore.Http;

namespace InternetStore.Infrastructure.ExtensionMethods
{
    public static class UrlExtensions
    {
        public static string Path(this HttpRequest httpRequest)
        {
            return httpRequest.QueryString.HasValue ? $"{httpRequest.Path}{httpRequest.QueryString}" : httpRequest.Path.ToString();
        }
    }
}
