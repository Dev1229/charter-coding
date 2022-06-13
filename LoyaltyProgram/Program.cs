using LoyaltyProgram.BusinessObjects;
using System;

namespace LoyaltyProgram
{
    class Program
    {
        public static void Main(string[] args)
        {
            var rewardsStuff = new SetupRewardsStuff();

            // calculate the rewards points
            rewardsStuff.RunRewardsProgram();

            // get the three month period reward report
            rewardsStuff.GetRewardsReport();

            Console.ReadKey();
        }
    }
}
 