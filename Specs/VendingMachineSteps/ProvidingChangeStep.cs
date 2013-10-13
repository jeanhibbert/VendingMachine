using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using VendingMachineLibrary;
using NUnit.Framework;


namespace Specs.VendingMachineSteps
{
    [Binding]
    public class ProvidingChangeStep
    {
        
        private VendingMachine _vendingMachine;

        [Given(@"The vending machine has sufficient change")]
        public void GivenTheVendingMachineHasSufficientChange()
        {
            _vendingMachine = new VendingMachine();
            _vendingMachine.ChangeHolder.Fill();
        }

        private int _itemCost;
        [Given(@"I have purchased an item for ""65"" pence")]
        public void GivenIHavePurchasedAnItemFor65Pence()
        {
            _itemCost = 65;
        }

        private ChangeHolder _changeReturnedToUser;

        [When(@"I provide a 1 pound coin")]
        public void WhenIProvideA1PoundCoin()
        {
            ChangeHolder userChangeHolder = new ChangeHolder();
            Coin coin = new Coin()
            {
                CoinType = CoinType.OnePound
            };
            userChangeHolder.Add(coin);
            _changeReturnedToUser = _vendingMachine.PurchaseItem(userChangeHolder, _itemCost);
        }

        [Then(@"I should recieve ""35"" pence change")]
        public void ThenIShouldRecieve35PenceChange()
        {
            Assert.AreEqual(35, _changeReturnedToUser.TotalValue());
        }

        [Then(@"the couns returned should consist of two 20 pence coins and a 5 pence coin")]
        public void ThenTheCounsReturnedShouldConsistOfTwo20PenceCoinsAndA5PenceCoin()
        {
            int numTentyPenceCoins =
                _changeReturnedToUser.Coins
                .Where(coin => coin.CoinType == CoinType.TwentyPence)
                .Select(coin => coin).
                    Count();

            Assert.AreEqual(numTentyPenceCoins, 1);
            
            int numTenPenceCoins =
                _changeReturnedToUser.Coins.Where(coin => coin.CoinType == CoinType.TenPence).Select(coin => coin).
                    Count();

            Assert.AreEqual(numTenPenceCoins, 1);

            int numFivePenceCoins =
                _changeReturnedToUser.Coins.Where(coin => coin.CoinType == CoinType.FivePence).Select(coin => coin).
                    Count();

            Assert.AreEqual(numFivePenceCoins, 1);
        }

        [Then(@"The machine should thank the user for thier custom")]
        public void ThenTheMachineShouldThankTheUserForThierCustom()
        {
            Assert.AreEqual(_vendingMachine.MessageToUser, Constants.THANK_YOU);
        }


    }
}
