using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeeper.API.Authorization
{
    public class AdminLeadOrMemberRequirement : IAuthorizationRequirement
    {
        public AdminLeadOrMemberRequirement()
        {

        }
    }
}
