using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.DAL.Utilities;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL
{
    public class Helper
    {
        
        private static void BuildPassword(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.Password)) user.Password = user.Username.HashWith(user.Password);
        }
    }
}
