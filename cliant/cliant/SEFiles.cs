using System.Collections.Generic;

namespace cliant
{
    /// <summary>
    /// soundエフェクトの種類を表す列挙型
    /// </summary>
    enum SEType
    {
    }

    /// <summary>
    /// soundエフェクトのファイル名を提供するクラス
    /// </summary>
    static class SEFiles
    {
        static readonly Dictionary<SEType, string> seFileDic = new Dictionary<SEType, string>()
        {
        };

        /// <summary>
        /// <paramref name="sEType"/>で表されるSEのファイル名を返します。
        /// </summary>
        /// <param name="sEType"></param>
        /// <returns></returns>
        public static string GetName(SEType sEType)
        {
            return seFileDic[sEType];
        }
    }
}
