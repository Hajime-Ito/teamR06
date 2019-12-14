using System;
using System.Collections.Generic;
using System.Text;

namespace cliant
{
    class TreeData
    {
        public double LocationX { set; get; }
        public double LocationY { set; get; }
        public int GrowStage
        {
            get
            {
                int rate = 10;
                for (int i = 0; i < 7; i++)
                {
                    if(Point < rate)
                    {
                        return i;
                    }
                    rate *= 2;
                }

                return 6;
            }
        }
        public List<Decoration> Decorations { set; get; }
        public int Point { set; get; }
        public double Time { set; get; }
        public int DecoMax
        {
            get
            {
                switch (GrowStage)
                {
                    case 0:
                    case 1:
                        return 0;
                    case 2:
                        return 3;
                    case 3:
                        return 10;
                    case 4:
                        return 20;
                    case 5:
                        return 30;
                    default:
                        return 50;
                }
            }
        }

    }
}
