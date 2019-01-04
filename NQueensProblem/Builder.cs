using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NQueensProblem
{
    public class Builder
    {
        private readonly int _range;
        public readonly int[,] Board;

        public Builder(int range)
        {
            _range = range;
            Board = new int[_range, _range];
        }

        private bool CanPlaceQueen(int row, int col)
        {
            int i, j;
            for (i = 0; i < col; i++)
            {
                if (Board[row, i] == 1) return false;
            }
            for (i = row, j = col; i >= 0 && j >= 0; i--, j--)
            {
                if (Board[i, j] == 1) return false;
            }
            for (i = row, j = col; j >= 0 && i < _range; i++, j--)
            {
                if (Board[i, j] == 1) return false;
            }
            return true;
        }

        public bool Build(int col)
        {
            if (col >= _range) return true;
            for (int i = 0; i < _range; i++)
            {
                if (CanPlaceQueen(i, col))
                {
                    Board[i, col] = 1;
                    if (Build(col + 1)) return true;
                    // Backtracking is hella important in this one.  
                    Board[i, col] = 0;
                }
            }
            return false;
        }
    }
}
