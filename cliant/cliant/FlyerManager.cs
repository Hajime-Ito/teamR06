using System;
using System.Collections.Generic;
using System.Text;

namespace cliant
{
    //例外処理追加
    class FlyerManager
    {
        string[] myFlyerIDs;
        string[] reqFlyerIDs;
        Dictionary<string, FlyerData> myFlyerIDsandDatas;

        void SendManagerData()
        {
            string[] reqedIDs;

            BluetoothManager.SendIDs(myFlyerIDs);

            reqedIDs = BluetoothManager.GetReqIDs();

            List<FlyerData> reDatas = new List<FlyerData>();
            foreach(KeyValuePair<string,FlyerData> p in myFlyerIDsandDatas)
            {
                if (Array.IndexOf(reqedIDs, p.Key) >= 0)
                {
                    reDatas.Add(p.Value);
                }
            }

            FlyerData[] datas = reDatas.ToArray();
            BluetoothManager.SendDatas(datas);

        }

        void GetManagerData()
        {
            string[] yourFlyerIDs;
            List<string> result = new List<string>();

            yourFlyerIDs = BluetoothManager.GetIDs();

            foreach(string s in myFlyerIDs)
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
