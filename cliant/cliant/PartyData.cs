using System;
using System.Collections.Generic;
using System.Text;

namespace cliant
{
    enum Type
    {

    }
    class PartyData
    {
        public int Date { set; get; }
        public int Month { set; get; }
        public int Year { set; get; }
        public string Owner { set; get; }
        public string Message { set; get; }
        public Type Type { set; get; }
        public double LocationX { set; get; }
        public double LocationY { set; get; }

    }
}
