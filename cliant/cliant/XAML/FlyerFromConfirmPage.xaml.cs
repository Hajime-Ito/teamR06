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
    public partial class FlyerFromConfirmPage : ContentPage
    {
        public FlyerFromConfirmPage()
        {
            InitializeComponent();

            send_btn.Clicked += Send_btn_Clicked;
        }

        private void Send_btn_Clicked(object sender, EventArgs e)
        {
        }
    }
}