# BollnäsKris - Krisinfo utan internet

En app för att sprida krisinformation mellan telefoner när infrastrukturen är nere.

## Koncept

```
📱 ←→ 📱 ←→ 📱 ←→ 📱
   BLE/WiFi mesh
```

Meddelanden sprids automatiskt från telefon till telefon.

## Kom igång

### 1. Skapa MAUI-projekt

```bash
dotnet new maui -n BollnasKris
cd BollnasKris
```

### 2. Kopiera filerna

Kopiera in filerna från detta repo:
- `Models/` → `Models/`
- `Services/` → `Services/`
- `ViewModels/` → `ViewModels/`
- `Views/` → `Views/`

### 3. Installera NuGet-paket

```bash
# För BLE-kommunikation
dotnet add package Plugin.BLE

# För enklare MVVM (valfritt men rekommenderat)
dotnet add package CommunityToolkit.Mvvm
```

### 4. Registrera services i MauiProgram.cs

```csharp
public static MauiApp CreateMauiApp()
{
    var builder = MauiApp.CreateBuilder();
    builder
        .UseMauiApp<App>()
        .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        });

    // Registrera services
    builder.Services.AddSingleton<ICrisisService, WiFiCrisisService>();
    // Eller för BLE:
    // builder.Services.AddSingleton<ICrisisService, BleCrisisService>();

    builder.Services.AddSingleton<CrisisViewModel>();
    builder.Services.AddSingleton<CrisisPage>();

    return builder.Build();
}
```

### 5. Sätt CrisisPage som startsida i App.xaml.cs

```csharp
public App(CrisisPage crisisPage)
{
    InitializeComponent();
    MainPage = new NavigationPage(crisisPage);
}
```

### 6. Android permissions (Platforms/Android/AndroidManifest.xml)

```xml
<uses-permission android:name="android.permission.BLUETOOTH" />
<uses-permission android:name="android.permission.BLUETOOTH_ADMIN" />
<uses-permission android:name="android.permission.BLUETOOTH_SCAN" />
<uses-permission android:name="android.permission.BLUETOOTH_CONNECT" />
<uses-permission android:name="android.permission.BLUETOOTH_ADVERTISE" />
<uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
<uses-permission android:name="android.permission.ACCESS_WIFI_STATE" />
<uses-permission android:name="android.permission.CHANGE_WIFI_STATE" />
<uses-permission android:name="android.permission.INTERNET" />
```

### 7. iOS permissions (Platforms/iOS/Info.plist)

```xml
<key>NSBluetoothAlwaysUsageDescription</key>
<string>BollnäsKris behöver Bluetooth för att kommunicera med andra enheter</string>
<key>NSBluetoothPeripheralUsageDescription</key>
<string>BollnäsKris behöver Bluetooth för att kommunicera med andra enheter</string>
<key>NSLocalNetworkUsageDescription</key>
<string>BollnäsKris behöver nätverksåtkomst för att kommunicera lokalt</string>
```

## Arkitektur

```
BollnasKris/
├── Models/
│   └── CrisisMessage.cs      # Datamodell för meddelanden
├── Services/
│   ├── ICrisisService.cs     # Interface
│   ├── BleCrisisService.cs   # Bluetooth Low Energy impl
│   └── WiFiCrisisService.cs  # UDP broadcast impl
├── ViewModels/
│   └── CrisisViewModel.cs    # UI-logik
└── Views/
    └── CrisisPage.xaml       # Användargränssnitt
```

## Kommunikationsmetoder

### WiFi (UDP Broadcast)
- Enklast att komma igång med
- En telefon skapar hotspot
- Andra ansluter och kan skicka/ta emot

### BLE (Bluetooth Low Energy)
- Mer komplext men "riktigt" offline
- Kräver Plugin.BLE
- Fungerar utan WiFi överhuvudtaget

## Nästa steg (idéer för hackathon)

- [ ] Signering/verifiering av meddelanden
- [ ] Karta med platsinfo
- [ ] Push-notifikationer när nya meddelanden kommer
- [ ] Offline-lagring av meddelanden
- [ ] Admin-läge för att skicka officiella meddelanden
- [ ] Ljudsignal vid kritiska meddelanden

## Licens

MIT - Gör vad ni vill med koden!

---

Lycka till på hackathon! 🚀
