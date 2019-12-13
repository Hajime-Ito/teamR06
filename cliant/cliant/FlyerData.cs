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
        public DateTime Date { set; get; }
        public string Text { set; get; }
        public double LocationX { set; get; }
        public double LocationY { set; get; }
        public string LocationName { set; get; }
        public FlyerCategory Category { set; get; }
        public Stream AttachedFile { set; get; }
        public string ID { set; get; }

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
