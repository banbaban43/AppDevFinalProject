using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzapp.Model
{
    public class Score
    {
        [PrimaryKey, AutoIncrement]
        public int statId { get; set; }
        public int user_id { get; set; }
        public int QuizId { get; set; }
        public string username { get; set; }
        public int score { get; set; }
    }
}
