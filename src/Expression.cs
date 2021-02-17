using System;

namespace MultiCurrencyMoney
{
    interface Expression
    {
        Money reduce(Bank bank, string to);
        Expression plus(Expression addend);
        Expression times(int multiplier);
    }
}