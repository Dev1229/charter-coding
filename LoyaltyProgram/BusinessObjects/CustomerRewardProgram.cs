using System;

namespace LoyaltyProgram.BusinessObjects
{
    public class CustomerRewardCalculation : ILoyaltyProgram
    {
        public CustomerRewardCalculation(decimal amt)
        {
            TransactionAmt = amt;
        }

        private decimal TransactionAmt { get; set; }

        private int AdditionalRewardsCalculation()
        {
            int rewardPoints = 0;
            if (TransactionAmt > 100M)
                rewardPoints += Convert.ToInt32(TransactionAmt - 100M) * 1;
            return rewardPoints;
        }

        /// <summary>
        /// calculate rewards if transaction amt qualifies for regular, along with additional reward calculation if qualifies 
        /// </summary>
        /// <returns></returns>
        public int RewardsCalculation()
        {
            int rewardPoints = 0;
            if (TransactionAmt > 50M)
                rewardPoints = Convert.ToInt32(TransactionAmt - 50M) * 1;

            // calculate additional rewards 
            return rewardPoints += AdditionalRewardsCalculation();
        }
    }
}
