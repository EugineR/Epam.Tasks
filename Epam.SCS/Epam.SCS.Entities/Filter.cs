using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.Entities
{
    public class Filter
    {
        public IDictionary<int, RangeOfRate> Filters { get; }
        public Filter()
        {
            Filters = new Dictionary<int, RangeOfRate>();
        } 
    }
}
