using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public static class LongExtensions
    {
        public static DateTime ToDateTime(this long unixTimeStamp)
        {
            return new DateTime(1970, 1, 1).AddSeconds(unixTimeStamp);
        }
    }
}
