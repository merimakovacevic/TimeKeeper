using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    public class MembersRepository: Repository<Member>
    {
        public MembersRepository(TimeKeeperContext context) : base(context) { }

        public override void Update(Member member, int id)
        {
            Member old = Get(id);

            if (old != null)
            {
                _context.Entry(old).CurrentValues.SetValues(member);
                old.Employee = member.Employee;
                old.Team = member.Team;
                old.Role = member.Role;
            }
        }
    }
}
