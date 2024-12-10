using Quizzpp.Model;

namespace Quizzapp.Pages;

public partial class LoginPage : ContentPage
{
    private readonly LocalDBService _localDbService;
    public LoginPage(LocalDBService _dbService)
	{
		InitializeComponent();
        _localDbService = _dbService;
	}

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;
        var loginCheck = await _localDbService.PangLogin(username, password);

        //||

        if (String.IsNullOrEmpty(UsernameEntry.Text) || String.IsNullOrEmpty(PasswordEntry.Text))
        {
            DisplayAlert("Alert", "Please enter a username and password.", "Okay");
        }
        else if (loginCheck != null)
        {
            /*
            
            var navpage = new NavigationPage(new QuizFlyoutPage(_localDbService));
            navpage.IsEnabled = true;
            navpage.IsVisible = true;
       
            navpage.BarBackgroundColor = Colors.White;
            navpage.BarTextColor = Colors.Black;
            */
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            await Navigation.PushAsync(new QuizListPage(_localDbService, username));
            
            //Application.Current.MainPage = new NavigationPage(new AppShell(_localDbService));
        }
        else
        {
            DisplayAlert("Alert", "Invalid username or password.", "Okay");
        }

    }

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        UsernameEntry.Text = string.Empty;
        PasswordEntry.Text = string.Empty;
        await Navigation.PushAsync(new SignUpPage(_localDbService));
    }
}