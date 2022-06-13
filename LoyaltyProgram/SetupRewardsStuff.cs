using LoyaltyProgram.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace LoyaltyProgram
{
    public class SetupRewardsStuff
    {
        private ILoyaltyProgram loyalty;
        private Customer cust;
        private decimal TransactionAmt;
        private int PointsReceived;

        /// <summary>
        /// calculate the program
        /// </summary>
        public void RunRewardsProgram()
        {
            // create a customer
            cust = new Customer();
            cust.CustId = 101;
            cust.Name = "cust1";

            // calculate the rewards
            TransactionAmt = 120;
            loyalty = new CustomerRewardCalculation(TransactionAmt);
            PointsReceived = loyalty.RewardsCalculation();
            cust.AddRewards(new RewardPoints
            {
                EarnedDate = DateTime.UtcNow.AddDays(-60),
                Points = PointsReceived
            });

            Console.WriteLine($"\nCustomer received {PointsReceived} point(s) for ${TransactionAmt} purchase \n \n");
        }

        /// <summary>
        /// get the rewards report 
        /// </summary>
        public void GetRewardsReport()
        {
            var customers = new List<Customer>();

            loyalty = new CustomerRewardCalculation(51);
            cust.AddRewards(new RewardPoints
            {
                EarnedDate = DateTime.UtcNow.AddDays(-30),
                Points = loyalty.RewardsCalculation()
            });

            loyalty = new CustomerRewardCalculation(250);
            cust.AddRewards(new RewardPoints
            {
                EarnedDate = DateTime.UtcNow,
                Points = loyalty.RewardsCalculation()
            });

            loyalty = new CustomerRewardCalculation(102);
            cust.AddRewards(new RewardPoints
            {
                EarnedDate = DateTime.UtcNow,
                Points = loyalty.RewardsCalculation()
            });

            // add the customer to the list
            customers.Add(cust);

            // create a new customer
            cust = new Customer();
            cust.CustId = 102;
            cust.Name = "cust2";

            loyalty = new CustomerRewardCalculation(75);
            cust.AddRewards(new RewardPoints
            {
                EarnedDate = DateTime.UtcNow.AddDays(-90),
                Points = loyalty.RewardsCalculation()
            });

            loyalty = new CustomerRewardCalculation(105);
            cust.AddRewards(new RewardPoints
            {
                EarnedDate = DateTime.UtcNow.AddDays(-60),
                Points = loyalty.RewardsCalculation()
            });

            loyalty = new CustomerRewardCalculation(400);
            cust.AddRewards(new RewardPoints
            {
                EarnedDate = DateTime.UtcNow.AddDays(-30),
                Points = loyalty.RewardsCalculation()
            });

            loyalty = new CustomerRewardCalculation(214);
            cust.AddRewards(new RewardPoints
            {
                EarnedDate = DateTime.UtcNow,
                Points = loyalty.RewardsCalculation()
            });

            // add the customer to the list
            customers.Add(cust);

            // create a new customer
            cust = new Customer();
            cust.CustId = 103;
            cust.Name = "cust3";

            loyalty = new CustomerRewardCalculation(45);
            cust.AddRewards(new RewardPoints
            {
                EarnedDate = DateTime.UtcNow.AddDays(-90),
                Points = loyalty.RewardsCalculation()
            });

            loyalty = new CustomerRewardCalculation(165);
            cust.AddRewards(new RewardPoints
            {
                EarnedDate = DateTime.UtcNow.AddDays(-60),
                Points = loyalty.RewardsCalculation()
            });

            loyalty = new CustomerRewardCalculation(785);
            cust.AddRewards(new RewardPoints
            {
                EarnedDate = DateTime.UtcNow.AddDays(-30),
                Points = loyalty.RewardsCalculation()
            });

            loyalty = new CustomerRewardCalculation(95);
            cust.AddRewards(new RewardPoints
            {
                EarnedDate = DateTime.UtcNow,
                Points = loyalty.RewardsCalculation()
            });

            loyalty = new CustomerRewardCalculation(95);
            cust.AddRewards(new RewardPoints
            {
                EarnedDate = DateTime.UtcNow.AddDays(-105),
                Points = loyalty.RewardsCalculation()
            });

            // add the customer to the list
            customers.Add(cust);

            /// get the report for all customers with each month and total rewards
            var customerRewards = customers.GroupBy(c => new { c.CustId, c.Name })
                                           .Select(x => new { key = x.Key, custRewards = x.SelectMany(r => r.RewardPoints).ToList() })
                                           .ToList();

            Console.WriteLine("Last three month customer reward points for each month and total: \n");
            Console.WriteLine("=================================================================");
            Console.WriteLine("CustomerName     Month       RewardsByMonth      TotalRewards");
            Console.WriteLine("=================================================================");
            foreach(var cust in customerRewards)
            {
                // filter only last three months rewards and per month to get per month rewards
                var rewardsByMonth = cust.custRewards.Where(m => DateTime.Compare(m.EarnedDate, DateTime.Today.AddMonths(-3)) >= 0)
                                                     .GroupBy(x => x.EarnedDate.Month)
                                                     .Select(r => new { month = r.Key, points = r.Select(p => p.Points).Sum() })
                                                     .OrderBy(o => o.month)
                                                     .ToList();

                // get total reward points for the given customer
                var totalRewards = cust.custRewards.Sum(c => c.Points);
                foreach (var rewards in rewardsByMonth)
                {
                    // get the month name from month index
                    var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(rewards.month);
                    Console.WriteLine($"{cust.key.Name}             {month}             {rewards.points}            {totalRewards}");
                }
            }
        }
    }
}
