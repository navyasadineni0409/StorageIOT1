using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace AzureIOT.Repositories
{
    public class QueueRepository
    {
        static string connectionString = "DefaultEndpointsProtocol=https;AccountName=storagesn230113;AccountKey=hec6ImTmJwRF49x9QhdWRPiMxiy9x+P9q2UPPUdqxCrlaod8YRGqkN9m6qsCvr9oS0alkRgjN9Cg+AStlod6Wg==;EndpointSuffix=core.windows.net";
        public static async Task<bool> CreateQueue(string queueName)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentNullException("Enter queue name");
            }
            try
            {
                QueueClient container = new QueueClient(connectionString, queueName);
                await container.CreateIfNotExistsAsync();
                if (container.Exists())
                {
                    Console.WriteLine("Queue Created:" + container.Name);
                    return true;
                }
                else
                {
                    Console.WriteLine("Check Azure emulater connection and try again");
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task InsertMessage(string queueName, string msg)
        {
            if (string.IsNullOrEmpty(msg))
            {
                throw new ArgumentNullException("Enter message");
            }

            QueueClient container = new QueueClient(connectionString, queueName);
            await container.CreateIfNotExistsAsync();
            if (container.Exists())
            {
                var data = container.SendMessage(msg);
                Console.WriteLine("Queue msg not send");
            }
            else
            {
                Console.WriteLine("Queue msg not sent");
            }
        }
        public static PeekedMessage[] PeekMessage(string queueName, int peekValue)
        {
            QueueClient container = new QueueClient(connectionString, queueName);
            PeekedMessage[] msg = null;
            if (container.Exists())
            {
                msg = container.PeekMessages(peekValue);
            }
            return msg;
        }
        public static async Task UpdateMessage(string queueName, string data)
        {
            QueueClient container = new QueueClient(connectionString, queueName);
            if (container.Exists())
            {
                QueueMessage[] msg = container.ReceiveMessages();
                container.UpdateMessage(msg[0].MessageId, msg[0].PopReceipt, data, TimeSpan.FromSeconds(180));
            }
        }
        public static async Task DequeueMessage(string queueName)
        {
            QueueClient container = new QueueClient(connectionString, queueName);
            if (container.Exists())
            {
                QueueMessage[] msg = container.ReceiveMessages();
                System.Console.WriteLine("Dequeue message" + msg[0].Body);
                container.DeleteMessage(msg[0].MessageId, msg[0].PopReceipt);
            }
        }
        public static async Task DeleteQueue(string queueName)
        {
            QueueClient container = new QueueClient(connectionString, queueName);
            if (container.Exists())
            {
                await container.DeleteAsync();
            }
        }

    }
}
