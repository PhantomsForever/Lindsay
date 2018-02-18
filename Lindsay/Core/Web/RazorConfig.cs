using Nancy.ViewEngines.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lindsay.Core.Web
{
    public class RazorConfig : IRazorConfiguration
    {
        public IEnumerable<string> GetAssemblyNames()
        {
            yield return "Lindsay";
        }
        public IEnumerable<string> GetDefaultNamespaces()
        {
            yield return "Lindsay.Core.Web.Models";
        }
        public bool AutoIncludeModelNamespace
        {
            get
            {
                return true;
            }
        }
    }
}