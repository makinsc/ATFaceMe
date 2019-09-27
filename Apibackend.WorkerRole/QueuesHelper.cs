using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WRColaTokens
{
    public static class QueuesHelper
    {
        private static readonly int _maxRetries = 10;
        private static readonly int _timeToWaitUntilNextTry = 2000;
        public static QueueClient GetQueueClient(IServiceBusManager sb, string queueName)
        {
            QueueClient client;
            int retries = 0;
            do
            {
                retries++;
                client = sb.CreateQueueClient(queueName);
                if (client != null) break;
                Thread.Sleep(_timeToWaitUntilNextTry);
            }
            while (retries != _maxRetries);

            return client;
        }
    }
}
