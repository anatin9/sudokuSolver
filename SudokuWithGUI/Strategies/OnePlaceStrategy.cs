using Sudoku;
using System.Collections.Generic;

namespace SudokuWithGUI
{
    /*
         * compare the passed cell against other cells in same column or row
         * if those other cells contain a matching value remove from list of possible choices
         * return true if a cell is updated, false otherwise
         */
    class OnePlace : Strategies
    {
        private List<Cell> emptyCells = new List<Cell>();
        private List<Cell> solvableCells = new List<Cell>();

        public override void Check(Board brd)
        {
            foreach (Cell c in brd.board)
            {
                emptyCells.Clear();
                //if there are any cells in same column add them to the list of cells to compare.
                if (brd.CountEmptyCol(c.xPos) > 0)
                    for (int i = 0; i < brd.n; i++)
                    {
                        if (brd.board[i, c.yPos].displayedChar == '-' && i != c.xPos)
                        {
                            emptyCells.Add(brd.board[i, c.yPos]);
                        }
                    }
                //if there are any cells in same row add them to the list of cells to compare.
                if (brd.CountEmptyRow(c.yPos) > 0)
                    for (int i = 0; i < brd.n; i++)
                    {
                        if (brd.board[c.xPos, i].displayedChar == '-' && i != c.yPos)
                        {
                            emptyCells.Add(brd.board[c.xPos, i]);
                        }
                    }
                if(brd.CountAround(c.xPos, c.yPos) > 0)
                {
                    int xSubDivs = c.xPos / brd.subDivN;
                    int ySubDivs = c.yPos / brd.subDivN;
                    for(int i=xSubDivs; i<xSubDivs*brd.subDivN; i++)
                    {
                        for(int j=ySubDivs; j<ySubDivs*brd.subDivN; j++)
                        {
                            if (brd.board[i, j].displayedChar == '-' && c.xPos != i && c.yPos != j)
                                emptyCells.Add(brd.board[i, j]);
                        }
                    }
                }

                //removed matching values from cells in same column
                foreach (Cell ct in emptyCells)
                {
                    foreach (char cc in ct.charChoices)
                    {
                        if (c.charChoices.Contains(cc))
                            c.charChoices.Remove(cc);
                    }
                }
                //removed matching values from cells in same row
                foreach (Cell ct in emptyCells)
                {
                    foreach (char cc in ct.charChoices)
                    {
                        if (c.charChoices.Contains(cc))
                            c.charChoices.Remove(cc);
                    }
                }
                if (c.charChoices.Count == 1)
                {
                    solvableCells.Add(c);
                }
            }
        }

        public override bool Apply(Board brd)
        {
            if (solvableCells.Count >= 1)
            {
                foreach (Cell c in solvableCells)
                {
                    c.displayedChar = c.charChoices[0];
                }
                return true;
            }
            return false;
        }
    }
}
