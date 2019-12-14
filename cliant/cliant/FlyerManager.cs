using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace cliant
{
    //例外処理追加
    class FlyerManager
    {
        public List<FlyerData> MyFlyers { get; private set; } = new List<FlyerData>();

        public void GetManagerData(double locateX, double locateY, double distance)
        {
            ServerData.SFlyerData[] result;
            var val = AppServerManager.GetFlyeies(locateX, locateY, distance);
            if (val.IsSuccess)
            {
                result = val.Value.ToArray();
            }
            else
            {
                result = new ServerData.SFlyerData[0];
            }

            foreach (var s in result)
            {
                if(!MyFlyers.Any(f => f.Id == s.FlyerKey))
                {
                    var sp = s.time.Split(':');

                    MyFlyers.Add(new FlyerData()
                    {
                        Id = s.FlyerKey,
                        Date = new DateTime(s.year,s.month,s.date, int.Parse(sp[0]), int.Parse(sp[1]),0),
                        Text = s.message
                    });
                }
            }
        }
    }
}
