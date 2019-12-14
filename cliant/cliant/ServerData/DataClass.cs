using System;
using System.Collections.Generic;
using System.Text;

namespace cliant.ServerData
{
    public class SPidOnly
    {
        public string pid { get; set; }
    }

    public class SUidOnly
    {
        public string uid { get; set; }
    }

    public class SFlyerKeyOnly
    {
        public string FlyerKey { get; set; }
    }

    public class STreeKeyOnly
    {
        public string TreeKey { get; set; }
    }

    public class SUpdateUser
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string pid { get; set; }
    }

    public class STreePot
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string TreeKey { get; set; }
    }

    public class SMyTreePot
    {
        public string TreeKey { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
        public bool isNear { get; set; }
        public string TreeName { get; set; }
    }

    public class SGetMyTreePot {
        public string owner { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
        public double distance { get; set; }
    }

    public class SLocationAndDistance
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public double distance { get; set; }
    }

    public class SMakeTree
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string owner { get; set; }
        public string TreeName { get; set; }
    }

    public class STreeData
    {
        public string TreeKey { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string owner { get; set; }
        public string TreeName { get; set; }
        public int point { get; set; }
    }

    public class SPartyData
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string owner { get; set; }
        public int kind { get; set; }
        public string message { get; set; }
        public int dueday { get; set; }
        public int duemonth { get; set; }
        public int dueyear { get; set; }
        public string title { get; set; }
    }

    public class SHotSpotData
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public int n { get; set; }
    }

    public class SDecorationData
    {
        public int kind { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public string message { get; set; }
        public int date { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }
    public class SAddDecoration
    {
        public string TreeKey { get; set; }
        public int kind { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public string message { get; set; }
        public int date { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }

    public class SAddFlyer
    {
        public int year { get; set; }
        public int month { get; set; }
        public int date { get; set; }
        public string message { get; set; }
        public string time { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
    }

    public class SUpdateFlyer
    {
        public string FlyerKey { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
    }

    public class SFlyerData
    {
        public int year { get; set; }
        public int month { get; set; }
        public int date { get; set; }
        public string message { get; set; }
        public string time { get; set; }
        public string FlyerKey { get; set; }

    }

}
