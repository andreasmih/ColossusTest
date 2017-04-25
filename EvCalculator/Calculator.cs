using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvCalculator
{
    public class Calculator
    {
        /// <summary>
        /// In a sports betting pool, there are a number of legs, say 5 legs, where each leg is a football match, 
        /// and every leg have the same number of selections, {home win, draw, away win} for example, we can represent it as array of integer {1, 2, 3}
        /// and players place tickets like {1, 1, 1, 2, 3} into the pool which means they bet on leg 1 to leg 3: home win, leg 4 draw and leg 5 away wins.
        /// If the outcome of the match is exactly as the ticket selected, the ticket wins the that leg of the pool. 
        /// When the ticket selects all the correct outcome of the legs, it wins the top pool prize;
        /// Some pools have consolation prizes for tickets missing 1 leg, or more, e.g. a ticket get 5/6 legs correct, can win consolation 1 prize for a pool with 
        /// When more than 1 ticket wins a prize, they share the prize.
        /// Every outcome of the match has a certain probability and they should add up to the total of 1.
        /// The EV (expected value) of a ticket = the prize to win X probability of winning
        /// </summary>
        /// <param name="tickets">e.g. {{1,1,1,1,1},{2,2,2,2,2},{3,3,3,3,3},{1,1,2,3,3}} for 4 tickets</param>
        /// <param name="probabilities">e.g. {{0.333, 0.333, 0.333},{0.333, 0.333, 0.333},{0.333, 0.333, 0.333},{0.333, 0.333, 0.333},{0.333, 0.333, 0.333}}</param>
        /// <param name="prizes">e.g.{ 5000 } for single winning prize, or { 1000000, 1000, 500 } for win, consolation 1 and consolation 2 prizes</param>
        /// <returns>Ev in 8 decimal places, e.g. { 20.57613169m, 20.57613169m, 20.57613169m, 20.57613169m, 20.57613169m }</returns>
        public static decimal[] CalculateEV(int[,] tickets, double[,] probabilities, decimal[] prizes)
        {
            var count = tickets.GetLength(0);
            var evResults = new decimal[count];

            var numOfLegs = probabilities.GetLength(0);
            var numOfSelections = probabilities.GetLength(1);

            Trace.Assert(numOfLegs == tickets.GetLength(1));

            //You code goes here
            //Use the unit tests to find out the requirements



            return evResults;
        }
    }
}
