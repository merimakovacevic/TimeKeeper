using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.DAL.Utilities;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL
{
    public static class Builder
    {
        public static async Task Build<T>(this T entity, TimeKeeperContext context)
        {
            if (typeof(T) == typeof(Member)) await BuildRelations(entity as Member, context);
            if (typeof(T) == typeof(Project)) await BuildRelations(entity as Project, context);
            if (typeof(T) == typeof(Day)) await BuildRelations(entity as Day, context);
            if (typeof(T) == typeof(JobDetail)) await BuildRelations(entity as JobDetail, context);
            if (typeof(T) == typeof(Customer)) await BuildRelations(entity as Customer, context);
            if (typeof(T) == typeof(Employee)) await BuildRelations(entity as Employee, context);
            if (typeof(T) == typeof(User)) BuildPassword(entity as User);
        }

        private static void BuildPassword(User user)
        {
            if (!string.IsNullOrWhiteSpace(user.Password)) user.Password = user.Username.HashWith(user.Password);
        }

        private static async Task BuildRelations(Customer entity, TimeKeeperContext context)
        {
            if (entity.Image == null) entity.Image = "";
            entity.Status = await context.CustomerStatuses.FindAsync(entity.Status.Id);
        }


        private static async Task BuildRelations(Project entity, TimeKeeperContext context)
        {
            if (entity.Description == null) entity.Description = "";
            entity.Team = await context.Teams.FindAsync(entity.Team.Id);
            entity.Customer = await context.Customers.FindAsync(entity.Customer.Id);
            entity.Status = await context.ProjectStatuses.FindAsync(entity.Status.Id);
            entity.Pricing = await context.PricingStatuses.FindAsync(entity.Pricing.Id);
        }

        private static async Task BuildRelations(Employee entity, TimeKeeperContext context)
        {
            if (entity.Image == null) entity.Image = "";
            entity.EndDate = new DateTime(1, 1, 1);
            entity.Position = await context.EmployeePositions.FindAsync(entity.Position.Id);
            entity.Status = await context.EmploymentStatuses.FindAsync(entity.Status.Id);
        }

        private static async Task BuildRelations(Member entity, TimeKeeperContext context)
        {
            entity.Team = await context.Teams.FindAsync(entity.Team.Id);
            entity.Role = await context.Roles.FindAsync(entity.Role.Id);
            entity.Employee = await context.Employees.FindAsync(entity.Employee.Id);
            entity.Status = await context.MemberStatuses.FindAsync(entity.Status.Id);
        }

        private static async Task BuildRelations(Day entity, TimeKeeperContext context)
        {
            entity.Employee = await context.Employees.FindAsync(entity.Employee.Id);
            entity.DayType = await context.DayTypes.FindAsync(entity.DayType.Id);
        }

        private static async Task BuildRelations(JobDetail entity, TimeKeeperContext context)
        {
            entity.Day = await context.Calendar.FindAsync(entity.Day.Id);
            entity.Project = await context.Projects.FindAsync(entity.Project.Id);
        }

        /*----------*----------*/

        public static void Update<T>(this T oldEnt, T newEnt)
        {
            if (typeof(T) == typeof(Member)) UpdateRelations(oldEnt as Member, newEnt as Member);
            if (typeof(T) == typeof(Project)) UpdateRelations(oldEnt as Project, newEnt as Project);
            if (typeof(T) == typeof(Day)) UpdateRelations(oldEnt as Day, newEnt as Day);
            if (typeof(T) == typeof(Employee)) UpdateRelations(oldEnt as Employee, newEnt as Employee);
            if (typeof(T) == typeof(JobDetail)) UpdateRelations(oldEnt as JobDetail, newEnt as JobDetail);
            if (typeof(T) == typeof(Customer)) UpdateRelations(oldEnt as Customer, newEnt as Customer);
        }

        private static void UpdateRelations(Employee oldEnt, Employee newEnt)
        {
            oldEnt.Status = newEnt.Status;
            oldEnt.Position = newEnt.Position;
        }

        private static void UpdateRelations(Customer oldEnt, Customer newEnt)
        {
            oldEnt.Status = newEnt.Status;
            oldEnt.HomeAddress = newEnt.HomeAddress;
        }

        private static void UpdateRelations(Project oldEnt, Project newEnt)
        {
            oldEnt.Team = newEnt.Team;
            oldEnt.Customer = newEnt.Customer;
            oldEnt.Status = newEnt.Status;
            oldEnt.Pricing = newEnt.Pricing;
        }

        private static void UpdateRelations(Member oldEnt, Member newEnt)
        {
            oldEnt.Team = newEnt.Team;
            oldEnt.Role = newEnt.Role;
            oldEnt.Employee = newEnt.Employee;
            oldEnt.Status = newEnt.Status;
        }

        private static void UpdateRelations(Day oldEnt, Day newEnt)
        {
            oldEnt.Employee = newEnt.Employee;
            oldEnt.DayType = newEnt.DayType;
        }

        private static void UpdateRelations(JobDetail oldEnt, JobDetail newEnt)
        {
            oldEnt.Day = newEnt.Day;
            oldEnt.Project = newEnt.Project;
        }

        /*----------*----------*/

        public static bool CanDelete<T>(this T entity)
        {
            if (typeof(T) == typeof(Team)) return HasNoChildren(entity as Team);
            if (typeof(T) == typeof(Role)) return HasNoChildren(entity as Role);
            if (typeof(T) == typeof(Customer)) return HasNoChildren(entity as Customer);
            if (typeof(T) == typeof(Project)) return HasNoChildren(entity as Project);
            if (typeof(T) == typeof(Employee)) return HasNoChildren(entity as Employee);
            if (typeof(T) == typeof(Day)) return HasNoChildren(entity as Day);
            return true;
        }

        private static bool HasNoChildren(Team t)
        {
            return t.Projects.Count + t.Members.Count == 0;
        }

        private static bool HasNoChildren(Role r)
        {
            return r.Members.Count == 0;
        }

        private static bool HasNoChildren(Customer c)
        {
            return c.Projects.Count == 0;
        }

        private static bool HasNoChildren(Project p)
        {
            return p.Tasks.Count == 0;
        }

        private static bool HasNoChildren(Employee e)
        {
            return e.Calendar.Count + e.Members.Count == 0;
        }

        private static bool HasNoChildren(Day d)
        {
            return d.JobDetails.Count == 0;
        }

    }
}
