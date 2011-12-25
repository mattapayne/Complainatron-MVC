USE [db393ce04f17294b27ba1f9fac0023fe9b]
GO
/****** Object:  Table [dbo].[Complaints]    Script Date: 12/01/2011 09:37:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Complaints](
	[ComplaintId] [uniqueidentifier] NOT NULL,
	[ComplaintText] [nvarchar](max) NOT NULL,
	[Longitude] [decimal](9, 6) NOT NULL,
	[Latitude] [decimal](9, 6) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[IPAddress] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Complaints] PRIMARY KEY CLUSTERED 
(
	[ComplaintId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 12/01/2011 09:37:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[StatusCode] [nvarchar](50) NOT NULL,
	[LogText] [nvarchar](max) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Likes]    Script Date: 12/01/2011 09:37:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Likes](
	[LikeId] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Count] [int] NOT NULL,
	[ComplaintId] [uniqueidentifier] NOT NULL,
	[IPAddress] [nvarchar](50) NULL,
 CONSTRAINT [PK_Likes] PRIMARY KEY CLUSTERED 
(
	[LikeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dislikes]    Script Date: 12/01/2011 09:37:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dislikes](
	[DislikeId] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Count] [int] NOT NULL,
	[ComplaintId] [uniqueidentifier] NOT NULL,
	[IPAddress] [nvarchar](50) NULL,
 CONSTRAINT [PK_Dislikes] PRIMARY KEY CLUSTERED 
(
	[DislikeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CountedComplaints]    Script Date: 12/01/2011 09:37:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CountedComplaints]
AS
SELECT DISTINCT c.ComplaintId, c.ComplaintText, c.Longitude, c.Latitude, c.Username, c.DateCreated, COUNT(l.Count) AS Likes, COUNT(dl.Count) AS Dislikes
FROM         dbo.Complaints AS c LEFT OUTER JOIN
                      dbo.Likes AS l ON l.ComplaintId = c.ComplaintId LEFT OUTER JOIN
                      dbo.Dislikes AS dl ON dl.ComplaintId = c.ComplaintId
GROUP BY c.ComplaintId, c.ComplaintText, c.Longitude, c.Latitude, c.Username, c.DateCreated
GO

/****** Object:  Default [DF_Complaints_ComplaintId]    Script Date: 12/01/2011 09:37:26 ******/
ALTER TABLE [dbo].[Complaints] ADD  CONSTRAINT [DF_Complaints_ComplaintId]  DEFAULT (newid()) FOR [ComplaintId]
GO
/****** Object:  Default [DF_Complaints_DateCreated]    Script Date: 12/01/2011 09:37:26 ******/
ALTER TABLE [dbo].[Complaints] ADD  CONSTRAINT [DF_Complaints_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
/****** Object:  Default [DF_Logs_Id]    Script Date: 12/01/2011 09:37:26 ******/
ALTER TABLE [dbo].[Logs] ADD  CONSTRAINT [DF_Logs_Id]  DEFAULT (newid()) FOR [Id]
GO
/****** Object:  Default [DF_Likes_LikeId]    Script Date: 12/01/2011 09:37:26 ******/
ALTER TABLE [dbo].[Likes] ADD  CONSTRAINT [DF_Likes_LikeId]  DEFAULT (newid()) FOR [LikeId]
GO
/****** Object:  Default [DF_Likes_Count]    Script Date: 12/01/2011 09:37:26 ******/
ALTER TABLE [dbo].[Likes] ADD  CONSTRAINT [DF_Likes_Count]  DEFAULT ((0)) FOR [Count]
GO
/****** Object:  Default [DF_Dislikes_DislikeId]    Script Date: 12/01/2011 09:37:26 ******/
ALTER TABLE [dbo].[Dislikes] ADD  CONSTRAINT [DF_Dislikes_DislikeId]  DEFAULT (newid()) FOR [DislikeId]
GO
/****** Object:  ForeignKey [FK_Likes_Complaints]    Script Date: 12/01/2011 09:37:26 ******/
ALTER TABLE [dbo].[Likes]  WITH CHECK ADD  CONSTRAINT [FK_Likes_Complaints] FOREIGN KEY([ComplaintId])
REFERENCES [dbo].[Complaints] ([ComplaintId])
GO
ALTER TABLE [dbo].[Likes] CHECK CONSTRAINT [FK_Likes_Complaints]
GO
/****** Object:  ForeignKey [FK_Dislikes_Complaints]    Script Date: 12/01/2011 09:37:26 ******/
ALTER TABLE [dbo].[Dislikes]  WITH CHECK ADD  CONSTRAINT [FK_Dislikes_Complaints] FOREIGN KEY([ComplaintId])
REFERENCES [dbo].[Complaints] ([ComplaintId])
GO
ALTER TABLE [dbo].[Dislikes] CHECK CONSTRAINT [FK_Dislikes_Complaints]
GO
