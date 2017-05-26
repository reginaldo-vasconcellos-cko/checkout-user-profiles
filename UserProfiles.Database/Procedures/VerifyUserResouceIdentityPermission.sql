create procedure [dbo].[VerifyUserResouceIdentityPermission] 
(
	@userId int,
	@identityType int,
	@identityId int
)
as
begin

	declare @resourceIdentityId int = (select [Id] from [dbo].[ResourceIdentity] 
										where [IdentityType] = @identityType and [IdentityId] = @identityId);

	select * 
	from [dbo].[UserResourceIdentity]
	where [UserId] = @userId
	and [ResourceIdentityId] = @resourceIdentityId;
	
end