using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;
using VendingMachineLibrary;

namespace Specs.VendingMachineSteps
{
    [Binding]
    public class HandlingInsufficientChangeStep
    {
        private VendingMachine _vendingMachine = new VendingMachine();
        private int _itemCost;
        private ChangeHolder _changeReturnedToUser;

        [Given(@"The vending machine has insufficient change")]
        public void GivenTheVendingMachineHasInsufficientChange()
        {
            // Dont fill the vending machine
        }

        [When(@"I have attempted to purchased an item for ""65"" pence using 1 pound coin")]
        public void WhenIHaveAttemptedToPurchasedAnItemFor65PenceUsing1PoundCoin()
        {
            _itemCost = 65;
            ChangeHolder userChangeHolder = new ChangeHolder();
            Coin coin = new Coin()
            {
                CoinType = CoinType.OnePound
            };
            userChangeHolder.Add(coin);
            _changeReturnedToUser = _vendingMachine.PurchaseItem(userChangeHolder, _itemCost);
        }

        [Then(@"I the user should recieve a one pound coin as his change")]
        public void ThenITheUserShouldRecieveAOnePoundCoinAsHisChange()
        {
            // Ensure there is only 1 coin
            Assert.AreEqual(_changeReturnedToUser.Coins.Select(c => c).Count(), 1);

            Coin coin = _changeReturnedToUser.Coins.Select(c => c).First();

            // Ensure that the coin is a one pound coin
            Assert.AreEqual(coin.CoinType, CoinType.OnePound);

        }
        
        [Then(@"The machine should provide the insufficient change message")]
        public void ThenTheMachineShouldProvideTheInsufficientChangeMessage()
        {
            Assert.AreEqual(_vendingMachine.MessageToUser, Constants.SORRY_NO_CHANGE);
        }




    }
}
