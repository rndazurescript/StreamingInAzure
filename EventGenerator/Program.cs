using Microsoft.Azure.ServiceBus;
using Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace EventGenerator
{
    class Program
    {
        // How many duplicate messages the emulator
        // should emit
        private const double duplicationFactor = 0.01;
        private const double MaxWalkDurationInMinutes = 60.0;
        static string eventHubName;
        static string connectionString;
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            var config = File.ReadAllText("secrets.json");
            dynamic configObject = JsonConvert.DeserializeObject(config);
            eventHubName = configObject.EventHubName;
            connectionString = configObject.ConnectionString;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Press Ctrl-C to stop the sender process");
            Console.WriteLine("Press Enter to start now");
            Console.ReadLine();
            SendRandomMessages();
        }

        static void SendRandomMessages()
        {
            // Create a Service Bus queue client to send messages to the Service Bus queue.
            var eventHubClient = new QueueClient(connectionString, eventHubName);

            Int64 count = 0;
            while (true)
            {
                try
                {
                    var generatedMessage = GenerateMessage();
                    var jsonmessage = JsonConvert.SerializeObject(generatedMessage);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, jsonmessage);
                    eventHubClient.SendAsync(new Message(Encoding.UTF8.GetBytes(jsonmessage))).GetAwaiter().GetResult(); // Use synchronous send to preserve message ordering.
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("{0} messages sent!", ++count);
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                    Console.ResetColor();
                }

                Thread.Sleep(200);
            }
        }

        private static Guid lastId = Guid.NewGuid();
        public static DogWalk GenerateMessage()
        {
            // If not send duplicate, change the outgoing ID
            if (rnd.NextDouble() > duplicationFactor)
            {
                lastId = Guid.NewGuid();
            }

            // Allow missing dog ids and pet sitter ids
            return new DogWalk()
            {
                Id = lastId,
                DogId = rnd.Next((int)Dog.GENERATION_MIN_ID, (int)Dog.GENERATION_MAX_ID + 100),
                PetSitterId = rnd.Next((int)PetSitter.GENERATION_MIN_ID, (int)PetSitter.GENERATION_MAX_ID + 100),
                WalkDurationInMinutes = MaxWalkDurationInMinutes * rnd.NextDouble()
            };
        }

    }
}
