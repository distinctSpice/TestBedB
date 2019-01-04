using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Derangement
{
    public class DerangementComputer
    {
        // Memoization
        private Dictionary<ulong, ulong> _lookUp = new Dictionary<ulong, ulong>();

        public ulong GetDerangement(ulong n)
        {
            if (_lookUp.ContainsKey(n))
                return _lookUp[n];
            else if (n == 0)
                return 1;
            else if (n == 1)
                return 0;
            else
            {
                var result = (n - 1) * (GetDerangement(n - 1) + GetDerangement(n - 2));
                _lookUp[n] = result;
                return result;
            }

            //Code Golf
            //return _lookUp.ContainsKey(n) ? _lookUp[n] : n == 0 ? 1 : n == 1 ? 0 : (n - 1) * (GetDerangement(n - 1) + GetDerangement(n - 2));
        }

        public IEnumerable<ulong> GetDerangements(IEnumerable<ulong> ns)
        {
            foreach (var n in ns)
            {
                yield return GetDerangement(n);
            }
        }
    }
}