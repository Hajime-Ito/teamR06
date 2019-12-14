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
    public partial class SettingsPage : ContentPage
    {
        MainPage main = new MainPage();
        public SettingsPage()
        {
            InitializeComponent();

            main.ChangeColor("FF0000");
        }
    }
}