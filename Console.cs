using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using Fiddler;
using System.Windows.Forms;
using System.Drawing;

[assembly: Fiddler.RequiredVersion("2.2.4.0")]

namespace FiddlerConsole
{
    public class Console : IAutoTamper
    {

        private string HOST = "fiddler.fc.com";
        private string TEMP_PATH = "Scripts/FiddlerConsole/";

        //constructor
        public Console()
        {
            if (!Directory.Exists(TEMP_PATH))
            {
                Directory.CreateDirectory(TEMP_PATH);
            }
        }

        //load UI
        public void OnLoad()
        {
            /**
             * 好像不需要这个界面了
            var oPage = new TabPage("FiddlerConsole");
            using (var stream = GetType().Assembly.GetManifestResourceStream("FiddlerConsole.icon.jpg"))
            {
                if (stream != null)
                {
                    FiddlerApplication.UI.imglSessionIcons.Images.Add(Image.FromStream(stream));
                    oPage.ImageIndex = Enum.GetNames(typeof(SessionIcons)).Length;
                }
            }
            //MessageBox.Show(oPage.ImageIndex.ToString());
            
            var oView = new ConsolePanel();
            oView.showTips();
            oPage.Controls.Add(oView);

            //FiddlerApplication.UI.tabsViews.TabPages.Add(oPage);
            * */
        }

        public void OnBeforeUnload() 
        {
            //清空临时文件
            Array.ForEach(Directory.GetFiles(TEMP_PATH), File.Delete);
        }

        //发特殊请求之后，Fiddler自动返回响应，不经过互联网
        public void AutoTamperRequestBefore(Session oSession)
        {
            if (oSession.host == HOST)//会话为console请求
            {
                //["wmj",{"name":"aslin"}]
                //{flag:"user", data:["wmj", {"name":"aslin"}]}-理想
                var r = new Regex(@"flag=\w+", RegexOptions.IgnoreCase);
                var flagM = r.Matches(oSession.PathAndQuery);
                var flag = "";
                if (flagM.Count > 0) 
                {
                    flag = flagM[0].ToString().Replace("flag=", "");
                }
                var data = Uri.UnescapeDataString(System.Text.Encoding.UTF8.GetString(oSession.RequestBody)).Replace("value=", "");
                if (flag != "")
                {
                    data = "{flag:'" + flag + "',data:" + data + "}";
                }
                var filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".json";
                var path = TEMP_PATH + filename;
                oSession["ui-backcolor"] = "black";
                oSession["ui-color"] = "white";
                System.IO.File.WriteAllText(path, data);
                oSession["x-replywithfile"] = AppDomain.CurrentDomain.BaseDirectory + path.Replace("/", @"\");
            }
        }

        public void AutoTamperRequestAfter(Session oSession)
        { 
            
        }

        //解析html文件中<include>语法
        public void AutoTamperResponseBefore(Session oSession)
        {
            //*.html/*.htm，且同级或上级有.incre/目录
            //MessageBox.Show(oSession.url);
            if (oSession.host == HOST)//会话为console请求
            {
                oSession.oResponse["Access-Control-Allow-Origin"] = "*";//允许跨域访问
            }
        }

        public void AutoTamperResponseAfter(Session oSession)
        { 
            
        }

        public void OnBeforeReturningError(Session oSession)
        { 
            
        }
    }
}
