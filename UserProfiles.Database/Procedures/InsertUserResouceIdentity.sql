create procedure [dbo].[InsertUserResouceIdentity] 
(
	@userId int,
	@identityType int,
	@identityId int
)
as
begin

	declare @resourceIdentityId int = (select [Id] from [dbo].[ResourceIdentity] 
										where [IdentityType] = @identityType and [IdentityId] = @identityId);

	insert into [dbo].[UserResourceIdentity] ([UserId], [ResourceIdentityId])
	values (@userId, @resourceIdentityId);
	
end