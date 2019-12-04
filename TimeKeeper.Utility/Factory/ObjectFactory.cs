using System;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Utility.Factory
{
    public static class ObjectFactory
    {
        public static string MakeUsername(this Employee employee)
        {
            return (employee.FirstName + employee.LastName.Substring(0, 2)).ToLower();
        }

        public static User CreateUser(this Employee employee)
        {
            User user = new User
            {
                Id = employee.Id,
                Name = employee.FullName,
                Username = employee.MakeUsername(),
                Password = "$ch00l",
                Role = "user"
            };

            return user;
        }

    };
}
