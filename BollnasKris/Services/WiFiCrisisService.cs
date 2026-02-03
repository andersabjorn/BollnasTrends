using System.Net;
using System.Net.Sockets;
using System.Text;
using BollnasKris.Models;

namespace BollnasKris.Services;

/// <summary>
/// WiFi-baserad implementation av ICrisisService
/// Använder UDP broadcast för att skicka meddelanden på lokalt nätverk
///
/// Scenario: En telefon skapar hotspot, andra ansluter
/// Alla kan sedan skicka/ta emot via UDP broadcast
/// </summary>
public class WiFiCrisisService : ICrisisService
{
    public event EventHandler<CrisisMessage>? MessageReceived;
    public bool IsListening { get; private set; }

    private UdpClient? _udpClient;
    private CancellationTokenSource? _cancellationTokenSource;

    // Port för UDP broadcast
    private const int BroadcastPort = 45678;

    public async Task StartListeningAsync()
    {
        IsListening = true;
        _cancellationTokenSource = new CancellationTokenSource();

        try
        {
            _udpClient = new UdpClient(BroadcastPort);
            _udpClient.EnableBroadcast = true;

            // Starta lyssnande i bakgrunden
            _ = ListenForMessagesAsync(_cancellationTokenSource.Token);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Kunde inte starta WiFi-lyssnare: {ex.Message}");
        }

        await Task.CompletedTask;
    }

    private async Task ListenForMessagesAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested && _udpClient != null)
        {
            try
            {
                var result = await _udpClient.ReceiveAsync(cancellationToken);
                var json = Encoding.UTF8.GetString(result.Buffer);
                var message = CrisisMessage.FromJson(json);

                if (message != null)
                {
                    message.ReceivedAt = DateTime.UtcNow;
                    MessageReceived?.Invoke(this, message);
                }
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid mottagning: {ex.Message}");
            }
        }
    }

    public Task StopListeningAsync()
    {
        IsListening = false;
        _cancellationTokenSource?.Cancel();
        _udpClient?.Close();
        _udpClient?.Dispose();
        _udpClient = null;

        return Task.CompletedTask;
    }

    public async Task BroadcastMessageAsync(CrisisMessage message)
    {
        message.HopCount++;
        var json = message.ToJson();
        var bytes = Encoding.UTF8.GetBytes(json);

        try
        {
            using var client = new UdpClient();
            client.EnableBroadcast = true;

            // Broadcast till alla på nätverket
            var endpoint = new IPEndPoint(IPAddress.Broadcast, BroadcastPort);
            await client.SendAsync(bytes, bytes.Length, endpoint);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Kunde inte skicka meddelande: {ex.Message}");
        }
    }
}
