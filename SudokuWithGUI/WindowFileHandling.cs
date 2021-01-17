using Microsoft.Win32;
using Sudoku;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SudokuWithGUI
{
    public class WindowFileHandling
    {
        public Board OpenFile(OpenFileDialog fileChooser)
        {
            Board brd = null;
            if (fileChooser.ShowDialog() == true)
            {
                Stream fileStream = fileChooser.OpenFile();
                StreamReader sr = new StreamReader(fileStream);
                brd = new Board(sr);

                if (brd != null && !brd.valid)
                {
                    MessageBox.Show("Invalid board: " + brd.reason);
                }
                if (brd == null)
                {
                    MessageBox.Show("Couldn't open the file");
                }
            }
            return brd;
        }

        internal void SaveFile(SaveFileDialog saveFile, Board brd)
        {
            saveFile.ShowDialog();
            if(saveFile.FileName != "")
            {
                var fs = saveFile.OpenFile();
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(brd.n);
                foreach (char c in brd.vals)
                    sw.Write(c + " ");
                sw.WriteLine();
                for(int i=0; i<brd.n; i++)
                {
                    for(int j=0; j<brd.n; j++)
                    {
                        sw.Write(brd.board[i, j].displayedChar + " ");
                    }
                    sw.WriteLine();
                }
                sw.Close();
                fs.Close();
            }
        }
    }
}
