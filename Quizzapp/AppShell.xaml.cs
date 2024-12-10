using Quizzpp.Model;

namespace Quizzapp
{
    public partial class AppShell : Shell
    {
        private readonly LocalDBService _localDbService;

        public AppShell(LocalDBService _dbService)
        {
            InitializeComponent();
            _localDbService = _dbService;

        }

        public AppShell()
        {
            InitializeComponent();

        }

        private void QuizListPageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void AvailableQuizzesButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
