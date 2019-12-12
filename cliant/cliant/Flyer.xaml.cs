using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cliant
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Flyer : ContentPage
    {
        List<FlyerItem> flyers = new List<FlyerItem>();

        public Flyer()
        {
            InitializeComponent();
            flyers.Add(new FlyerItem("FUNクリスマス会","函館","2020年1月10日","サンプルテキスト", "イベント") );
            flyers.Add(new FlyerItem("FUNクリスマス会", "函館", "2020年1月10日", "サンプルテキスト", "イベント"));
            flyers.Add(new FlyerItem("FUNクリスマス会", "函館", "2020年1月10日", "サンプルテキスト", "イベント"));
            flyers.Add(new FlyerItem("FUNクリスマス会", "函館", "2020年1月10日", "サンプルテキスト", "イベント"));

            FlyerList.ItemsSource = flyers;
        }
    }
}