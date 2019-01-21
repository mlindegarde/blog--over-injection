namespace FullBoar.Examples.OverInjection.Reference.Model
{
    public class Withdrawal : ITransaction
    {
        #region Properties
        public int Amount { get; set; }

        public bool IsValid => Amount > 0;
        #endregion

        #region Constructors
        public Withdrawal() { }

        public Withdrawal(int amount)
        {
            Amount = amount;
        }
        #endregion
    }
}
