using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCalculator.Tests
{
    [TestClass()]
    public class CalculatorTests
    {
        [TestMethod()]
        public void CalculateEvBasicTest()
        {
            var tickets = new int[,] { { 1, 1, 1, 1, 1 }, { 2, 2, 2, 2, 2 }, { 3, 3, 3, 3, 3 }, { 1, 1, 2, 3, 3 } };
            var probs = new double[,] { { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333, 0.33333333, 0.33333333 }, 
                { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333,0.33333333,0.33333333 } };
            var prize = new decimal[] { 5000m };
            var expected = new decimal[] { 20.57613169m, 20.57613169m, 20.57613169m, 20.57613169m };
            var result = Calculator.CalculateEV(tickets, probs, prize);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CalculateEvDifferentPoolTest()
        {
            var tickets = new int[,] { { 1, 1, 1 }, { 2, 2, 2 }, { 3, 3, 3 }, { 1, 1, 2 } };
            var probs = new double[,] { { 0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353 }, 
                                        { 0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353 }, 
                                        { 0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353 } };
            var prize = new decimal[] { 5000m };
            var expected = new decimal[] { 1.01770812m, 1.01770812m, 1.01770812m, 1.01770812m };
            var result = Calculator.CalculateEV(tickets, probs, prize);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void CalculateEvChangedProbabilitiesTest()
        {
            var tickets = new int[,] { { 1, 1, 1, 1, 1 }, { 2, 2, 2, 2, 2 }, { 3, 3, 3, 3, 3 }, { 1, 1, 2, 3, 3 } };
            var probs = new double[,] { { 0.25,0.5,0.25 }, { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333, 0.33333333, 0.33333333 },
                { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333,0.33333333,0.33333333 } };
            var prize = new decimal[] { 2000m };
            var expected = new decimal[] { 6.17283951m, 12.34567901m, 6.17283951m, 6.17283951m };
            var result = Calculator.CalculateEV(tickets, probs, prize);
            CollectionAssert.AreEqual(expected, result);
        }


        [TestMethod()]
        public void CalculateEvSharedPrizeTest()
        {
            var tickets = new int[,] { { 1, 1, 1, 1, 1 }, { 2, 2, 2, 2, 2 }, { 3, 3, 3, 3, 3 }, { 1, 1, 1, 1, 1 } };
            var probs = new double[,] { { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333, 0.33333333, 0.33333333 },
                { 0.33333333, 0.33333333, 0.33333333 }, { 0.33333333,0.33333333,0.33333333 } };
            var prize = new decimal[] { 2000m };
            var expected = new decimal[] { 4.11522634m, 8.23045267m, 8.23045267m, 4.11522634m };
            var result = Calculator.CalculateEV(tickets, probs, prize);
            CollectionAssert.AreEqual(expected, result);
        }

        /// <summary>
        /// Additional test for bonus point
        /// </summary>
        [TestMethod()]
        public void BonusTest_CalculateEvConsolationPoolTest()
        {
            var tickets = new int[,] { { 8, 8, 8, 8, 8, 8 }, { 9, 9, 9, 9, 9, 9 }, { 9, 8, 8, 8, 8, 8 }, { 8, 8, 8, 8, 8, 8 } };
            var probs = new double[,] { { 0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353 },
                                        { 0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353 },
                                        { 0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353 },
                                        { 0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353 },
                                        { 0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353 },
                                        { 0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353,0.05882353 }};
            var prize = new decimal[] { 500000m, 1000, 500 };
            var expected = new decimal[] { 0.03562911m, 0.07954405m, 0.06297237m, 0.03562911m };
            var result = Calculator.CalculateEV(tickets, probs, prize);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}