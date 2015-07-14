SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* DIMENSION TABLE: dbo.Browsers */
CREATE TABLE [dbo].[Browsers](
	[BrowserId] [uniqueidentifier] NOT NULL,
	[BrowserMajorName] [varchar] (50) NOT NULL,
	[BrowserMinorName] [varchar] (50) NOT NULL,
	[BrowserVersion] [varchar] (16) NOT NULL

	CONSTRAINT [PK_Browsers] PRIMARY KEY CLUSTERED ([BrowserId] ASC),
	CONSTRAINT [AK_Browsers] UNIQUE ([BrowserMajorName],[BrowserMinorName],[BrowserVersion])
)
GO

/* DIMENSION TABLE: dbo.UserAgents */
CREATE TABLE [dbo].[UserAgents](
	[UserAgentId] [uniqueidentifier] NOT NULL,
	[UserAgentName] [varchar] (200) NOT NULL

	CONSTRAINT [PK_UserAgents] PRIMARY KEY CLUSTERED ([UserAgentId] ASC),
	CONSTRAINT [AK_UserAgents] UNIQUE ([UserAgentName])
)
GO

/* DIMENSION TABLE: dbo.Screens */
CREATE TABLE [dbo].[Screens](
	[ScreenId] [uniqueidentifier] NOT NULL,
	[ScreenHeight] [int] NOT NULL,
	[ScreenWidth] [int] NOT NULL,
	[Text] [nvarchar] (100) NOT NULL

	CONSTRAINT [PK_Screens] PRIMARY KEY CLUSTERED ([ScreenId] ASC),
	CONSTRAINT [AK_Screens] UNIQUE ([ScreenHeight], [ScreenWidth], [Text])
)
GO

/* DIMENSION TABLE: dbo.OS */
CREATE TABLE [dbo].[OS](
	[OSId] [uniqueidentifier] NOT NULL,
	[OSName] [varchar] (250) NOT NULL

	CONSTRAINT [PK_OS] PRIMARY KEY CLUSTERED ([OSId] ASC),
	CONSTRAINT [AK_OS] UNIQUE ([OSName])
)
GO