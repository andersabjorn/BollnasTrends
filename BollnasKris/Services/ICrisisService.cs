using BollnasKris.Models;

namespace BollnasKris.Services;

/// <summary>
/// Interface för att skicka och ta emot krismeddelanden
/// Kan implementeras med BLE, WiFi Hotspot, eller annat
/// </summary>
public interface ICrisisService
{
    /// <summary>
    /// Event som triggas när ett nytt meddelande tas emot
    /// </summary>
    event EventHandler<CrisisMessage>? MessageReceived;

    /// <summary>
    /// Startar lyssnande efter meddelanden
    /// </summary>
    Task StartListeningAsync();

    /// <summary>
    /// Stoppar lyssnande
    /// </summary>
    Task StopListeningAsync();

    /// <summary>
    /// Skickar ett meddelande till enheter i närheten
    /// </summary>
    Task BroadcastMessageAsync(CrisisMessage message);

    /// <summary>
    /// Om tjänsten är aktiv och lyssnar
    /// </summary>
    bool IsListening { get; }
}
