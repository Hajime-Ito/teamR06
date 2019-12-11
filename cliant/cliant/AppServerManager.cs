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
        /// ユーザー情報を更新します。
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async static Task<bool> UpdateUser(double locationX, double locationY, string pid)
        {
            var arg = new UpdateUserArg()
            {
                locationX = locationX,
                locationY = locationY,
                pid = pid
            };

            return await ServerDataManager.Post(arg, Paths[KnownPaths.Account]);
        }

        /// <summary>
        /// pidが所有するTreePotを新しく作成します。
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async static Task<bool> NewTreePotOwner(string pid)
        {
            var arg = new NewTreePotOwnerArg()
            {
                pid = pid
            };

            return await ServerDataManager.Post(arg, Paths[KnownPaths.Tree])
                && await ServerDataManager.Post(arg, Paths[KnownPaths.TreePot]);
        }

        /// <summary>
        /// keyで指定されたTreePotを育てます。
        /// </summary>
        /// <param name="treeKey"></param>
        /// <returns></returns>
        public async static Task<bool> GrowTreePot(string treeKey)
        {
            var arg = new GrowTreePotArg()
            {
                TreeKey = treeKey
            };

            return await ServerDataManager.Put(arg, Paths[KnownPaths.TreePot]);
        }

        /// <summary>
        /// keyで指定されたTreePotを削除します。
        /// </summary>
        /// <param name="treeKey"></param>
        /// <returns></returns>
        public async static Task<bool> RemoveTreePot(string treeKey)
        {
            var arg = new RemoveTreePotArg()
            {
                TreeKey = treeKey
            };

            return await ServerDataManager.Delete(arg, Paths[KnownPaths.TreePot]);
        }

        /// <summary>
        /// uidを取得します。
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static Task<Success<GetUidRet[]>> GetUid()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// uidからpidを取得します。
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async static Task<Success<GetPidRet>> GetPid(string uid)
        {
            var arg = new GetPidArg()
            {
                uid = uid
            };

            return await ServerDataManager.Get<GetPidRet, GetPidArg>(arg, Paths[KnownPaths.Account]);
        }

        /// <summary>
        /// (locationX,locationY)周辺のTreePotを取得します。
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <returns></returns>
        public async static Task<Success<GetTreePotsRet[]>> GetTreePots(double locationX, double locationY)
        {
            var arg = new GetTreePotsArg()
            {
                locationX = locationX,
                locationY = locationY
            };

            return await ServerDataManager.Get<GetTreePotsRet[], GetTreePotsArg>(arg, Paths[KnownPaths.TreePot_View]);
        }

        /// <summary>
        /// pidに対応するアカウントが作成したTreePotを取得します。
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async static Task<Success<GetTreePotsRet[]>> GetMyTreePots(string pid)
        {
            var arg = new GetMyTreePotsArg()
            {
                pid = pid
            };

            return await ServerDataManager.Get<GetTreePotsRet[], GetMyTreePotsArg>(arg, Paths[KnownPaths.TreePot]);
        }

        /// <summary>
        /// (locationX,locationY)周辺のTreeを取得します。
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <returns></returns>
        public async static Task<Success<GetTreesRet[]>> GetTrees(double locationX, double locationY)
        {
            var arg = new GetTreesArg()
            {
                locationX = locationX,
                locationY = locationY
            };

            return await ServerDataManager.Get<GetTreesRet[], GetTreesArg>(arg, Paths[KnownPaths.Tree]);
        }
    }
}
