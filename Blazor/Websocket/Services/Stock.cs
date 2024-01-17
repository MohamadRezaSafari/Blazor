using Microsoft.AspNetCore.SignalR.Client;
using System.Net.WebSockets;
using System.Text.Json;

namespace Websocket.Services;

public class Stock
{
    private HubConnection connection;

    public Stock()
    {
        

        connection = new HubConnectionBuilder()
            .WithUrl("wss://ws.finnhub.io?token=cmjrgapr01qnr60h1hs0cmjrgapr01qnr60h1hsg")
            .Build();
    }

    public async void Send()
    {
        //var test = await connection
        //    .SendAsync();

        var uri = new Uri("wss://ws.finnhub.io?token=cmjrgapr01qnr60h1hs0cmjrgapr01qnr60h1hsg");

        var aa = new ClientWebSocket();
        await aa
            .ConnectAsync(uri,
            CancellationToken.None);

        var obj = new
        {
            type = "subscribe",
            symbol = "BINANCE:BTCUSDT"
        };

        // System.Text.Json
        string jsonStringSystem = JsonSerializer.Serialize(obj);
        var result = await aa.SendAsync(, WebSocketMessageType.Text, false, CancellationToken.None);
    }
}
