using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuWithGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuWithGUI.Tests
{
    [TestClass()]
    public class CellTests
    {
        Cell cell;
        [TestMethod()]
        public void CellTest()
        {
            char[] c = { 'a', 'b', 'c', 'd' };
            cell = new Cell(c, 1, 2);
            Assert.AreEqual(cell.xPos, 1);
            Assert.AreEqual(cell.yPos, 2);
            foreach(char t in c)
                Assert.IsTrue(cell.charChoices.Contains(t));
            Assert.AreEqual(cell.numValidChoices, 4);
            Assert.IsFalse(cell.charChoices.Contains('6'));
        }

        [TestMethod()]
        public void CellTest1()
        {
            char c = 'a';
            cell = new Cell(c, 1, 2);
            Assert.AreEqual(cell.xPos, 1);
            Assert.AreEqual(cell.yPos, 2);
            Assert.IsNotNull(cell.charChoices);
            Assert.AreEqual(cell.charChoices.Count, 0);
            Assert.AreEqual(cell.numValidChoices, 1);
            Assert.IsFalse(cell.charChoices.Contains('6'));
        }

        [TestMethod()]
        public void ClearChoicesTest()
        {
            char[] c = { 'a', 'b', 'c', 'd' };
            cell = new Cell(c, 1, 2);
            foreach (char t in c)
                Assert.IsTrue(cell.charChoices.Contains(t));
            Assert.AreEqual(cell.numValidChoices, 4);
            cell.ClearChoices();
            foreach (char t in c)
                Assert.IsFalse(cell.charChoices.Contains(t));
            Assert.AreEqual(cell.numValidChoices, 0);
        }

        [TestMethod()]
        public void AddChoiceTest()
        {
            char[] c = { 'a', 'b', 'c', 'd' };
            cell = new Cell(c, 1, 2);
            foreach (char t in c)
                Assert.IsTrue(cell.charChoices.Contains(t));
            Assert.AreEqual(cell.numValidChoices, 4);
            cell.AddChoice('g');
            foreach (char t in c)
                Assert.IsTrue(cell.charChoices.Contains(t));
            Assert.AreEqual(cell.numValidChoices, 5);
            Assert.AreEqual(cell.charChoices[4], 'g');
        }
    }
}