using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ChatClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var url = "htttp://localhost:5000/signalr/chat";

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();
        }
    }
}
