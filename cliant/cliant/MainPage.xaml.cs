using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace cliant
{
    //ページ遷移をチェックする為の関数型(cの関数ポインタみたいなもの)
    public delegate bool PageMoveChecker(Page before, Page next); 

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        //ページ遷移をチェックする為の関数を入れる変数
        public PageMoveChecker PageMoveChecker { get; set; } = null;

        public MainPage()
        {
            InitializeComponent();

            masterPage.listView.ItemSelected += OnItemSelected;   
            
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterItem;
            if (item != null)
            {
                //次のページ
                var next = (Page)Activator.CreateInstance(item.TargetType);
                //現在のページ
                var before = ((NavigationPage)Detail).CurrentPage;
                //遷移確認用の関数が代入されていない時と、
                //遷移確認用の関数の結果がtrueの時に画面遷移
                if (PageMoveChecker == null || PageMoveChecker(next, before))
                {
                    Detail = new NavigationPage(next);
                    masterPage.listView.SelectedItem = null;
                    IsPresented = false;
                }
                else
                {
                    //画面遷移失敗時の処理
                }
            }
        }
    }
}
