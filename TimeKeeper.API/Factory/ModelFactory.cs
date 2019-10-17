﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Models;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.API.Factory
{
    public static class ModelFactory
    {
        public static TeamModel Create(this Team team)
        {
            return new TeamModel
            {
                Id = team.Id,
                Name = team.Name,
                Members = team.Members.Select(x => x.Master()).ToList(),
                Projects = team.Projects.Select(x => x.Master()).ToList()
            };
        }
        public static EmployeeModel Create(this Employee employee)
        {
            return new EmployeeModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName=employee.LastName,
                Email=employee.Email,
                Phone=employee.Phone,
                Position=new MasterModel { Id=employee.Position.Id, Name=employee.Position.Name},
                Birthday=employee.Birthday,
                BeginDate=employee.BeginDate,
                EndDate=employee.EndDate,
                Status=new MasterModel { Id=employee.Status.Id, Name=employee.Status.Name},
                Members = employee.Members.Select(x => x.Master()).ToList()
            };
        }
        public static CustomerModel Create(this Customer customer)
        {
            return new CustomerModel
            {
                Id = customer.Id,
                Name = customer.Name,
                ContactName = customer.ContactName,
                EmailAddress = customer.EmailAddress,
                HomeAddress = new MasterModelAddress { Street = customer.HomeAddress.Street, City=customer.HomeAddress.City },
                Status = new MasterModel { Id = customer.Status.Id, Name = customer.Status.Name },
                Projects = customer.Projects.Select(x => x.Master()).ToList()
            };
        }
    }
}
