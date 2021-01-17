using SudokuWithGUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Sudoku
{
    public class SolverLoop
    {
        public Cell[,] InitialBoard { get; set; }
        public Board brd;
        private int oneChoiceUses = 0;
        private int onePlaceUses = 0;
        private int guessUses = 0;
        private long onePlaceTime = 0;
        private long oneChoiceTime = 0;
        private long guessTime = 0;
        private long totalTime = 0;
        private bool changes = true;
        private Strategies st;

        /*
         * read in the file from stream and instantiate everything
         * attempt to solve the puzzle
         */
        public bool Run(Board board)
        {
            brd = board;
            InitialBoard = new Cell[brd.n, brd.n];
            brd.DisplayBoard();
            
            for(int i=0; i<brd.n; i++)
            {
                for(int j=0; j<brd.n; j++)
                {
                    InitialBoard[i, j] = brd.board[i, j];
                }
            }
               
            //if the board is still valid and a change has been made keep trying.
            while (changes && brd.valid)
            {
                //reset changes and update the list of choices for each cell.
                changes = false;
                brd.UpdateChoices();
                //if there are no choices at the current cell skip.

                
                changes = OneChoice(brd);
                    if (changes) continue;
                changes = OnePlace(brd);
                    if (changes) continue;
                changes = Guess(brd);
            }
            
            //check if any empty cells remaining. change to false if not completely solved.
            foreach (Cell c in brd.board)
            {
                if(c.displayedChar =='-' && brd.valid)
                {
                    brd.valid = false;
                    brd.reason = "Couldn't solve";
                    break;
                }
            }
            brd.DisplayBoard();
            return brd.valid;
        }
        
        private bool OneChoice(Board brd)
        {
            bool temp;
            Strategies st = new OneChoiceStrategy();
            temp = st.Run(brd);
            if (temp) oneChoiceUses++;
            oneChoiceTime += st.timer.ElapsedMilliseconds;
            totalTime += st.timer.ElapsedMilliseconds;
            return temp;
        }
        private bool OnePlace(Board brd)
        {
            bool temp = false;
            Strategies st = new OnePlace();
            temp = st.Run(brd);
            if (temp) onePlaceUses++;
            onePlaceTime += st.timer.ElapsedMilliseconds;
            totalTime += st.timer.ElapsedMilliseconds;
            return temp;
        }
        private bool Guess(Board brd)
        {
            bool guessed = false;
            Strategies st = new Guess();
            guessed = st.Run(brd);
            if(guessed) guessUses++;
            totalTime += st.timer.ElapsedMilliseconds;
            guessTime += st.timer.ElapsedMilliseconds;
            return guessed;
        }
        
        
    }
}
