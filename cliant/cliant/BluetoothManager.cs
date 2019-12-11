using Newtonsoft.Json;
using Plugin.BluetoothLE;
using Plugin.BluetoothLE.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace cliant
{
    static class BluetoothManager
    {
        static class Guids
        {
            public static readonly Guid service = new Guid("cb04fe4d-5229-48b5-87b4-6fdf0be3d985");
            public static readonly Guid ids =     new Guid("cb04fe4d-0000-0000-0000-000000000000");
            public static readonly Guid reqIDs =  new Guid("cb04fe4d-0000-0000-0000-000000000001");
            public static readonly Guid datas =   new Guid("cb04fe4d-0000-0000-0000-000000000002");
        }

        static List<string> nowReqID = new List<string>();

        static private List<T> GetValues<T>(Guid guid)
        {
            List<T> values = new List<T>();
            var scan = CrossBleAdapter.Current.Scan();

            if(scan == null) { return null; }

            scan.Subscribe(scanResult =>
            {
                // Once finding the device/scanresult you want
                scanResult.Device.Connect();

                scanResult.Device.GetKnownCharacteristics(Guids.service, new Guid[] { guid }).Subscribe(async characteristic =>
                {
                    var read = await characteristic.Read();
                    var str = Encoding.UTF8.GetString(read.Data);
                    var data = JsonConvert.DeserializeObject<T[]>(str);
                    values.AddRange(data);
                });

            });

            return values;
        }

        static public string[] GetIDs()
        {
            return GetValues<string>(Guids.ids).ToArray();
        }

        static public string[] GetReqIDs()
        {
            return GetValues<string>(Guids.reqIDs).ToArray();
        }

        static public FlyerData[] GetDatas()
        {
            return GetValues<FlyerData>(Guids.datas).Where(data =>
            {
                for (int i = 0; i < nowReqID.Count; i++)
                {
                    if(data.ID == nowReqID[i])
                    {
                        return true;
                    }
                }
                return false;

            }).ToArray();
        }

        static IGattServer server = null;
        static Plugin.BluetoothLE.Server.IGattCharacteristic characteristicIDs;
        static Plugin.BluetoothLE.Server.IGattCharacteristic characteristicReqIDs;
        static Plugin.BluetoothLE.Server.IGattCharacteristic characteristicDatas;

        static async void TryServerInit()
        {
            if(server == null)
            {
                server = await CrossBleAdapter.Current.CreateGattServer();
            }

            if(server.Services.Count != 0) { return; }
            var service = server.CreateService(Guids.service, true);

            characteristicIDs = service.AddCharacteristic(
                    Guids.ids,
                    CharacteristicProperties.Read | CharacteristicProperties.Write,
                    GattPermissions.Read | GattPermissions.Write
            );

            characteristicReqIDs = service.AddCharacteristic(
                Guids.reqIDs,
                CharacteristicProperties.Read | CharacteristicProperties.Write,
                GattPermissions.Read | GattPermissions.Write
            );

            characteristicDatas = service.AddCharacteristic(
                Guids.datas,
                CharacteristicProperties.Read | CharacteristicProperties.Write,
                GattPermissions.Read | GattPermissions.Write
            );

            server.AddService(service);
        }

        public static void ServerStop()
        {
            if(server == null || server.Services.Count == 0) { return; }
            server.ClearServices();
        }

        static public void SendIDs(string[] ids)
        {
            TryServerInit();

            characteristicIDs.WhenReadReceived().Subscribe(observer =>
            {
                try
                {
                    var str = JsonConvert.SerializeObject(ids);
                    observer.Value = Encoding.UTF8.GetBytes(str);
                    observer.Status = GattStatus.Success;
                }
                catch (Exception)
                {
                    observer.Status = GattStatus.Failure;
                }
            });
        }

        static public void SendReqIDs(string[] reqIds)
        {
            TryServerInit();

            foreach (var id in reqIds)
            {
                if(!nowReqID.Any(s => s == id))
                {
                    nowReqID.Add(id);
                }
            }

            characteristicReqIDs.WhenReadReceived().Subscribe(observer =>
            {
                try
                {
                    var str = JsonConvert.SerializeObject(nowReqID);
                    observer.Value = Encoding.UTF8.GetBytes(str);
                    observer.Status = GattStatus.Success;
                }
                catch (Exception)
                {
                    observer.Status = GattStatus.Failure;
                }
            });
        }

        static public void SendDatas(FlyerData[] flyerDatas)
        {
            TryServerInit();

            characteristicDatas.WhenReadReceived().Subscribe(observer =>
            {
                try
                {
                    var str = JsonConvert.SerializeObject(flyerDatas);
                    observer.Value = Encoding.UTF8.GetBytes(str);
                    observer.Status = GattStatus.Success;
                }
                catch (Exception)
                {
                    observer.Status = GattStatus.Failure;
                }
            });
        }
    }
}
