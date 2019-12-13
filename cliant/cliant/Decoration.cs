using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace cliant
{

    public enum Decotype
    {
        presentBox,
        snowMan,
        bell,
        doubleBell,
        santa,
        ball
    }
    class Decoration
    {
        public string Picture { set; get; }
        public Decotype Decotype { set; get; }
        public double LocationX { set; get; }
        public double LocationY { set; get; }
        public string Message { set; get; }
        public int Year { set; get; }
        public int Month{ set; get; }
        public int Date { set; get; }
    }
}
