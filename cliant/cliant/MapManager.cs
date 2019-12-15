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

        double PlocationX { get { return Location.Latitude; }  }
        double PlocationY { get { return Location.Longitude; } }
        Location Location { get; set; }

        string pid;
        double radius;//表示半径

        Xamarin.Forms.GoogleMaps.Map Map { get; set; }

        DateTime beforeUpdateTime;

        public MapManager(Xamarin.Forms.GoogleMaps.Map map)
        {
            Map = map;
            map.HasScrollEnabled = false;
            map.IsShowingUser = true;
            beforeUpdateTime = DateTime.Now;

            Device.StartTimer(new TimeSpan(33), () =>
            {
                MapUpdate();
                return true;
            });
        }

        private void MapUpdate()
        {
            if ((DateTime.Now - beforeUpdateTime).TotalMilliseconds >= 500)
            {
                PosUpdate();
                UpdatePin();
                UpdateHotSpot();
            }
        }

        public void PosUpdate() 
        {
            //gps情報を更新
            var location = Geolocation.GetLastKnownLocationAsync();
            Map.MoveToRegion(new MapSpan(new Position(PlocationX, PlocationY), Map.VisibleRegion.LatitudeDegrees, Map.VisibleRegion.LongitudeDegrees));
            location.Wait();
            Location = location.Result;
            //サーバーへ位置情報をアップロード
            AppServerManager.UpdateUser(PlocationX, PlocationY, pid);
        }

        //mapのピンを管理する関数
        public void UpdatePin() 
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
                var ret = AppServerManager.GetTrees(PlocationX, PlocationY, radius);
                if (ret.IsSuccess)
                {
                    nextTreeDatas = ret.Value.Select(s => new TreeData()
                    {
                        LocationX = s.locationX,
                        LocationY = s.locationY,
                        Point = s.point,
                        TreeKey = s.TreeKey,
                        Decorations = AppServerManager.GetDecoration(s.TreeKey).Value.Select((d) => { 
                            return new Decoration()
                            {
                                Date = d.date,
                                Month = d.month,
                                Year = d.year,
                                Decotype = (Decotype)d.kind,
                                LocationX = d.posX,
                                LocationY = d.posY,
                                Message = d.message,
                            };
                        }).ToList()

                    }).ToList();

                    newTreeDatas = new List<TreeData>(nextTreeDatas);
                    newTreeDatas.RemoveAll(a => treeDatas.Any(b =>
                        b.TreeKey == a.TreeKey));

                    oldTreeDatas = new List<TreeData>(treeDatas);
                    oldTreeDatas.RemoveAll(a => nextTreeDatas.Any(b =>
                        b.TreeKey == a.TreeKey));
                }
                
            }

            foreach (PartyData p in newPartyDatas)
            {
                Pin boardwalkPin = new Pin
                {
                    Position = new Position(p.LocationX, p.LocationY),
                    Label = "Party",
                    Type = PinType.Generic
                };
                boardwalkPin.Clicked += PartyPinClicked;
                Map.Pins.Add(boardwalkPin);
            }

            foreach (TreeData p in newTreeDatas)
            {
                Pin boardwalkPin = new Pin
                {
                    Position = new Position(p.LocationX, p.LocationY),
                    Label = "Tree",
                    Type = PinType.Generic,
                };
                boardwalkPin.Clicked += TreePinClicked;
                Map.Pins.Add(boardwalkPin);
            }

            foreach (var item in oldPartyDatas)
            {
                var pin = Map.Pins.First(p => p.Label == "Party" && p.Position.Latitude == item.LocationX && p.Position.Longitude == item.LocationY);
                Map.Pins.Remove(pin);
            }
            foreach (var item in oldTreeDatas)
            {
                var pin = Map.Pins.First(p => p.Label == "Tree" && p.Position.Latitude == item.LocationX && p.Position.Longitude == item.LocationY);
                Map.Pins.Remove(pin);
            }
        }

        private void TreePinClicked(object sender, EventArgs e)
        {
        }

        private void PartyPinClicked(object sender, EventArgs e)
        {
        }

        public void UpdateHotSpot() 
        {
            Map.Circles.Clear();
            var ret = AppServerManager.GetHotSpots(PlocationX, PlocationY, radius);
            if (ret.IsSuccess)
            {
                foreach (var item in ret.Value)
                {
                    Map.Circles.Add(new Circle()
                    {
                        Center = new Position(item.locationX, item.locationY),
                        StrokeColor = new Color(1, 0, 0, 0.6),
                        FillColor = new Color(1, 0, 0, 0.3),
                        Radius = new Distance(item.n * 50)
                    });
                }
            }
        }

    }
}
