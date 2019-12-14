using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace cliant
{
    enum FlyerCategory
    {
        Party,
        Shopping,
        BigEvent
    }

    class FlyerData
    {
        public DateTime Date { set; get; }
        public string Text { set; get; }
        public double LocationX { set; get; }
        public double LocationY { set; get; }
        public string LocationName { set; get; }
        public FlyerCategory Category { set; get; }
        public string Id { set; get; }

        public bool EqualsId(FlyerData flyerData)
        {
            if (flyerData == null)
            {
                return false;
            }

            return (Id == flyerData.Id);
        }
    }
}
