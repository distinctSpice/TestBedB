using System;
using System.Collections.Generic;
using System.Linq;

namespace NQueensProblem
{
    public class Checker
    {
        private readonly int[] _positions;
        private readonly List<int> _flaggedIndexes;
        private bool _validDiagonals;

        public Checker(params int[] positions)
        {
            _positions = positions;
            _flaggedIndexes = new List<int>();
        }

        public bool Check() => CheckForDuplicateRows() && CheckForDiagonals(_positions, true);

        private bool CheckForDuplicateRows() => _positions.Distinct().Count() == _positions.Length;

        // TODO: Can be optimized further?
        private bool CheckForDiagonals(int[] positions, bool flagIndex)
        {
            for (int i = 0; i < positions.Length; i++)
                for (int j = 0; j < positions.Length; j++)
                {
                    if (i == j || Math.Abs(j - i) != Math.Abs(positions[j] - positions[i]))
                        continue;
                    if (flagIndex)
                        FlagIndex(i);
                    _validDiagonals = false;
                    return _validDiagonals;
                }

            return true;
        }

        private void FlagIndex(int index)
        {
            if (!_flaggedIndexes.Contains(index))
                _flaggedIndexes.Add(index);
        }

        private int[] SwapIndexes(int a, int b)
        {
            var copy = (int[])_positions.Clone();
            int temp = copy[a];
            copy[a] = copy[b];
            copy[b] = temp;
            return copy;
        }

        public int[] TryToFixDiagonalDefects()
        {
            if (_validDiagonals)
                throw new InvalidOperationException("Sequence is already valid");

            // Brute force
            for (int i = 0; i < _flaggedIndexes.Count; i++)
                for (int j = 0; j < _positions.Length; j++)
                {
                    if (i == j) continue;
                    var copy = SwapIndexes(i, j);
                    if (CheckForDiagonals(copy, false))
                        return copy;
                }
            throw new ArgumentException("Sequence can't be fixed");
        }
    }
}