using System;
using System.Collections.Generic;
using System.Text;

namespace TimeKeeper.Domain.Entities
{
    public class User : BaseClass
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }//Will have to be encrypted
        public string Role { get; set; }//Enum?
    }
}
