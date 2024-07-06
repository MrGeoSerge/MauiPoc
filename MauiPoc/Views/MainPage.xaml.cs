namespace MauiPoc.Views;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _vm;
    int count = 0;

    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
        _vm = vm;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await _vm.LoadAsync();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string textToCopy = editorInput.Text;

        await Clipboard.SetTextAsync(textToCopy);

        await DisplayAlert("Copied", "Text copied to clipboard", "OK");
    }
}

