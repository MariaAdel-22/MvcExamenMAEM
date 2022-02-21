using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcExamenMAEM.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession sesion, string key, object value)
        {

            string data = JsonConvert.SerializeObject(value);
            sesion.SetString(key, data);
        }

        public static T GetObject<T>(this ISession sesion, string key)
        {

            string data = sesion.GetString(key);

            if (data == null)
            {

                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
