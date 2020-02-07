﻿namespace FullBoar.Examples.OverInjection.RefactoringDemo.Model
{
    public class Deposit : ITransaction
    {
        #region Properties
        public int Amount { get; set; }

        public bool IsValid => Amount > 0;
        #endregion

        #region Constructors
        public Deposit() { }

        public Deposit(int amount)
        {
            Amount = amount;
        }
        #endregion
    }
}
