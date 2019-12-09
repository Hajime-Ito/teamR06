using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace cliant
{
    static class ServerDataManager
    {
        public static HttpClient HttpClient { get; private set; } = new HttpClient();

        public static string Uri { get; set; } = "";
        
        public static bool Upload<T>(T value, string path)
        {
            //HttpClient.PostAsJsonAsync("api/products", value);

            return false;
        }
        
        public static bool Download<RetT>(out RetT value, string path) where RetT : class
        {
            value = null;
            return false;
        }

        public static bool Download<RetT,ArgT>(ArgT arg, out RetT value, string path) where RetT : class
        {
            value = null;
            return false;
        }
    }
}
