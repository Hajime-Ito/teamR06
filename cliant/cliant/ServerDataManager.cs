using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
    static class ServerDataManager
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
        /// Http送信関数。<typeparamref name="T"/>型の<paramref name="value"/>をJsonとして<paramref name="path"/>に送信し、
        /// 成功したかどうかを返します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async static Task<bool> Upload<T>(T value, string path)
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(path, value);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Http受信関数。Json形式の<typeparamref name="T"/>型の結果を<paramref name="path"/>から取得します。
        /// 結果には通信が成功したかどうかの情報が含まれます。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public async static Task<Success<T>> Download<T>(string path) where T : class
        {
            HttpResponseMessage response = await HttpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return new Success<T>(await response.Content.ReadAsAsync<T>(), true);
            }

            return new Success<T>(null, false);
        }

        /// <summary>
        /// Http受信関数。<typeparamref name="ArgT"/>型の<paramref name="arg"/>をJsonとして<paramref name="path"/>に送信し、
        /// Json形式の<typeparamref name="RetT"/>型の結果を取得します。
        /// 結果には通信が成功したかどうかの情報が含まれます。
        /// </summary>
        /// <typeparam name="RetT"></typeparam>
        /// <typeparam name="ArgT"></typeparam>
        /// <param name="arg"></param>
        /// <param name="value"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async static Task<Success<RetT>> Download<RetT,ArgT>(ArgT arg, string path) where RetT : class
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(path, arg);
            if (response.IsSuccessStatusCode)
            {
                return new Success<RetT>(await response.Content.ReadAsAsync<RetT>(), true);
            }

            return new Success<RetT>(null, false);
        }
    }
}
