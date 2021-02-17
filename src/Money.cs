
using System;

namespace MultiCurrencyMoney
{
    class Money : Expression
    {
        static public Money dollar(int amount)
        {
            return new Money(amount, "USD");
        }

        static public Money franc(int amount)
        {
            return new Money(amount, "CHF");
        }

        public Money(int amount, String currency)
        {
            this.amount = amount;
            this.currency = currency;
        }

        public int amount 
        {
            get;
            protected set;
        }

        public string currency
        {
            get;
            protected set;
        }
        
        public override string ToString() => amount + " " + currency;

        public override bool Equals(object obj)
        {
            Money money = (Money)obj;
            if (money == null || !currency.Equals(money.currency))
            {
                return false;
            }            
            
            return money.amount == amount;
        }

        public Expression times(int multiplier) => 
            new Money(amount * multiplier, currency);
        public Money reduce(Bank bank, string to)
        {
            var rate = bank.rate(currency, to);
            return new Money(amount / rate, to);
        }
        public Expression plus(Expression addend)
            => new Sum(this, addend);
    }
}