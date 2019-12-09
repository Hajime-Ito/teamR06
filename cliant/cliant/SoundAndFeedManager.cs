using System;
using System.Linq;
using Plugin.SimpleAudioPlayer;
using Xamarin.Essentials;

namespace cliant
{
    /// <summary>
    /// 音声と振動をサポートするクラス
    /// </summary>
    class SoundAndFeedManager
    {
        public bool CanVibrate { get; }

        readonly ISimpleAudioPlayer[] players;
        bool playBgm = false;

        const double BGM_SPEED = 1.0;

        /// <summary>
        /// 全てのBGMを再生/停止します。
        /// </summary>
        public bool PlayBgm
        {
            get => playBgm;
            set
            {
                if(value != playBgm)
                {
                    playBgm = value;

                    if (playBgm)
                    {
                        foreach (var p in players)
                        {
                            if (!p.IsPlaying)
                            {
                                p.Play();
                            }
                        }
                    }
                    else
                    {
                        foreach (var p in players)
                        {
                            if (p.IsPlaying)
                            {
                                p.Stop();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 音声と振動をサポートするクラスを初期化します。
        /// </summary>
        public SoundAndFeedManager()
        {
            players = BGMFiles.NAME.Select((fileName) =>
            {
                var p = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

                using (var stream = typeof(App).Assembly.GetManifestResourceStream($"XFSimpleAudio.Resources.{fileName}"))
                {
                    p.Load(stream);
                }

                p.Loop = true;

                return p;
            }).ToArray();

            try
            {
                Vibration.Cancel();
                CanVibrate = true;
            }
            catch (FeatureNotSupportedException)
            {
                CanVibrate = false;
            }
        }

        /// <summary>
        /// <paramref name="dist"/>に応じてBgmのVoluemを更新します。
        /// </summary>
        /// <param name="dist"></param>
        public void BgmVoluemUpdate(double dist)
        {
            var d = Math.Max(0, dist);

            for (int i = 0; i < players.Length; i++)
            {
                var p = players[i];

                p.Volume = Math.Max(0.0, BGM_SPEED * (BGMFiles.REACH[i] - d));
                p.Volume = Math.Min(p.Volume, 1.0);
            }
        }

        /// <summary>
        /// <paramref name="seType"/>で指定したSEを<paramref name="volume"/>で指定された音量で再生します。
        /// </summary>
        /// <param name="seType"></param>
        /// <param name="volume"></param>
        public void PlaySE(SEType seType, double volume = 1.0)
        {
            var p = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

            using (var stream = typeof(App).Assembly.GetManifestResourceStream($"XFSimpleAudio.Resources.{SEFiles.GetName(seType)}"))
            {
                p.Load(stream);
            }

            p.Volume = volume;
            p.Play();
        }

        public void Viberate(double ms = 500.0)
        {
            if (CanVibrate)
            {
                Vibration.Vibrate(ms);
            }
        }

        public void ViberateCancel()
        {
            if (CanVibrate)
            {
                Vibration.Cancel();
            }
        }
    }
}
