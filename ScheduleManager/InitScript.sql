USE [ScheduleManager]
GO
ALTER TABLE [dbo].[TimeOffRequest] DROP CONSTRAINT [FK_TimeOffRequest_ManagerID]
GO
ALTER TABLE [dbo].[TimeOffRequest] DROP CONSTRAINT [FK_TimeOffRequest_Employee]
GO
ALTER TABLE [dbo].[Shift] DROP CONSTRAINT [FK_Shift_Employee]
GO
ALTER TABLE [dbo].[PickupRequest] DROP CONSTRAINT [FK_PickupRequest_Shift]
GO
ALTER TABLE [dbo].[PickupRequest] DROP CONSTRAINT [FK_PickupRequest_Employee]
GO
ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [FK_Employee_Rank]
GO
ALTER TABLE [dbo].[Availability] DROP CONSTRAINT [FK_Availability_Employee]
GO
/****** Object:  Table [dbo].[TimeOffRequest]    Script Date: 2/27/2023 11:20:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TimeOffRequest]') AND type in (N'U'))
DROP TABLE [dbo].[TimeOffRequest]
GO
/****** Object:  Table [dbo].[Shift]    Script Date: 2/27/2023 11:20:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Shift]') AND type in (N'U'))
DROP TABLE [dbo].[Shift]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2/27/2023 11:20:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[Rank]    Script Date: 2/27/2023 11:20:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rank]') AND type in (N'U'))
DROP TABLE [dbo].[Rank]
GO
/****** Object:  Table [dbo].[PickupRequest]    Script Date: 2/27/2023 11:20:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PickupRequest]') AND type in (N'U'))
DROP TABLE [dbo].[PickupRequest]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 2/27/2023 11:20:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
DROP TABLE [dbo].[Employee]
GO
/****** Object:  Table [dbo].[Availability]    Script Date: 2/27/2023 11:20:28 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Availability]') AND type in (N'U'))
DROP TABLE [dbo].[Availability]
GO
USE [master]
GO
/****** Object:  Database [ScheduleManager]    Script Date: 2/27/2023 11:20:28 PM ******/
DROP DATABASE [ScheduleManager]
GO
/****** Object:  Database [ScheduleManager]    Script Date: 2/27/2023 11:20:28 PM ******/
CREATE DATABASE [ScheduleManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ScheduleManager', FILENAME = N'C:\Users\Public\ScheduleManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ScheduleManager_log', FILENAME = N'C:\Users\Public\ScheduleManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ScheduleManager] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ScheduleManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ScheduleManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ScheduleManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ScheduleManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ScheduleManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ScheduleManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [ScheduleManager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ScheduleManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ScheduleManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ScheduleManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ScheduleManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ScheduleManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ScheduleManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ScheduleManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ScheduleManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ScheduleManager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ScheduleManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ScheduleManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ScheduleManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ScheduleManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ScheduleManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ScheduleManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ScheduleManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ScheduleManager] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ScheduleManager] SET  MULTI_USER 
GO
ALTER DATABASE [ScheduleManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ScheduleManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ScheduleManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ScheduleManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ScheduleManager] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ScheduleManager] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ScheduleManager] SET QUERY_STORE = OFF
GO
USE [ScheduleManager]
GO
/****** Object:  Table [dbo].[Availability]    Script Date: 2/27/2023 11:20:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Availability](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[EffectiveDate] [datetime] NOT NULL,
	[MondayStart] [datetime] NOT NULL,
	[MondayEnd] [datetime] NOT NULL,
	[TuesdayStart] [datetime] NOT NULL,
	[TuesdayEnd] [datetime] NOT NULL,
	[WednesdayStart] [datetime] NOT NULL,
	[WednesdayEnd] [datetime] NOT NULL,
	[ThursdayStart] [datetime] NOT NULL,
	[ThursdayEnd] [datetime] NOT NULL,
	[FridayStart] [datetime] NOT NULL,
	[FridayEnd] [datetime] NOT NULL,
	[SaturdayStart] [datetime] NOT NULL,
	[SaturdayEnd] [datetime] NOT NULL,
	[SundayStart] [datetime] NOT NULL,
	[SundayEnd] [datetime] NOT NULL,
 CONSTRAINT [PK_Availability] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 2/27/2023 11:20:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[RankID] [int] NOT NULL,
	[Password] [varchar](50) NULL,
	[Username] [varchar](50) NULL,
	[Phone] [varchar](10) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PickupRequest]    Script Date: 2/27/2023 11:20:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PickupRequest](
	[ShiftID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[ManagerID] [int] NULL,
 CONSTRAINT [PK_PickupRequest] PRIMARY KEY CLUSTERED 
(
	[ShiftID] ASC,
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rank]    Script Date: 2/27/2023 11:20:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rank](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NULL,
 CONSTRAINT [PK_Rank] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2/27/2023 11:20:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shift]    Script Date: 2/27/2023 11:20:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shift](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IsOpen] [bit] NOT NULL,
	[EmployeeID] [int] NULL,
	[Date] [date] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[Notes] [text] NULL,
 CONSTRAINT [PK_Shift] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeOffRequest]    Script Date: 2/27/2023 11:20:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeOffRequest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[ManagerID] [int] NOT NULL,
 CONSTRAINT [PK_TimeOffRequest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (1, N'Genrale', N'Manjeri', 3, N'GM', N'GM', N'1234567890', N'gm@gm.com', CAST(N'2001-01-01' AS Date))
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (2, N'Smol', N'Manger', 2, N'M', N'M', N'1234567890', N'm@m.com', CAST(N'2002-01-01' AS Date))
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (6, N'Max', N'Connor', 1, N'Max', N'Max', N'1234567890', N'max@max.com', CAST(N'1992-12-05' AS Date))
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (7, N'William', N'White', 1, N'William', N'William', N'1234567890', N'william@william.com', CAST(N'1987-11-15' AS Date))
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (8, N'Kent', N'Shrock', 1, N'Kent', N'Kent', N'1234567890', N'kent@kent.com', CAST(N'1990-06-08' AS Date))
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (9, N'James', N'Kelly', 1, N'James', N'James', N'1234567890', N'james@james.com', CAST(N'1989-11-28' AS Date))
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Rank] ON 

INSERT [dbo].[Rank] ([ID], [Title]) VALUES (1, N'Team Member')
INSERT [dbo].[Rank] ([ID], [Title]) VALUES (2, N'Manager')
INSERT [dbo].[Rank] ([ID], [Title]) VALUES (3, N'General Manager')
SET IDENTITY_INSERT [dbo].[Rank] OFF
GO
ALTER TABLE [dbo].[Availability]  WITH CHECK ADD  CONSTRAINT [FK_Availability_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([ID])
GO
ALTER TABLE [dbo].[Availability] CHECK CONSTRAINT [FK_Availability_Employee]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Rank] FOREIGN KEY([RankID])
REFERENCES [dbo].[Rank] ([ID])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Rank]
GO
ALTER TABLE [dbo].[PickupRequest]  WITH CHECK ADD  CONSTRAINT [FK_PickupRequest_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([ID])
GO
ALTER TABLE [dbo].[PickupRequest] CHECK CONSTRAINT [FK_PickupRequest_Employee]
GO
ALTER TABLE [dbo].[PickupRequest]  WITH CHECK ADD  CONSTRAINT [FK_PickupRequest_Shift] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([ID])
GO
ALTER TABLE [dbo].[PickupRequest] CHECK CONSTRAINT [FK_PickupRequest_Shift]
GO
ALTER TABLE [dbo].[Shift]  WITH CHECK ADD  CONSTRAINT [FK_Shift_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([ID])
GO
ALTER TABLE [dbo].[Shift] CHECK CONSTRAINT [FK_Shift_Employee]
GO
ALTER TABLE [dbo].[TimeOffRequest]  WITH CHECK ADD  CONSTRAINT [FK_TimeOffRequest_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeOffRequest] CHECK CONSTRAINT [FK_TimeOffRequest_Employee]
GO
ALTER TABLE [dbo].[TimeOffRequest]  WITH CHECK ADD  CONSTRAINT [FK_TimeOffRequest_ManagerID] FOREIGN KEY([ManagerID])
REFERENCES [dbo].[Employee] ([ID])
GO
ALTER TABLE [dbo].[TimeOffRequest] CHECK CONSTRAINT [FK_TimeOffRequest_ManagerID]
GO
USE [master]
GO
ALTER DATABASE [ScheduleManager] SET  READ_WRITE 
GO
