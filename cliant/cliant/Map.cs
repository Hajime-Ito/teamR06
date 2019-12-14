using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace cliant
{
    class Map
    {
        List<PartyData> partyDatas = new List<PartyData>();
        List<TreeData> treeDatas = new List<TreeData>();
        List<Hotspot> hotspots = new List<Hotspot>();
        Location Location;
        double PlocationX { get { return Location.Latitude; }  }
        double PlocationY { get { return Location.Longitude; } }
        string pid;
        double radius;//表示半径
        
        void PosUpdate() 
        {
            //gps情報を更新
            var location = Geolocation.GetLastKnownLocationAsync();
            location.Wait();
            Location = location.Result;
            //サーバーへ位置情報をアップロード
            AppServerManager.UpdateUser(PlocationX, PlocationY, pid).Wait();
        }

        //mapのピンを管理する関数
        void UpdatePin(Xamarin.Forms.GoogleMaps.Map map ) 
        {

            //サーバーからオブジェクト情報をダウンロード
            List<PartyData> SpartyDatas = new List<PartyData>();
            List<TreeData> StreeDatas = new List<TreeData>();
            List<Hotspot> Shotspots = new List<Hotspot>();

            var retP = AppServerManager.GetParties(PlocationX, PlocationY, radius);
            retP.Wait();
            if (retP.Result.IsSuccess)
            {
                ServerData.SPartyData[] sPartyDatas = retP.Result.Value;
                for(int i = 0;i<sPartyDatas.Length;i++)
                {
                    PartyData pData = new PartyData();
                    pData.LocationX = sPartyDatas[i].locationX;
                    pData.LocationY = sPartyDatas[i].locationY;
                    SpartyDatas.Add(pData);
                }
            }
            else
            {
                SpartyDatas = new List<PartyData>(partyDatas);
            }

            var retT = AppServerManager.GetTrees(PlocationX, PlocationY, radius);
            retT.Wait();
            if (retT.Result.IsSuccess)
            {
                ServerData.STreeData[] sTreeDatas  = retT.Result.Value;
                for(int i = 0; i < sTreeDatas.Length; i++)
                {
                    TreeData tData = new TreeData();
                    tData.LocationX = sTreeDatas[i].locationX;
                    tData.LocationY = sTreeDatas[i].locationY;
                    StreeDatas.Add(tData);
                }              
            }
            else
            {
                StreeDatas = new List<TreeData>(treeDatas);
            }




            //サーバーから取得したそれぞれのリスト情報


            //サーバーにはあるが、自分が持っていないリスト
            List<PartyData> NewPartyDatas = new List<PartyData>();
            List<TreeData> NewTreeDatas = new List<TreeData>();
            List<Hotspot> NewHotspots = new List<Hotspot>();
            //自分は持っているが、サーバーにないリスト
            List<PartyData> LostPartyDatas = new List<PartyData>();
            List<TreeData> LostTreeDatas = new List<TreeData>();
            List<Hotspot> LostHotspots = new List<Hotspot>();
            //mapに表示されていないパーティーとツリーをピンとして追加

            foreach (PartyData p in SpartyDatas)
            {
                if (partyDatas.IndexOf(p) < 0)
                {
                    NewPartyDatas.Add(p);
                }
            }
            foreach(PartyData p in partyDatas)
            {
                if(SpartyDatas.IndexOf(p) < 0)
                {
                    LostPartyDatas.Add(p);
                }
            }

            foreach (TreeData p in StreeDatas)
            {
                if (treeDatas.IndexOf(p) < 0)
                {
                    NewTreeDatas.Add(p);
                }
            }
            foreach (TreeData p in treeDatas)
            {
                if (StreeDatas.IndexOf(p) < 0)
                {
                    LostTreeDatas.Add(p);
                }
            }

            foreach (Hotspot p in Shotspots)
            {
                if (hotspots.IndexOf(p) < 0)
                {
                    NewHotspots.Add(p);
                }
            }
            foreach (Hotspot p in hotspots)
            {
                if (StreeDatas.IndexOf(p) < 0)
                {
                    LostHotspots.Add(p);
                }
            }


            foreach (PartyData p in NewPartyDatas)
            {
                Pin boardwalkPin = new Pin
                {
                    Position = new Position(36.9641949, -122.0177232),
                    Label = "Boardwalk",
                    Address = "Santa Cruz",
                    Type = PinType.Place
                };
            }

            foreach (TreeData p in NewTreeDatas)
            {
                Pin boardwalkPin = new Pin
                {
                    Position = new Position(36.9641949, -122.0177232),
                    Label = "Boardwalk",
                    Address = "Santa Cruz",
                    Type = PinType.Place
                };
            }

            foreach (Hotspot p in NewHotspots)
            {
                map.AddCircle(new CircleOptions()
                .InvokeCenter(new LatLng(35.697d, 139.773d)) // 秋葉原駅付近
                .InvokeRadius(2000)
                .InvokeFillColor(Color.Yellow)
                .InvokeStrokeColor(Color.YellowGreen)
                .InvokeStrokeWidth(2f)
                );
            }

            //そのピンのクリックイベントを定義

            //mapに表示されているパーティーとツリーのうち、自身のリストにないものを削除
        }
        void DisplayHS() 
        {

        }

    }
}
