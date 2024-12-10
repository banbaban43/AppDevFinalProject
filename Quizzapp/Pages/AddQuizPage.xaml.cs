using Quizzapp.Model;
using Quizzpp.Model;

namespace Quizzapp.Pages;

public partial class AddQuizPage : ContentPage
{
    private readonly LocalDBService _localDbService;
    string _user;
    public AddQuizPage(LocalDBService _dbService, string user)
    {
        InitializeComponent();
        _localDbService = _dbService;
        _user = user;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(EntryTitle.Text) || String.IsNullOrEmpty(EntryDescription.Text))
        { 
            
        }
        else
        {
            string t = EntryTitle.Text;
            string d = EntryDescription.Text;

            var newQuiz = new Quiz
            {
                Title = t,
                Description = d,
                CreatedBy = _user,
                isPublic = "false"
            };

            await _localDbService.Create(newQuiz);

            await Navigation.PushModalAsync(new ManageQuizPage(_localDbService,_user));
            //await Navigation.PopAsync();
        }
    }
}