using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;


namespace WRColaTokens
{
    #region Interface

    public interface IServiceBusManager
    {
        QueueClient CreateQueueClient(string queueName);
        void CreateQueue(string queueName);
        TopicClient CreateTopicClient(string topicName);
        SubscriptionClient CreateSubscriptionClient(string topicName, string subscriptionName);
        void CreateSubscription(string topicName, string subscriptionName, string sqlFilter, string correlationFilter);
        BrokeredMessage CreateBrokeredMessage(string body, Dictionary<string, string> promotedProperties = null, string correlationId = null, string sessionId = null);
        void SendMessage(QueueClient queueClient, BrokeredMessage message);
        MessageReceiver GetMessageReceiver(string path);
        MessageReceiver CreateMessageReceiver(string queuePath, bool isDeadLetter);
        MessageReceiver CreateMessageReceiver(string topicPath, string subscriptionName, bool isDeadLetter);
    }

    #endregion

    #region Class

    public class ServiceBusManager : IServiceBusManager
    {
        #region Private variables

        private readonly NamespaceManager _namespaceManager;
        private readonly MessagingFactory _factory;

        #endregion

        #region Constructor

        public ServiceBusManager(string sbConnectionString)
        {
            // TODO: include security using SAS
            _namespaceManager = NamespaceManager.CreateFromConnectionString(sbConnectionString);

            // TODO: use Crete method
            _factory = MessagingFactory.CreateFromConnectionString(sbConnectionString);
        }

        #endregion

        #region Queue Methods

        public QueueClient CreateQueueClient(string queueName)
        {
            if (!_namespaceManager.QueueExists(queueName))
            {
                return null;
            }
            return _factory.CreateQueueClient(queueName, ReceiveMode.PeekLock);
        }

        public void CreateQueue(string queueName)
        {
            if (_namespaceManager.QueueExists(queueName)) return;
            var queueDesc = GetQueueDescription(queueName);
            _namespaceManager.CreateQueue(queueDesc);
        }

        private QueueDescription GetQueueDescription(string queueName)
        {
            return new QueueDescription(queueName)
            {
                MaxSizeInMegabytes = 5120,
                DefaultMessageTimeToLive = new TimeSpan(1, 0, 0)
            };
        }

        public MessageReceiver GetMessageReceiver(string path)
        {
            return _factory.CreateMessageReceiver(path, ReceiveMode.PeekLock);
        }

        /// <summary>
        /// Create a new <see cref="MessageReceiver"/> object using the specified Service Bus Queue path.
        /// </summary>
        /// <param name="connectionString">The connection string to access the desired service namespace.</param>
        /// <param name="queuePath">The Service Bus Queue path.</param>
        /// <param name="isDeadLetter">True if the desired path is the deadletter queue.</param>
        public MessageReceiver CreateMessageReceiver(string queuePath, bool isDeadLetter)
        {
            return _factory
                .CreateMessageReceiver(isDeadLetter
                    ? QueueClient.FormatDeadLetterPath(queuePath)
                    : queuePath, ReceiveMode.PeekLock);
        }

        /// <summary>
        /// Create a new <see cref="MessageReceiver"/> object using the specified Service Bus Topic Subscription path.
        /// </summary>
        /// <param name="topicPath">The Service Bus Topic path.</param>
        /// <param name="subscriptionName">The Service Bus Topic Subscription name.</param>
        /// <param name="isDeadLetter">True if the desired path is the deadletter subqueue.</param>
        public MessageReceiver CreateMessageReceiver(string topicPath, string subscriptionName, bool isDeadLetter)
        {
            return _factory
                .CreateMessageReceiver(isDeadLetter
                    ? SubscriptionClient.FormatDeadLetterPath(topicPath, subscriptionName)
                    : SubscriptionClient.FormatSubscriptionPath(topicPath, subscriptionName),
                    ReceiveMode.PeekLock);
        }

        #endregion

        #region Topics methods

        public TopicClient CreateTopicClient(string topicName)
        {
            if (!_namespaceManager.TopicExists(topicName)) CreateTopic(topicName);
            return _factory.CreateTopicClient(topicName);
        }

        private void CreateTopic(string topicName)
        {
            var description = new TopicDescription(topicName)
            {
            };
            _namespaceManager.CreateTopic(description);
        }

        #endregion

        #region Subscription methods

        public SubscriptionClient CreateSubscriptionClient(string topicName, string subscriptionName)
        {
            if (!_namespaceManager.SubscriptionExists(topicName, subscriptionName))
            {
                return null;
            }

            return _factory.CreateSubscriptionClient(topicName, subscriptionName, ReceiveMode.PeekLock);
        }

        public void CreateSubscription(string topicName, string subscriptionName, string sqlFilter, string correlationFilter)
        {
            if (_namespaceManager.SubscriptionExists(topicName, subscriptionName)) return;

            var subscriptionDescription = GetSubscriptionDescription(topicName, subscriptionName);

            if (!string.IsNullOrEmpty(sqlFilter))
            {
                _namespaceManager.CreateSubscription(subscriptionDescription, new SqlFilter(sqlFilter));
            }
            else if (!string.IsNullOrEmpty(correlationFilter))
            {
                _namespaceManager.CreateSubscription(subscriptionDescription, new CorrelationFilter(correlationFilter));
            }
            else
            {
                _namespaceManager.CreateSubscription(subscriptionDescription);
            }

        }

        private SubscriptionDescription GetSubscriptionDescription(string topicName, string subscriptionName)
        {
            return new SubscriptionDescription(topicName, subscriptionName)
            {

            };
        }

        #endregion

        #region Messages methods

        /// <summary>
        /// Method to create brokered messages
        /// </summary>
        /// <param name="body">JSON string that is include as the body of the message</param>
        /// <param name="promotedProperties">Properties from the body that whant to be promoted as header properties.
        /// These are useful for filtering message in order to create filter for subscriptions.
        /// If the dictionary is null any property is promoted.</param>
        /// <param name="correlationId">CorrelationId property for the brokered message. 
        /// Used for subscriptions routing purposes. If it's null the message won't have CorrelationId</param>
        /// <param name="sessionId">SessionId property for the brokered message.
        /// Used for correlation purposes. If it´s null the message won't have SessionId and won't be sent to a queue with 
        /// te property "RequiredSession" set to true.</param>
        /// <returns>A brokered message</returns>
        public BrokeredMessage CreateBrokeredMessage(
            string body,
            Dictionary<string, string> promotedProperties = null,
            string correlationId = null,
            string sessionId = null)
        {
            var message = new BrokeredMessage(body);

            if (promotedProperties != null && promotedProperties.Count > 0)
            {
                foreach (var property in promotedProperties)
                {
                    message.Properties.Add(property.Key, property.Value);
                }
            }

            if (correlationId != null) message.CorrelationId = correlationId;
            if (sessionId != null) message.SessionId = sessionId;

            return message;
        }

        public void SendMessage(QueueClient queueClient, BrokeredMessage message)
        {
            queueClient.Send(message);
        }

        #endregion
    }

    #endregion
}
