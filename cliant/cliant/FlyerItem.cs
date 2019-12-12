using System;
using System.Collections.Generic;
using System.Text;

namespace cliant
{
    class FlyerItem
    {
        public string Title { get; set; }
        public string Place { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
        public string Categoly { get; set; }
        public FlyerItem(string Title,string Place, string Date, string Text,string Categoly)
        {
            this.Title = Title;
            this.Place = Place;
            this.Date = Date;
            this.Text = Text;
            this.Categoly = Categoly;
        }

    }
}
