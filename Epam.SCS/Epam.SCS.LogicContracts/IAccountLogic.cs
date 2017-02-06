using Epam.SCS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.LogicContracts
{
    public interface IAccountLogic
    {
        bool CanRegister(string login);

        bool CanLogin(string login, string password);

        string GetUsersRole(string login);

        Account GetByID(int id);

        bool Contains(int id);

        bool ChangeRole(int id);

        Account[] GetAll();
        int GetIdByLogin(string login);
    }
}