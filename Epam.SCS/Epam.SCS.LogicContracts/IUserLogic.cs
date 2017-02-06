using Epam.SCS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.LogicContracts
{
    public interface IUserLogic
    {
        void Add(User user);
        bool AddSkillToUser(int userID, int skillID, int rate);
        bool Contains(int id);
        IEnumerable<User> GetAll();
        User GetByID(int id); 
        bool Update(int id,User user);
        bool RemoveUsersSkill(int userID, int skillID);
        bool RemoveUser(int id);
        void CreateReportAboutAllUsers();
        int GetIDByAccID(int ID);
    }
}