using System;
using System.Collections.Generic;
using System.Text;

namespace cliant.ServerData
{
    public class PidOnly
    {
        public string pid { get; set; }
    }

    public class UidOnly
    {
        public string uid { get; set; }
    }

    public class FlyerKeyOnly
    {
        public string FlyerKey { get; set; }
    }

    public class TreeKeyOnly
    {
        public string TreeKey { get; set; }
    }

    public class UpdateUser
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string pid { get; set; }
    }

    public class TreePot
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string TreeKey { get; set; }
    }

    public class MyTreePot
    {
        public string TreeKey { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
        public bool isNear { get; set; }
        public string TreeName { get; set; }
    }

    public class GetMyTreePot {
        public string owner { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
        public double distance { get; set; }
    }

    public class LocationAndDistance
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public double distance { get; set; }
    }

    public class MakeTree
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string owner { get; set; }
        public string TreeName { get; set; }
    }

    public class TreeData
    {
        public string TreeKey { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
        public string owner { get; set; }
        public string TreeName { get; set; }
        public int point { get; set; }
    }

    public class PartyData
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

    public class HotSpotData
    {
        public double locationX { get; set; }
        public double locationY { get; set; }
        public int n { get; set; }
    }

    public class DecorationData
    {
        public int kind { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public string message { get; set; }
        public int date { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }
    public class AddDecoration
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

    public class AddFlyer
    {
        public int year { get; set; }
        public int month { get; set; }
        public int date { get; set; }
        public string message { get; set; }
        public string time { get; set; }
        public string FlyerKey { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
    }

    public class UpdateFlyer
    {
        public string FlyerKey { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
    }

    public class FlyerData
    {
        public int year { get; set; }
        public int month { get; set; }
        public int date { get; set; }
        public string message { get; set; }
        public string time { get; set; }
        public string FlyerKey { get; set; }

    }

}
