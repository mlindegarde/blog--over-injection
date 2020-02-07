﻿namespace FullBoar.Examples.OverInjection.RefactoringDemo.Model
{
    public class Fee : ITransaction
    {
        #region Properties
        public int Amount { get; set; }

        public bool IsValid => Amount > 0;
        #endregion

        #region Constructors
        public Fee() { }

        public Fee(int amount)
        {
            Amount = amount;
        }
        #endregion
    }
}
