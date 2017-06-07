using Microsoft.AspNetCore.Authorization;

namespace UserProfiles.WebApi.Security.Attributes
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
