using Microsoft.AspNetCore.Authorization;

namespace UserProfiles.Security.Attributes
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
