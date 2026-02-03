using BollnasKris.Models;

namespace BollnasKris.Services;

/// <summary>
/// BLE-implementation av ICrisisService
/// Använder Plugin.BLE för cross-platform Bluetooth Low Energy
///
/// NuGet-paket som behövs:
/// - Plugin.BLE
///
/// Permissions som behövs:
/// - Android: BLUETOOTH, BLUETOOTH_ADMIN, ACCESS_FINE_LOCATION
/// - iOS: NSBluetoothAlwaysUsageDescription i Info.plist
/// </summary>
public class BleCrisisService : ICrisisService
{
    public event EventHandler<CrisisMessage>? MessageReceived;
    public bool IsListening { get; private set; }

    // TODO: Lägg till Plugin.BLE beroenden
    // private readonly IBluetoothLE _bluetoothLE;
    // private readonly IAdapter _adapter;

    // Unikt service UUID för BollnäsKris-appen
    // Generera ett nytt för er app: https://www.uuidgenerator.net/
    private const string ServiceUuid = "12345678-1234-1234-1234-123456789abc";
    private const string CharacteristicUuid = "12345678-1234-1234-1234-123456789def";

    public BleCrisisService()
    {
        // TODO: Initiera Plugin.BLE
        // _bluetoothLE = CrossBluetoothLE.Current;
        // _adapter = CrossBluetoothLE.Current.Adapter;
    }

    public async Task StartListeningAsync()
    {
        IsListening = true;

        // TODO: Implementera BLE scanning
        // _adapter.DeviceDiscovered += OnDeviceDiscovered;
        // await _adapter.StartScanningForDevicesAsync();

        // Simulering för test:
        await SimulateIncomingMessage();
    }

    public Task StopListeningAsync()
    {
        IsListening = false;

        // TODO: Stoppa BLE scanning
        // await _adapter.StopScanningForDevicesAsync();

        return Task.CompletedTask;
    }

    public async Task BroadcastMessageAsync(CrisisMessage message)
    {
        // Öka hop count när vi vidarebefordrar
        message.HopCount++;

        // TODO: Implementera BLE advertising/GATT server
        // Detta kräver att telefonen agerar som en BLE peripheral

        await Task.CompletedTask;
    }

    /// <summary>
    /// Simulerar ett inkommande meddelande för testning
    /// Ta bort denna i produktion
    /// </summary>
    private async Task SimulateIncomingMessage()
    {
        await Task.Delay(2000); // Vänta 2 sek

        var testMessage = new CrisisMessage
        {
            Content = "⚠️ TESTMEDDELANDE: Samling vid Bollnäs torg kl 14:00. Vatten och förnödenheter delas ut.",
            Source = "Räddningstjänsten Bollnäs",
            Priority = 1,
            Location = "Bollnäs torg"
        };

        MessageReceived?.Invoke(this, testMessage);
    }
}
