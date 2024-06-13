CREATE TABLE [dbo].[Tickets]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductId] INT NOT NULL,
	[TicketCategoryId] INT NOT NULL,
	[TicketsIssue]  int not null,
	[Available] int not null,
	[Blocked]  int not null,
	[Sold]  int not null,	
	[TicketPrice]  decimal(10, 2) not null,
	[CreatedBy]        INT            NULL,
    [CreatedDateTime]  DATETIME       NULL,
    [UpdatedBy]        INT            NULL,
    [UpdatedDateTime]  DATETIME       NULL,
    [IsDeleted]        BIT            NOT NULL DEFAULT 0,
	 CONSTRAINT [FK_Ticket_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Ticket_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id])
)
