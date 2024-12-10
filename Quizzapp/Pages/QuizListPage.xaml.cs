
using Quizzapp.Model;
using Quizzpp.Model;

namespace Quizzapp.Pages;

public partial class QuizListPage : ContentPage
{
    private readonly LocalDBService _localDbService;
    string _user;
    public QuizListPage(LocalDBService _dbService, string user)
	{
		InitializeComponent();
        _localDbService = _dbService;
        _user = user;
        //makeQuiz();
        Task.Run(async () => listView.ItemsSource = await _localDbService.GetAvailableQuizzes());
	}

    private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var quiz = (Quiz)e.Item;
        await Navigation.PushModalAsync(new AnswerQuizPage(_localDbService, _user, quiz));

    }

    private void listView_Scrolled(object sender, ScrolledEventArgs e)
    {

    }

    private void AddQuiz_Clicked(object sender, EventArgs e)
    {

    }

    async void makeQuiz()
    {
        var quiztest = new Quiz
        {
            CreatedBy = "Author1",
            Title = "Title1",
            isPublic = "True"
        };

        var quiztest1 = new Quiz
        {
            CreatedBy = "Author2",
            Title = "Title2",
            isPublic = "True"
        };

        var quiztest2 = new Quiz
        {
            CreatedBy = "Author3",
            Title = "Title3",
            isPublic = "True"
        };


        await _localDbService.Create(quiztest);
        await _localDbService.Create(quiztest1);
        await _localDbService.Create(quiztest2);


    }

    private async void ManageButton_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ManageQuizPage(_localDbService, _user));
    }

    public async void Refresh()
    {
        Task.Delay(500);

        await Task.Run(() => {
            MainThread.BeginInvokeOnMainThread(async () =>
            {

                listView.ItemsSource = await _localDbService.GetYourQuizzes(_user);

            });
        });

    }

    private async void listView_Refreshing(object sender, EventArgs e)
    {
        //listView.ItemsSource = await _localDbService.GetYourQuizzes(_user);

    }
}