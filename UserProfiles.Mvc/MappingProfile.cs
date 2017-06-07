using AutoMapper;
using UserProfiles.Common.Models.Entities;
using UserProfiles.Mvc.Models;

namespace UserProfiles.Mvc
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<ClaimBase, Claim>();
            CreateMap<Claim, ClaimBase>();
            CreateMap<RoleDto, Role>();
            CreateMap<RoleDto, RoleDto>();
            CreateMap<ResourceIdentityDto, ResourceIdentity>();
            CreateMap<ResourceIdentity, ResourceIdentityDto>();
        }
    }
}
