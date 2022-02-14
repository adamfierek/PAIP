using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ChatClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var url = "http://localhost:5000/signalr/chat";

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            connection.On<string>("ReceiveMessage", message =>
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(message);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            });

            await connection.StartAsync();

            var line = "";

            do
            {
                line = Console.ReadLine();
                if(line!="")
                {
                    await connection.SendAsync("SendMessage", line);
                }
            }
            while (!string.IsNullOrEmpty(line));
            
        }
    }
}
