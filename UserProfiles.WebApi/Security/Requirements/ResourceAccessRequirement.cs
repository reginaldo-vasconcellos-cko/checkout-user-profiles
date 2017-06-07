using Microsoft.AspNetCore.Authorization;
using UserProfiles.Common.Models.Enums;

namespace UserProfiles.WebApi.Security.Requirements
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
