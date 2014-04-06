using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;

namespace PopClient
{
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public class HtmlInterop
    {
        public void ScriptNotify(string reqValue)
        {
            // 引数のJson文字列をDictionaryに変換
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, object> reqDic = 
                jss.Deserialize<Dictionary<string, object>>(reqValue);

            MainWindow mw = ((MainWindow)Application.Current.MainWindow) as MainWindow;
            mw._notifyIcon.BalloonTipTitle = reqDic["BalloonTipTitle"] as string;
            mw._notifyIcon.BalloonTipText = reqDic["BalloonTipText"] as string;
            mw._notifyIcon.ShowBalloonTip((int)reqDic["ShowBalloonTip"]);
        }
    }
}
