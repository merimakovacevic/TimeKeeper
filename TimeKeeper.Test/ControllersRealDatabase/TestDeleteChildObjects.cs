﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.ControllersRealDatabase
{
    [TestFixture]
    public class TestDeleteChildObjects : BaseRealDatabase
    {
        [Test, Order(1)]
        public void GetProjects()
        {
            var projects = unit.Projects.Get().ToList();

            Assert.AreEqual(11, projects.Count());        
        }

        [Test, Order(2)]
        public void GetTeamAfterDeletingProject()
        {
            //initially, there are 11 projects in the real database
            var projects = unit.Projects.Get().ToList();
            Assert.AreEqual(11, projects.Count());

            Project project = unit.Projects.Get(8);
            unit.Projects.Delete(project);
            unit.Save();

            //now there are 10 projects
            projects = unit.Projects.Get().ToList();
            Assert.AreEqual(10, projects.Count());

            //this team has 2 projects, and one of them was deleted
            Team team = unit.Teams.Get(3);

            //only one project should be left, but there are actually two showing up - the test fails
            Assert.AreEqual(1, team.Projects.Count);

            project.Deleted = false;
            unit.Save();
        }
    }
}
