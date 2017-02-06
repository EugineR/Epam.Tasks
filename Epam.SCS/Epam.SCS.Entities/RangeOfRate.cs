using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.SCS.Entities
{
    public class RangeOfRate
    {
        public RangeOfRate(int minValue,int maxValue)
        {
            if (minValue>maxValue)
            {
                throw new ArgumentException("Min value must lower max value of rate.");
            }
            this.MinValue = minValue;
            this.MaxValue = maxValue;

        }
        public int MinValue { get; }
        public int MaxValue { get; }
    }
}
