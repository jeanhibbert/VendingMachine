using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachineLibrary
{
    public class VendingMachine
    {
        private ChangeHolder _changeHolder = new ChangeHolder();

        public ChangeHolder ChangeHolder
        {
            get { return _changeHolder; }
            set { _changeHolder = value; }
        }

        public ChangeHolder PurchaseItem(ChangeHolder userChangeHolder, int itemCost)
        {
            int userChangeValue = userChangeHolder.TotalValue();

            // If the user didnt provide enough money return thier change
            if (userChangeValue < itemCost)
            {
                this.MessageToUser = Constants.NOT_ENOUGH_MONEY_PROVIDED;
                return userChangeHolder;
            }

            // If change required is not available then just return original change
            if ((userChangeValue - itemCost) > _changeHolder.TotalValue())
            {
                this.MessageToUser = Constants.SORRY_NO_CHANGE;
                return userChangeHolder;
            }

            // Attempt to return change
            ChangeHolder changeForUser = new ChangeHolder();

            int totalChangeRequiredByUser = userChangeHolder.TotalValue() - itemCost;

            Enum.GetValues(typeof(CoinType))
                .Cast<CoinType>()
                .OrderByDescending(coinType => (int)coinType)
                .ToList<CoinType>()
                .ForEach(coinType =>
                    {
                        if ((int)coinType <= itemCost)
                        {
                            _changeHolder.Coins
                                .Where(c => c.Weight == (int) coinType && c.Weight <= totalChangeRequiredByUser)
                                .TakeWhile(c =>
                                               {
                                                   if (changeForUser.TotalValue() + c.Weight <= totalChangeRequiredByUser)
                                                   {
                                                       changeForUser.Add(c);
                                                   }
                                                   return changeForUser.TotalValue() <= totalChangeRequiredByUser;
                                               })
                                .Select(c => c).ToList(); // ToList() needs to be executed to override 
                                                            // the deferred execution behaviour of Linq
                        }                                  
                    }
                );
            


            // Remove the coins returned from the changeholder.
            changeForUser.Coins.ForEach(coin => _changeHolder.Coins.Remove(coin));

            this.MessageToUser = Constants.THANK_YOU;
            return changeForUser;
        }

        public string MessageToUser { get; set; }
    }
}
