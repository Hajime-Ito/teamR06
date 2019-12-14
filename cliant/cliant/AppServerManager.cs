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

        static Dictionary<KnownPaths, string> paths = new Dictionary<KnownPaths, string>()
        {
            {KnownPaths.Tree,"Tree" },
            {KnownPaths.TreePot,"TreePot" },
            {KnownPaths.Account,"Account" },
            {KnownPaths.TreePot_View,"TreePot/View" }
        };

        #region Account

        /// <summary>
        /// uidを生成します
        /// </summary>
        /// <returns></returns>
        public async static Task<Success<UidOnly>> GetUid()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ユーザー情報を更新します。
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async static Task<bool> UpdateUser(double locationX, double locationY, string pid)
        {
            var arg = new UpdateUser()
            {
                locationX = locationX,
                locationY = locationY,
                pid = pid
            };

            return await ServerDataManager.Post(arg, paths[KnownPaths.Account]);
        }

        /// <summary>
        /// uidからpidを取得します
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async static Task<Success<PidOnly>> GetPid(string uid)
        {
            var arg = new UidOnly()
            {
                uid = uid
            };

            return await ServerDataManager.Get<PidOnly, UidOnly>(arg, paths[KnownPaths.Account]);
        }

        #endregion

        #region TreePot

        /// <summary>
        /// 植木鉢セッションを開始
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="treeKey"></param>
        /// <returns></returns>
        public async static Task<bool> StartPotSession(double locationX, double locationY, string treeKey)
        {
            var arg = new TreePot()
            {
                locationX = locationX,
                locationY = locationY,
                TreeKey = treeKey
            };
            return await ServerDataManager.Post(arg, paths[KnownPaths.TreePot]);
        }

        //植木鉢セッションを終了

        public async static Task<bool> StartPotSession(double locationX, double locationY, string treeKey)
        {
            var arg = new TreePot()
            {
                locationX = locationX,
                locationY = locationY,
                TreeKey = treeKey
            };
            return await ServerDataManager.Post(arg, paths[KnownPaths.TreePot]);
        }

        void StopTreePotSession()
   /TreePot DELETE

   //自分の持つ植木鉢情報を取得
   ({ MyTreePot}...) GetMyTreePot(GetMyTreePot)
   /TreePot GET

   //現在位置から、一定距離内のTreePotのリストを取得 
   ({ TreePot}...) TreePot(LocationAndDistance)
   /TreePot/View GET

        #endregion

        #region Tree

        #endregion

        #region Party

        #endregion


        #region Deciration

        #endregion

        #region HotSpot

        #endregion

    }
}
