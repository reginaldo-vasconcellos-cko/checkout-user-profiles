using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using UserProfiles.Api.Models.Enums;

namespace UserProfiles.Api.Security.Requirements
{
    public class ResourceAccessRequirement : IAuthorizationRequirement
    {
        public int Id { get; }
        public IdentityType IdentityType { get; }

        public ResourceAccessRequirement(int id, IdentityType identityType)
        {
            Id = id;
            IdentityType = identityType;
        }
    } 
}
