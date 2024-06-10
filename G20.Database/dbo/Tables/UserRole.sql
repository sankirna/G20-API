CREATE TABLE [dbo].[UserRole]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[UserId]       INT            NULL,
	[RoleId]       INT            NULL,
	CONSTRAINT [FK_UserRole_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_UserRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
)
