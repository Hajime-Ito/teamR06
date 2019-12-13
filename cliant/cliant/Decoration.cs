using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace cliant
{

    enum Decotype
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
        Stream picture;
        Decotype decotype;
        double locationX;
        double locationY;
        string message;
        int year;
        int month;
        int date;
    }
}
