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
        public IDALActionManager DALAcess { get; set; }

        public BusinessUser(IDALActionManager DAL)
        {
            DALAcess = DAL;
        }

        //---------------[ The User implementation ] --------------------

        public User GetUserById(int id)
        {
            return DALAcess.GetUserById(id);
        }

        public User GetUserByPseudo(string pseudo)
        {
            return DALAcess.GetUserByPseudo(pseudo);
        }

        public List<User> GetUserData()
        {
            return DALAcess.GetUserData();
        }

        public void InsertUser(string pseudo)
        {
            DALAcess.InsertUser(pseudo);
        }

        public User DeleteUser(User user)
        {
            return DALAcess.DeleteUser(user);
        }

        public User UpdateUser(User user)
        {
            return DALAcess.UpdateUser(user);
        }

        

    }
}
