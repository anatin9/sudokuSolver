using SudokuWithGUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sudoku
{
    /*
     * The template
     */
    abstract class Strategies
    {
        public Stopwatch timer = new Stopwatch();
        public abstract void Check(Board b);
        public abstract bool Apply(Board b);

        public bool Run(Board b)
        {
            timer.Start();
            Check(b);
            timer.Stop();
            return (Apply(b));
        }
    }
}
