CREATE TABLE [dbo].[SponsorTickets]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SponsorId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [MatchId] INT NOT NULL, 
    [AllocatedTickets] INT NOT NULL, 
    [AllocatedDate] DATETIME NOT NULL, 
    [ModifiedDate] DATETIME NULL, 
    [AllocateBy] INT NULL
)
