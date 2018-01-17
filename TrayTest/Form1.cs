using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrayTest
{
    public partial class Form1 : Window
    {
        private NotifyIcon notifyIcon = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)  
        {  
            //初始化图片属性  
            notifyIcon.Icon = new Icon("Icon1.ico");  
            //初始化是不可见的  
            notifyIcon.Visible = true;  
            //为notifyIcon添加Click事件  
            notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);  
            //为当前窗体添加窗体形状改变响应函数  
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);  
        }  
  
        private void Form1_SizeChanged(object sender, EventArgs e)  
        {  
            //如果当前状态的状态为最小化，则显示状态栏的程序托盘  
            if (this.WindowState == FormWindowState.Minimized)  
            {  
                //不在Window任务栏中显示  
                this.ShowInTaskbar = false;  
                //使图标在状态栏中显示  
                this.notifyIcon.Visible = true;  
            }  
        }  
  
        private void notifyIcon_Click(object sender, EventArgs e)  
        {  
            //设置窗体为正常状态  
            this.WindowState = FormWindowState.Normal;  
            //激活窗体  
            this.Activate();  
            //托盘设置为不可见  
            this.notifyIcon.Visible = false;  
            //程序在Window任务栏中显示  
            this.ShowInTaskbar = true;  
        }

        private void InitialTray()
        {
            //隐藏主窗体
            this.Visibility = Visibility.Hidden;

            //设置托盘的各个属性
            notifyIcon = new NotifyIcon();
            notifyIcon.BalloonTipText = "systray runnning...";
            notifyIcon.Text = "systray";
            notifyIcon.Icon = new System.Drawing.Icon("http://www.cnblogs.com/res/spring.ico");
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(2000);
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);

            //设置菜单项
            MenuItem setting1 = new MenuItem("setting1");
            MenuItem setting2 = new MenuItem("setting2");
            MenuItem setting = new MenuItem("setting", new MenuItem[] { setting1, setting2 });

            //帮助选项
            MenuItem help = new MenuItem("help");

            //关于选项
            MenuItem about = new MenuItem("about");

            //退出菜单项
            MenuItem exit = new MenuItem("exit");
            exit.Click += new EventHandler(exit_Click);

            //关联托盘控件
            MenuItem[] childen = new MenuItem[] { setting, help, about, exit };
            notifyIcon.ContextMenu = new ContextMenu(childen);

            //窗体状态改变时候触发
            this.StateChanged += new EventHandler(SysTray_StateChanged);
        }

        /// <summary>
        /// 鼠标单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //如果鼠标左键单击
            if (e.Button == MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    this.Activate();
                }
            }
        }

        /// <summary>
        /// 窗体状态改变时候触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SysTray_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Visibility = Visibility.Hidden;
            }
        }


        /// <summary>
        /// 退出选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, EventArgs e)
        {
            if (System.Windows.MessageBox.Show("sure to exit?",
                                               "application",
                                                MessageBoxButton.YesNo,
                                                MessageBoxImage.Question,
                                                MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
