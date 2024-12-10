using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzapp.Model
{
    public class Quiz
    {
        [PrimaryKey, AutoIncrement]
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; } // Username
        public string Description {  get; set; }

        public string isPublic { get; set; }
    }
}
