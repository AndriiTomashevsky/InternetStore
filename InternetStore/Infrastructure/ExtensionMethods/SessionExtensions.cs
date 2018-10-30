using InternetStore.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InternetStore.Infrastructure
{
    public static class SessionExtensions
    {
        public static void SetCartToSession(this ISession session, string key, Cart cart)
        {
            session.SetString(key, JsonConvert.SerializeObject(cart));
        }

        public static T GetCartFromSession<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            if (value == null)
            {
                return default(T);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
        }
    }
}
