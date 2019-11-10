using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    public class MembersRepository: Repository<Member>
    {
        public MembersRepository(TimeKeeperContext context) : base(context) { }

        private void Build(Member member)
        {
            member.Team = _context.Teams.Find(member.Team.Id);
            member.Employee = _context.Employees.Find(member.Employee.Id);
            member.Role = _context.Roles.Find(member.Role.Id);
            member.Status = _context.MemberStatuses.Find(member.Status.Id);
        }

        public override void Insert(Member member)
        {
            Build(member);
            base.Insert(member);
        }

        public override void Update(Member member, int id)
        {
            Member old = Get(id);
            ValidateUpdate(member, id);

            if (old != null)
            {
                Build(member);
                _context.Entry(old).CurrentValues.SetValues(member);
                old.Employee = member.Employee;
                old.Team = member.Team;
                old.Role = member.Role;
                old.Status = member.Status;
            }
        }
    }
}
