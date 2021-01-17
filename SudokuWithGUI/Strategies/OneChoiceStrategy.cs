using Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuWithGUI
{
    /*
 * if there is only one possible choice available to a cell, verify if it's a valid choice and insert.
 */
    class OneChoiceStrategy : Strategies
    {
        private List<Cell> list = new List<Cell>();

        public override void Check(Board b)
        {
            b.UpdateChoices();
            foreach (Cell c in b.board)
            {
                if (c.displayedChar == '-' && c.charChoices.Count == 1)
                    list.Add(c);
            }
        }

        public override bool Apply(Board b)
        {
            if (list.Count >= 1)
            {
                foreach (Cell c in list)
                {
                    c.displayedChar = c.charChoices[0];
                    //b.DisplayBoard();
                }
                return true;
            }
            return false;
        }
    }
}
