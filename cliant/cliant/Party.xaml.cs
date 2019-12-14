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
    Party_View<PartyRet> partys = new Party_View<PartyRet>()
    public partial class Party : ContentPage
    {
        public Party()
        {
            InitializeComponent();
        }
    }
}