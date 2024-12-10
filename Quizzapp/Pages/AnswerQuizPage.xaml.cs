using Quizzapp.Model;
using Quizzpp.Model;

namespace Quizzapp.Pages;

public partial class AnswerQuizPage : ContentPage
{
    private readonly LocalDBService _dbService;
    string _user;
    public Quiz _quiz;
    public int questionNumber;
    public int totalQuestions;
    public int score;
    List<Question> questions;
    public AnswerQuizPage(LocalDBService dbservice, string user, Quiz quiz)
    {
        InitializeComponent();
        _dbService = dbservice;
        _user = user;
        _quiz = quiz;
        GetQuestions1();
        questionNumber = 0;
        score = 0;

        QuizTitleLabel.Text = quiz.Title;
        AuthorLabel.Text = quiz.CreatedBy;
        
    }

    public async void GetQuestions1()
    {
        questions = await _dbService.GetQuestions(_quiz.QuizId);
        totalQuestions = questions.Count;
        if (totalQuestions > 0)
        {
            QuestionLabel.Text = questions[questionNumber].QuestionText;
            NumbersLabel.Text = "" + (questionNumber + 1) + "/" + totalQuestions;
        }
        else
        {
            DisplayAlert("Alert", "This quiz has no questions yet.", "Okay");
            await Navigation.PopAsync();
        }
    }

    public async void OnAnswerSubmitted()
    {
        if (questions[questionNumber].CorrectAnswer == EntryAnswer.Text)
        {
            AnswerLabel.TextColor = Colors.LightGreen;
            AnswerLabel.Text = "Correct";
            score++;

        }
        else
        {
            AnswerLabel.TextColor = Colors.Red;
            AnswerLabel.Text = "Incorrect, right answer: " + questions[questionNumber].CorrectAnswer;
            
        }
        EntryAnswer.Text = string.Empty;
        EntryAnswer.IsEnabled = false;
        await Task.Delay(1000);

        AnswerLabel.Text = string.Empty;

        questionNumber++;
       
        if (questionNumber < totalQuestions)
        {
            QuestionLabel.Text = questions[questionNumber].QuestionText;
            NumbersLabel.Text = "" + (questionNumber + 1) + "/" + totalQuestions;
            EntryAnswer.IsEnabled = true;
        }
           
        else
        {
            DisplayAlert("Game Over", "Score: " + score + "/" + totalQuestions, "Okay");
            await Navigation.PopModalAsync();
        }
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        if (EntryAnswer.Text != null)
        {
            OnAnswerSubmitted();
         
            
        }


    }
}