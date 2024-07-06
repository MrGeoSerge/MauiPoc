using CommunityToolkit.Mvvm.ComponentModel;
using MauiPoc.Models.Permissions;
using MauiPoc.Services;
using Microsoft.AppCenter.Crashes;
using Plugin.Firebase.CloudMessaging;
using System.Windows.Input;

namespace MauiPoc.ViewModels;
public partial class MainPageViewModel : ObservableObject
{
    private readonly ILogger<MainPageViewModel> _logger;

    [ObservableProperty]
    private string _token = string.Empty;

    private readonly ApiService _apiService;

    public MainPageViewModel(ILogger<MainPageViewModel> logger)
    {
        _apiService = new ApiService();
        _logger = logger;
        SendDataCommand = new Command(async () => await SendDataAsync());
        CopyToBufferCommand = new Command(async () => await CopyToBufferAsync());
    }

    public ICommand SendDataCommand { get; }

    public ICommand CopyToBufferCommand { get; }

    private async Task SendDataAsync()
    {
        try
        {
            var result = await _apiService.SendDataAsync(_token);
            await Application.Current.MainPage.DisplayAlert("Token has been sent", result, "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
    }
    private async Task CopyToBufferAsync()
    {
        await Clipboard.SetTextAsync(_token);

        await Application.Current.MainPage.DisplayAlert("Copied", "Text copied to clipboard", "OK");
    }

    public async Task LoadAsync()
    {
        try
        {
            if (!CrossFirebaseCloudMessaging.IsSupported)
            {
                _logger.LogWarning("Firebase messaging not supported on this device");
                return;
            }

            var status = await Permissions.CheckStatusAsync<NotificationPermission>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<NotificationPermission>();
            }
            await CrossFirebaseCloudMessaging.Current.CheckIfValidAsync();
            var firebaseToken = await CrossFirebaseCloudMessaging.Current.GetTokenAsync();

            if (!string.IsNullOrEmpty(firebaseToken))
            {
                _logger.LogInformation("Firebase token {token}", firebaseToken);

                Token = firebaseToken;
            }
            else
            {
                throw new InvalidOperationException("Firebase token could not be retrieved");
            }

        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
            _logger.LogError(ex, "Error getting firebase token: {message}", ex.Message);
        }
    }
}
