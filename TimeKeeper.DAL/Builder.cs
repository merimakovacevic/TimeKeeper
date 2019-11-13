using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL
{
    public static class Builder
    {
        public static void Build<T>(this T entity, TimeKeeperContext context)
        {
            if (typeof(T) == typeof(Project)) Create(entity as Project, context);
        }

        public static void Relate<T>(this T oldEntity, T newEntity)
        {
            if (typeof(T) == typeof(Project)) Modify(oldEntity as Project, newEntity as Project);
        }

        private static void Create(Project project, TimeKeeperContext context)
        {
            project.Team = context.Teams.Find(project.Team.Id);
            project.Customer = context.Customers.Find(project.Customer.Id);
            project.Status = context.ProjectStatuses.Find(project.Status.Id);
            project.Pricing = context.PricingStatuses.Find(project.Pricing.Id);
        }

        private static void Modify(Project oldProject, Project newProject)
        {
            oldProject.Team = newProject.Team;
            oldProject.Customer = newProject.Customer;
            oldProject.Status = newProject.Status;
            oldProject.Pricing = newProject.Pricing;
        }
    }
}
