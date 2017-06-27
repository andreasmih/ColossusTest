using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OddsCalculator
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
        /// When more than 1 ticket wins a prize, the tickets share the prize.
        /// Every outcome of the match has a certain probability and they should add up to the total of 1.
        /// This calculator will need to work out the value of each ticket in the form of decimal winning odds.
        /// When a winning ticket needs to share the prize with others, its value decreases, so its winning odds is not 100%, not 0% either.
        /// In addition, if the pool has consolation prizes, the function should return the SUM of winning odds for every ticket, 
        /// e.g. odds of winning the win prize + odds of getting consolation 1 prize + odds of getting consolation 2 prizes + ... + odds of getting the consolation up to the prizes parameter
        /// </summary>
        /// <param name="tickets">e.g. {{1,1,1,1,1},{2,2,2,2,2},{3,3,3,3,3},{1,1,2,3,3}} for 4 tickets</param>
        /// <param name="probabilities">e.g. {{0.333, 0.333, 0.333},{0.333, 0.333, 0.333},{0.333, 0.333, 0.333},{0.333, 0.333, 0.333},{0.333, 0.333, 0.333}}</param>
        /// <param name="prizes">e.g. Win == 5/5, Consolation1==4/5, Consolation2==3/5 and so on</param>
        /// <returns>Odds in 8 decimal places, e.g. Win prize odds { 0.00411522m, 0.00411522m, 0.00411522m, 0.00411522m, 0.00411522m }</returns>
        public static decimal[] CalculateWinningOdds(int[,] tickets, double[,] probabilities, PrizeType prizes)
        {
            var count = tickets.GetLength(0);
            var numOfLegs = probabilities.GetLength(0);
            var numOfSelections = probabilities.GetLength(1);

            var results = new decimal[count];

            Trace.Assert(numOfLegs == tickets.GetLength(1));

            Dictionary<string, List<int>> dictionary = new Dictionary<string, List<int> >();
            decimal[,] oddsDecomposition = new decimal[count,3];

            for (int i = 0; i < count; ++i)
            {
                decimal odds = 1.0m;
                decimal lost1matchOdd = 0.0m;
                decimal lost2matchOdd = 0.0m;
                string key = "";

                for (int match = 0; match < tickets.GetLength(1); ++match)
                {
                    odds *= (decimal)probabilities[match, tickets[i, match] - 1];
                    key += tickets[i, match].ToString();
                }

                /// dictionary creation for full wins
                if (!dictionary.ContainsKey(key))
                    dictionary.Add(key, new List<int>());
                dictionary[key].Add(i);

                if (prizes != PrizeType.Win)
                {
                    
                    for (int lostmatch1=0; lostmatch1<tickets.GetLength(1); lostmatch1++)
                    {
                        decimal intermLost2matchOdd = 0.0m;
                        decimal intermLost1matchOdd = odds / (decimal)probabilities[lostmatch1, tickets[i, lostmatch1] - 1];
                        intermLost1matchOdd *= (decimal)(1 - probabilities[lostmatch1, tickets[i, lostmatch1] - 1]);

                        // create a *1111 key for 1 lost matches
                        string memokey1 = key;
                        StringBuilder sb = new StringBuilder(key);
                        sb[lostmatch1] = '*';
                        key = sb.ToString();
                        if (!dictionary.ContainsKey(key))
                            dictionary.Add(key, new List<int>());
                        dictionary[key].Add(i);


                        if (prizes == PrizeType.Consolation2)
                        {
                            for (int lostmatch2 = lostmatch1+1; lostmatch2 < tickets.GetLength(1); lostmatch2++)
                            {
                                if(lostmatch1 != lostmatch2)
                                {
                                    intermLost2matchOdd = intermLost1matchOdd / (decimal)probabilities[lostmatch2, tickets[i, lostmatch2] - 1];
                                    intermLost2matchOdd *= (decimal)(1 - probabilities[lostmatch2, tickets[i, lostmatch2] - 1]);
                                    lost2matchOdd += intermLost2matchOdd;

                                    // create a *1*11 key for 2 lost matches
                                    string memokey = key;
                                    StringBuilder sb2 = new StringBuilder(key);
                                    sb2[lostmatch2] = '*';
                                    key = sb2.ToString();
                                    if (!dictionary.ContainsKey(key))
                                        dictionary.Add(key, new List<int>());
                                    dictionary[key].Add(i);
                                    key = memokey;
                                }

                            }
                        }
                        key = memokey1;
                        lost1matchOdd += intermLost1matchOdd;             
                    }
                }
                oddsDecomposition[i, 0] = odds;
                oddsDecomposition[i, 1] = lost1matchOdd;
                oddsDecomposition[i, 2] = lost2matchOdd;
                
            }

            foreach (KeyValuePair<string, List<int>> entry in dictionary)
            {
                if(entry.Value.Count > 1)
                {
                    for (int i = 0; i < entry.Value.Count; ++i)
                    {
                        int stars = entry.Key.Split('*').Length - 1;
                        oddsDecomposition[entry.Value[i], stars] /= entry.Value.Count;
                     }
                }
            }

            for(int i=0; i<count; ++i)      
                results[i] = decimal.Round(oddsDecomposition[i, 0] + oddsDecomposition[i, 1] + oddsDecomposition[i, 2],8);
           
     
            return results;
    }

        public enum PrizeType
        {
            Win,
            Consolation1,
            Consolation2
        }
    }
}

