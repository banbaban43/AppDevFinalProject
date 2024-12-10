
using Quizzapp.Model;
using Quizzpp.Model;

namespace Quizzapp.Pages;

public partial class SignUpPage : ContentPage
{
    private readonly LocalDBService _localDbService;
    public SignUpPage(LocalDBService _dbService)
    {
        InitializeComponent();
        _localDbService = _dbService;
    }

    private async void SignUpButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(UsernameEntry.Text) || String.IsNullOrEmpty(PasswordEntry.Text) ||
            String.IsNullOrEmpty(ConfirmPasswordEntry.Text) || String.IsNullOrEmpty(EmailEntry.Text))
        {
            DisplayAlert("Alert", "Please complete all the fields.", "Okay");
        }
        else if (PasswordEntry.Text == ConfirmPasswordEntry.Text)
        {
            await _localDbService.Create(new Users
            {
                username = UsernameEntry.Text,
                password = PasswordEntry.Text,
                email = EmailEntry.Text
            });

            DisplayAlert("Success!", "Account successfully created.", "Okay");
            await Navigation.PopAsync();
        }
        else
        {
            DisplayAlert("Alert", "Password does not match.", "Okay");
        }

    }
}