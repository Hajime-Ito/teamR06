using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace cliant
{
    //例外処理追加
    class FlyerManager
    {
        string[] myFlyerIDs;
        string[] reqFlyerIDs;
        List<FlyerData> myFlyerIDsandDatas;

        public void GetManagerData(double locateX, double locateY, double distance)
        {
            string[] yourFlyerIDs;
            List<string> result;
            var val = AppServerManager.GetFlyeies(locateX, locateY, distance);
            if (val.IsSuccess)
            {
                result = val.Value.
            }
            else
            {
                result = new List<string>();
            }

            foreach (string s in myFlyerIDs)
            {
                if (Array.IndexOf(yourFlyerIDs, s)<0)
                {
                    result.Add(s);
                }
            }

            reqFlyerIDs = result.ToArray();
            if (reqFlyerIDs.Length != 0)
            {
                BluetoothManager.SendReqIDs(reqFlyerIDs);
            }

            BluetoothManager.GetDatas();
        }


    }
}
