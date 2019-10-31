using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.Test.RepositoriesTest
{
    [TestFixture]
    public class TestMembers : TestBase
    {
        [Test, Order(1)]
        public void GetAllMembers()
        {
            //Act
            int membersCount = unit.Members.Get().Count();

            //Assert
            Assert.AreEqual(6, membersCount); //there are 2 members in the test database
        }

        [Test, Order(2)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public void GetMemberById(int id, int employeeId)
        {
            var result = unit.Members.Get(id);
            Assert.AreEqual(result.Employee.Id, employeeId);
        }

        [Test, Order(3)]
        public void GetMemberByWrongId()
        {
            int id = 40; //Member with id doesn't exist in the test database
            var result = unit.Members.Get(id);
            Assert.IsNull(result);
        }


        [Test, Order(4)]
        public void InsertMember()
        {
            Member member = new Member
            {
                Team = unit.Teams.Get(1),
                Employee = unit.Employees.Get(58),
                Role = unit.Roles.Get(1),
                Status = unit.MemberStatuses.Get(1)
            };
            unit.Members.Insert(member);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(7, member.Id);//id of the new member will be 7
        }

        [Test, Order(5)]
        public void ChangeMembersRole()
        {
            int id = 2;//Try to change the member with id
            int roleId = 3;
            Member member = new Member
            {
                Id = id,
                Team = unit.Teams.Get(1),
                Employee = unit.Employees.Get(58),
                Role = unit.Roles.Get(roleId),
                Status = unit.MemberStatuses.Get(1)
            };
            unit.Members.Update(member, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(roleId, member.Role.Id);
        }

        [Test, Order(6)]
        public void ChangeMembersStatus()
        {
            int id = 2;//Try to change the member with id
            int statusId = 3;
            Member member = new Member
            {
                Id = id,
                Team = unit.Teams.Get(1),
                Employee = unit.Employees.Get(58),
                Role = unit.Roles.Get(1),
                Status = unit.MemberStatuses.Get(statusId)
            };
            unit.Members.Update(member, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
            Assert.AreEqual(statusId, member.Status.Id);
        }

        [Test, Order(7)]
        public void ChangeMemberWithWrongId()
        {
            int id = 40;//Try to change the member with id (doesn't exist)
            Member member = new Member
            {
                Id = id,
                Team = unit.Teams.Get(1),
                Employee = unit.Employees.Get(58),
                Role = unit.Roles.Get(1),
                Status = unit.MemberStatuses.Get(1)
            };
            unit.Members.Update(member, id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }

        [Test, Order(8)]
        public void DeleteMember()
        {
            int id = 2;//Try to delete the member with id

            unit.Members.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(1, numberOfChanges);
        }

        [Test, Order(9)]
        public void DeleteMemberWithWrongId()
        {
            int id = 40;//Try to delete the member with id (doesn't exist)

            unit.Members.Delete(id);
            int numberOfChanges = unit.Save();
            Assert.AreEqual(0, numberOfChanges);
        }
    }
}
