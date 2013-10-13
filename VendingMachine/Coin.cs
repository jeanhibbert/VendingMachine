using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachineLibrary
{
    public class Coin
    {
        public int Weight
        {
            get { return (int) (this.CoinType); }
        }
    
        public CoinType CoinType { get; set; }
    }

    public enum CoinType
    {
        OnePence = 1, 
        TwoPence = 2, 
        FivePence = 5, 
        TenPence = 10, 
        TwentyPence = 20, 
        FiftyPence = 50, 
        OnePound = 100
    }
}
