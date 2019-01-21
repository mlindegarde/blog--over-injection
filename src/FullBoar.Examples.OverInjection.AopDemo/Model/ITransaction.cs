namespace FullBoar.Examples.OverInjection.AopDemo.Model
{
    public interface ITransaction
    {
        #region Properties
        int Amount { get; set; }
        bool IsValid { get; }
        #endregion
    }
}
