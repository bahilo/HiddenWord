
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiddenWordCommon.classes
{
    public class User
    {
        public string Pseudo { get; set; }
        public int Id { get; set; }
        public  List<Statistic> UserStats { get; set; }
        public List<Words> UserWordsStats { get; set; }

        public User()
        {
            UserStats = new List<Statistic>();
            //setupUserStat(this.id);
        }  
    }
}
