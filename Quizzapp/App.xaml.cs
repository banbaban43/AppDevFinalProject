using Quizzapp.Pages;

namespace Quizzapp
{
    public partial class App : Application
    {
        public App(LoginPage mainPage)
        {
            InitializeComponent();

            MainPage = new NavigationPage(mainPage);
            //MainPage = new MainFlyoutPage();
            //MainPage = new QuizTabbedPage();
        }
    }
}
