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
    public class SkillLogic : ISkillLogic
    {

        private ISkillDao skillDao;
        public SkillLogic()
        {
            skillDao = DaoProvider.SkillDao;
        }
        public bool Contains(int id)
        {
            return skillDao.Contains(id);
        }

        public bool Delete(int id)
        {           
            return skillDao.Remove(id);
        }

        public IEnumerable<Skill> GetAll()
        {
            return skillDao.GetAll().ToArray();
        }

        public Skill GetById(int id)
        {
            return skillDao.GetByID(id);
        }

        public int GetMaxId()
        {
            return skillDao.GetMaxId();
        }

        public int GetRateById(int id)
        {
            return skillDao.GetRateById(id);
        }

        public IEnumerable<Skill> GetSkillsByIds(int[] IDs)
        {             
            if (IDs.Length == 0)
            {
                return new Skill[0];
            }

            return skillDao.GetSkillsByIDs(IDs).ToArray();
        }

        public bool Save(Skill newSkill)
        {           
            return skillDao.Add(newSkill);            
        }

        public bool Update(int id, Skill skill)
        {
            return skillDao.Update(id, skill);
        }
    }
}
