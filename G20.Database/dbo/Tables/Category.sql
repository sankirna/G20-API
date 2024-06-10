CREATE TABLE [dbo].[Category]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL,
    [ExpirationDate ] DATETIME NULL, 
    [CreatedBy]        INT            NULL,
    [CreatedDateTime]  DATETIME       NULL,
    [UpdatedBy]        INT            NULL,
    [UpdatedDateTime]  DATETIME       NULL,
    [IsDeleted]        BIT            NOT NULL DEFAULT 0,
    CONSTRAINT [FK_Category_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Category_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
