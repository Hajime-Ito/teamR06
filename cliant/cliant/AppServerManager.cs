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
    public static class AppServerManager
    {
        enum KnownPaths
        {
            Tree,
            TreePot,
            Account,
            TreePot_View,
            Party,
            TreeDecoration,
            HotSpot,
            Flyer
        }

        static Dictionary<KnownPaths, string> paths = new Dictionary<KnownPaths, string>()
        {
            {KnownPaths.Tree,"/Tree" },
            {KnownPaths.TreePot,"/TreePot" },
            {KnownPaths.Account,"/Account" },
            {KnownPaths.TreePot_View,"/TreePot/View" },
            {KnownPaths.Party,"/Party" },
            {KnownPaths.TreeDecoration,"/TreeDecoration" },
            {KnownPaths.HotSpot,"/HotSpot" },
            {KnownPaths.Flyer,"/Flyer" }
        };

        #region Account

        /// <summary>
        /// uidを生成します
        /// </summary>
        /// <returns></returns>
        public async static Task<Success<SUidOnly>> GetUid()
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
        public static bool UpdateUser(double locationX, double locationY, string pid)
        {
            var arg = new SUpdateUser()
            {
                locationX = locationX,
                locationY = locationY,
                pid = pid
            };

            return ServerDataManager.Post(arg, paths[KnownPaths.Account]);
        }

        /// <summary>
        /// uidからpidを取得します
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static Success<SPidOnly> GetPid(string uid)
        {
            var arg = new SUidOnly()
            {
                uid = uid
            };

            return ServerDataManager.Get<SPidOnly, SUidOnly>(arg, paths[KnownPaths.Account]);
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
        public static bool StartTreePotSession(double locationX, double locationY, string treeKey)
        {
            var arg = new STreePot()
            {
                locationX = locationX,
                locationY = locationY,
                TreeKey = treeKey
            };
            return ServerDataManager.Post(arg, paths[KnownPaths.TreePot]);
        }

        /// <summary>
        /// 植木鉢セッションを終了
        /// </summary>
        /// <param name="treeKey"></param>
        /// <returns></returns>
        public static bool StopTreePotSession(string treeKey)
        {
            var arg = new STreeKeyOnly()
            {
                TreeKey = treeKey
            };
            return ServerDataManager.Delete(arg, paths[KnownPaths.TreePot]);
        }

        /// <summary>
        /// 自分の持つ植木鉢情報を取得
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static Success<SMyTreePot[]> GetMyTreePot(string owner, double locationX, double locationY, double distance)
        {
            var arg = new SGetMyTreePot()
            {
                owner = owner,
                locationX = locationX,
                locationY = locationX,
                distance = distance
            };
            return ServerDataManager.Get<SMyTreePot[],SGetMyTreePot>(arg, paths[KnownPaths.TreePot]);
        }

        /// <summary>
        /// 現在位置から、一定距離内のTreePotのリストを取得 
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public async static Task<Success<STreePot[]>> GetTreePots(double locationX, double locationY, double distance)
        {
            var arg = new SLocationAndDistance()
            {
                locationX = locationX,
                locationY = locationX,
                distance = distance
            };
            return ServerDataManager.Get<STreePot[], SLocationAndDistance>(arg, paths[KnownPaths.TreePot_View]);
        }

        #endregion

        #region Tree

        /// <summary>
        /// 植木鉢にポイントを加算
        /// </summary>
        /// <param name="treeKey"></param>
        /// <returns></returns>
        public static bool GrowTree(string treeKey)
        {
            var arg = new STreeKeyOnly()
            {
                TreeKey = treeKey
            };
            return ServerDataManager.Put(arg, paths[KnownPaths.Tree]);
        }

        /// <summary>
        /// Tree(実態)の作成
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="treeName"></param>
        /// <returns></returns>
        public static Success<STreeKeyOnly> MakeTree(string owner, double locationX, double locationY, string treeName)
        {
            var arg = new SMakeTree()
            {
                owner = owner,
                locationX = locationX,
                locationY =locationY,
                TreeName = treeName
            };
            return ServerDataManager.Post<STreeKeyOnly, SMakeTree>(arg, paths[KnownPaths.Tree]);
        }

        /// <summary>
        /// 現在位置から、一定距離内のTreeのリストを取得
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static Success<ServerData.STreeData[]> GetTrees(double locationX, double locationY, double distance)
        {
            var arg = new SLocationAndDistance()
            {
                locationX = locationX,
                locationY = locationY,
                distance = distance
            };
            return ServerDataManager.Get<ServerData.STreeData[], SLocationAndDistance>(arg, paths[KnownPaths.Tree]);
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
        public static bool AddParty(double locationX, double locationY, int day, int month, int year, int kind, string message, string owner)
        {
            var arg = new SPartyData()
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

            return ServerDataManager.Post(arg, paths[KnownPaths.Party]);
        }

        /// <summary>
        /// 現在位置から、一定距離内のPartyのリストを取得
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static Success<SPartyData[]> GetParties(double locationX, double locationY, double distance)
        {
            var arg = new SLocationAndDistance()
            {
                distance = distance,
                locationX = locationX,
                locationY = locationY,
            };

            return ServerDataManager.Get<SPartyData[], SLocationAndDistance>(arg, paths[KnownPaths.Party]);
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
        public static bool AddDecoration(int kind, string message, int posX, int posY, int date, int month, int year, string treeKey)
        {
            var arg = new SAddDecoration()
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

            return ServerDataManager.Post(arg, paths[KnownPaths.TreeDecoration]);
        }


        public static Success<SDecorationData[]> GetDecoration(string treeKey)
        {
            var arg = new STreeKeyOnly()
            {
                TreeKey = treeKey
            };

            return ServerDataManager.Get<SDecorationData[], STreeKeyOnly>(arg, paths[KnownPaths.TreeDecoration]);
        }


        #endregion

        #region HotSpot

        public static Success<SHotSpotData[]> GetHotSpots(double locationX, double locationY, double distance)
        {
            var arg = new SLocationAndDistance()
            {
                distance = distance,
                locationX = locationX,
                locationY = locationY,
            };

            return ServerDataManager.Get<SHotSpotData[], SLocationAndDistance>(arg, paths[KnownPaths.HotSpot]);
        }

        #endregion

        #region Flyer

        /// <summary>
        /// チラシ情報をserverに送ります。
        /// </summary>
        /// <param name="date"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="message"></param>
        /// <param name="time"></param>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <returns></returns>
        public static Success<SFlyerKeyOnly> AddFlyer(int date, int month, int year, string message, string time, double locationX, double locationY)
        {
            var arg = new SAddFlyer()
            {
                date = date,
                month = month,
                year = year,
                message = message,
                time = time,
                locationX = locationX,
                locationY = locationY,
            };

            return ServerDataManager.Post<SFlyerKeyOnly, SAddFlyer>(arg, paths[KnownPaths.Flyer]);
        }

        /// <summary>
        /// チラシ情報を更新する
        /// </summary>
        /// <param name="locationX"></param>
        /// <param name="locationY"></param>
        /// <param name="flayerKey"></param>
        /// <returns></returns>
        public static bool UpdateFlyer(double locationX, double locationY, string flayerKey)
        {
            var arg = new SUpdateFlyer()
            {
                locationX = locationX,
                locationY = locationY,
                FlyerKey = flayerKey
            };

            return ServerDataManager.Put<SUpdateFlyer>(arg, paths[KnownPaths.Flyer]);
        }
        public static Success<SFlyerData[]> GetFlyeies(double locationX, double locationY, double distance)
        {
            var arg = new SLocationAndDistance()
            {
                locationX = locationX,
                locationY = locationY,
                distance = distance
            };

            return ServerDataManager.Get<SFlyerData[], SLocationAndDistance>(arg, paths[KnownPaths.Flyer]);
        }
    #endregion

}
}
