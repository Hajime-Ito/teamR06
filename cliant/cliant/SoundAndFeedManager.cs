using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.SimpleAudioPlayer;

namespace cliant
{
    /// <summary>
    /// 音声と振動をサポートするクラス
    /// </summary>
    class SoundAndFeedManager
    {

        readonly ISimpleAudioPlayer[] players;
        bool playBgm = false;

        string[] bgmFile =
        {
            "",
            "",
            "",
            ""
        };

        static readonly double[] BGM_REACH =
        {
            0.1,
            0.5,
            1.0,
            10
        };

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
            players = bgmFile.Select((fileName) =>
            {
                var p = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();

                using (var stream = typeof(App).Assembly.GetManifestResourceStream($"XFSimpleAudio.Resources.{fileName}"))
                {
                    p.Load(stream);
                }

                p.Loop = true;

                return p;
            }).ToArray();
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

                p.Volume = Math.Max(0.0, BGM_SPEED * (BGM_REACH[i] - d));
                p.Volume = Math.Min(p.Volume, 1.0);
            }
        }
    }
}
