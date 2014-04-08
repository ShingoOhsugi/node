using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PopClient
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class MainWindow : Window
    {
        public NotifyIcon _notifyIcon;


        public MainWindow()
        {
            InitializeComponent();


            //WebBrowserのレンダリングモードはデフォルトIE7であるため、HTML5が使えない。
            //なのでIE9に変更する。
            string FEATURE_BROWSER_EMULATION
                = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            string FEATURE_DOCUMENT_COMPATIBLE_MODE
                = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_DOCUMENT_COMPATIBLE_MODE";
            string exename = Process.GetCurrentProcess().MainModule.ModuleName;
            using (RegistryKey regkey1 =
                Registry.CurrentUser.CreateSubKey(FEATURE_BROWSER_EMULATION))
            using (RegistryKey regkey2 =
                Registry.CurrentUser.CreateSubKey(FEATURE_DOCUMENT_COMPATIBLE_MODE))
            {
                regkey1.SetValue(exename, 9000, RegistryValueKind.DWord);
                regkey2.SetValue(exename, 90000, RegistryValueKind.DWord);
                regkey1.Close();
                regkey2.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //タスクバーに表示されないようにする
            ShowInTaskbar = false;

            //タスクトレイアイコンを初期化する
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Text = "HOTLINE by ndc";
            _notifyIcon.Icon = new Icon("img/notifyIcon.ico");

            //タスクトレイに表示する
            _notifyIcon.Visible = true;

            //アイコンにコンテキストメニュー「終了」を追加する
            ContextMenuStrip menuStrip = new ContextMenuStrip();

            ToolStripMenuItem exitItem = new ToolStripMenuItem();
            exitItem.Text = "終了";
            menuStrip.Items.Add(exitItem);
            exitItem.Click += new EventHandler(exitItem_Click);

            _notifyIcon.ContextMenuStrip = menuStrip;

            //タスクトレイアイコンのクリックイベントハンドラを登録する
            _notifyIcon.MouseClick += 
                new System.Windows.Forms.MouseEventHandler(_notifyIcon_MouseClick);


            wbMain.ObjectForScripting = new HtmlInterop();
            wbMain.Navigate(new Uri("http://ndcapp.herokuapp.com/"));
        }

        private void exitItem_Click(object sender, EventArgs e)
        {
            _notifyIcon.Dispose();
            System.Windows.Application.Current.Shutdown();
        }
        
        private void _notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //ウィンドウを可視化
                this.Visibility = Visibility.Visible;
                this.WindowState = WindowState.Normal;
                this.Activate();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //閉じるのをキャンセルする
            e.Cancel = true;

            //ウィンドウを非可視にする
            Visibility = Visibility.Collapsed;
        }




    }
}
