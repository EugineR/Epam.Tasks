using Epam.SCS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.DalContracts
{
    public interface IUserDao
    {
        bool Add(User user);
        bool AddSkillToUser(int userID, int skillID, int rate);
        bool Contains(int id);
        IEnumerable<User> GetAll();
        User GetByID(int id);
        IDictionary<Skill, int> GetUsersSkills(int ID);   
        bool RemoveUsersSkill(int userID, int skillID); 
        bool Update(int id, User user);
        bool RemoveUser(int id);
        int GetIDByAccID(int ID);
    }
}