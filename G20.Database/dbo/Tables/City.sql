CREATE TABLE [dbo].[City] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Name]    VARCHAR (100) NOT NULL,
    [StateId] INT           NULL,
    [CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    [IsDeleted]       BIT            NULL,
    CONSTRAINT [PK__City__3214EC0713C8B88C] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__City__StateId__06CD04F7] FOREIGN KEY ([StateId]) REFERENCES [dbo].[State] ([Id])
);

