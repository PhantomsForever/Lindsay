using LindsayPlugin;
using LindsayPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lindsay.Core.Plugin
{
    public class PluginManager : MarshalByRefObject, IPluginManager
    {
        private List<string> PossiblePlugins = new List<string>();
        private Dictionary<string, Plugin> Plugins = new Dictionary<string, Plugin>();
        private PluginSandbox sandbox;
        private static string PluginPath
        {
            get
            {
                return Path.Combine(Application.StartupPath, "Plugins");
            }
        }
        public PluginManager()
        {
            if (!Directory.Exists(PluginPath))
                Directory.CreateDirectory(PluginPath);
            sandbox = new PluginSandbox();
            foreach(var file in Directory.GetFiles(PluginPath, "*.dll"))
            {
                PossiblePlugins.Add(file);
            }
        }
        public void ReadPlugins()
        {
            foreach(var plugin in PossiblePlugins)
            {
                PossiblePlugins.Remove(plugin);
                var asm = Assembly.Load(plugin);
                var Plugin = new Plugin();
                foreach(var attr in asm.GetCustomAttributes())
                {
                    if(attr is PluginNameAttribute)
                    {
                        Plugin.Name = (attr as PluginNameAttribute).Name;
                    }
                    else if(attr is PluginDescriptionAttribute)
                    {
                        Plugin.Description = (attr as PluginDescriptionAttribute).Description;
                    }
                    else if(attr is PluginAuthorAttribute)
                    {
                        Plugin.Author = (attr as PluginAuthorAttribute).Author;
                    }
                    else if (attr is PluginTypeAttribute)
                    {
                        Plugin.EntryType = (attr as PluginTypeAttribute).PluginType as LPlugin;
                    }
                }
                if (string.IsNullOrEmpty(Plugin.Name))
                    continue;
                Plugin.File = plugin;
                Plugins.Add(Plugin.Name, Plugin);
            }
        }
        public void LoadPlugin(Plugin plugin)
        {
            var domain = sandbox.CreateSandboxDomain(plugin);
            LPlugin type = null;
            try
            {
                domain.AssemblyResolve += Domain_AssemblyResolve;
                domain.UnhandledException += Domain_UnhandledException;
                type = domain.CreateInstanceAndUnwrap(plugin.File, (plugin.EntryType as Type).FullName) as LPlugin;
                type.Load(this);
            }
            catch(Exception)
            {

            }
            finally
            {

            }
        }

        private void Domain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }

        private Assembly Domain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}