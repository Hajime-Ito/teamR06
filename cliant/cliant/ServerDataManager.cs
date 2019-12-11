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
        enum API
        {
            User,
            NewDecoration,
            NewTreePotOwner,
            GrowPotSpot,
            GetUid,
            GetPid,
            TreePot,
            HotSpot,
            Tree,
            Decoration,
            Party
        }

        static Dictionary<API, string> pathOfAPI = new Dictionary<API, string>()
        {
            {API.User, "" },
            {API.NewDecoration, "" },
            {API.NewTreePotOwner, "" },
            {API.GrowPotSpot, "" },
            {API.GetUid, "" },
            {API.GetPid, "" },
            {API.TreePot, "" },
            {API.HotSpot, "" },
            {API.Tree, "" },
            {API.Decoration, "" },
            {API.Party, "" }

        };


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
        public async static Task<Success<RetT>> Download<RetT, ArgT>(ArgT arg, string path) where RetT : class
        {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(path, arg);
            if (response.IsSuccessStatusCode)
            {
                return new Success<RetT>(await response.Content.ReadAsAsync<RetT>(), true);
            }

            return new Success<RetT>(null, false);
        }

        public async static Task<bool> UploadUser(double locationX, double locationY, string pid)
        {
            ServerData.UserArg user = new ServerData.UserArg()
            {
                locationX = locationX,
                locationY = locationY,
                pid = pid
            };

            return await Upload(user, pathOfAPI[API.User]);
        }

        public async static Task<bool> UploadNewDecoration(string TreeKey, int kind, int posX, int posY, string message)
        {
            ServerData.NewDecorationArg newDecoration = new ServerData.NewDecorationArg()
            {
                TreeKey = TreeKey,
                kind = kind,
                posX = posX,
                posY = posY,
                message = message
            };

            return await Upload(newDecoration, pathOfAPI[API.NewDecoration]);
        }

        public async static Task<bool> UploadNewTreePotOwner(string pid)
        {
            ServerData.NewTreePotOwnerArg newTreePotOwner = new ServerData.NewTreePotOwnerArg()
            {
                pid = pid
            };

            return await Upload(newTreePotOwner, pathOfAPI[API.NewTreePotOwner]);
        }

        public async static Task<bool> UploadGrowPotSpot(string TreeKey)
        {
            ServerData.GrowPotSpot newTreePotOwner = new ServerData.GrowPotSpot()
            {
                TreeKey = TreeKey
            };

            return await Upload(newTreePotOwner, pathOfAPI[API.GrowPotSpot]);

        }

        public async static Task<Success<ServerData.GetUidRet>> DownloadGetUid()
        {


            return await Download<ServerData.GetUidRet>(pathOfAPI[API.GetUid]);
        }

        public async static Task<Success<ServerData.GetPidRet>> DownloadGetPid(string uid)
        {
            ServerData.GetPidArg getPidArg = new ServerData.GetPidArg()
            {
                uid = uid
            };

            return await Download<ServerData.GetPidRet>(pathOfAPI[API.GetPid]);
        }

        public async static Task<Success<ServerData.TreePotRet>> DownloadTreePot(double locationX, double locationY)
        {
            ServerData.TreePotArg treePotArg = new ServerData.TreePotArg()
            {
                locationX = locationX,
                locationY = locationY
            };

            return await Download<ServerData.TreePotRet>(pathOfAPI[API.TreePot]);
        }



        public async static Task<Success<ServerData.HotSpotRet>> DownloadHotSpot(double locationX, double locationY)
        {
            ServerData.HotSpotArg hotSpotArg = new ServerData.HotSpotArg()
            {
                locationX = locationX,
                locationY = locationY
            };

            return await Download<ServerData.HotSpotRet>(pathOfAPI[API.HotSpot]);
        }

        public async static Task<Success<ServerData.TreeRet>> DownloadTree(double locationX, double locationY)
        {
            ServerData.TreeArg treeArg = new ServerData.TreeArg()
            {
                locationX = locationX,
                locationY = locationY
            };

            return await Download<ServerData.TreeRet>(pathOfAPI[API.Tree]);
        }

        public async static Task<Success<ServerData.DecorationRet>> DownloadDecoration(string TreeKey)
        {
            ServerData.DecorationArg decorationArg = new ServerData.DecorationArg()
            {
                TreeKey = TreeKey
            };

            return await Download<ServerData.DecorationRet>(pathOfAPI[API.Decoration]);
        }

        public async static Task<Success<ServerData.PartyRet>> DownloadParty(double locationX, double locationY)
        {
            ServerData.PartyArg partyArg = new ServerData.PartyArg()
            {
                locationX = locationX,
                locationY = locationY
            };

            return await Download<ServerData.PartyRet>(pathOfAPI[API.Party]);
        }

    }
}
