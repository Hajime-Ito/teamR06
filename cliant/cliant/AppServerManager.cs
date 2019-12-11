using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Web;
using cliant.ServerData;

namespace cliant
{
    static class AppServerManager
    {
        enum KnownPaths
        {
            Tree,
            TreePot,
            Account,
            TreePot_View,
        }

        static Dictionary<KnownPaths, string> Paths = new Dictionary<KnownPaths, string>()
        {
            {KnownPaths.Tree,"Tree" },
            {KnownPaths.TreePot,"TreePot" },
            {KnownPaths.Account,"Account" },
            {KnownPaths.TreePot_View,"TreePot/View" }
        };

        /// <summary>
        /// ユーザー情報をサーバーにアップロードします。
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async static Task<bool> UploadUser(double locationX, double locationY, string pid)
        {
            var arg = new UserArg()
            {
                locationX = locationX,
                locationY = locationY,
                pid = pid
            };

            return await ServerDataManager.Post(arg, Paths[KnownPaths.Account]);
        }
    }
}
