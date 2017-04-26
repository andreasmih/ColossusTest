using Microsoft.VisualStudio.TestTools.UnitTesting;
using OddsCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddsCalculator.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void CalculateWinningOddsBasicTest()
        {
            var tickets = new int[,] { { 1, 1, 1, 1, 1 }, { 2, 2, 2, 2, 2 }, { 3, 3, 3, 3, 3 }, { 1, 1, 2, 3, 3 } };
            var probs = new double[,] { { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333, 0.33333333, 0.33333333 }, 
                { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333,0.33333333,0.33333333 } };
            var prize = Calculator.PrizeType.Win;
            var expected = new decimal[] { 0.00411523m, 0.00411523m, 0.00411523m, 0.00411523m };
            var result = Calculator.CalculateWinningOdds(tickets, probs, prize);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CalculateWinningOddsDifferentPoolTest()
        {
            var tickets = new int[,] { { 1, 1, 1 }, { 2, 2, 2 }, { 3, 3, 3 }, { 1, 1, 2 } };
            var probs = new double[,] { { 0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353 }, 
                                        { 0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353 }, 
                                        { 0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353 } };
            var prize = Calculator.PrizeType.Win;
            var expected = new decimal[] { 0.00020354m, 0.00020354m, 0.00020354m, 0.00020354m };
            var result = Calculator.CalculateWinningOdds(tickets, probs, prize);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CalculateWinningOddsChangedProbabilitiesTest()
        {
            var tickets = new int[,] { { 1, 1, 1, 1, 1 }, { 2, 2, 2, 2, 2 }, { 3, 3, 3, 3, 3 }, { 1, 1, 2, 3, 3 } };
            var probs = new double[,] { { 0.25,0.5,0.25 }, { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333, 0.33333333, 0.33333333 },
                { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333,0.33333333,0.33333333 } };
            var prize = Calculator.PrizeType.Win;
            var expected = new decimal[] { 0.00308642m, 0.00617284m, 0.00308642m, 0.00308642m };
            var result = Calculator.CalculateWinningOdds(tickets, probs, prize);
            CollectionAssert.AreEqual(expected, result);
        }


        [TestMethod()]
        public void CalculateWinningOddsSharedPrizeTest()
        {
            var tickets = new int[,] { { 1, 1, 1, 1, 1 }, { 2, 2, 2, 2, 2 }, { 3, 3, 3, 3, 3 }, { 1, 1, 1, 1, 1 } };
            var probs = new double[,] { { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333, 0.33333333, 0.33333333 },
                { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333,0.33333333,0.33333333 } };
            var prize = Calculator.PrizeType.Win;
            var expected = new decimal[] { 0.00205761m, 0.00411523m, 0.00411523m, 0.00205761m };
            var result = Calculator.CalculateWinningOdds(tickets, probs, prize);
            CollectionAssert.AreEqual(expected, result);
        }

        /// <summary>
        /// Additional test for bonus point
        /// </summary>
        [TestMethod()]
        public void BonusTest_CalculateWinningOddsConsolationTest()
        {
            var tickets = new int[,] { { 1, 1, 1, 1, 1 }, { 2, 2, 2, 2, 2 }, { 3, 3, 2, 2, 2 }, { 2, 2, 2, 2, 2 } };
            var probs = new double[,] { { 0.25,0.5,0.25 }, { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333, 0.33333333, 0.33333333 },
                { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333,0.33333333,0.33333333 } };
            var prize = Calculator.PrizeType.Consolation2;
            var expected = new decimal[] { 0.18518519m, 0.11831276m, 0.13991770m, 0.11831276m };
            var result = Calculator.CalculateWinningOdds(tickets, probs, prize);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}