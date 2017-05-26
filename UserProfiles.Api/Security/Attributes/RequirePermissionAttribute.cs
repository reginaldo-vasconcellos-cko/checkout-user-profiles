using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace UserProfiles.Api.Security.Attributes
{
    public class RequirePermissionAttribute : AuthorizeAttribute
    {
        public RequirePermissionAttribute(string permission) : base("RequirePermission")
        {
            Permission = permission;
        }

        public string Permission { get; }
    }
}
