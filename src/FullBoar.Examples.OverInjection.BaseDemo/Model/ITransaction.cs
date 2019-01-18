namespace FullBoar.Examples.OverInjection.BaseDemo.Model
{
    public interface ITransaction
    {
        #region Properties
        int Amount { get; set; }
        bool IsValid { get; }
        #endregion
    }
}
