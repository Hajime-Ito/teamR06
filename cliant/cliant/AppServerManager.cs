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
            Party,
            TreeDecoration
        }

        static Dictionary<KnownPaths, string> paths = new Dictionary<KnownPaths, string>()
        {
            {KnownPaths.Tree,"Tree" },
            {KnownPaths.TreePot,"TreePot" },
            {KnownPaths.Account,"Account" },
            {KnownPaths.TreePot_View,"TreePot/View" },
            {KnownPaths.Party,"Party" },
            {KnownPaths.TreeDecoration,"TreeDecoration" }
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
        public async static Task<bool> StartTreePotSession(double locationX, double locationY, string treeKey)
        {
            var arg = new TreePot()
            {
                locationX = locationX,
                locationY = locationY,
                TreeKey = treeKey
            };
            return await ServerDataManager.Post(arg, paths[KnownPaths.TreePot]);
        }

        /// <summary>
        /// 植木鉢セッションを終了
        /// </summary>
        /// <param name="treeKey"></param>
        /// <returns></returns>
        public async static Task<bool> StopTreePotSession(string treeKey)
        {
            var arg = new TreeKeyOnly()
            {
                TreeKey = treeKey
            };
            return await ServerDataManager.Delete(arg, paths[KnownPaths.TreePot]);
        }

        /// <summary>
        /// 自分の持つ植木鉢情報を取得
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public async static Task<Success<MyTreePot[]>> GetMyTreePot(string owner, double locationX, double locationY, double distance)
        {
            var arg = new GetMyTreePot()
            {
                owner = owner,
                locationX = locationX,
                locationY = locationX,
                distance = distance
            };
            return await ServerDataManager.Get<MyTreePot[],GetMyTreePot>(arg, paths[KnownPaths.TreePot]);
        }

        /// <summary>
        /// 現在位置から、一定距離内のTreePotのリストを取得 
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public async static Task<Success<TreePot[]>> GetTreePots(double locationX, double locationY, double distance)
        {
            var arg = new LocationAndDistance()
            {
                locationX = locationX,
                locationY = locationX,
                distance = distance
            };
            return await ServerDataManager.Get<TreePot[], LocationAndDistance>(arg, paths[KnownPaths.TreePot_View]);
        }

        #endregion

        #region Tree

        /// <summary>
        /// 植木鉢にポイントを加算
        /// </summary>
        /// <param name="treeKey"></param>
        /// <returns></returns>
        public async static Task<bool> GrowTree(string treeKey)
        {
            var arg = new TreeKeyOnly()
            {
                TreeKey = treeKey
            };
            return await ServerDataManager.Put(arg, paths[KnownPaths.Tree]);
        }

        /// <summary>
        /// Tree(実態)の作成
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="treeName"></param>
        /// <returns></returns>
        public async static Task<Success<TreeKeyOnly>> MakeTree(string owner, double locationX, double locationY, string treeName)
        {
            var arg = new MakeTree()
            {
                owner = owner,
                locationX = locationX,
                locationY =locationY,
                TreeName = treeName
            };
            return await ServerDataManager.Post<TreeKeyOnly, MakeTree>(arg, paths[KnownPaths.Tree]);
        }

        /// <summary>
        /// 現在位置から、一定距離内のTreeのリストを取得
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public async static Task<Success<TreeData[]>> GetTrees(double locationX, double locationY, double distance)
        {
            var arg = new LocationAndDistance()
            {
                locationX = locationX,
                locationY = locationY,
                distance = distance
            };
            return await ServerDataManager.Get<TreeData[], LocationAndDistance>(arg, paths[KnownPaths.Tree]);
        }

        #endregion

        #region Party

        /// <summary>
        /// Partyを追加
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="kind"></param>
        /// <param name="message"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public async static Task<bool> AddParty(double locationX, double locationY, int day, int month, int year, int kind, string message, string owner)
        {
            var arg = new PartyData()
            {
                locationX = locationX,
                locationY = locationY,
                dueday = day,
                duemonth = month,
                dueyear = year,
                kind = kind,
                message = message,
                owner = owner
            };

            return await ServerDataManager.Post(arg, paths[KnownPaths.Party]);
        }

        /// <summary>
        /// 現在位置から、一定距離内のPartyのリストを取得
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public async static Task<Success<PartyData[]>> GetParties(double locationX, double locationY, double distance)
        {
            var arg = new LocationAndDistance()
            {
                distance = distance,
                locationX = locationX,
                locationY = locationY,
            };

            return await ServerDataManager.Get<PartyData[], LocationAndDistance>(arg, paths[KnownPaths.Party]);
        }

        #endregion


        #region Deciration

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="message"></param>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="date"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public async static Task<bool> AddDecoration(int kind, string message, int posX, int posY, int date, int month, int year, string treeKey)
        {
            var arg = new AddDecoration()
            {
                TreeKey = treeKey,
                kind = kind,
                message = message,
                posX = posX,
                posY =posY,
                date = date,
                month = month,
                year = year
            };

            return await ServerDataManager.Post(arg, paths[KnownPaths.TreeDecoration]);
        }


        public async static Task<Success<DecorationData[]>> GetHotSpots(string treeKey)
        {
            var arg = new TreeKeyOnly()
            {
                TreeKey = treeKey
            };

            return await ServerDataManager.Get<DecorationData[], TreeKeyOnly>(arg, paths[KnownPaths.TreeDecoration]);
        }


        #endregion

        #region HotSpot

        public async static Task<Success<LocationData[]>> GetHotSpots(double locationX, double locationY, double distance)
        {
            var arg = new LocationAndDistance()
            {
                distance = distance,
                locationX = locationX,
                locationY = locationY,
            };

            return await ServerDataManager.Get<LocationData[], LocationAndDistance>(arg, paths[KnownPaths.Party]);
        }

        #endregion

}
}
