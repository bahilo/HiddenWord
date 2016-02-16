using HiddenWordCommon.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordCommon.Interfaces.Business
{
    public interface IUsersManager
    {
        List<User> GetUserData();
        User GetUserById(int id);
        User GetUserByPseudo(string pseudo);
        void InsertUser(string pseudo);
        User DeleteUser(User user);
        User UpdateUser(User user);
        //User SelectUser();
    }
}
