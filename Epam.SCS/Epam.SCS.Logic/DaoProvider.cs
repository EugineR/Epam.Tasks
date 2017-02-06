using Epam.SCS.DalContracts;
using Epam.SCS.Dal.DBDao;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.Logic
{
    internal class DaoProvider
    {
        static DaoProvider()
        {
            if (ConfigurationManager.AppSettings["DaoMode"] == "DB")
            {
                UserDao = new DBUserDao();
                SkillDao = new DBSkillDao();
                AccountDao = new DBAccountDao();
                PhotoDao = new DBPhotoDao();
            }
        }
        
        public static IUserDao UserDao { get; }
        public static ISkillDao SkillDao { get; }
        public static IAccountDao AccountDao { get; }
        public static IPhotoDao PhotoDao { get; }
    }
}
