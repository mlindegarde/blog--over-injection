namespace FullBoar.Examples.OverInjection.FinalDemo.Model
{
    public interface ITransaction
    {
        #region Properties
        int Amount { get; set; }
        bool IsValid { get; }
        #endregion
    }
}
