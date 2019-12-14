using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace cliant
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        MasterItem globalItem;

        public MainPage()
        {
            InitializeComponent();

            masterPage.listView.ItemSelected += OnItemSelected;            
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterItem;
            globalItem = item;

            if (item != null)
            {
                var nav = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));

                nav.BarBackgroundColor = Color.FromHex("FF0000");
                Detail = nav;
                
                masterPage.listView.SelectedItem = null;
                IsPresented = false;
            }
        }        
    }
}
