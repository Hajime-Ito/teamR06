using System;
using System.Collections.Generic;
using System.Text;

namespace cliant.ServerData
{
    class PidOnly
    {
        public string pid { get; set; }
    }

    class UidOnly
    {
        public string uid { get; set; }
    }

    class TreeKeyOnly
    {
        public string TreeKey { get; set; }
    }

    class UpdateUser
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string pid { get; set; }
    }

    class TreePot
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string TreeKey { get; set; }
    }

    class MyTreePot
    {
        public string TreeKey { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
        public bool isNear { get; set; }
        public string TreeName { get; set; }
    }

    class GetMyTreePot {
        public string owner { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
        public double distance { get; set; }
    }

    class LocationAndDistance
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public double distance { get; set; }
    }

    class MakeTree
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string owner { get; set; }
        public string TreeName { get; set; }
    }

    class TreeData
    {
        public string TreeKey { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string owner { get; set; }
        public string TreeName { get; set; }
        public int point { get; set; }
    }

    class PartyData
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string owner { get; set; }
        public int kind { get; set; }
        public string message { get; set; }
        public int dueday { get; set; }
        public int duemonth { get; set; }
        public int dueyear { get; set; }
    }

    class LocationData
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
    }
}
