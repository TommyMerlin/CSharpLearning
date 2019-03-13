using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using AutoCAD;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //创建定时器对象
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        //定义全局标识区分文本是否发生改变  如未改变 则手动保存后不写入文档（避免手动多次control + S占用）
        bool isChange = false;

        public MainWindow()
        {
            AcadApplication app = new AcadApplication();
            AcadDocument doc = new AcadDocument();
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 300);
            dispatcherTimer.IsEnabled = false;
        }

        /// <summary>
        /// 按钮点击事件 关闭窗口  暂时取消按钮（关闭窗口功能由 control + Q 完成）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 窗体加载时默认位置出现在桌面右上角  并加载上次关闭时的文本  默认文本状态为只读（双击改变为可编辑）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            Left = SystemParameters.PrimaryScreenWidth - Width;
            Top = 0;// SystemParameters.PrimaryScreenHeight;

            ReadLineXML("memoire.xml");
            rbox_1.IsReadOnly = true;
            isChange = false;
        }

        /// <summary>
        /// 双击窗体或文本域 改变文本的只读状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbox_1_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            //rbox_1.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Gray);
            rbox_1.IsReadOnly = rbox_1.IsReadOnly ? false : true;
        }

        /// <summary>
        /// 定时执行    文本发生改变时触发计时  
        ///     5分钟后如果未手动保存则触发自动保存 如手动保存或关闭程序则终止计时  自动保存触发后终止计时 下次文本改变后重新触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            CreateXML();
            dispatcherTimer.Stop();
        }

        /// <summary>
        /// 新建/追加XML文档
        /// </summary>
        private void CreateXML()
        {
            if (!isChange) { return; }

            XmlDocument xml = new XmlDocument();

            //创建根节点对象
            XmlElement root;

            //新建或加载XML文件
            if (!File.Exists("memoire.xml"))
            {
                XmlDeclaration header = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
                xml.AppendChild(header);
                root = xml.CreateElement("memoire");
                xml.AppendChild(root);
            }
            else
            {
                xml.Load("memoire.xml");
                root = xml.DocumentElement;
            }

            //momoireText节点的Index属性每次保存时自增 方便直接读取最新记录
            int index = 0;
            if (xml.SelectNodes("/memoire/momoireText").Count > 0)
            {
                index = xml.SelectNodes("/memoire/momoireText").Cast<XmlNode>().ToList().Select(p => int.Parse(p.Attributes["Index"].Value)).Max();
            }

            //创建momoireText节点 包含创建时间、序号两个属性  时间取系统时间  序号自动递增
            XmlElement memoireText = xml.CreateElement("momoireText");
            memoireText.SetAttribute("DateTime", DateTime.Now.ToString());
            memoireText.SetAttribute("Index", (++index).ToString());
            root.AppendChild(memoireText);

            //输入的文本逐行读取并保存
            TextRange tr = new TextRange(rbox_1.Document.ContentStart, rbox_1.Document.ContentEnd);
            string text = tr.Text;
            string[] items = text.Split("\r\n".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            int seq = 0;

            foreach (var item in items)
            {
                XmlElement mItem = xml.CreateElement("mItems");
                mItem.SetAttribute("Seq", seq++.ToString());
                mItem.SetAttribute("Content", item);
                mItem.SetAttribute("Status", "1");

                memoireText.AppendChild(mItem);
            }

            xml.Save("memoire.xml");

            isChange = false;//默认首次加载时 因为要读取上次关闭之前的文档 isChange会变为true 所以需要在加载完成后将其重新置为false
        }

        /// <summary>
        /// 读取XML文档
        /// </summary>
        /// <param name="fileName"></param>
        private void ReadLineXML(string fileName)
        {
            XmlDocument xml = new XmlDocument();

            //加载XML文档
            if (!File.Exists("memoire.xml")) { return; }
            xml.Load(fileName);

            XmlElement root = xml.DocumentElement;//获取根节点


            //取Index值最大的momoireText节点中的所有子节点及其属性（即最新的一次保存记录）
            int index = xml.SelectNodes("/memoire/momoireText").Cast<XmlNode>().Select(p => int.Parse(p.Attributes["Index"].Value)).Max();
            XmlNodeList xnl = xml.SelectNodes("/memoire/momoireText[@Index='" + index.ToString() + "']/mItems");

            //读取XML文件 并加载
            foreach (XmlNode item in xnl)
            {
                //string text = item.Attributes["Seq"].InnerText;
                string value = item.Attributes["Content"].Value;
                rbox_1.AppendText(value + "\r\n");
            }

            //xml.RemoveAll();
        }

        /// <summary>
        /// 程序退出时 自动保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CreateXML();
        }

        /// <summary>
        /// 键盘监听 按下control + Q 时 保存并退出  按下control + S 时 保存文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.Q))
            {
                Close();
            }
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.S))
            {
                CreateXML();
                dispatcherTimer.Stop();
            }
        }

        /// <summary>
        /// 每次文本内容改变时  开启定时器 5 分钟后自动保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbox_1_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            isChange = true;
            dispatcherTimer.Start();
        }
    }
}

