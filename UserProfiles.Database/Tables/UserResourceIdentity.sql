CREATE TABLE [dbo].[UserResourceIdentity](
	[UserId] [int] NOT NULL,
	[ResourceIdentityId] [int] NOT NULL,
 CONSTRAINT [PK__UserReso__1788CC4CC529BFC9] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ResourceIdentityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserResourceIdentity]  WITH CHECK ADD  CONSTRAINT [FK_UserResourceIdentity_ResourceIdentity] FOREIGN KEY([ResourceIdentityId])
REFERENCES [dbo].[ResourceIdentity] ([Id])
GO

ALTER TABLE [dbo].[UserResourceIdentity] CHECK CONSTRAINT [FK_UserResourceIdentity_ResourceIdentity]
GO

ALTER TABLE [dbo].[UserResourceIdentity]  WITH CHECK ADD  CONSTRAINT [FK_UserResourceIdentity_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO

ALTER TABLE [dbo].[UserResourceIdentity] CHECK CONSTRAINT [FK_UserResourceIdentity_User]
GO


