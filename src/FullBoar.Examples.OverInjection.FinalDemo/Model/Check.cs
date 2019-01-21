namespace FullBoar.Examples.OverInjection.FinalDemo.Model
{
    public class Check : ITransaction
    {
        #region Properties
        public int Number { get; set; }
        public int Amount { get; set; }

        public bool IsValid => Amount > 0;
        #endregion

        #region Constructors
        public Check() { }

        public Check(int number, int amount)
        {
            Number = number;
            Amount = amount;
        }
        #endregion
    }
}
