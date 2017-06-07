-- [Account] --
SET IDENTITY_INSERT [dbo].[Merchant] ON

insert into [dbo].[Merchant] ([Id], [Name]) values (1, 'Adidas');
go
insert into [dbo].[Merchant] ([Id], [Name]) values (2, 'Nike');

SET IDENTITY_INSERT [dbo].[Merchant] OFF

-- [Business] --
go
SET IDENTITY_INSERT [dbo].[Business] ON

insert into [dbo].[Business] ([Id], [MerchantId], [Name]) values (1, 1, 'Adidas Shoes');
go
insert into [dbo].[Business] ([Id], [MerchantId], [Name]) values (2, 1, 'Adidas Accessories');
go
insert into [dbo].[Business] ([Id], [MerchantId], [Name]) values (3, 2, 'Nike Shoes');
go
insert into [dbo].[Business] ([Id], [MerchantId], [Name]) values (4, 2, 'Nike Accessories');

SET IDENTITY_INSERT [dbo].[Business] OFF

-- [ResourceIdentity] --
go
SET IDENTITY_INSERT [dbo].[ResourceIdentity] ON

insert into [dbo].[ResourceIdentity] ([Id], [IdentityType], [IdentityId]) values (1, 1, 1);
go
insert into [dbo].[ResourceIdentity] ([Id], [IdentityType], [IdentityId]) values (2, 1, 2);
go
insert into [dbo].[ResourceIdentity] ([Id], [IdentityType], [IdentityId]) values (3, 2, 1);
go
insert into [dbo].[ResourceIdentity] ([Id], [IdentityType], [IdentityId]) values (4, 2, 2);
go
insert into [dbo].[ResourceIdentity] ([Id], [IdentityType], [IdentityId]) values (5, 2, 3);
go
insert into [dbo].[ResourceIdentity] ([Id], [IdentityType], [IdentityId]) values (6, 2, 4);


SET IDENTITY_INSERT [dbo].[ResourceIdentity] OFF

-- [User] --

SET IDENTITY_INSERT [dbo].[User] ON

insert into [dbo].[User] ([Id]) values (1);

SET IDENTITY_INSERT [dbo].[User] OFF

-- [UserResourceIdentity] -- 
go 
insert into [dbo].[UserResourceIdentity] ([UserId], [ResourceIdentityId]) values (1, 1);
go 
insert into [dbo].[UserResourceIdentity] ([UserId], [ResourceIdentityId]) values (1, 2);
go 
insert into [dbo].[UserResourceIdentity] ([UserId], [ResourceIdentityId]) values (1, 3);
go 
insert into [dbo].[UserResourceIdentity] ([UserId], [ResourceIdentityId]) values (1, 4);
go 
insert into [dbo].[UserResourceIdentity] ([UserId], [ResourceIdentityId]) values (1, 5);
go 
insert into [dbo].[UserResourceIdentity] ([UserId], [ResourceIdentityId]) values (1, 6);

-- [Transaction] -- 
go
insert into [dbo].[Transaction] ([MerchantId], [BusinessId], [TransactionDate], [Amount], [Currency]) values (1, 1, '2017-04-12 00:00:00', 150, 'EUR');
go
insert into [dbo].[Transaction] ([MerchantId], [BusinessId], [TransactionDate], [Amount], [Currency]) values (1, 1, '2017-02-20 00:00:00', 100, 'EUR');
go
insert into [dbo].[Transaction] ([MerchantId], [BusinessId], [TransactionDate], [Amount], [Currency]) values (1, 1, '2017-01-24 00:00:00', 125, 'EUR');
go
insert into [dbo].[Transaction] ([MerchantId], [BusinessId], [TransactionDate], [Amount], [Currency]) values (1, 2, '2017-02-02 00:00:00', 132, 'EUR');
go
insert into [dbo].[Transaction] ([MerchantId], [BusinessId], [TransactionDate], [Amount], [Currency]) values (1, 2, '2017-04-12 00:00:00', 200, 'EUR');
go
insert into [dbo].[Transaction] ([MerchantId], [BusinessId], [TransactionDate], [Amount], [Currency]) values (2, 3, '2017-03-20 00:00:00', 410, 'EUR');
go
insert into [dbo].[Transaction] ([MerchantId], [BusinessId], [TransactionDate], [Amount], [Currency]) values (2, 3, '2017-05-20 00:00:00', 123, 'EUR');
go
insert into [dbo].[Transaction] ([MerchantId], [BusinessId], [TransactionDate], [Amount], [Currency]) values (2, 3, '2017-03-20 00:00:00', 982, 'EUR');
go
insert into [dbo].[Transaction] ([MerchantId], [BusinessId], [TransactionDate], [Amount], [Currency]) values (2, 4, '2017-02-20 00:00:00', 234, 'EUR');
go
insert into [dbo].[Transaction] ([MerchantId], [BusinessId], [TransactionDate], [Amount], [Currency]) values (2, 4, '2017-03-20 00:00:00', 150, 'EUR');
go
insert into [dbo].[Transaction] ([MerchantId], [BusinessId], [TransactionDate], [Amount], [Currency]) values (2, 4, '2017-04-20 00:00:00', 150, 'EUR');
go
insert into [dbo].[Transaction] ([MerchantId], [BusinessId], [TransactionDate], [Amount], [Currency]) values (2, 4, '2017-03-20 00:00:00', 150, 'EUR');

-- [Claims] --
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'claim.list');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'claim.details');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'claim.create');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'claim.edit');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'role.list');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'role.details');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'role.create');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'role.edit');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'user.list');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'user.details');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'user.create');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'user.edit');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'identity.list');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'identity.getRoles');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'identity.getPermissions');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'transaction.list');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'transaction.getByMerchant');
go
insert into [Hub.Identity].[dbo].[Claims] ([Type], [Value]) values ('feature', 'transaction.getByBusiness');

