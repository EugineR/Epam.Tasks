using Epam.SCS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.DalContracts
{
    public interface ISkillDao
    {
        bool Add(Skill skill);

        IEnumerable<Skill> GetAll();

        IEnumerable<Skill> GetSkillsByIDs(int[] IDs);

        int GetMaxId();

        bool Contains(int id);

        bool Remove(int id);

        Skill GetByID(int id);

        bool Update(int id, Skill skill);
        int GetRateById(int id);

    }
}