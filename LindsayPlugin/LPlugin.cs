using LindsayPlugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LindsayPlugin
{
    public interface LPlugin
    {
        void Load(IPluginManager _manager);
        void Unload();
    }
    public class PluginNameAttribute : Attribute
    {
        string name;
        public PluginNameAttribute(string name)
        {
            this.name = name;
        }
        public string Name
        {
            get
            {
                return name;
            }
        }
    }
    public class PluginDescriptionAttribute : Attribute
    {
        string description;
        public PluginDescriptionAttribute(string description)
        {
            this.description = description;
        }
        public string Description
        {
            get
            {
                return description;
            }
        }
    }
    public class PluginAuthorAttribute : Attribute
    {
        string author;
        public PluginAuthorAttribute(string author)
        {
            this.author = author;
        }
        public string Author
        {
            get
            {
                return author;
            }
        }
    }
    public class PluginTypeAttribute : Attribute
    {
        Type type;
        public PluginTypeAttribute(Type type)
        {
            this.type = type;
        }
        public Type PluginType
        {
            get
            {
                return type;
            }
        }
    }
}