using LindsayPlugin.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LindsayPlugin.Interfaces
{
    public interface IDatabaseHandler
    {
        bool AuthenticateUser(string user, string pass);
        User GetUserData(string id);
        void CreateUser(string user, string mail, string pass);
        void DeleteUser(string id);
        void BanUser(string id, string reason = "");
        void UnbanUser(string id);
    }
}