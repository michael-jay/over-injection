﻿namespace FullBoar.Examples.OverInjection.BrokerDemo.Model
{
    public interface ITransaction
    {
        #region Properties
        int Amount { get; set; }
        bool IsValid { get; }
        #endregion
    }
}
