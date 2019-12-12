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
    public partial class PartyForm : ContentPage
    {
        public PartyForm()
        {
            InitializeComponent();
            create_btn.Clicked += Create_btn_Clicked;
        }

        private void Create_btn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PartyFormDate(), true);
        }
    }
}