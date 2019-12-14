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
    public partial class PartyFormPage : ContentPage
    {
        public PartyFormPage()
        {
            InitializeComponent();

            partyform_btn.Clicked += Partyform_btn_Clicked;
        }

        private void Partyform_btn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PartyFormTimePage());
        }
    }
}