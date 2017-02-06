using Epam.SCS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.LogicContracts
{
    public interface ISkillLogic
    {
        IEnumerable<Skill> GetAll();

        IEnumerable<Skill> GetSkillsByIds(int[] IDs);

        Skill GetById(int id);

        bool Save(Skill newSkill);

        int GetMaxId();

        bool Delete(int id);

        bool Contains(int id);

        bool Update(int id, Skill skill);

        int GetRateById(int id);
    }
}
