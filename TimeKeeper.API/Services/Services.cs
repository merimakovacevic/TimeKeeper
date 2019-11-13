﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Factory;
using TimeKeeper.API.Models;
using TimeKeeper.DAL;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Services
{
    public static class Services
    {
        //public static bool IsTeamMember(this Employee firstEmployee, Employee secondEmployee, Team team)
        //{
        //    bool isTeamMember = true;
            
        //}

        public static List<EmployeeModel> GetEmployeeTeams(this UnitOfWork unit, int userId)
        {
            List<Team> userTeams= unit.Teams.Get(x => x.Members.Any(y => y.Employee.Id == userId)).ToList();
            HashSet<Employee> employees = new HashSet<Employee>();
            foreach (Team team in userTeams)
            {
                // team.Members.Select(x => employees.Add(x.Employee));
                foreach (Member member in team.Members)
                {
                    employees.Add(member.Employee);
                }
            }
            return employees.Select(x => x.Create()).ToList();
        }
    }
}
