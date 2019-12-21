using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Domain.Entities;

namespace TimeKeeper.DAL.Repositories
{
    public class MembersRepository: Repository<Member>
    {
        public MembersRepository(TimeKeeperContext context) : base(context) { }

        public override void Insert(Member member)
        {
            member.Build(_context);
            base.Insert(member);
        }

        public override void Update(Member member, int id)
        {
            Member old = Get(id);
            ValidateUpdate(member, id);

            if (old != null)
            {
                member.Build(_context);
                _context.Entry(old).CurrentValues.SetValues(member);
                old.Update(member);
            }
        }
    }
}
