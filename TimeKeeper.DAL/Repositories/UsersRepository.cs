using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;
using TimeKeeper.Utility.Services;

namespace TimeKeeper.DAL.Repositories
{
    /*
    public class UsersRepository : Repository<User>
    {
        
        public UsersRepository(TimeKeeperContext context) : base(context) { }
        public void InsertUser(Employee employee)
        {
            User user = new User
            {
                Id = employee.Id,
                Name = employee.FullName,
                Username = employee.MakeUsername(),
                Password = "$ch00l",
                Role = "user"
            };

            //Does the Add call need to be implemented differently?            
            _context.Users.Add(user);
        }
        
    }
    */
}
