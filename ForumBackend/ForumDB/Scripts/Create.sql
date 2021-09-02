Go

CREATE TABLE [dbo].[t_users](
	[f_id] [int] IDENTITY(1,1) NOT NULL,
	[f_email] [varchar](30) NULL,
	[f_userName] [varchar](30) NOT NULL,
	[f_password] [varchar](20) NOT NULL,
	[f_createAt] [datetime] NOT NULL,
 CONSTRAINT [PK_t_users] PRIMARY KEY CLUSTERED 
(
	[f_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]	
) ON [PRIMARY]

Go

CREATE TABLE [dbo].[t_posts](
	[f_id] [int] IDENTITY(1,1) NOT NULL,
	[f_userId] [int] NOT NULL,
	[f_topic] [nvarchar](30) NOT NULL,
	[f_content] [nvarchar](max) NULL,
	[f_CreateAt] [datetime] NOT NULL,
 CONSTRAINT [PK_t_posts] PRIMARY KEY CLUSTERED 
(
	[f_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

Go

CREATE TABLE [dbo].[t_replies](
	[f_id] [int] IDENTITY(1,1) NOT NULL,
	[f_userId] [int] NOT NULL,
	[f_postId] [int] NOT NULL,
	[f_content] [nvarchar](max) NULL,
	[f_createAt] [datetime] NULL,
 CONSTRAINT [PK_t_replies] PRIMARY KEY CLUSTERED 
(
	[f_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

Go

ALTER TABLE [dbo].[t_posts]  WITH CHECK ADD  CONSTRAINT [FK_t_posts_t_users] FOREIGN KEY([f_userId])
REFERENCES [dbo].[t_users] ([f_id])

Go

ALTER TABLE [dbo].[t_posts] CHECK CONSTRAINT [FK_t_posts_t_users]

Go

ALTER TABLE [dbo].[t_replies]  WITH CHECK ADD  CONSTRAINT [FK_t_replies_t_posts] FOREIGN KEY([f_postId])
REFERENCES [dbo].[t_posts] ([f_id])
GO

ALTER TABLE [dbo].[t_replies] CHECK CONSTRAINT [FK_t_replies_t_posts]
GO

ALTER TABLE [dbo].[t_replies]  WITH CHECK ADD  CONSTRAINT [FK_t_replies_t_users] FOREIGN KEY([f_userId])
REFERENCES [dbo].[t_users] ([f_id])
GO

ALTER TABLE [dbo].[t_replies] CHECK CONSTRAINT [FK_t_replies_t_users]
GO