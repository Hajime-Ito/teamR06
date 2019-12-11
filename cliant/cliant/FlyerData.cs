using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace cliant
{
    enum FlyerCategory
    {

    }
    class FlyerData
    {
        DateTime date { set; get; }
        string text { set; get; }
        double locationX { set; get; }
        double locationY { set; get; }
        string locationName { set; get; }
        FlyerCategory category { set; get; }
        Stream attachedFile { set; get; }
        string ID { set; get; }

        public bool EqualsIdentity(FlyerData flyerData)
        {
            if (flyerData == null)
            {
                return false;
            }

            return (this.ID == flyerData.ID);
        }

        void ContentUpdate()
        {
            throw new NotImplementedException("ContentUpdateは未実装です");
        }



    }
}
