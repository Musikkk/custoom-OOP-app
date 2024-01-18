using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppzo_opp_app.Models
{
    public class UserModel
    {
        public UserModel() { }

        public UserModel(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; set; }

        public string Password { get; set; }


        public List<int> BookIds = new List<int>();
    }
}
