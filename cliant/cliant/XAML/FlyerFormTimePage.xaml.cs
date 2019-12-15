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
    public partial class FlyerFormTimePage : ContentPage
    {
        FlyerModel flyer;

        public FlyerFormTimePage()
        {
            InitializeComponent();
            
            next_desc.Clicked += Next_desc_Clicked;                     
        }

        private void Next_desc_Clicked(object sender, EventArgs e)
        {
            if (DateTime.Now > start_date.Date)
            {

            } else
            {
                int year = start_date.Date.Year;

                Navigation.PushAsync(new FlyerFormDescriptionPage());
            }
            
        }
    }
}