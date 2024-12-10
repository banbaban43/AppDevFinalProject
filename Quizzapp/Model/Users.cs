using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzapp.Model
{
    public class Users
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("user_id")]

        public int user_id { get; set; }
        [Column("username")]
        public string username { get; set; }
        [Column("password")]
        public string password { get; set; }
        [Column("email")]
        public string email { get; set; }


    }
}
