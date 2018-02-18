using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lindsay.Core.Web
{
    public class WebServer
    {
        private IDisposable _webApplication;
        public void Start()
        {
            _webApplication = WebApp.Start<Startup>("http://*");
        }
    }
}