namespace Calculator
{
    public class BankAccount
    {
        public decimal Balance { get; private set; }

        public void Deposit(decimal v)
        {
            if (v <= 0)
                throw new ArgumentException();

            if (v > 100)
                v = 99;

            Balance += v;
        }

        public void Withdraw(decimal v)
        {
            if (v <= 0)
                throw new ArgumentException();

            if (v > Balance)
                throw new InvalidOperationException();

            Balance -= v;

        }
    }
}
