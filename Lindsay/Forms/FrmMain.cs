using Lindsay.Core.Plugin;
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
        private PluginManager _manager;
        public FrmMain()
        {
            InitializeComponent();
            _manager = new PluginManager();
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
    }
}
