using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachineLibrary
{
    public class ChangeHolder
    {
        private List<Coin> _coins = new List<Coin>();

        public List<Coin> Coins
        {
            get { return _coins; }
            set { _coins = value; }
        }

        public void Fill()
        {

            foreach (CoinType coinType in Enum.GetValues(typeof(CoinType)))
            {
                for (int i = 0; i < 100; i++)
                {
                    Coin coin = new Coin()
                                    {
                                        CoinType = coinType,
                                    };
                    Add(coin);
                }
            }

        }

        public void Add(Coin coin)
        {
            Coins.Add(coin);
        }

        public int TotalValue()
        {
            int totalValue = 0;

            totalValue = Coins.Select(coin => coin).Sum(coin => coin.Weight);

            return totalValue;
        }

    }
}
