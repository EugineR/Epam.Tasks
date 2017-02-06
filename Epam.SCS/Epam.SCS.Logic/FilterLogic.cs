using Epam.SCS.LogicContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.SCS.Entities;
using Epam.SCS.DalContracts;
using Epam.SCS.Dal.DBDao;

namespace Epam.SCS.Logic
{
    public class FilterLogic : IFilterLogic
    {
        private Filter filter;
        
        public FilterLogic()
        { 
            filter = new Filter();
        }
        public void AddToFilter(int skillID, RangeOfRate range)
        {
            try
            {
                filter.Filters.Add(skillID, range);
            }
            catch (Exception)
            {
                throw new ArgumentException("Filter by this criterion has already been added");
            }
        }

        public void RemoveFromFilter(int skillID)
        {
            try
            {
                filter.Filters.Remove(skillID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<KeyValuePair<int, RangeOfRate>> GetAll()
        {
            foreach (KeyValuePair<int, RangeOfRate> kvp in filter.Filters)
            {
                yield return new KeyValuePair<int, RangeOfRate>(kvp.Key, kvp.Value);
            }
        }       

    }
}
