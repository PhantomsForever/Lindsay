using Lindsay.Core.Plugin;
using Lindsay.Core.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lindsay.Forms
{
    public partial class FrmMain : Form
    {
        public static FrmMain Instance { get; private set; }
        public WebServer wServer { get; private set; }
        public PluginManager _manager { get; private set; }
        public FrmMain()
        {
            InitializeComponent();
            Instance = this;
            _manager = new PluginManager();
            wServer = new WebServer();
            _manager.ReadPlugins();
            foreach(var plugin in _manager.Plugins)
            {
                var lvi = new ListViewItem(new string[]
                {
                    plugin.Key, plugin.Value.Description, plugin.Value.Author
                })
                { Tag = plugin.Value };
                listView1.Items.Add(lvi);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("You must select a plugin");
                return;
            }
            var plugin = listView1.SelectedItems.Cast<ListViewItem>().Where(lvi => lvi != null).Select(lvi => lvi.Tag as Plugin).First();
            _manager.LoadPlugin(plugin);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            wServer.Start();
        }
    }
}
