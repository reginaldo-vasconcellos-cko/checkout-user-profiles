create procedure [dbo].[InsertUserResouceIdentity] 
(
	@userId int,
	@resourceIdentityId int
)
as
begin

	insert into [dbo].[UserResourceIdentity] ([UserId], [ResourceIdentityId])
	values (@userId, @resourceIdentityId);
	
end