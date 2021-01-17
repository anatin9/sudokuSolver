using Microsoft.Win32;
using Sudoku;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SudokuWithGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Board brd;
        private Button currentSelection;
        private Brush csColor;
        private Stopwatch timer;
        private bool finished;
        private WindowFileHandling IO;
        public MainWindow()
        {
            InitializeComponent();
            timer = new Stopwatch();
            Check.IsEnabled = false;
            Solve.IsEnabled = false;
            finished = false;
            IO = new WindowFileHandling();
        }
        /*
         * Open file dialog window to open puzzle file.
         * create board if valid puzzle, alarm user if invalid.
         */
        private void OpenFiles(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            
            openFile.Filter = "txt files| *.txt";
            openFile.Title = "Open Sudoku Puzzle";
            brd = IO.OpenFile(openFile);
            
            if (brd != null && brd.valid)
            {
                Check.IsEnabled = true;
                Solve.IsEnabled = true;
                sudokuGrid.IsEnabled = false;
                CreateGrid();
            }
        }

        private void SaveFiles(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "txt files| *.txt";
            saveFile.Title = "Save Sudoku Puzzle";
            IO.SaveFile(saveFile, brd);

        }

        private void Check_Puzzle(object sender, RoutedEventArgs e)
        {
            int empty = 0;
            brd.DisplayBoard();
            foreach(Cell c in brd.board)
            {
                if (!brd.CheckAll(c.displayedChar, c.xPos, c.yPos) && c.displayedChar != '-')
                {
                    MessageBox.Show("[" + c.xPos + " ," + c.yPos + "] -- Is incorrect");

                    return;
                }
                else if (c.displayedChar == '-')
                    empty++;
            }
            if (empty == 0)
            {
                finished = true;
                timer.Stop();
                MessageBox.Show("Puzzle finished in: "+ (timer.ElapsedMilliseconds / 1000/60).ToString() +
                    ":"+ (timer.ElapsedMilliseconds/1000).ToString()+":"+ timer.ElapsedMilliseconds.ToString());
            }
        }

        private void SolvePuzzle(object sender, RoutedEventArgs e)
        {
            sudokuGrid.IsEnabled = true;
            SolverLoop l = new SolverLoop();
            l.Run(brd);
            UpdateGrid();
        }

        private void TimerBtn(object sender, RoutedEventArgs e)
        {
            if (timer.IsRunning)
            {
                sudokuGrid.IsEnabled = false;
                timer.Stop();
            }
            else
            {
                sudokuGrid.IsEnabled = true;
                timer.Start();
            }
        }

        private void UpdateGrid()
        {
            int x=0, y=0;
            foreach(Button b in sudokuGrid.Children)
            {
                b.Content = brd.board[y, x].displayedChar;
                x++;
                if (x == brd.n)
                {
                    x = 0;
                    y++;
                }
            }
        }
        private void UpdateBoard()
        {
            int x=0, y=0, ind;
            ind = sudokuGrid.Children.IndexOf(currentSelection);
            while (ind - (brd.n*(x+1)) >= 0 && x<brd.n)
            {
                x++;
            }
            y = ind-x*brd.n;
            brd.board[x, y].displayedChar = (char)currentSelection.Content;
        }

        private void CreateGrid()
        {
            sudokuGrid.Children.Clear();
            sudokuGrid.ShowGridLines = true;
            sudokuGrid.ColumnDefinitions.Clear();
            sudokuGrid.RowDefinitions.Clear();
            //add grid and rows to board
            for(int i =0; i< brd.n; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                RowDefinition rd = new RowDefinition();
                sudokuGrid.ColumnDefinitions.Add(cd);
                sudokuGrid.RowDefinitions.Add(rd);                
            }
            int count = 1;
            bool fill = true;
            for(int i=0; i<brd.n; i++)
            {
                if (i % brd.subDivN == 0)
                    fill = !fill;
                for (int j=0; j<brd.n; j++)
                {
                    Button uiButton = new Button();
                    uiButton.Content = brd.board[i, j].displayedChar;
                    uiButton.Name = "Button" + count.ToString();
                    if (brd.board[i, j].displayedChar == '-')
                        uiButton.Click += new RoutedEventHandler(Button_Click);
                    if (j % brd.subDivN == 0)
                        fill = !fill;
                    if (fill)
                        uiButton.Background = Brushes.Aquamarine;
                    Grid.SetColumn(uiButton, j);
                    Grid.SetRow(uiButton, i);
                    sudokuGrid.Children.Add(uiButton);
                    count++;
                }
                if(brd.subDivN%2==1)
                    fill = !fill;
            }
            CreateOptions();
        }

        private void CreateOptions()
        {
            int count = 1;
            cellChoices.ColumnDefinitions.Clear();
            cellChoices.RowDefinitions.Clear();
            RowDefinition rd = new RowDefinition();
            cellChoices.RowDefinitions.Add(rd);
            for(int i=0; i<brd.n; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cellChoices.ColumnDefinitions.Add(cd);
            }
            foreach(char c in brd.vals)
            {
                Button choiceButton = new Button();
                choiceButton.Content = c;
                choiceButton.Background = Brushes.Orange;
                choiceButton.Name = "ChoiceBtn" + count.ToString();
                choiceButton.Click += new RoutedEventHandler(Choice_Click);
                Grid.SetColumn(choiceButton, count - 1);
                Grid.SetRow(choiceButton, 0);
                cellChoices.Children.Add(choiceButton);
                count++;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if(currentSelection!=null)
                currentSelection.Background = csColor;
            currentSelection = (Button)sender;
            csColor = currentSelection.Background;
            currentSelection.Background = Brushes.Red;
        }

        private void Choice_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            currentSelection.Background = csColor;
            currentSelection.Content = b.Content;
            UpdateBoard();
        }


    }
}
