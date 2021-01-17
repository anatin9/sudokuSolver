using SudokuWithGUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Sudoku
{
    public class Board
    {
        public int n { get; set; }
        public int subDivN { get; set; }
        public char[] vals { get; set; }
        public Cell[,] board { get; set; }
        public bool valid { get; set; }
        public string reason { get; set; }

        /*
         * default constructor for the board.
         * reads in the stream to build, and auto checks for invalid inputs
         */
        public Board(StreamReader sr)
        {
            valid = true;                   //default to true
            n = Int16.Parse(sr.ReadLine()); //dimensions of the board
            subDivN = (Int16)Math.Sqrt(n);  //dimensions of a subBoard
            string temp = sr.ReadLine();

            temp.Split(' ');                //split on spaces
            int i = 0;
            vals = new char[n];
            
            foreach (string x in temp.Split(' '))   //readin possible char choices
            {
                vals[i] = Char.Parse(x);
                i++;
            }

            CheckIfInputValid();             //verify still valid.
            board = new Cell[n, n];
            string[] inarr;
            for(i=0; i<n; i++)               //read in the board
            {
                if (sr.EndOfStream)            //if not at end of stream return;
                {
                    valid = false;
                    reason = "Invalid dimensions";
                    return;
                }
                inarr = sr.ReadLine().Split(' ');
                if (inarr.Length < n)
                {
                    valid = false;
                    reason = "Invalid dimensions";
                    return;
                }
                for (int j=0; j<n; j++)
                {
                    CheckIfInputValid(Char.Parse(inarr[j]));
                    board[i, j] = new Cell(Char.Parse(inarr[j]), i, j);
                }
            }
            if (!sr.EndOfStream)            //if not at end of stream return;
            {
                return;
            }
        }
        public Board(Board b)
        {
            n = b.n;
            subDivN = b.subDivN;
            vals = new char[n];
            int count = 0;
            foreach(char c in b.vals)
            {
                vals[count] = c;
                count++;
            }
            board = new Cell[n, n];
            foreach(Cell c in b.board)
            {
                board[c.yPos, c.xPos] = c;
            }
            valid = b.valid;
            reason = b.reason;
        }

        /*
         * set puzzle invalid if invalid parameters.
         */
        private void CheckIfInputValid()
        {
            if (n != vals.Length)
            {
                valid = false;
                reason = "Invalid dimensions";
            }
            if (n != 4 && n != 9 && n != 16 && n != 25 && n != 36)
            {
                valid = false;
                reason = "Unsupported size";
            }
        }

        /*
         * set puzzle invalid if bad char
         */
        private void CheckIfInputValid(char c)
        {
            if (!vals.Contains(c) && c!='-')
            {
                valid = false;
                reason = "Invalid character";
            }
        }

        /*
         * return true or false for a char if valid to the puzzle
         */
        public bool InputValid(char c)
        {
            if (c == '\0')
                return false;
            if (!vals.Contains(c) && c != '-')
                return false;
            return true;
        }

        public bool CheckAll(char c, int col, int row)
        {
            bool isValid = true;
            if(!CheckCol(c, col, 1) || !CheckRow(c, row, 1) || !CheckAround(c, col, row, 1 ))
            {
                isValid = false;
            }
            return isValid;
        }
        /*
         * Checks a given column for a char
         * return false if not valid entry
         * return true  if valid entry
         */
        public bool CheckCol(char c, int col, int allowed)
        {
            if (c == '\0' || col < 0 || col > (n-1))
                return false;
            int count = 0;
            for (int i=0; i<n; i++)
            {
                if (board[i, col].displayedChar == c)
                    count++;
            }
            if (count == allowed)
                return true;
            else
                return false;
        }

        /*
         * checks a given row for a char
         * return false if not valid entry
         * return true  if valid entry
         */
        public bool CheckRow(char c, int row, int allowed)
        {
            if (c == '\0' || row < 0 || row > (n - 1))
                return false;
            int count = 0;
            for(int j=0; j<n; j++)
            {
                if (board[row,j].displayedChar == c)
                    count++;
            }
            if (count == allowed)
                return true;
            else
                return false;
        }

        /*
        * checks a board subdivision for the given val
        * return false if not valid entry
        * return true  if valid entry
        */
        public bool CheckAround(char c, int col, int row, int allowed)
        {
            if (c == '\0' || col < 0 || col > (n - 1) || row < 0 || row > (n - 1))
                return false;
            int subDivXIndex = (row / subDivN);
            int subDivYIndex = (col / subDivN);
            int count = 0;
            
            for(int i=subDivXIndex*subDivN; i<subDivN*(subDivXIndex+1); i++)
            {
                for(int j=subDivYIndex*subDivN; j<subDivN*(subDivYIndex+1); j++)
                {
                    if (board[j, i].displayedChar == c)
                        count++;
                }
            }
            if (count == allowed)
                return true;
            else
                return false;
        }

        /*
         * return the number of empty cells in the passed row
         */
        public int CountEmptyRow(int row)
        {
            if (row < 0 || row > (n - 1))
                return 0;
            int count = 0;
            for(int i=0; i<n; i++)
            {
                if(board[row, i].displayedChar == '-')
                {
                    count++;
                }
            }
            return count;
        }

        /*
         * return the number of empty cells in the passed column
         */
        public int CountEmptyCol(int col)
        {
            if (col < 0 || col > (n - 1))
                return 0;
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                if (board[i, col].displayedChar == '-')
                {
                    count++;
                }
            }
            return count;
        }

        /*
         * return the number of empty cells in the subBoard of the passed cell
         */
        public int CountAround(int col, int row)
        {
            if (col < 0 || col > (n - 1) || row < 0 || row > (n - 1))
                return 0;
            int subDivXIndex = (row / subDivN);
            int subDivYIndex = (col / subDivN);
            int count = 0;
            for (int i = subDivXIndex * subDivN; i < subDivN * (subDivXIndex + 1); i++)
            {
                for (int j = subDivYIndex * subDivN; j < subDivN * (subDivYIndex + 1); j++)
                {
                    if (board[j, i].displayedChar == '-')
                        count++;
                }
            }
            return count;
        }

        /*
         * Print the board to the console.
         */
        public void DisplayBoard()
        {
            int divX = 0;
            int divY = 0;
            for(int i=0; i<n; i++)
            {
                if ((subDivN * divY) == i)
                {
                    Console.WriteLine(new String('-', n*2+subDivN)); //lines dividing the top and bottom of subBoards
                    divY++;
                }
                for(int j=0; j<n; j++)
                {
                    if ((subDivN*divX) == j)    //lines dividing left and right of subBoards
                    {
                        Console.Write("|");
                        divX++;
                    }
                    Console.Write(board[i, j].displayedChar+" "); //the value in the cell
                }
                divX = 0;
                Console.WriteLine("|");
            }
            Console.WriteLine(new String('-', n * 2 + subDivN));
        }

        /*
         * analyzes each empty cell in the board to create a list of possible choices for that cell
         */
        public void UpdateChoices()
        {
            foreach(Cell c in board)
            {
                if (c.charChoices != null)
                    c.ClearChoices();
                if (c.displayedChar == '-')
                {
                    foreach (char val in vals)
                    {
                        if(CheckAround(val, c.xPos, c.yPos, 0) && CheckCol(val, c.yPos, 0) && CheckRow(val, c.xPos, 0))
                        {
                            c.charChoices.Add(val);
                        }
                    }
                }
            }
        }
    }
}
