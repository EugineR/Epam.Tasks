using Epam.SCS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.LogicContracts
{
    public interface IFilterLogic
    {
        void AddToFilter(int skillID, RangeOfRate range);
        void RemoveFromFilter(int skillID);
        IEnumerable<KeyValuePair<int, RangeOfRate>> GetAll();
    }
}
