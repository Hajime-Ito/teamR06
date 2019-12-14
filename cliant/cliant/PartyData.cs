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
        int Date { set; get; }
        int Month { set; get; }
        int Year { set; get; }
        string Owner { set; get; }
        string Message { set; get; }
        Type Type { set; get; }
        double LocationX { set; get; }
        double LocationY { set; get; }

    }
}
