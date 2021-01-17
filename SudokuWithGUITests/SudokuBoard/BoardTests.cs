using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Tests
{
    [TestClass()]
    public class BoardTests
    {
        Board brd;
        //change this to directory of puzzles
        private static string puzzlePath = "C:/Users/anati/Documents/cs5700/cs5700f18-shared/hw4b/SamplePuzzles/Input/";
        StreamReader sr = new StreamReader(puzzlePath + "Puzzle-4x4-0001.txt");

        //test valid creation from stream
        [TestMethod()]
        public void BoardTest()
        {
            //4x4
            brd = new Board(sr);
            Assert.AreEqual(brd.n, 4);
            Assert.AreEqual(brd.subDivN, 2);
            Assert.AreEqual(brd.valid, true);
            Assert.AreEqual(brd.board[0, 0].displayedChar, '2');
            foreach(char c in brd.vals)
            {
                Assert.IsNotNull(c);
            }
            Assert.IsNull(brd.reason);
            Assert.IsNotNull(brd.board);
            Assert.AreEqual(brd.board.Length, 16);
            //9x9
            sr = new StreamReader(puzzlePath + "Puzzle-9x9-0001.txt");
            brd = new Board(sr);
            Assert.AreEqual(brd.n, 9);
            Assert.AreEqual(brd.subDivN, 3);
            Assert.AreEqual(brd.valid, true);
            Assert.AreEqual(brd.board[0, 0].displayedChar, '4');
            foreach (char c in brd.vals)
            {
                Assert.IsNotNull(c);
            }
            Assert.IsNull(brd.reason);
            Assert.IsNotNull(brd.board);
            Assert.AreEqual(brd.board.Length, 81);
            //25x25
            sr = new StreamReader(puzzlePath + "Puzzle-25x25-0101.txt");
            brd = new Board(sr);
            Assert.AreEqual(brd.n, 25);
            Assert.AreEqual(brd.subDivN, 5);
            Assert.AreEqual(brd.valid, true);
            Assert.AreEqual(brd.board[0, 0].displayedChar, '-');
            foreach (char c in brd.vals)
            {
                Assert.IsNotNull(c);
            }
            Assert.IsNull(brd.reason);
            Assert.IsNotNull(brd.board);
            Assert.AreEqual(brd.board.Length, 625);
        }

        //test valid copy from board
        [TestMethod()]
        public void BoardTest1()
        {
            //9x9
            sr = new StreamReader(puzzlePath + "Puzzle-9x9-0001.txt");
            brd = new Board(sr);
            Board test = new Board(brd);
            Assert.AreEqual(test.n, 9);
            Assert.AreEqual(test.subDivN, 3);
            Assert.AreEqual(test.valid, true);
            Assert.AreEqual(test.board[0, 0].displayedChar, '4');
            foreach (char c in test.vals)
            {
                Assert.IsNotNull(c);
            }
            Assert.IsNull(test.reason);
            Assert.IsNotNull(test.board);
            Assert.AreEqual(test.board.Length, 81);
            Assert.AreNotSame(test, brd);
            //16x16
            sr = new StreamReader(puzzlePath + "Puzzle-16x16-0001.txt");
            brd = new Board(sr);
            test = new Board(brd);
            Assert.AreEqual(test.n, 16);
            Assert.AreEqual(test.subDivN, 4);
            Assert.AreEqual(test.valid, true);
            Assert.AreEqual(test.board[0, 0].displayedChar, '7');
            foreach (char c in test.vals)
            {
                Assert.IsNotNull(c);
            }
            Assert.IsNull(test.reason);
            Assert.IsNotNull(test.board);
            Assert.AreEqual(test.board.Length, 256);
            Assert.AreNotSame(test, brd);
        }

        //test invalid board import
        [TestMethod()]
        public void InputValidTest()
        {
            //25x25
            sr = new StreamReader(puzzlePath + "Puzzle-25x25-0101.txt");
            brd = new Board(sr);
            Assert.IsTrue(brd.InputValid('5'));
            foreach (char c in brd.vals)
                Assert.IsTrue(brd.InputValid(c));
            Assert.IsFalse(brd.InputValid('z'));
            Assert.IsTrue(brd.InputValid('C'));
            Assert.IsFalse(brd.InputValid('c'));
            //9x9
            sr = new StreamReader(puzzlePath + "Puzzle-9x9-0001.txt");
            brd = new Board(sr);
            Assert.IsTrue(brd.InputValid('5'));
            foreach (char c in brd.vals)
                Assert.IsTrue(brd.InputValid(c));
            Assert.IsFalse(brd.InputValid('z'));
            Assert.IsTrue(brd.InputValid('4'));
            Assert.IsFalse(brd.InputValid('$'));
            //4x4
            sr = new StreamReader(puzzlePath + "Puzzle-4x4-0001.txt");
            brd = new Board(sr);
            Assert.IsTrue(brd.InputValid('1'));
            foreach (char c in brd.vals)
                Assert.IsTrue(brd.InputValid(c));
            Assert.IsFalse(brd.InputValid('z'));
            Assert.IsTrue(brd.InputValid('3'));
            Assert.IsFalse(brd.InputValid('$'));
        }

        //check all directions to see if a char is a valid input.
        [TestMethod()]
        public void CheckAllTest()
        {
            //25x25
            sr = new StreamReader(puzzlePath + "Puzzle-25x25-0101.txt");
            brd = new Board(sr);
            Assert.IsFalse(brd.CheckAll(brd.board[0, 0].displayedChar, 0, 0));
            Assert.IsFalse(brd.CheckAll('6', 0, 0));
            Assert.IsTrue(brd.CheckAll('2', 0, 0));
            //9x9
            sr = new StreamReader(puzzlePath + "Puzzle-9x9-0001.txt");
            brd = new Board(sr);
            Assert.IsTrue(brd.CheckAll(brd.board[0, 0].displayedChar, 0, 0));
            Assert.IsFalse(brd.CheckAll('6', 0, 0));
            Assert.IsFalse(brd.CheckAll('2', 0, 0));
            Assert.IsFalse(brd.CheckAll('8', 1, 0));
        }

        //check single column
        [TestMethod()]
        public void CheckColTest()
        {
            //25x25
            sr = new StreamReader(puzzlePath + "Puzzle-25x25-0101.txt");
            brd = new Board(sr);
            Assert.IsFalse(brd.CheckAll(brd.board[0, 0].displayedChar, 0, 0));
            Assert.IsFalse(brd.CheckAll('6', 0, 0));
            Assert.IsTrue(brd.CheckAll('2', 0, 0));
            //9x9
            sr = new StreamReader(puzzlePath + "Puzzle-9x9-0001.txt");
            brd = new Board(sr);
            Assert.IsTrue(brd.CheckAll(brd.board[0, 0].displayedChar, 0, 0));
            Assert.IsFalse(brd.CheckAll('6', 0, 0));
            Assert.IsFalse(brd.CheckAll('2', 0, 0));
            Assert.IsFalse(brd.CheckAll('8', 1, 0));
            //25x25
            sr = new StreamReader(puzzlePath + "Puzzle-25x25-0101.txt");
            brd = new Board(sr);
            Assert.IsFalse(brd.CheckCol(brd.board[0, 0].displayedChar, 0, 0));
            Assert.IsTrue(brd.CheckCol(brd.board[0, 0].displayedChar, 0, 1));
            Assert.IsFalse(brd.CheckCol('6', 0, 0));
            Assert.IsFalse(brd.CheckCol('2', 0, 0));
            Assert.IsTrue(brd.CheckCol('6', 0, 1));
            Assert.IsTrue(brd.CheckCol('2', 0, 1));

            //9x9
            sr = new StreamReader(puzzlePath + "Puzzle-9x9-0001.txt");
            brd = new Board(sr);
            Assert.IsTrue(brd.CheckCol(brd.board[2, 0].displayedChar, 2, 0));
            Assert.IsFalse(brd.CheckCol(brd.board[2, 0].displayedChar, 2, 1));
            Assert.IsFalse(brd.CheckCol('6', 2, 0));
            Assert.IsTrue(brd.CheckCol('2', 2, 0));
            Assert.IsTrue(brd.CheckCol('6', 2, 1));
            Assert.IsFalse(brd.CheckCol('2', 2, 1));
        }

        [TestMethod()]
        public void CheckRowTest()
        {
            //25x25
            sr = new StreamReader(puzzlePath + "Puzzle-25x25-0101.txt");
            brd = new Board(sr);
            Assert.IsFalse(brd.CheckAll(brd.board[0, 0].displayedChar, 0, 0));
            Assert.IsFalse(brd.CheckAll('6', 0, 0));
            Assert.IsTrue(brd.CheckAll('2', 0, 0));
            //9x9
            sr = new StreamReader(puzzlePath + "Puzzle-9x9-0001.txt");
            brd = new Board(sr);
            Assert.IsTrue(brd.CheckAll(brd.board[0, 0].displayedChar, 0, 0));
            Assert.IsFalse(brd.CheckAll('6', 0, 0));
            Assert.IsFalse(brd.CheckAll('2', 0, 0));
            Assert.IsFalse(brd.CheckAll('8', 1, 0));

            //25x25
            sr = new StreamReader(puzzlePath + "Puzzle-25x25-0101.txt");
            brd = new Board(sr);
            Assert.IsFalse(brd.CheckRow(brd.board[0, 0].displayedChar, 0, 0));
            Assert.IsFalse(brd.CheckRow(brd.board[0, 0].displayedChar, 0, 1));
            Assert.IsTrue(brd.CheckRow('6', 0, 0));
            Assert.IsFalse(brd.CheckRow('2', 0, 0));
            Assert.IsFalse(brd.CheckRow('6', 0, 1));
            Assert.IsTrue(brd.CheckRow('2', 0, 1));

            //9x9
            
            sr = new StreamReader(puzzlePath + "Puzzle-9x9-0001.txt");
            brd = new Board(sr);
            Assert.IsFalse(brd.CheckRow(brd.board[2, 0].displayedChar, 2, 0));
            Assert.IsTrue(brd.CheckRow(brd.board[2, 0].displayedChar, 2, 1));
            Assert.IsFalse(brd.CheckRow('6', 1, 0));
            Assert.IsTrue(brd.CheckRow('2', 1, 0));
            Assert.IsTrue(brd.CheckRow('6', 1, 1));
            Assert.IsFalse(brd.CheckRow('2', 1, 1));
        }

        [TestMethod()]
        public void CheckAroundTest()
        {
            //25x25
            sr = new StreamReader(puzzlePath + "Puzzle-25x25-0101.txt");
            brd = new Board(sr);
            Assert.IsFalse(brd.CheckAll(brd.board[0, 0].displayedChar, 0, 0));
            Assert.IsFalse(brd.CheckAll('6', 0, 0));
            Assert.IsTrue(brd.CheckAll('2', 0, 0));
            //9x9
            sr = new StreamReader(puzzlePath + "Puzzle-9x9-0001.txt");
            brd = new Board(sr);
            Assert.IsTrue(brd.CheckAll(brd.board[0, 0].displayedChar, 0, 0));
            Assert.IsFalse(brd.CheckAll('6', 0, 0));
            Assert.IsFalse(brd.CheckAll('2', 0, 0));
            Assert.IsFalse(brd.CheckAll('8', 1, 0));
        }

        [TestMethod()]
        public void CountEmptyRowTest()
        {
            //25x25
            sr = new StreamReader(puzzlePath + "Puzzle-25x25-0101.txt");
            brd = new Board(sr);
            Assert.AreEqual(brd.CountEmptyRow(0), 11);
            Assert.AreEqual(brd.CountEmptyRow(1), 8);
            Assert.AreEqual(brd.CountEmptyRow(2), 11);
            Assert.AreEqual(brd.CountEmptyRow(3), 9);
            Assert.AreEqual(brd.CountEmptyRow(4), 9);
            Assert.AreNotEqual(brd.CountEmptyRow(0), 12);
            Assert.AreNotEqual(brd.CountEmptyRow(1), 9);
            Assert.AreNotEqual(brd.CountEmptyRow(2), 10);
            Assert.AreNotEqual(brd.CountEmptyRow(3), 8);
            Assert.AreNotEqual(brd.CountEmptyRow(4), 5);

            //9x9
            sr = new StreamReader(puzzlePath + "Puzzle-9x9-0001.txt");
            brd = new Board(sr);
            Assert.AreEqual(brd.CountEmptyRow(0), 2);
            Assert.AreEqual(brd.CountEmptyRow(1), 5);
            Assert.AreEqual(brd.CountEmptyRow(2), 3);
            Assert.AreEqual(brd.CountEmptyRow(3), 5);
            Assert.AreEqual(brd.CountEmptyRow(4), 5);
            Assert.AreNotEqual(brd.CountEmptyRow(0), 12);
            Assert.AreNotEqual(brd.CountEmptyRow(1), 8);
            Assert.AreNotEqual(brd.CountEmptyRow(2), 11);
            Assert.AreNotEqual(brd.CountEmptyRow(3), 9);
            Assert.AreNotEqual(brd.CountEmptyRow(4), 9);

            //4x4
            sr = new StreamReader(puzzlePath + "Puzzle-4x4-0002.txt");
            brd = new Board(sr);
            Assert.AreEqual(brd.CountEmptyRow(0), 1);
            Assert.AreEqual(brd.CountEmptyRow(1), 1);
            Assert.AreEqual(brd.CountEmptyRow(2), 1);
            Assert.AreEqual(brd.CountEmptyRow(3), 1);
            Assert.AreNotEqual(brd.CountEmptyRow(0), 12);
            Assert.AreNotEqual(brd.CountEmptyRow(1), 8);
            Assert.AreNotEqual(brd.CountEmptyRow(2), 11);
            Assert.AreNotEqual(brd.CountEmptyRow(3), 9);
        }

        [TestMethod()]
        public void CountEmptyColTest()
        {
            //25x25
            sr = new StreamReader(puzzlePath + "Puzzle-25x25-0101.txt");
            brd = new Board(sr);
            Assert.AreEqual(brd.CountEmptyCol(0), 1);
            Assert.AreEqual(brd.CountEmptyCol(1), 7);
            Assert.AreEqual(brd.CountEmptyCol(2), 7);
            Assert.AreEqual(brd.CountEmptyCol(3), 6);
            Assert.AreEqual(brd.CountEmptyCol(4), 9);
            Assert.AreNotEqual(brd.CountEmptyCol(0), 12);
            Assert.AreNotEqual(brd.CountEmptyCol(1), 9);
            Assert.AreNotEqual(brd.CountEmptyCol(2), 10);
            Assert.AreNotEqual(brd.CountEmptyCol(3), 8);
            Assert.AreNotEqual(brd.CountEmptyCol(4), 6);
            
            //9x9
            sr = new StreamReader(puzzlePath + "Puzzle-9x9-0001.txt");
            brd = new Board(sr);
            Assert.AreEqual(brd.CountEmptyCol(0), 5);
            Assert.AreEqual(brd.CountEmptyCol(1), 3);
            Assert.AreEqual(brd.CountEmptyCol(2), 4);
            Assert.AreEqual(brd.CountEmptyCol(3), 4);
            Assert.AreEqual(brd.CountEmptyCol(4), 3);
            Assert.AreNotEqual(brd.CountEmptyCol(0), 12);
            Assert.AreNotEqual(brd.CountEmptyCol(1), 8);
            Assert.AreNotEqual(brd.CountEmptyCol(2), 11);
            Assert.AreNotEqual(brd.CountEmptyCol(3), 9);
            Assert.AreNotEqual(brd.CountEmptyCol(4), 9);

            //4x4
            sr = new StreamReader(puzzlePath + "Puzzle-4x4-0002.txt");
            brd = new Board(sr);
            Assert.AreEqual(brd.CountEmptyCol(0), 1);
            Assert.AreEqual(brd.CountEmptyCol(1), 1);
            Assert.AreEqual(brd.CountEmptyCol(2), 1);
            Assert.AreEqual(brd.CountEmptyCol(3), 1);
            Assert.AreNotEqual(brd.CountEmptyCol(0), 12);
            Assert.AreNotEqual(brd.CountEmptyCol(1), 8);
            Assert.AreNotEqual(brd.CountEmptyCol(2), 11);
            Assert.AreNotEqual(brd.CountEmptyCol(3), 9);
        }

        [TestMethod()]
        public void CountAroundTest()
        {
            //25x25
            sr = new StreamReader(puzzlePath + "Puzzle-25x25-0101.txt");
            brd = new Board(sr);
            Assert.AreEqual(brd.CountAround(0, 0), 9);
            Assert.AreEqual(brd.CountAround(6, 6), 6);
            Assert.AreEqual(brd.CountAround(6, 2), 5);
            Assert.AreEqual(brd.CountAround(2, 6), 11);
            Assert.AreEqual(brd.CountAround(24, 24), 0);
            Assert.AreNotEqual(brd.CountAround(0, 0), 11);
            Assert.AreNotEqual(brd.CountAround(6, 6), 12);
            Assert.AreNotEqual(brd.CountAround(6, 2), 3);
            Assert.AreNotEqual(brd.CountAround(2, 6), 4);
            Assert.AreNotEqual(brd.CountAround(24, 24), 5);
            
            //9x9
            sr = new StreamReader(puzzlePath + "Puzzle-9x9-0001.txt");
            brd = new Board(sr);
            Assert.AreEqual(brd.CountAround(0, 0), 4);
            Assert.AreEqual(brd.CountAround(6, 6), 4);
            Assert.AreEqual(brd.CountAround(6, 2), 3);
            Assert.AreEqual(brd.CountAround(2, 6), 3);
            Assert.AreEqual(brd.CountAround(8, 8), 4);
            Assert.AreNotEqual(brd.CountAround(0, 0), 11);
            Assert.AreNotEqual(brd.CountAround(6, 6), 12);
            Assert.AreNotEqual(brd.CountAround(6, 2), 4);
            Assert.AreNotEqual(brd.CountAround(2, 6), 4);
            Assert.AreNotEqual(brd.CountAround(8, 8), 5);
            
            //4x4
            sr = new StreamReader(puzzlePath + "Puzzle-4x4-0002.txt");
            brd = new Board(sr);
            Assert.AreEqual(brd.CountAround(0, 0), 1);
            Assert.AreEqual(brd.CountAround(2, 1), 1);
            Assert.AreEqual(brd.CountAround(1, 2), 1);
            Assert.AreEqual(brd.CountAround(3, 3), 1);
            Assert.AreNotEqual(brd.CountAround(0, 0), 2);
            Assert.AreNotEqual(brd.CountAround(2, 1), 2);
            Assert.AreNotEqual(brd.CountAround(1, 2), 2);
            Assert.AreNotEqual(brd.CountAround(3, 3), 2);
        }

        [TestMethod()]
        public void UpdateChoicesTest()
        {
            List<char> cl = new List<char>();
            //25x25
            sr = new StreamReader(puzzlePath + "Puzzle-25x25-0101.txt");
            brd = new Board(sr);
            brd.UpdateChoices();
            Assert.IsNotNull(brd.board[0, 0].charChoices);
            foreach (char c in brd.board[0, 0].charChoices)
                cl.Add(c);
            brd.UpdateChoices();
            Assert.IsTrue(brd.board[0, 0].charChoices.Count == cl.Count);
            foreach(char c in cl)
            {
                Assert.IsTrue(brd.board[0, 0].charChoices.Contains(c));
                Assert.IsTrue(brd.InputValid(c));
            }
            cl.Clear();
            //9x9
            sr = new StreamReader(puzzlePath + "Puzzle-9x9-0001.txt");
            brd = new Board(sr);
            brd.UpdateChoices();
            Assert.IsNotNull(brd.board[0, 0].charChoices);
            foreach (char c in brd.board[0, 0].charChoices)
                cl.Add(c);
            brd.UpdateChoices();
            Assert.IsTrue(brd.board[0, 0].charChoices.Count == cl.Count);
            foreach (char c in cl)
            {
                Assert.IsTrue(brd.board[0, 0].charChoices.Contains(c));
                Assert.IsTrue(brd.InputValid(c));
            }
            cl.Clear();
            //4x4
            sr = new StreamReader(puzzlePath + "Puzzle-4x4-0002.txt");
            brd = new Board(sr);
            brd.UpdateChoices();
            Assert.IsNotNull(brd.board[0, 0].charChoices);
            foreach (char c in brd.board[0, 0].charChoices)
                cl.Add(c);
            brd.UpdateChoices();
            Assert.IsTrue(brd.board[0, 0].charChoices.Count == cl.Count);
            foreach (char c in cl)
            {
                Assert.IsTrue(brd.board[0, 0].charChoices.Contains(c));
                Assert.IsTrue(brd.InputValid(c));
            }
        }
    }
}