namespace BollnasKris.Models;

/// <summary>
/// Representerar ett krismeddelande som kan spridas mellan enheter
/// </summary>
public class CrisisMessage
{
    /// <summary>
    /// Unikt ID för meddelandet (för att undvika dubbletter)
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Meddelandets innehåll
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Vem som skickade meddelandet (t.ex. "Räddningstjänsten", "Kommunen")
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// När meddelandet skapades
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// När vi tog emot meddelandet
    /// </summary>
    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Prioritetsnivå (1 = kritiskt, 2 = viktigt, 3 = information)
    /// </summary>
    public int Priority { get; set; } = 2;

    /// <summary>
    /// Antal hopp meddelandet har gjort (för mesh-spårning)
    /// </summary>
    public int HopCount { get; set; } = 0;

    /// <summary>
    /// Plats om relevant (t.ex. "Bollnäs torg")
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Konverterar till JSON för att skicka via BLE/WiFi
    /// </summary>
    public string ToJson()
    {
        return System.Text.Json.JsonSerializer.Serialize(this);
    }

    /// <summary>
    /// Skapar ett CrisisMessage från JSON
    /// </summary>
    public static CrisisMessage? FromJson(string json)
    {
        try
        {
            return System.Text.Json.JsonSerializer.Deserialize<CrisisMessage>(json);
        }
        catch
        {
            return null;
        }
    }
}
