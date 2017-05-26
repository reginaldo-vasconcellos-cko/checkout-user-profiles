CREATE TABLE [dbo].[Transaction](
	[Id] [int] NOT NULL IDENTITY,
	[MerchantId] [int] NOT NULL,
	[BusinessId] [int] NOT NULL,
	[TransactionDate] [datetimeoffset](7) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Currency] [char](3) NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Business] FOREIGN KEY([BusinessId])
REFERENCES [dbo].[Business] ([Id])
GO

ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Business]
GO

ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Merchant] FOREIGN KEY([MerchantId])
REFERENCES [dbo].[Merchant] ([Id])
GO

ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Merchant]
GO