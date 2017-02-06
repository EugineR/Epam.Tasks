using Epam.SCS.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.SCS.Entities;
using Epam.SCS.DalContracts;

namespace Epam.SCS.Logic
{
    public class AccountLogic : IAccountLogic
    {
        private IAccountDao accountDao;

        public AccountLogic()
        {
            accountDao = DaoProvider.AccountDao;
        }       

        public bool CanRegister(string login)
        {
            return accountDao.CanRegister(login);
        }

        public bool CanLogin(string login, string password)
        {
            return accountDao.CheckUser(login, GetPasswordsHash(password));
        }

        public string GetUsersRole(string login)
        {
            return accountDao.GetRole(login);
        }

        

        public Account GetByID(int id)
        {
            return accountDao.GetByID(id);
        }

        public bool Contains(int id)
        {
            return accountDao.Contains(id);
        }

        public bool ChangeRole(int id)
        {
            return accountDao.ChangeRole(id);
        }

        public Account[] GetAll()
        {
            return accountDao.GetAll().ToArray();
        }
        public int GetIdByLogin(string login)
        {
            return accountDao.GetIDByLogin(login);
        }

        private static string GetPasswordsHash(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            using (var hash = System.Security.Cryptography.SHA512.Create())
            {
                var hashedInputBytes = hash.ComputeHash(bytes);

                var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                foreach (var b in hashedInputBytes)
                    hashedInputStringBuilder.Append(b.ToString("X2"));
                return hashedInputStringBuilder.ToString();
            }
        }
    }
}
