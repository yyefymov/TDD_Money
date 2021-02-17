using System;
using System.Collections;

namespace MultiCurrencyMoney
{
    class Bank
    {

        private class Pair
        {
            private string from;
            private string to;

            public Pair(string from, string to)
            {
                this.from = from;
                this.to = to;
            }

            public override bool Equals(object obj)
            {                
                var pair = (Pair) obj;
                return from.Equals(pair.from) && to.Equals(pair.to);
            }
            
            public override int GetHashCode()
            {
                return 0;
            }
        }

        private Hashtable rates = new Hashtable();

        public Money reduce(Expression source, string to)
            => source.reduce(this, to);
        
        public int rate(string from, string to)
        {
            if (from.Equals(to))
            {
                return 1;
            }
            return (int)rates[new Pair(from, to)];
        }

        public void addRate(string from, string to, int rate)
        {
            rates.Add(new Pair(from, to), rate);
        }
    }
}