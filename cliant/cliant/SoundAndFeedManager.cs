using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace cliant
{
    public enum MusicMode
    {
        None = 0,
        Low = 1,
        MidLow = 2,
        MidHi = 3,
        Hi = 4
    }

    static class SoundAndFeedManager
    {
        static MusicMode musicMode = MusicMode.Low;
        static public MusicMode MusicMode
        {
            get { return musicMode; }
            set
            {
                if(MusicMode.None <= value && value <= MusicMode.Hi &&  musicMode != value)
                {

                }
            }
        }


        /// <summary>
        /// 指定したミリ秒の間デバイスを振動させます。
        /// 戻り値はデバイスが振動に対応しているか
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static bool Vibrate(double ms = 500.0)
        {
            try
            {
                Vibration.Vibrate(ms);
                return true;
            }
            catch (FeatureNotSupportedException)
            {
                return false;
            }
        }

        /// <summary>
        /// デバイスの振動をキャンセルします。
        /// 戻り値はデバイスが振動に対応しているか
        /// </summary>
        /// <returns></returns>
        public static bool CancelVibrate()
        {
            try
            {
                Vibration.Cancel();
                return true;
            }
            catch (FeatureNotSupportedException)
            {
                return false;
            }
        }
    }
}
