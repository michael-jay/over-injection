﻿using System;

namespace FullBoar.Examples.OverInjection.BrokerDemo.Messaging.Broker
{
    public interface IMessageBroker
    {
        #region Methods
        void Publish<TMessage>(TMessage message);
        Guid Subscribe<TMessage>(Action<TMessage> action);
        #endregion
    }
}