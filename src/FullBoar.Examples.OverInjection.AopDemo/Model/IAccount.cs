﻿namespace FullBoar.Examples.OverInjection.AopDemo.Model
{
    public interface IAccount
    {
        void Process(Deposit deposit);
        void Process(Withdrawal withdrawal);
        void Process(Check check);
        void Process(Fee fee);

        int GetBalance();
    }
}