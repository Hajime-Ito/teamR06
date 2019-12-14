using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cliant
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TreeAnimationPage : ContentPage
    {
        public TreeAnimationPage()
        {
            InitializeComponent();
        }

        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            // X座標            
            x.Text = String.Format("%1$.5f", args.Location.X);
            // Y座標
            y.Text = String.Format("%1$.5f", args.Location.Y);

            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    // タップを開始した瞬間
                    /*
                    // X座標            
                    x.Text = String.Format("%1$.5f", args.Location.X);
                    // Y座標
                    y.Text = String.Format("%1$.5f", args.Location.Y);
                    */
                    x.Text = "押されました";
                    break;

                case TouchActionType.Moved:
                    //タップ中
                    break;

                case TouchActionType.Released:
                    // タップが離れたとき
                    break;
            }
        }
    }
}