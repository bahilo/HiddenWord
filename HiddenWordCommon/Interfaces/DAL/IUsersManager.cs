
using HiddenWordCommon.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordCommon.Interfaces.DAL
{
    public interface IUsersManager
    {
        List<User> GetUserData();
        User GetUserById(int id);
        User GetUserByPseudo(string pseudo);
        void InsertUser(string pseudo);
        List<Statistic> getUsersStatistics();
        User DeleteUser(User user);
        User UpdateUser(User user);
    }
}
