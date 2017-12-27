using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// 加载superScoket
using SuperSocket.Common;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketEngine;
using SuperSocket.SocketEngine.Configuration;

namespace ServicesLoader
{
    public partial class ServiceLoaderForm : Form
    {
        public ServiceLoaderForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult ret = this.openFileDialog.ShowDialog();
            if (ret == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = this.openFileDialog.FileName;
                if (!this.textBox1.Text.EndsWith(".dll"))
                {
                    MessageBox.Show("加载的 : " + this.textBox1.Text + "不合法!");
                    this.textBox1.Text = "";
                    return;
                }

                IWorkItem curServerWorkItem;
                try
                {
                    var serviceType = Type.GetType("LibServer.GameServer, LibServer", true);
                    curServerWorkItem = Activator.CreateInstance(serviceType) as IWorkItem;
                }
                catch {
                    return;
                }
                
                if (curServerWorkItem.Start())
                {
                    MessageBox.Show("服务器启动成功");
                }
                
            }
            
        }

    }
}
