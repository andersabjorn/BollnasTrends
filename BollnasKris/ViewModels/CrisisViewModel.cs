using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BollnasKris.Models;
using BollnasKris.Services;

namespace BollnasKris.ViewModels;

public class CrisisViewModel : INotifyPropertyChanged
{
    private readonly ICrisisService _crisisService;
    private readonly HashSet<string> _receivedMessageIds = new();

    public ObservableCollection<CrisisMessage> Messages { get; } = new();

    private bool _isListening;
    public bool IsListening
    {
        get => _isListening;
        set { _isListening = value; OnPropertyChanged(); OnPropertyChanged(nameof(StatusText)); }
    }

    private bool _autoRelay = true;
    public bool AutoRelay
    {
        get => _autoRelay;
        set { _autoRelay = value; OnPropertyChanged(); }
    }

    public string StatusText => IsListening ? "🟢 Lyssnar..." : "🔴 Avstängd";

    public ICommand ToggleListeningCommand { get; }
    public ICommand SendTestMessageCommand { get; }
    public ICommand ClearMessagesCommand { get; }

    public CrisisViewModel(ICrisisService crisisService)
    {
        _crisisService = crisisService;
        _crisisService.MessageReceived += OnMessageReceived;

        ToggleListeningCommand = new RelayCommand(async () => await ToggleListeningAsync());
        SendTestMessageCommand = new RelayCommand(async () => await SendTestMessageAsync());
        ClearMessagesCommand = new RelayCommand(ClearMessages);
    }

    private async Task ToggleListeningAsync()
    {
        if (IsListening)
        {
            await _crisisService.StopListeningAsync();
            IsListening = false;
        }
        else
        {
            await _crisisService.StartListeningAsync();
            IsListening = true;
        }
    }

    private void OnMessageReceived(object? sender, CrisisMessage message)
    {
        // Undvik dubbletter
        if (_receivedMessageIds.Contains(message.Id))
            return;

        _receivedMessageIds.Add(message.Id);

        // Lägg till i listan (på UI-tråden)
        // I MAUI: MainThread.BeginInvokeOnMainThread(() => ...);
        Messages.Insert(0, message); // Nyaste först

        // Auto-relay om aktiverat
        if (AutoRelay && message.HopCount < 10) // Max 10 hopp
        {
            _ = _crisisService.BroadcastMessageAsync(message);
        }
    }

    private async Task SendTestMessageAsync()
    {
        var message = new CrisisMessage
        {
            Content = "📢 Testmeddelande skickat från denna enhet",
            Source = "Test",
            Priority = 3
        };

        await _crisisService.BroadcastMessageAsync(message);
    }

    private void ClearMessages()
    {
        Messages.Clear();
        _receivedMessageIds.Clear();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

/// <summary>
/// Enkel RelayCommand implementation
/// I MAUI kan ni använda CommunityToolkit.Mvvm istället
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Func<Task>? _executeAsync;
    private readonly Action? _execute;

    public RelayCommand(Func<Task> executeAsync) => _executeAsync = executeAsync;
    public RelayCommand(Action execute) => _execute = execute;

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => true;

    public async void Execute(object? parameter)
    {
        if (_executeAsync != null)
            await _executeAsync();
        else
            _execute?.Invoke();
    }
}
