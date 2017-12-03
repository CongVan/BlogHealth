USE [master]
GO
/****** Object:  Database [BlogHealth]    Script Date: 03/12/2017 9:53:15 CH ******/
CREATE DATABASE [BlogHealth]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BlogHealth', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\BlogHealth.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BlogHealth_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\BlogHealth_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BlogHealth] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BlogHealth].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BlogHealth] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BlogHealth] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BlogHealth] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BlogHealth] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BlogHealth] SET ARITHABORT OFF 
GO
ALTER DATABASE [BlogHealth] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BlogHealth] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BlogHealth] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BlogHealth] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BlogHealth] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BlogHealth] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BlogHealth] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BlogHealth] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BlogHealth] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BlogHealth] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BlogHealth] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BlogHealth] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BlogHealth] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BlogHealth] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BlogHealth] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BlogHealth] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BlogHealth] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BlogHealth] SET RECOVERY FULL 
GO
ALTER DATABASE [BlogHealth] SET  MULTI_USER 
GO
ALTER DATABASE [BlogHealth] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BlogHealth] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BlogHealth] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BlogHealth] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [BlogHealth] SET DELAYED_DURABILITY = DISABLED 
GO
USE [BlogHealth]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 03/12/2017 9:53:15 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Categories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Slug] [varchar](100) NULL,
	[Color] [varchar](50) NULL,
	[ParentID] [int] NULL,
	[Level] [int] NULL,
	[Priority] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 03/12/2017 9:53:15 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Posts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](300) NULL,
	[IDCategory] [int] NULL,
	[Likes] [int] NULL,
	[Views] [int] NULL,
	[Shares] [int] NULL,
	[Comments] [int] NULL,
	[CreateDate] [datetime] NULL,
	[Update] [datetime] NULL,
	[Content] [nvarchar](max) NULL,
	[Tag] [nvarchar](300) NULL,
	[Rates] [int] NULL,
	[Slug] [varchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

GO
INSERT [dbo].[Categories] ([ID], [Name], [Slug], [Color], [ParentID], [Level], [Priority]) VALUES (10, N'Mục cha 2', N'muc-cha-2', N'indigo-text', NULL, 1, 5)
GO
INSERT [dbo].[Categories] ([ID], [Name], [Slug], [Color], [ParentID], [Level], [Priority]) VALUES (13, N'Mục cha 3', N'muc-cha-3', N'deep-purple-text', NULL, 1, 2)
GO
INSERT [dbo].[Categories] ([ID], [Name], [Slug], [Color], [ParentID], [Level], [Priority]) VALUES (14, N'Con 2', N'con-2', N'deep-orange-text', 10, 2, 2)
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 

GO
INSERT [dbo].[Posts] ([ID], [Title], [IDCategory], [Likes], [Views], [Shares], [Comments], [CreateDate], [Update], [Content], [Tag], [Rates], [Slug]) VALUES (1, N'test1', 14, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'tag1', NULL, N'test1')
GO
INSERT [dbo].[Posts] ([ID], [Title], [IDCategory], [Likes], [Views], [Shares], [Comments], [CreateDate], [Update], [Content], [Tag], [Rates], [Slug]) VALUES (2, N'test1', 14, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'tag', NULL, N'test1')
GO
INSERT [dbo].[Posts] ([ID], [Title], [IDCategory], [Likes], [Views], [Shares], [Comments], [CreateDate], [Update], [Content], [Tag], [Rates], [Slug]) VALUES (3, N'123', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'123')
GO
INSERT [dbo].[Posts] ([ID], [Title], [IDCategory], [Likes], [Views], [Shares], [Comments], [CreateDate], [Update], [Content], [Tag], [Rates], [Slug]) VALUES (4, N'rwrqqwr ', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'rq', NULL, N'rwrqqwr-')
GO
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
USE [master]
GO
ALTER DATABASE [BlogHealth] SET  READ_WRITE 
GO
