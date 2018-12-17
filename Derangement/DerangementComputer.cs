using System.Collections.Generic;

namespace Derangement
{
    public class DerangementComputer
    {
        public ulong GetDerangement(ulong n)
        {
            if (n == 0)
                return 1;
            else if (n == 1)
                return 0;
            else
                return (n - 1) * (GetDerangement(n - 1) + GetDerangement(n - 2));
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
