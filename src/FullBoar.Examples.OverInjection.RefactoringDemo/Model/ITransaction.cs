﻿namespace FullBoar.Examples.OverInjection.RefactoringDemo.Model
{
    public interface ITransaction
    {
        #region Properties
        int Amount { get; set; }
        bool IsValid { get; }
        #endregion
    }
}
