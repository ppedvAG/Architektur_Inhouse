
namespace Calculator.Tests
{
    //Bankkonto
    //- Kontostand abfragen
    //- Betrag einzahlen(nicht Negativ or 0)
    //- Betrag abheben(nicht Negativ)
    //	- Darf nicht unter 0 fallen
    //- Neues Konto hat 0 als Kontostand

    public class BankAccountTests
    {
        [Fact]
        public void New_bankaccount_should_have_zero_as_balance()
        {
            var ba = new BankAccount();

            Assert.Equal(0m, ba.Balance);
        }

        [Fact]
        public void Deposit_should_add_to_balance()
        {
            var ba = new BankAccount();

            ba.Deposit(4m);
            ba.Deposit(4m);

            Assert.Equal(8m, ba.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Deposit_a_negative_or_zero_value_should_throw_ArgumentEx(decimal value)
        {
            var ba = new BankAccount();

            Assert.Throws<ArgumentException>(() => ba.Deposit(value));
        }

        [Fact]
        public void Withdraw_should_substract_from_balance()
        {
            var ba = new BankAccount();
            ba.Deposit(10m);

            ba.Withdraw(4m);

            Assert.Equal(6m, ba.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Withdraw_a_negative_or_zero_value_should_throw_ArgumentEx(decimal value)
        {
            var ba = new BankAccount();

            Assert.Throws<ArgumentException>(() => ba.Withdraw(value));
        }

        [Fact]
        public void Withdraw_more_than_balance_throws_InvalidOperationEx()
        {
            var ba = new BankAccount();
            ba.Deposit(10m);
          
            Assert.Throws<InvalidOperationException>(() => ba.Withdraw(12m));
        }
    }
}
