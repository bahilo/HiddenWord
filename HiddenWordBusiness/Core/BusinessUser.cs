using HiddenWordCommon.classes;
using HiddenWordCommon.Interfaces.DAL;
using HiddenWordDALXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordBusiness.Core
{
    public class BusinessUser : HiddenWordCommon.Interfaces.Business.IUsersManager
    {
        public IDALActionManager DALAccess { get; set; }

        public BusinessUser(IDALActionManager DAL)
        {
            DALAccess = DAL;
        }

        //---------------[ The User implementation ] --------------------

        public User GetUserById(int id)
        {
            return DALAccess.GetUserById(id);
        }

        public User GetUserByPseudo(string pseudo)
        {
            return DALAccess.GetUserByPseudo(pseudo);
        }

        public List<User> GetUserData()
        {
            return DALAccess.GetUserData();
        }

        public void InsertUser(string pseudo)
        {
            DALAccess.InsertUser(pseudo);
        }

        public User DeleteUser(User user)
        {
            return DALAccess.DeleteUser(user);
        }

        public User UpdateUser(User user)
        {
            return DALAccess.UpdateUser(user);
        }

        

    }
}
