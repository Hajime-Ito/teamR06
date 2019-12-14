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
        public FlyerFormTimePage()
        {
            InitializeComponent();

            next_desc.Clicked += Next_desc_Clicked;
        }

        private void Next_desc_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FlyerFormDescriptionPage());
        }
    }
}