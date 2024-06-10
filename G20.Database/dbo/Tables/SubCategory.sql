CREATE TABLE [dbo].[SubCategory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL,
    [CategoryId] INT NOT NULL,
    [ExpirationDate ] DATETIME NULL, 
    [CreatedBy]        INT            NULL,
    [CreatedDateTime]  DATETIME       NULL,
    [UpdatedBy]        INT            NULL,
    [UpdatedDateTime]  DATETIME       NULL,
    [IsDeleted]        BIT            NOT NULL DEFAULT 0,
    CONSTRAINT [FK_SubCategory_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]),
    CONSTRAINT [FK_SubCategory_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_SubCategory_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
