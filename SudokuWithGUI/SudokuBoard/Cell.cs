using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuWithGUI
{
    public class Cell
    {
        public int numValidChoices { get; set; }
        public List<char> charChoices { get; set; }
        public char displayedChar { get; set; }
        public bool valid { get; set; }
        public string reason;
        public int yPos;
        public int xPos;

        public Cell(char []choices, int x, int y)
        {
            charChoices = new List<char>();
            charChoices = choices.ToList<char>();
            numValidChoices = charChoices.Count();
            displayedChar = '-';
            valid = true;
            yPos = y;
            xPos = x;
        }
        public Cell(char c, int x, int y)
        {
            charChoices = new List<char>();
            displayedChar = c;
            numValidChoices = 1;
            valid = true;
            yPos = y;
            xPos = x;
        }
        public void ClearChoices()
        {
            charChoices.Clear();
            numValidChoices = 0;
        }
        public void AddChoice(char newChoice)
        {
            charChoices.Add(newChoice);
            numValidChoices++;
        }

    }
}
