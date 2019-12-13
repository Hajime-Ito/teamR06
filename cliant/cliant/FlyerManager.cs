using System;
using System.Collections.Generic;
using System.Text;

namespace cliant
{

    class FlyerManager
    {
        //myFlyerDatasから自分が持っているIDを求める
        string[] myFlyerIDs;
        string[] reqFlyerIDs;
        Dictionary<string, FlyerData> myFlyerIDsandDatas;

        void SendManagerData()
        {

            BluetoothManager.SendIDs(myFlyerIDs);

            reqFlyerIDs = BluetoothManager.GetReqIDs();

            List<FlyerData> reDatas = new List<FlyerData>();
            foreach(KeyValuePair<string,FlyerData> p in myFlyerIDsandDatas)
            {
                if (Array.IndexOf(reqFlyerIDs, p.Key) >= 0)
                {
                    reDatas.Add(p.Value);
                }
            }

            FlyerData[] Datas = reDatas.ToArray();
            BluetoothManager.SendDatas(Datas);

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

            BluetoothManager.SendReqIDs(reqFlyerIDs);
        }


    }
}
