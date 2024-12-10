
using Quizzapp.Model;
using Quizzpp.Model;
using System.Windows.Input;

namespace Quizzapp.Pages;

public partial class ManageQuizPage : ContentPage
{
    private readonly LocalDBService _localDbService;
    string _user;
    Quiz quiz;
    //public ICommand RefreshCommand { get; set; }
    //public bool IsRefreshing { get; set; }
    public ManageQuizPage(LocalDBService _dbService, string user)
	{
		InitializeComponent();
        _localDbService = _dbService;
        _user = user;
        //RefreshCommand = new Command(Refresh);

        //listView.RefreshCommand = new Command(Refresh);

        Task.Run(async () => listView.ItemsSource = await _localDbService.GetYourQuizzes(user));


    }

    private bool isPageActive;

    protected override void OnAppearing()
    {
        base.OnAppearing();
        isPageActive = true;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        isPageActive = false;
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

    private async void AddQuiz_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AddQuizPage(_localDbService, _user));

    }

    private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {

    }

    private async void listView_Scrolled(object sender, ScrolledEventArgs e)
    {
      
    }

    private async void listView_ItemTapped_1(object sender, ItemTappedEventArgs e)
    {
        quiz = (Quiz)e.Item;

        var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

        switch (action)
        {
            case "Edit":
                await Navigation.PushModalAsync(new EditQuizPage(_localDbService, quiz, _user));
                break;
            case "Delete":
                await _localDbService.Delete(quiz);
                Refresh();

                break;
            default:
                break;
        }
    }

    private async void listView_Refreshing(object sender, EventArgs e)
    {
     
    }

    private async void listView_Refreshing_1(object sender, EventArgs e)
    {

        Task.Delay(500);

        await Task.Run(() => {
            MainThread.BeginInvokeOnMainThread(async () =>
            {

                listView.ItemsSource = await _localDbService.GetYourQuizzes(_user);
     
            });
        });

        
      
    }

    private async void Switch_Toggled(object sender, ToggledEventArgs e)
    {

    }

    private async void GoBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new QuizListPage(_localDbService, _user));
    }
}