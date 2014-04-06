using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MainWindow : Window
    {
        private NotifyIcon _notifyIcon;


        public MainWindow()
        {
            InitializeComponent();
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



            wbMain.Navigate(new Uri("http://www.yahoo.co.jp"));
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
                Visibility = Visibility.Visible;
                WindowState = WindowState.Normal;
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
