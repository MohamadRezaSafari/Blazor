using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Websocket.Services.Dto;

namespace Websocket.Services;

public class Stock
{
    private readonly Uri uri =
        new Uri("wss://ws.finnhub.io?token=cmjrgapr01qnr60h1hs0cmjrgapr01qnr60h1hsg");
    private ClientWebSocket webSocket;

    public async Task ConnectAsync()
    {
        webSocket = new ClientWebSocket();
        await webSocket.ConnectAsync(uri, CancellationToken.None);
    }

    public async IAsyncEnumerable<string> SendAsync(object obj)
    {
        string sendMessage = JsonSerializer.Serialize(obj);
        ArraySegment<byte> sendBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(sendMessage));
        await webSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);

        while (webSocket.State == WebSocketState.Open)
        {
            WebSocketReceiveResult result;
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
            do
            {
                result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                Console.WriteLine("Received message: " + message);
                yield return message;
            }
            while (!result.EndOfMessage);
        }

        yield return String.Empty;
    }

    public async Task<ResponseDto> DeserializeResponse(string message)
    {
        var response = JsonSerializer.Deserialize<ResponseDto>(message);
        return await Task.FromResult(response);
    }
}
