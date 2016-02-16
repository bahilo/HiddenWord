using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenWordCommon.classes
{
    public class Statistic
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SetupId { get; set; }
        public int WordId { get; set; }
        public int NbTry { get; set; }

        public User User { get; set; }
        public Words Word { get; set; }
        public Setup Setup { get; set; }

        public List<User> UserList { get; set; }
        public List<Words> WordList { get; set; }   
        public List<Setup> SetupList { get; set; }


        

        public Statistic()
        { 
            UserList = new List<User>();
            WordList = new List<Words>();
            SetupList = new List<Setup>();

        }
       

    }
}
