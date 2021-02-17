using System;

namespace MultiCurrencyMoney
{
    class Sum : Expression
    {
        public Sum(Expression augend, Expression addend)
        {
            this.augend = augend;
            this.addend = addend;
        }
        public Expression augend
        {get; private set;}
        public Expression addend
        {get; private set;}

        public Money reduce(Bank bank, string to)
        {
            int amount = augend.reduce(bank, to).amount 
                + addend.reduce(bank,to).amount;
            return new Money(amount, to);
        }
        public Expression plus(Expression addend)
            => new Sum(this, addend);

        public Expression times(int multiplier)
            => new Sum(augend.times(multiplier), addend.times(multiplier));
    }
}