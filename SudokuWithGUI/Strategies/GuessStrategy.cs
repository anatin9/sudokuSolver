using Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuWithGUI
{
    /*
     * if there are 2 choices left for a cell, choose the first choice.
     * else do nothing, return true if changes made
     */
    class Guess : Strategies
    {
        private bool changes;

        public override void Check(Board brd)
        {
            changes = false;
            List<char> list;
            foreach (Cell c in brd.board)
            {
                list = new List<char>(c.charChoices);
                if (c.charChoices.Count < (brd.n / 4))
                {
                    foreach (char v in list)
                    {
                        Board temp = new Board(brd);
                        temp.board[c.yPos, c.xPos].displayedChar = v;
                        SolverLoop l = new SolverLoop();
                        if (l.Run(temp))
                        {
                            c.displayedChar = v;
                            changes = true;
                            break;
                        }
                    }

                }
                if (changes)
                    break;
            }
        }

        public override bool Apply(Board brd)
        { //update value if a valid choice and return true
            return changes;
        }
    }
}
