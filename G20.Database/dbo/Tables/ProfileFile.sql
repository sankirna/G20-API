CREATE TABLE [dbo].[ProfileFile]
(
	[Id] INT NOT NULL IDENTITY , 
    [ProfileId] INT NOT NULL, 
    [FileId] INT NOT NULL,
    [CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    [IsDeleted]       BIT            NULL,
    CONSTRAINT [PK_Profile_File] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Profile_File_AspNetUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Profile_File_AspNetUsers1] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_File_Id] FOREIGN KEY ([FileId]) REFERENCES [dbo].[File] ([Id])
)
