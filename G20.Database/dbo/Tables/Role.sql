CREATE TABLE [dbo].[Role]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Name]             NVARCHAR (max)     NULL,
	[CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    [IsDeleted]       BIT            NOT NULL DEFAULT 0,
	CONSTRAINT [FK_Role_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Role_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
