using System.Collections.Generic;

namespace LoyaltyProgram.BusinessObjects
{
    public class Customer
    {
        public int CustId { get; set; }
        public string Name { get; set; }
        public ICollection<RewardPoints> RewardPoints { get; private set; }

        public Customer()
        {
            RewardPoints = new List<RewardPoints>();
        }

        public void AddRewards(RewardPoints rewardPoint)
        {
            if (rewardPoint.Points > 0)
            {
                RewardPoints.Add(new RewardPoints
                {
                    EarnedDate = rewardPoint.EarnedDate,
                    Points = rewardPoint.Points
                });
            }
        }
    }
}
