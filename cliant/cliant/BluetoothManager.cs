using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Plugin.BluetoothLE;
using System.Runtime.InteropServices;
using System.Reactive.Linq;
using Newtonsoft.Json;

namespace cliant
{
    class BluetoothManager
    {
        static class Guids
        {
            public static readonly Guid service = new Guid("cb04fe4d-5229-48b5-87b4-6fdf0be3d985");
            public static readonly Guid ids = new Guid("00000000-0000-0000-0000-000000000000");
            public static readonly Guid reqIDs = new Guid("00000000-0000-0000-0000-000000000001");
            public static readonly Guid datas = new Guid("00000000-0000-0000-0000-000000000002");
        }

        public string[] GetIDs()
        {
            List<string> ids = new List<string>();

            CrossBleAdapter.Current.Scan().Subscribe(scanResult =>
            {
                // Once finding the device/scanresult you want
                scanResult.Device.Connect();

                scanResult.Device.GetKnownCharacteristics(Guids.service, new Guid[] { Guids.ids }).Subscribe(async characteristic =>
                {
                    var read = await characteristic.Read();
                    ids.Add(Encoding.UTF8.GetString(read.Data));
                });

            });

            return ids.ToArray();
        }

        public string[] GetReqIDs()
        {
            List<string> reqIDs = new List<string>();

            CrossBleAdapter.Current.Scan().Subscribe(scanResult =>
            {
                // Once finding the device/scanresult you want
                scanResult.Device.Connect();

                scanResult.Device.GetKnownCharacteristics(Guids.service, new Guid[] { Guids.reqIDs }).Subscribe(async characteristic =>
                {
                    var read = await characteristic.Read();
                    reqIDs.Add(Encoding.UTF8.GetString(read.Data));
                });

            });

            return reqIDs.ToArray();
        }

        public FlyerData[] GetDatas()
        {
            List<FlyerData> datas = new List<FlyerData>();

            CrossBleAdapter.Current.Scan().Subscribe(scanResult =>
            {
                // Once finding the device/scanresult you want
                scanResult.Device.Connect();

                scanResult.Device.GetKnownCharacteristics(Guids.service, new Guid[] { Guids.datas }).Subscribe(async characteristic =>
                {
                    var read = await characteristic.Read();
                    var str = Encoding.UTF8.GetString(read.Data);
                    datas.Add(JsonConvert.DeserializeObject<FlyerData>(str));
                });

            });

            return datas.ToArray();
        }



        public void SendIDs(string[] ids)
        {
            throw new NotImplementedException();
        }

        public void SendReqIDs(string[] reqIds)
        {
            throw new NotImplementedException();
        }

        public void SendDatas(FlyerData[] flyerDatas)
        {
            throw new NotImplementedException();
        }

    }
}
