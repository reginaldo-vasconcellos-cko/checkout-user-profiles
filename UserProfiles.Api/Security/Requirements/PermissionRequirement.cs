﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using UserProfiles.Api.Security.Attributes;

namespace UserProfiles.Api.Security.Requirements
{
    public class PermissionRequirement : AuthorizationHandler<PermissionRequirement>, IAuthorizationRequirement
    {
        private const string _claimType = "feature";

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var filterContext = context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;

            if (filterContext != null)
            {
                var controllerActionDescriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;

                if (controllerActionDescriptor != null)
                {

                    var permissionAttr
                        = controllerActionDescriptor.MethodInfo.GetCustomAttribute(typeof(RequirePermissionAttribute), true) as RequirePermissionAttribute;

                    if (permissionAttr != null && context.User.HasClaim(_claimType, permissionAttr.Permission))
                        context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
