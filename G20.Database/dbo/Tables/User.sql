CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserName]             NVARCHAR (256)     NULL,
	[Email]                NVARCHAR (256)     NULL,
	[UserTypeId]           int     NULL,
	[PhoneNumber]          NVARCHAR (MAX)     NULL,
	[Password] NVARCHAR (MAX)     NULL,
	[CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    [IsDeleted]       BIT            NOT NULL DEFAULT 0,
	CONSTRAINT [FK_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_User_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
