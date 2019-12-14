using System;
using cliant;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerDataManager.BaseAddress = new Uri("https://p2hack2019-4f273.firebaseapp.com");
            while (true)
            {
                string com = Console.ReadLine();

                switch (com)
                {
                    case "hs":
                        var retT = AppServerManager.GetHotSpots(41.797769, 140.757218, 1);
                        retT.Wait();
                        var ret = retT.Result;
                        Console.WriteLine($"is success{ret.IsSuccess}");
                        if (ret.IsSuccess)
                        {
                            foreach (var item in ret.Value)
                            {
                                Console.WriteLine($"X:{item.locationX}");
                                Console.WriteLine($"Y:{item.locationY}");
                                Console.WriteLine($"N:{item.n}");
                                Console.WriteLine();
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
