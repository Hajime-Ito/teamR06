using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Helpers;
using Xamarin.Forms.GoogleMaps.Extensions;

namespace cliant
{
    class MapManager
    {
        List<PartyData> partyDatas = new List<PartyData>();
        List<TreeData> treeDatas = new List<TreeData>();
        List<Hotspot> hotspots = new List<Hotspot>();
        Location Location;
        double PlocationX { get { return Location.Latitude; }  }
        double PlocationY { get { return Location.Longitude; } }
        string pid;
        double radius;//表示半径
        Xamarin.Forms.GoogleMaps.Map Map { get; set; }


        void PosUpdate() 
        {
            //gps情報を更新
            var location = Geolocation.GetLastKnownLocationAsync();
            location.Wait();
            Location = location.Result;
            //サーバーへ位置情報をアップロード
            AppServerManager.UpdateUser(PlocationX, PlocationY, pid);
        }

        //mapのピンを管理する関数
        void UpdatePin(Xamarin.Forms.GoogleMaps.Map map) 
        {
            List<PartyData> nextPartyDatas = new List<PartyData>();
            List<TreeData> nextTreeDatas = new List<TreeData>();

            List<PartyData> newPartyDatas = new List<PartyData>();
            List<TreeData> newTreeDatas = new List<TreeData>();

            List<PartyData> oldPartyDatas = new List<PartyData>();
            List<TreeData> oldTreeDatas = new List<TreeData>();

            //サーバーからオブジェクト情報をダウンロード
            {
                var ret = AppServerManager.GetParties(PlocationX, PlocationY, radius);
                if (ret.IsSuccess)
                {
                    nextPartyDatas = ret.Value.Select(s => new PartyData()
                    {
                        Date = s.dueday,
                        Month = s.duemonth,
                        Year = s.dueyear,
                        Type = (Type)s.kind,
                        LocationX = s.locationX,
                        LocationY = s.locationY,
                        Message = s.message,
                        Owner = s.owner

                    }).ToList();

                    newPartyDatas = new List<PartyData>(nextPartyDatas);
                    newPartyDatas.RemoveAll(a => partyDatas.Any(b =>
                        b.Date == a.Date &&
                        b.Month == a.Month &&
                        b.Year == a.Year &&
                        b.LocationX == a.LocationX &&
                        b.LocationY == a.LocationY &&
                        b.Owner == a.Owner));

                    oldPartyDatas = new List<PartyData>(partyDatas);
                    oldPartyDatas.RemoveAll(a => nextPartyDatas.Any(b =>
                        b.Date == a.Date &&
                        b.Month == a.Month &&
                        b.Year == a.Year &&
                        b.LocationX == a.LocationX &&
                        b.LocationY == a.LocationY &&
                        b.Owner == a.Owner));
                }
            }
            {
                var ret = AppServerManager.GetParties(PlocationX, PlocationY, radius);
                if (ret.IsSuccess)
                {
                    nextPartyDatas = ret.Value.Select(s => new PartyData()
                    {
                        Date = s.dueday,
                        Month = s.duemonth,
                        Year = s.dueyear,
                        Type = (Type)s.kind,
                        LocationX = s.locationX,
                        LocationY = s.locationY,
                        Message = s.message,
                        Owner = s.owner

                    }).ToList();

                    newPartyDatas = new List<PartyData>(nextPartyDatas);
                    newPartyDatas.RemoveAll(a => partyDatas.Any(b =>
                        b.Date == a.Date &&
                        b.Month == a.Month &&
                        b.Year == a.Year &&
                        b.LocationX == a.LocationX &&
                        b.LocationY == a.LocationY &&
                        b.Owner == a.Owner));

                    oldPartyDatas = new List<PartyData>(partyDatas);
                    oldPartyDatas.RemoveAll(a => nextPartyDatas.Any(b =>
                        b.Date == a.Date &&
                        b.Month == a.Month &&
                        b.Year == a.Year &&
                        b.LocationX == a.LocationX &&
                        b.LocationY == a.LocationY &&
                        b.Owner == a.Owner));
                }
            }

            foreach (PartyData p in newPartyDatas)
            {
                Pin boardwalkPin = new Pin
                {
                    Position = new Position(36.9641949, -122.0177232),
                    Label = "Boardwalk",
                    Address = "Santa Cruz",
                    Type = PinType.Place
                };
            }

            foreach (TreeData p in newTreeDatas)
            {
                Pin boardwalkPin = new Pin
                {
                    Position = new Position(36.9641949, -122.0177232),
                    Label = "Boardwalk",
                    Address = "Santa Cruz",
                    Type = PinType.Place
                };
            }

            //そのピンのクリックイベントを定義

            //mapに表示されているパーティーとツリーのうち、自身のリストにないものを削除
        }
        void DisplayHS() 
        {

        }

    }
}
