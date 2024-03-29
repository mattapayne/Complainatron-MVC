/****** Object:  ForeignKey [FK_ComplaintTags_Complaints]    Script Date: 12/31/2011 10:08:26 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ComplaintTags_Complaints]') AND parent_object_id = OBJECT_ID(N'[dbo].[ComplaintTags]'))
ALTER TABLE [dbo].[ComplaintTags] DROP CONSTRAINT [FK_ComplaintTags_Complaints]
GO
/****** Object:  ForeignKey [FK_ComplaintTags_Tags]    Script Date: 12/31/2011 10:08:26 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ComplaintTags_Tags]') AND parent_object_id = OBJECT_ID(N'[dbo].[ComplaintTags]'))
ALTER TABLE [dbo].[ComplaintTags] DROP CONSTRAINT [FK_ComplaintTags_Tags]
GO
/****** Object:  Table [dbo].[ComplaintTags]    Script Date: 12/31/2011 10:08:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ComplaintTags]') AND type in (N'U'))
DROP TABLE [dbo].[ComplaintTags]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 12/31/2011 10:08:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Logs]') AND type in (N'U'))
DROP TABLE [dbo].[Logs]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 12/31/2011 10:08:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tags]') AND type in (N'U'))
DROP TABLE [dbo].[Tags]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/31/2011 10:08:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Complaints]    Script Date: 12/31/2011 10:08:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Complaints]') AND type in (N'U'))
DROP TABLE [dbo].[Complaints]
GO
/****** Object:  Table [dbo].[ComplaintSeverities]    Script Date: 12/31/2011 10:08:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ComplaintSeverities]') AND type in (N'U'))
DROP TABLE [dbo].[ComplaintSeverities]
GO
/****** Object:  Default [DF_ComplaintSeverity_DateCreated]    Script Date: 12/31/2011 10:08:26 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ComplaintSeverity_DateCreated]') AND parent_object_id = OBJECT_ID(N'[dbo].[ComplaintSeverities]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ComplaintSeverity_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ComplaintSeverities] DROP CONSTRAINT [DF_ComplaintSeverity_DateCreated]
END


End
GO
/****** Object:  Default [DF_ComplaintSeverity_IsDefault]    Script Date: 12/31/2011 10:08:26 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ComplaintSeverity_IsDefault]') AND parent_object_id = OBJECT_ID(N'[dbo].[ComplaintSeverities]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ComplaintSeverity_IsDefault]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ComplaintSeverities] DROP CONSTRAINT [DF_ComplaintSeverity_IsDefault]
END


End
GO
/****** Object:  Table [dbo].[ComplaintSeverities]    Script Date: 12/31/2011 10:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ComplaintSeverities]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ComplaintSeverities](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[IsDefault] [bit] NOT NULL,
 CONSTRAINT [PK_ComplaintSeverity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Complaints]    Script Date: 12/31/2011 10:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Complaints]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Complaints](
	[Id] [uniqueidentifier] NOT NULL,
	[ComplaintText] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[FacebookUserId] [bigint] NOT NULL,
	[FacebookUserName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Latitude] [decimal](10, 6) NULL,
	[Longitude] [decimal](10, 6) NULL,
	[ComplaintSeverityId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Complaints] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/31/2011 10:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[FacebookId] [bigint] NOT NULL,
	[Email] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 12/31/2011 10:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tags](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 12/31/2011 10:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Logs]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Logs](
	[Id] [uniqueidentifier] NOT NULL,
	[Message] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[ComplaintTags]    Script Date: 12/31/2011 10:08:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ComplaintTags]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ComplaintTags](
	[ComplaintId] [uniqueidentifier] NOT NULL,
	[TagId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ComplaintTags] PRIMARY KEY CLUSTERED 
(
	[ComplaintId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Default [DF_ComplaintSeverity_DateCreated]    Script Date: 12/31/2011 10:08:26 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ComplaintSeverity_DateCreated]') AND parent_object_id = OBJECT_ID(N'[dbo].[ComplaintSeverities]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ComplaintSeverity_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ComplaintSeverities] ADD  CONSTRAINT [DF_ComplaintSeverity_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
END


End
GO
/****** Object:  Default [DF_ComplaintSeverity_IsDefault]    Script Date: 12/31/2011 10:08:26 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ComplaintSeverity_IsDefault]') AND parent_object_id = OBJECT_ID(N'[dbo].[ComplaintSeverities]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ComplaintSeverity_IsDefault]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ComplaintSeverities] ADD  CONSTRAINT [DF_ComplaintSeverity_IsDefault]  DEFAULT ((0)) FOR [IsDefault]
END


End
GO
/****** Object:  ForeignKey [FK_ComplaintTags_Complaints]    Script Date: 12/31/2011 10:08:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ComplaintTags_Complaints]') AND parent_object_id = OBJECT_ID(N'[dbo].[ComplaintTags]'))
ALTER TABLE [dbo].[ComplaintTags]  WITH CHECK ADD  CONSTRAINT [FK_ComplaintTags_Complaints] FOREIGN KEY([ComplaintId])
REFERENCES [dbo].[Complaints] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ComplaintTags_Complaints]') AND parent_object_id = OBJECT_ID(N'[dbo].[ComplaintTags]'))
ALTER TABLE [dbo].[ComplaintTags] CHECK CONSTRAINT [FK_ComplaintTags_Complaints]
GO
/****** Object:  ForeignKey [FK_ComplaintTags_Tags]    Script Date: 12/31/2011 10:08:26 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ComplaintTags_Tags]') AND parent_object_id = OBJECT_ID(N'[dbo].[ComplaintTags]'))
ALTER TABLE [dbo].[ComplaintTags]  WITH CHECK ADD  CONSTRAINT [FK_ComplaintTags_Tags] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ComplaintTags_Tags]') AND parent_object_id = OBJECT_ID(N'[dbo].[ComplaintTags]'))
ALTER TABLE [dbo].[ComplaintTags] CHECK CONSTRAINT [FK_ComplaintTags_Tags]
GO
