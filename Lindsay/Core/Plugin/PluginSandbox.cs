using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lindsay.Core.Plugin
{
    public class PluginSandbox
    {
        public AppDomain CreateSandboxDomain(Plugin plugin, SecurityZone zone = SecurityZone.Untrusted)
        {
            var setup = new AppDomainSetup
            {
                ApplicationName = plugin.Name,
                ApplicationBase = Path.GetFullPath(Application.StartupPath),
                DynamicBase = Path.GetFullPath((plugin.EntryType as Type).Assembly.Location)
            };
            var evidence = new Evidence();
            evidence.AddHostEvidence(new Zone(zone));
            var permissions = GetPermissions(plugin, SecurityManager.GetStandardSandbox(evidence));
            var strongName = typeof(PluginSandbox).Assembly.Evidence.GetHostEvidence<StrongName>();
            return AppDomain.CreateDomain(plugin.Name, null, setup, permissions, strongName);
        }
        private PermissionSet GetPermissions(Plugin plugin, PermissionSet set)
        {
            set.AddPermission(new ReflectionPermission(PermissionState.Unrestricted));
            set.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            set.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, Path.Combine(Application.StartupPath, "Plugins")));
            return set;
        }
    }
}