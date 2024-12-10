using Quizzapp.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Quizzpp.Model
{

    public class LocalDBService
    {
        private const string DB_NAME = "quizzapp3_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDBService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Users>();
            _connection.CreateTableAsync<Quiz>();
            _connection.CreateTableAsync<Question>();
            _connection.CreateTableAsync<Score>();

        }

        public async void Init()
        {
            //await _connection.CreateTableAsync<Users>();
            //await _connection.CreateTableAsync<Quiz>();
            //await _connection.CreateTableAsync<Question>();

        }
        public async Task<Users> GetUser(string username)
        {
            Init();
            return await _connection.Table<Users>().Where(u => u.username == username).FirstOrDefaultAsync();
        }

        public async Task<List<Quiz>> GetAllQuiz()
        {

            return await _connection.Table<Quiz>().ToListAsync();
        }
        public Task<List<Quiz>> GetYourQuizzes(string user)
        {
            return _connection.Table<Quiz>().Where(x => x.CreatedBy == user).ToListAsync();
        }

        public Task<List<Quiz>> GetYourQuizzes2(string user)
        {
            return _connection.QueryAsync<Quiz>("SELECT Title, QuizId, Description FROM [Quiz] ");
        }

        public Task<List<Quiz>> GetAvailableQuizzes()
        {
       
            return _connection.Table<Quiz>().Where(x => x.isPublic == "True").ToListAsync();
        }

        public Task<List<Quiz>> GetAvailableQuizzes2(string user)
        {
            //return _connection.QueryAsync<Quiz>("SELECT Title, QuizId, Description, CreatedBy, Score.score, Score.username FROM Quiz INNER JOIN Score ON Score.QuizId = Quiz.QuizId WHERE isPublic = 'True'");
            return _connection.QueryAsync<Quiz>("SELECT q.Title, q.QuizId, q.Description, q.CreatedBy, u.user_id, u.username ,s.score, s.QuizId, s.user_id, s.username FROM Quiz as q FULL OUTER JOIN Scores as s ON s.QuizId = q.QuizId LEFT OUTER JOIN Users as u ON s.user_id = u.user_id  WHERE q.isPublic = 'True'");
        }



        public async Task<List<Question>> GetQuestions(int _QuizId)
        {

            return await _connection.Table<Question>().Where(x => x.QuizId == _QuizId).ToListAsync();
        }

        public async Task<List<Score>> GetStats(int user, int statid, string title)
        {

            return await _connection.Table<Score>().Where(x => x.user_id == user).ToListAsync();
        }

        public async Task Update(Quiz quiz)
        {
            Init();
            await _connection.UpdateAsync(quiz);
        }

        public async Task Update(Question question)
        {
            Init();
            await _connection.UpdateAsync(question);
        }


        public async Task<List<Users>> GetUsers()
        {
            Init();
            return await _connection.Table<Users>().ToListAsync();
        }

        public Task<Quiz> GetQuizID(string title, string author)
        {

            return _connection.Table<Quiz>().Where(x => x.CreatedBy.Equals(author) && x.Title.Equals(title)).FirstOrDefaultAsync();
        }

        public async Task<Users> PangLogin(string un, string pw)
        {

            return await _connection.Table<Users>().Where(x => x.username == un && x.password == pw).FirstOrDefaultAsync();
        }

        public async Task Create(Users users)
        {

            await _connection.InsertAsync(users);
        }

        public async Task Create(Question questions)
        {

            await _connection.InsertAsync(questions);
        }

        public async Task Create(Quiz quiz)
        {

            await _connection.InsertAsync(quiz);
        }

        public async Task Update(Users users)
        {
            Init();
            await _connection.UpdateAsync(users);
        }

        public async Task Delete(Users users)
        {
            Init();
            await _connection.DeleteAsync(users);
        }

        public async Task Delete(Quiz quiz)
        {
            Init();
            await _connection.DeleteAsync(quiz);
        }

        public async Task Delete(Question question)
        {
            Init();
            await _connection.DeleteAsync(question);
        }
    }

}
