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
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
        }

        private async void ToggleFlyoutButtonClicked(object sender, EventArgs e)
        {
            if (flyout.IsVisible)
            {
                await flyout.TranslateTo(0, flyout.Height, 300);
                flyout.IsVisible = !flyout.IsVisible;
            }
            else
            {
                await flyout.TranslateTo(0, flyout.Height, 0);
                flyout.IsVisible = !flyout.IsVisible;
                await flyout.TranslateTo(0, 0, 300);
            }
        }
    }
}