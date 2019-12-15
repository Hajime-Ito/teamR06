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
    public partial class FlyerFormPage : ContentPage
    {
        public FlyerFormPage()
        {
            InitializeComponent();
            flyerform_btn.Clicked += Flyerform_btn_Clicked;
        }

        private void Flyerform_btn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FlyerFormTimePage(), true);
        }
    }
}