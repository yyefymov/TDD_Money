using System;
using Xunit;

namespace MultiCurrencyMoney
{
    public class UnitTest1
    {
        [Fact]
        public void testMultiplication()
        {
            Money five = Money.dollar(5);
            Assert.Equal(Money.dollar(10), five.times(2));
            Assert.Equal(Money.dollar(15), five.times(3));
        }

        [Fact]
        public void testEquality()
        {
            Assert.True(Money.dollar(5).Equals(Money.dollar(5)));
            Assert.False(Money.dollar(5).Equals(Money.dollar(6)));

            Assert.False(Money.franc(5).Equals(Money.dollar(5)));
        }

        [Fact]
        public void testCurrency()
        {
            Assert.Equal("USD", Money.dollar(1).currency);
            Assert.Equal("CHF", Money.franc(1).currency);
        }

        [Fact]
        public void testSimpleAddition()
        {
            Money five = Money.dollar(5);
            Expression sum = five.plus(five);
            Bank bank = new Bank();
            Money reduced = bank.reduce(sum, "USD");
            Assert.Equal(Money.dollar(10), reduced);
        }

        [Fact]
        public void testPlusReturnsSum()
        {
            Money five = Money.dollar(5);
            Expression result = five.plus(five);
            Sum sum = (Sum)result;
            Assert.Equal(five, sum.augend);
            Assert.Equal(five, sum.addend);
        }

        [Fact]
        public void testReduceSum()
        {
            Expression sum = new Sum(Money.dollar(3), Money.dollar(4));
            Bank bank = new Bank();
            Money result = bank.reduce(sum, "USD");
            Assert.Equal(Money.dollar(7), result);
        }

        [Fact]
        public void testReduceMoney()
        {
            Bank bank = new Bank();
            Money result = bank.reduce(Money.dollar(1), "USD");
            Assert.Equal(Money.dollar(1), result);
        }

        [Fact]
        public void testReduceMoneyDifferentCurrency()
        {
            Bank bank = new Bank();
            bank.addRate("CHF", "USD", 2);
            Money result = bank.reduce(Money.franc(2), "USD");
            Assert.Equal(Money.dollar(1), result);
        }

        [Fact]
        public void testArrayEquals()
        {
            Assert.Equal(new object[] {"abc"}, new object[] {"abc"});
        }

        [Fact]
        public void testIdentityRate()
        {
            Assert.Equal(1, new Bank().rate("USD", "USD"));
        }

        [Fact]
        public void testMixedAddition()
        {
            Expression fiveBucks = Money.dollar(5);
            Expression tenFrancs = Money.franc(10);
            Bank bank = new Bank();
            bank.addRate("CHF", "USD", 2);
            Money result = bank.reduce(fiveBucks.plus(tenFrancs), "USD");
            Assert.Equal(Money.dollar(10), result);
        }

        [Fact]
        public void testSumPlusMoney()
        {
            Expression fiveBucks = Money.dollar(5);
            Expression tenFrancs = Money.franc(10);
            Bank bank = new Bank();
            bank.addRate("CHF", "USD", 2);
            Expression sum = new Sum(fiveBucks, tenFrancs).plus(fiveBucks);
            Money result = bank.reduce(sum, "USD");
            
            Assert.Equal(Money.dollar(15), result);
        }

        [Fact]
        public void testSumTimes()
        {
            Expression fiveBucks = Money.dollar(5);
            Expression tenFrancs = Money.franc(10);
            Bank bank = new Bank();
            bank.addRate("CHF", "USD", 2);
            Expression sum = new Sum(fiveBucks, tenFrancs).times(2);
            Money result = bank.reduce(sum, "USD");

            Assert.Equal(Money.dollar(20), result);
        }
    }
}
