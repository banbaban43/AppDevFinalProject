

using Quizzapp.Model;
using Quizzpp.Model;

namespace Quizzapp.Pages;

public partial class EditQuizPage : ContentPage
{
    private readonly LocalDBService _localDbService;
    public Quiz _quiz;
    private int _editQuestionId;
    public string _isPublic = "false";
    public string _user;
    public EditQuizPage(LocalDBService _dbService, Quiz quiz, string user)
	{
		InitializeComponent();
        _quiz = quiz;
        _localDbService = _dbService;
        _user = user;

        Task.Run(async () => listview.ItemsSource = await _localDbService.GetQuestions(_quiz.QuizId));

        EntryTitle.Text = _quiz.Title;
        EntryDescription.Text = _quiz.Description;
        if (_quiz.isPublic == "True")
        {
            switch1.IsToggled = true;
        }

    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        string t = EntryTitle.Text;
        string d = EntryDescription.Text;
        string p = switch1.IsToggled.ToString();

        if (string.IsNullOrEmpty(t) || string.IsNullOrEmpty(d)) 
        {
            
        }
        else
        {
                await _localDbService.Update(new Quiz
                {
                    CreatedBy = _quiz.CreatedBy,
                    QuizId = _quiz.QuizId,
                    Title = t,
                    Description = d,
                    isPublic = p
                });
            //DisplayAlert("x",p,"z");
            await Navigation.PushModalAsync(new ManageQuizPage(_localDbService, _user));
        }
        
    }

    private async void AddQuestionButton_Clicked(object sender, EventArgs e)
    {
        string q = EntryQuestion.Text;
        string a = EntryCorrectAns.Text;

        if (_editQuestionId == 0)
        {

            await _localDbService.Create(new Question
            {
                QuestionText = q,
                CorrectAnswer = a,
                QuizId = _quiz.QuizId
               
            });

        }
        else 
        {
            await _localDbService.Update(new Question 
            {
                Id = _editQuestionId,
                QuestionText = q,
                CorrectAnswer = a,
                QuizId = _quiz.QuizId,
          

            });
        
        }

        EntryQuestion.Text = string.Empty;
        EntryCorrectAns.Text = string.Empty;

       
        listview.ItemsSource = await _localDbService.GetQuestions(_quiz.QuizId);

   
    }

    private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var question = (Question)e.Item;
        var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

        switch (action) 
        {
            case "Edit":
                _editQuestionId = question.Id;
                EntryQuestion.Text = question.QuestionText;
                EntryCorrectAns.Text = question.CorrectAnswer;

                break;
            case "Delete":
                await _localDbService.Delete(question);

                listview.ItemsSource = await _localDbService.GetQuestions(_quiz.QuizId);



                break;
            default:
                break;
        }
    }

    private async void Switch_Toggled(object sender, ToggledEventArgs e)
    {
  
    }
}