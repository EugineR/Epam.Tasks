using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.Entities
{
    public class SkillEqualityComprarer : IEqualityComparer<Skill>
    {
        public bool Equals(Skill x, Skill y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Skill obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}