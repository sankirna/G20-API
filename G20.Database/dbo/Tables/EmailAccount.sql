CREATE TABLE [dbo].[EmailAccount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisplayName] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Host] [nvarchar](255) NOT NULL,
	[Username] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Port] [int] NOT NULL,
	[EnableSsl] [bit] NOT NULL,
	[MaxNumberOfEmails] [int] NOT NULL,
	[EmailAuthenticationMethodId] [int] NOT NULL,
	[ClientId] [nvarchar](max) NULL,
	[ClientSecret] [nvarchar](max) NULL,
	[TenantId] [nvarchar](max) NULL,
 CONSTRAINT [PK_EmailAccount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO