using System;
using System.Collections.Generic;
using System.Text;

namespace cliant
{
    class FlyerManager
    {
        //myFlyerDatasから自分が持っているIDを求める
        List<FlyerData> myFlyerDatas = new List<FlyerData>();
        List<string> yourFlyerIDs = new List<string>();
        List<string> reqFlyerIDs = new List<string>();

        void SendManagerData()
        {
            string[] myFlyerDatasA = { };
            string[] yourFlyerIDsA;
            string[] reqFlyerIDsA;

            BluetoothManager.SendIDs(myFlyerDatasA);

        }

        void GetManagerData()
        {

        }


    }
}
