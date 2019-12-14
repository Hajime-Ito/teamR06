using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Web;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
namespace cliant
{
    /// <summary>
    /// <typeparamref name="T"/>型の結果と、通信が成功したかどうかの情報を持つクラス
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Success<T>
    {
        public Success(T value, bool isSuccess)
        {
            Value = value;
            IsSuccess = isSuccess;
        }

        /// <summary>
        /// 結果の値
        /// </summary>
        public T Value { get; }
        /// <summary>
        /// 通信が成功したかどうか
        /// </summary>
        public bool IsSuccess { get; }
    }

    /// <summary>
    /// サーバーとのやり取りをサポートするクラス
    /// </summary>
    public static class ServerDataManager
    {
        /// <summary>
        /// Http通信のクライアント
        /// </summary>
        public static HttpClient HttpClient { get; private set; } = new HttpClient();

        /// <summary>
        /// ベースとなるURI
        /// </summary>
        public static Uri BaseAddress { get => HttpClient.BaseAddress; set { HttpClient.BaseAddress = value; } }

        /// <summary>
        /// Http送信関数。<typeparamref name="T"/>型の<paramref name="value"/>をJsonとして<paramref name="path"/>にPostし、
        /// 成功したかどうかを返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async static Task<bool> Post<T>(T value, string path)
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(path, value);
            return response.IsSuccessStatusCode;
        }
        public async static Task<Success<RetT>> Post<RetT, ArgT>(ArgT value, string path) where RetT : class
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(path, value);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                return new Success<RetT>(JsonConvert.DeserializeObject<RetT>(jsonString.Result), true);
            }

            return new Success<RetT>(null, false);
        }

        /// <summary>
        /// Http送信関数。<typeparamref name="T"/>型の<paramref name="value"/>をJsonとして<paramref name="path"/>にPutし、
        /// 成功したかどうかを返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async static Task<bool> Put<T>(T value, string path)
        {
            HttpResponseMessage response = await HttpClient.PutAsJsonAsync(path, value);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        ///  Http削除関数。<typeparamref name="ArgT"/>型の<paramref name="arg"/>をクエリとして<paramref name="path"/>に送信し、
        ///  目的のデータを削除します。また、成功したかどうかを返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arg"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async static Task<bool> Delete<T>(T arg, string path)
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync($"{path}?{MakeQuery(arg)}");
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Http受信関数。Json形式で表された<typeparamref name="T"/>型の結果を<paramref name="path"/>から取得します。
        /// 結果には通信が成功したかどうかの情報が含まれます。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public async static Task<Success<T>> Get<T>(string path) where T : class
        {
            HttpResponseMessage response = await HttpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                jsonString.Wait();
                return new Success<T>(JsonConvert.DeserializeObject<T>(jsonString.Result), true);
            }

            return new Success<T>(null, false);
        }

        /// <summary>
        /// Http受信関数。<typeparamref name="ArgT"/>型の<paramref name="arg"/>をクエリとして<paramref name="path"/>に送信し、
        /// Json形式で表された<typeparamref name="RetT"/>型の結果を取得します。
        /// 結果には通信が成功したかどうかの情報が含まれます。
        /// </summary>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="ArgT"></typeparam>
        /// <param name="arg"></param>
        /// <param name="value"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async static Task<Success<RetT>> Get<RetT, ArgT>(ArgT arg, string path) where RetT : class
        {
            HttpResponseMessage response = await HttpClient.GetAsync($"{path}?{MakeQuery(arg)}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return new Success<RetT>(JsonConvert.DeserializeObject<RetT>(jsonString), true);
            };

            return new Success<RetT>(null, false);
        }

        /// <summary>
        /// オブジェクトのプロパティーを取得し、httpクエリを生成します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MakeQuery<T>(T value)
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

            foreach (var prop in typeof(T).GetProperties())
            {
                queryString[prop.Name] = prop.GetValue(value).ToString();
            }

            return queryString.ToString();
        }
    }
}