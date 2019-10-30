using System;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Utility.Services
{
    public static class TimeKeeperUtilities
    {
        public static string MakeUsername(this Employee e)
        {
            return (e.FirstName + e.LastName.Substring(0, 2)).ToLower();
        }
    }
}
