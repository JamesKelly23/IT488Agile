USE [master]
GO

/****** Object:  Login [IIS APPPOOL\BoilingOwls]    Script Date: 4/4/2023 6:51:28 PM ******/
CREATE LOGIN [IIS APPPOOL\ScheduleManager] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO

/****** Object:  Database [ScheduleManager]    Script Date: 4/4/2023 7:15:56 PM ******/
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
EXEC sys.sp_db_vardecimal_storage_format N'ScheduleManager', N'ON'
GO
ALTER DATABASE [ScheduleManager] SET QUERY_STORE = OFF
GO
USE [ScheduleManager]
GO
/****** Object:  User [ScheduleManager]    Script Date: 4/4/2023 7:15:56 PM ******/
CREATE USER [ScheduleManager] FOR LOGIN [IIS APPPOOL\ScheduleManager] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [ScheduleManager]
GO
/****** Object:  Table [dbo].[Availability]    Script Date: 4/4/2023 7:15:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Availability](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[EffectiveDate] [datetime] NULL,
	[MondayStart] [datetime] NULL,
	[MondayEnd] [datetime] NULL,
	[TuesdayStart] [datetime] NULL,
	[TuesdayEnd] [datetime] NULL,
	[WednesdayStart] [datetime] NULL,
	[WednesdayEnd] [datetime] NULL,
	[ThursdayStart] [datetime] NULL,
	[ThursdayEnd] [datetime] NULL,
	[FridayStart] [datetime] NULL,
	[FridayEnd] [datetime] NULL,
	[SaturdayStart] [datetime] NULL,
	[SaturdayEnd] [datetime] NULL,
	[SundayStart] [datetime] NULL,
	[SundayEnd] [datetime] NULL,
 CONSTRAINT [PK_Availability] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 4/4/2023 7:15:56 PM ******/
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
/****** Object:  Table [dbo].[PickupRequest]    Script Date: 4/4/2023 7:15:56 PM ******/
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
/****** Object:  Table [dbo].[Rank]    Script Date: 4/4/2023 7:15:56 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 4/4/2023 7:15:56 PM ******/
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
/****** Object:  Table [dbo].[Shift]    Script Date: 4/4/2023 7:15:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shift](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IsOpen] [bit] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[Notes] [text] NULL,
 CONSTRAINT [PK_Shift] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeOffRequest]    Script Date: 4/4/2023 7:15:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeOffRequest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[ManagerID] [int] NULL,
	[Notes] [text] NULL,
 CONSTRAINT [PK_TimeOffRequest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Availability] ON 
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (1, 1, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T18:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T17:00:00.000' AS DateTime), CAST(N'2001-01-01T10:00:00.000' AS DateTime), CAST(N'2001-01-01T16:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T17:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T17:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T17:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T17:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (2, 2, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (5, 6, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T09:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T12:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T06:00:00.000' AS DateTime), CAST(N'2001-01-01T20:00:00.000' AS DateTime), CAST(N'2001-01-01T07:00:00.000' AS DateTime), CAST(N'2001-01-01T16:00:00.000' AS DateTime), CAST(N'2001-01-01T10:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (7, 7, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T17:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (9, 8, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T17:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T06:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T05:00:00.000' AS DateTime), CAST(N'2001-01-01T19:00:00.000' AS DateTime), CAST(N'2001-01-01T06:00:00.000' AS DateTime), CAST(N'2001-01-01T17:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T06:00:00.000' AS DateTime), CAST(N'2001-01-01T20:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (11, 9, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (12, 11, CAST(N'2023-03-11T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T15:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (13, 12, CAST(N'2023-03-16T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (14, 13, CAST(N'2023-03-21T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T08:00:00.000' AS DateTime), CAST(N'2001-01-01T06:00:00.000' AS DateTime), CAST(N'2001-01-01T09:00:00.000' AS DateTime), CAST(N'2001-01-01T05:00:00.000' AS DateTime), CAST(N'2001-01-01T10:00:00.000' AS DateTime), CAST(N'2001-01-01T04:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T03:00:00.000' AS DateTime), CAST(N'2001-01-01T12:00:00.000' AS DateTime), CAST(N'2001-01-01T02:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T01:00:00.000' AS DateTime), CAST(N'2001-01-01T14:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (15, 14, CAST(N'2023-03-21T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (16, 15, CAST(N'2023-04-02T18:45:11.123' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (18, 17, CAST(N'2023-04-03T23:22:24.273' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Availability] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (1, N'General', N'Manager', 3, N'GM', N'GM', N'1234567890', N'gm@gm.com', CAST(N'2001-01-01' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (2, N'Regular', N'Manager', 2, N'M', N'M', N'1234567890', N'm@m.com', CAST(N'2002-01-01' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (6, N'Max', N'Connor', 1, N'Max', N'Max', N'1234567890', N'max@max.com', CAST(N'1992-12-05' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (7, N'William', N'White', 1, N'William', N'William', N'1234567890', N'william@william.com', CAST(N'1987-11-15' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (8, N'Kent', N'Shrock', 1, N'Kent', N'Kent', N'123-456-78', N'kent@gmail.com', CAST(N'1990-06-12' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (9, N'James', N'Kelly', 1, N'James', N'James', N'1234567890', N'james@james.com', CAST(N'1989-11-28' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (11, N'New', N'Employee', 3, N'New', N'New', N'1234567890', N'new@new.com', CAST(N'2004-01-01' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (12, N'Michael', N'Jackson', 1, N'Michael', N'Michael', N'1234567890', N'michael@michael.com', CAST(N'1989-12-06' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (13, N'Jon', N'Snow', 1, N'jon', N'jon', N'1234567890', N'jon@jon.com', CAST(N'2040-12-20' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (14, N'Jimothy', N'Steel', 1, N'Bobby', N'Bobby', N'45', N'15111', CAST(N'2023-04-04' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (15, N'Jonathan', N'Breaker', 1, N'jon', N'jon', N'1234567890', N'jon@jon.com', CAST(N'1901-11-28' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (17, N'Timmy', N'Turner', 1, N'Timmy', N'Timmy', N'1234567890', N'thisIsCool@gmail.com', CAST(N'2023-05-04' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (1, 1, 0, 1)
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (1, 9, 1, 1)
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (2, 1, 0, 1)
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (5, 9, 1, 1)
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (8, 6, 0, 1)
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (20, 9, 1, 1)
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (1095, 2, 1, 1)
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (1135, 6, 1, 1)
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (1135, 7, 0, 1)
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (1142, 6, 1, 1)
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (1142, 8, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Rank] ON 
GO
INSERT [dbo].[Rank] ([ID], [Title]) VALUES (1, N'Team Member')
GO
INSERT [dbo].[Rank] ([ID], [Title]) VALUES (2, N'Manager')
GO
INSERT [dbo].[Rank] ([ID], [Title]) VALUES (3, N'General Manager')
GO
SET IDENTITY_INSERT [dbo].[Rank] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (15, N'Cashier')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (16, N'Fry Cook')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (17, N'Dining Room')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (18, N' Maintenence')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (19, N'Driver')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (24, N'Manager')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (25, N'Singer')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Shift] ON 
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1, 0, 9, CAST(N'2023-03-06T00:00:00.000' AS DateTime), CAST(N'2023-03-06T06:00:00.000' AS DateTime), CAST(N'2023-03-06T15:00:00.000' AS DateTime), N'General Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (2, 0, 1, CAST(N'2023-03-07T00:00:00.000' AS DateTime), CAST(N'2023-03-07T06:00:00.000' AS DateTime), CAST(N'2023-03-07T15:00:00.000' AS DateTime), N'General Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (4, 0, 1, CAST(N'2023-03-08T00:00:00.000' AS DateTime), CAST(N'2023-03-08T06:00:00.000' AS DateTime), CAST(N'2023-03-08T15:00:00.000' AS DateTime), N'General Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (5, 0, 9, CAST(N'2023-03-09T00:00:00.000' AS DateTime), CAST(N'2023-03-09T08:00:00.000' AS DateTime), CAST(N'2023-03-09T17:00:00.000' AS DateTime), N'General Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (6, 0, 1, CAST(N'2023-03-10T00:00:00.000' AS DateTime), CAST(N'2023-03-10T06:00:00.000' AS DateTime), CAST(N'2023-03-10T15:00:00.000' AS DateTime), N'General Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (7, 0, 2, CAST(N'2023-03-08T00:00:00.000' AS DateTime), CAST(N'2023-03-08T10:00:00.000' AS DateTime), CAST(N'2023-03-08T19:00:00.000' AS DateTime), N'Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (8, 0, 2, CAST(N'2023-03-09T00:00:00.000' AS DateTime), CAST(N'2023-03-09T11:00:00.000' AS DateTime), CAST(N'2023-03-09T20:00:00.000' AS DateTime), N'Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (10, 0, 2, CAST(N'2023-03-10T00:00:00.000' AS DateTime), CAST(N'2023-03-10T12:00:00.000' AS DateTime), CAST(N'2023-03-10T21:00:00.000' AS DateTime), N'Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (12, 1, 2, CAST(N'2023-03-11T00:00:00.000' AS DateTime), CAST(N'2023-03-11T13:00:00.000' AS DateTime), CAST(N'2023-03-11T22:00:00.000' AS DateTime), N'Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (14, 1, 6, CAST(N'2023-03-07T00:00:00.000' AS DateTime), CAST(N'2023-03-07T17:00:00.000' AS DateTime), CAST(N'2023-03-07T22:00:00.000' AS DateTime), N'Cashier', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (15, 0, 7, CAST(N'2023-03-08T00:00:00.000' AS DateTime), CAST(N'2023-03-08T12:00:00.000' AS DateTime), CAST(N'2023-03-08T19:00:00.000' AS DateTime), N'Manager in Training', N'I dont need notes but Im putting them anyways!')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (16, 0, 6, CAST(N'2023-03-08T00:00:00.000' AS DateTime), CAST(N'2023-03-08T08:00:00.000' AS DateTime), CAST(N'2023-03-08T13:00:00.000' AS DateTime), N'Cook', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (17, 0, 1, CAST(N'2023-03-15T00:00:00.000' AS DateTime), CAST(N'2023-03-15T06:00:00.000' AS DateTime), CAST(N'2023-03-15T15:00:00.000' AS DateTime), N'General Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (18, 0, 2, CAST(N'2023-03-15T00:00:00.000' AS DateTime), CAST(N'2023-03-15T15:00:00.000' AS DateTime), CAST(N'2023-03-15T23:00:00.000' AS DateTime), N'Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (19, 0, 6, CAST(N'2023-03-15T00:00:00.000' AS DateTime), CAST(N'2023-03-15T12:00:00.000' AS DateTime), CAST(N'2023-03-15T18:00:00.000' AS DateTime), N'Cashier', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (20, 0, 9, CAST(N'2023-03-16T00:00:00.000' AS DateTime), CAST(N'2023-03-16T06:00:00.000' AS DateTime), CAST(N'2023-03-16T15:00:00.000' AS DateTime), N'General Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (21, 0, 1, CAST(N'2023-03-16T00:00:00.000' AS DateTime), CAST(N'2023-03-16T06:00:00.000' AS DateTime), CAST(N'2023-03-16T15:00:00.000' AS DateTime), N'General Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (50, 0, 1, CAST(N'2023-03-13T00:00:00.000' AS DateTime), CAST(N'2023-03-13T06:00:00.000' AS DateTime), CAST(N'2023-03-13T15:00:00.000' AS DateTime), N'General Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (51, 0, 6, CAST(N'2023-03-13T00:00:00.000' AS DateTime), CAST(N'2023-03-13T09:00:00.000' AS DateTime), CAST(N'2023-03-13T18:00:00.000' AS DateTime), N'Floater', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (53, 0, 8, CAST(N'2023-03-13T00:00:00.000' AS DateTime), CAST(N'2023-03-13T06:00:00.000' AS DateTime), CAST(N'2023-03-13T15:00:00.000' AS DateTime), N'Cook', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (55, 0, 11, CAST(N'2023-03-13T00:00:00.000' AS DateTime), CAST(N'2023-03-13T06:00:00.000' AS DateTime), CAST(N'2023-03-13T15:00:00.000' AS DateTime), N'Cashier', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (56, 0, 12, CAST(N'2023-03-13T00:00:00.000' AS DateTime), CAST(N'2023-03-13T14:00:00.000' AS DateTime), CAST(N'2023-03-13T23:00:00.000' AS DateTime), N'Cashier', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (57, 0, 9, CAST(N'2023-03-13T00:00:00.000' AS DateTime), CAST(N'2023-03-13T13:00:00.000' AS DateTime), CAST(N'2023-03-13T22:00:00.000' AS DateTime), N'Cook', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (58, 0, 2, CAST(N'2023-03-13T00:00:00.000' AS DateTime), CAST(N'2023-03-13T13:00:00.000' AS DateTime), CAST(N'2023-03-13T22:00:00.000' AS DateTime), N'Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (59, 0, 1, CAST(N'2023-03-14T00:00:00.000' AS DateTime), CAST(N'2023-03-14T06:00:00.000' AS DateTime), CAST(N'2023-03-14T15:00:00.000' AS DateTime), N'General Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (60, 0, 1, CAST(N'2023-03-14T00:00:00.000' AS DateTime), CAST(N'2023-03-14T06:00:00.000' AS DateTime), CAST(N'2023-03-14T15:00:00.000' AS DateTime), N'General Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (61, 0, 1, CAST(N'2023-03-14T00:00:00.000' AS DateTime), CAST(N'2023-03-14T06:00:00.000' AS DateTime), CAST(N'2023-03-14T15:00:00.000' AS DateTime), N'General Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (69, 0, 1, CAST(N'2023-03-20T00:00:00.000' AS DateTime), CAST(N'2023-03-20T06:00:00.000' AS DateTime), CAST(N'2023-03-20T18:00:00.000' AS DateTime), N'GM', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (70, 0, 2, CAST(N'2023-03-20T00:00:00.000' AS DateTime), CAST(N'2023-03-20T11:00:00.000' AS DateTime), CAST(N'2023-03-20T22:00:00.000' AS DateTime), N'Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (72, 0, 2, CAST(N'2023-03-21T00:00:00.000' AS DateTime), CAST(N'2023-03-21T11:00:00.000' AS DateTime), CAST(N'2023-03-21T22:00:00.000' AS DateTime), N'Driver', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (73, 0, 6, CAST(N'2023-03-21T00:00:00.000' AS DateTime), CAST(N'2023-03-21T11:00:00.000' AS DateTime), CAST(N'2023-03-21T23:00:00.000' AS DateTime), N'Driver', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (74, 0, 8, CAST(N'2023-03-21T00:00:00.000' AS DateTime), CAST(N'2023-03-21T06:00:00.000' AS DateTime), CAST(N'2023-03-21T15:00:00.000' AS DateTime), N'Dining Room', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (75, 0, 9, CAST(N'2023-03-21T00:00:00.000' AS DateTime), CAST(N'2023-03-21T13:00:00.000' AS DateTime), CAST(N'2023-03-21T22:00:00.000' AS DateTime), N'Driver', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (76, 0, 11, CAST(N'2023-03-21T00:00:00.000' AS DateTime), CAST(N'2023-03-21T06:00:00.000' AS DateTime), CAST(N'2023-03-21T23:00:00.000' AS DateTime), N'Driver', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (77, 0, 12, CAST(N'2023-03-21T00:00:00.000' AS DateTime), CAST(N'2023-03-21T06:00:00.000' AS DateTime), CAST(N'2023-03-21T23:00:00.000' AS DateTime), N'Driver', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (78, 0, 13, CAST(N'2023-03-21T00:00:00.000' AS DateTime), CAST(N'2023-03-21T06:00:00.000' AS DateTime), CAST(N'2023-03-21T09:00:00.000' AS DateTime), N'Driver', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (81, 0, 9, CAST(N'2023-03-22T00:00:00.000' AS DateTime), CAST(N'2023-03-22T13:00:00.000' AS DateTime), CAST(N'2023-03-22T22:00:00.000' AS DateTime), N'', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (82, 0, 11, CAST(N'2023-03-22T00:00:00.000' AS DateTime), CAST(N'2023-03-22T06:00:00.000' AS DateTime), CAST(N'2023-03-22T23:00:00.000' AS DateTime), N'', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (83, 0, 1, CAST(N'2023-03-22T00:00:00.000' AS DateTime), CAST(N'2023-03-22T10:00:00.000' AS DateTime), CAST(N'2023-03-22T16:00:00.000' AS DateTime), N'Cook', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (84, 0, 1, CAST(N'2023-03-21T00:00:00.000' AS DateTime), CAST(N'2023-03-21T06:00:00.000' AS DateTime), CAST(N'2023-03-21T17:00:00.000' AS DateTime), N'Cashier', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (87, 0, 2, CAST(N'2023-03-25T00:00:00.000' AS DateTime), CAST(N'2023-03-25T11:00:00.000' AS DateTime), CAST(N'2023-03-25T20:00:00.000' AS DateTime), N'Cashier', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (88, 0, 1, CAST(N'2023-03-25T00:00:00.000' AS DateTime), CAST(N'2023-03-25T06:00:00.000' AS DateTime), CAST(N'2023-03-25T15:00:00.000' AS DateTime), N'GM', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (89, 0, 9, CAST(N'2023-03-25T00:00:00.000' AS DateTime), CAST(N'2023-03-25T13:00:00.000' AS DateTime), CAST(N'2023-03-25T22:00:00.000' AS DateTime), N'Floater', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (90, 0, 11, CAST(N'2023-03-25T00:00:00.000' AS DateTime), CAST(N'2023-03-25T06:00:00.000' AS DateTime), CAST(N'2023-03-25T15:00:00.000' AS DateTime), N'Floater', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (91, 0, 12, CAST(N'2023-03-25T00:00:00.000' AS DateTime), CAST(N'2023-03-25T10:00:00.000' AS DateTime), CAST(N'2023-03-25T19:00:00.000' AS DateTime), N'Floater', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (92, 0, 13, CAST(N'2023-03-25T00:00:00.000' AS DateTime), CAST(N'2023-03-25T06:00:00.000' AS DateTime), CAST(N'2023-03-25T13:00:00.000' AS DateTime), N'Floater', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (93, 0, 6, CAST(N'2023-03-25T00:00:00.000' AS DateTime), CAST(N'2023-03-25T10:00:00.000' AS DateTime), CAST(N'2023-03-25T23:00:00.000' AS DateTime), N'Maintenance', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (94, 0, 7, CAST(N'2023-03-25T00:00:00.000' AS DateTime), CAST(N'2023-03-25T17:00:00.000' AS DateTime), CAST(N'2023-03-25T23:00:00.000' AS DateTime), N'Maintenance', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1085, 0, 9, CAST(N'2023-03-26T00:00:00.000' AS DateTime), CAST(N'2023-03-26T06:00:00.000' AS DateTime), CAST(N'2023-03-26T15:00:00.000' AS DateTime), N'Cashier', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1087, 0, 6, CAST(N'2023-03-26T00:00:00.000' AS DateTime), CAST(N'2023-03-26T19:00:00.000' AS DateTime), CAST(N'2023-03-26T23:00:00.000' AS DateTime), N'Covering Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1088, 0, 9, CAST(N'2023-03-26T00:00:00.000' AS DateTime), CAST(N'2023-03-26T13:00:00.000' AS DateTime), CAST(N'2023-03-26T22:00:00.000' AS DateTime), N'Cook', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1089, 0, 11, CAST(N'2023-03-26T00:00:00.000' AS DateTime), CAST(N'2023-03-26T06:00:00.000' AS DateTime), CAST(N'2023-03-26T15:00:00.000' AS DateTime), N'Cook', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1090, 0, 13, CAST(N'2023-03-26T00:00:00.000' AS DateTime), CAST(N'2023-03-26T06:00:00.000' AS DateTime), CAST(N'2023-03-26T14:00:00.000' AS DateTime), N'Cashier', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1092, 0, 2, CAST(N'2023-03-26T00:00:00.000' AS DateTime), CAST(N'2023-03-26T13:00:00.000' AS DateTime), CAST(N'2023-03-26T22:00:00.000' AS DateTime), N'Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1093, 0, 12, CAST(N'2023-03-26T00:00:00.000' AS DateTime), CAST(N'2023-03-26T14:00:00.000' AS DateTime), CAST(N'2023-03-26T23:00:00.000' AS DateTime), N'Cashier', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1095, 0, 2, CAST(N'2023-04-05T00:00:00.000' AS DateTime), CAST(N'2023-04-05T10:00:00.000' AS DateTime), CAST(N'2023-04-05T16:00:00.000' AS DateTime), N'Driver', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1097, 0, 1, CAST(N'2023-04-03T00:00:00.000' AS DateTime), CAST(N'2023-04-03T07:00:00.000' AS DateTime), CAST(N'2023-04-03T15:00:00.000' AS DateTime), N'Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1098, 0, 6, CAST(N'2023-04-03T00:00:00.000' AS DateTime), CAST(N'2023-04-03T10:00:00.000' AS DateTime), CAST(N'2023-04-03T18:00:00.000' AS DateTime), N'Cook', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1099, 0, 8, CAST(N'2023-04-03T00:00:00.000' AS DateTime), CAST(N'2023-04-03T08:00:00.000' AS DateTime), CAST(N'2023-04-03T15:00:00.000' AS DateTime), N'British', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1100, 0, 12, CAST(N'2023-04-03T00:00:00.000' AS DateTime), CAST(N'2023-04-03T13:00:00.000' AS DateTime), CAST(N'2023-04-03T23:00:00.000' AS DateTime), N'', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1102, 0, 8, CAST(N'2023-04-04T00:00:00.000' AS DateTime), CAST(N'2023-04-04T07:30:00.000' AS DateTime), CAST(N'2023-04-04T16:45:00.000' AS DateTime), N'Cook', N'New note')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1104, 0, 1, CAST(N'2023-04-04T00:00:00.000' AS DateTime), CAST(N'2023-04-04T13:00:00.000' AS DateTime), CAST(N'2023-04-04T20:00:00.000' AS DateTime), N'', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1105, 0, 11, CAST(N'2023-04-04T00:00:00.000' AS DateTime), CAST(N'2023-04-04T09:00:00.000' AS DateTime), CAST(N'2023-04-04T20:00:00.000' AS DateTime), N'Floater', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1106, 0, 14, CAST(N'2023-04-04T00:00:00.000' AS DateTime), CAST(N'2023-04-04T08:00:00.000' AS DateTime), CAST(N'2023-04-04T19:00:00.000' AS DateTime), N'Dining Room', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1107, 0, 9, CAST(N'2023-04-04T00:00:00.000' AS DateTime), CAST(N'2023-04-04T07:00:00.000' AS DateTime), CAST(N'2023-04-04T22:00:00.000' AS DateTime), N'', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1109, 0, 8, CAST(N'2023-04-05T00:00:00.000' AS DateTime), CAST(N'2023-04-05T08:00:00.000' AS DateTime), CAST(N'2023-04-05T19:00:00.000' AS DateTime), N'Maintenance', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1111, 0, 2, CAST(N'2023-04-07T00:00:00.000' AS DateTime), CAST(N'2023-04-07T09:00:00.000' AS DateTime), CAST(N'2023-04-07T19:00:00.000' AS DateTime), N'Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1113, 0, 8, CAST(N'2023-04-07T00:00:00.000' AS DateTime), CAST(N'2023-04-07T11:00:00.000' AS DateTime), CAST(N'2023-04-07T20:00:00.000' AS DateTime), N'Floater', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1114, 0, 1, CAST(N'2023-04-08T00:00:00.000' AS DateTime), CAST(N'2023-04-08T06:00:00.000' AS DateTime), CAST(N'2023-04-08T17:00:00.000' AS DateTime), N'', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1115, 0, 6, CAST(N'2023-04-08T00:00:00.000' AS DateTime), CAST(N'2023-04-08T11:00:00.000' AS DateTime), CAST(N'2023-04-08T21:00:00.000' AS DateTime), N'', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1131, 1, 0, CAST(N'2023-04-04T00:00:00.000' AS DateTime), CAST(N'2023-04-04T01:15:00.000' AS DateTime), CAST(N'2023-04-04T03:45:00.000' AS DateTime), N'Cook', N'THIS WAS EDITED')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1133, 0, 6, CAST(N'2023-04-04T00:00:00.000' AS DateTime), CAST(N'2023-04-04T11:00:00.000' AS DateTime), CAST(N'2023-04-04T21:00:00.000' AS DateTime), N'', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1134, 0, 6, CAST(N'2023-04-05T00:00:00.000' AS DateTime), CAST(N'2023-04-05T12:00:00.000' AS DateTime), CAST(N'2023-04-05T21:00:00.000' AS DateTime), N'Cashier', N'''; DROP DATABASE;')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1135, 0, 6, CAST(N'2023-04-20T00:00:00.000' AS DateTime), CAST(N'2023-04-20T00:00:00.000' AS DateTime), CAST(N'2023-04-20T03:30:00.000' AS DateTime), N'Cashier', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1137, 1, 0, CAST(N'2023-04-19T00:00:00.000' AS DateTime), CAST(N'2023-04-19T00:00:00.000' AS DateTime), CAST(N'2023-04-19T03:00:00.000' AS DateTime), N'Unspecified', N'''; DROP DATABASE;')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1142, 0, 6, CAST(N'2023-04-15T00:00:00.000' AS DateTime), CAST(N'2023-04-15T09:00:00.000' AS DateTime), CAST(N'2023-04-15T17:00:00.000' AS DateTime), N'Cashier', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1143, 0, 6, CAST(N'2023-04-06T00:00:00.000' AS DateTime), CAST(N'2023-04-06T06:00:00.000' AS DateTime), CAST(N'2023-04-06T15:00:00.000' AS DateTime), N'Fry Cook', N'')
GO
SET IDENTITY_INSERT [dbo].[Shift] OFF
GO
SET IDENTITY_INSERT [dbo].[TimeOffRequest] ON 
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (2, 2, CAST(N'2023-05-16T00:00:00.000' AS DateTime), CAST(N'2023-05-31T00:00:00.000' AS DateTime), 0, 1, N'')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (4, 6, CAST(N'2023-09-30T00:00:00.000' AS DateTime), CAST(N'2023-09-30T00:00:00.000' AS DateTime), 1, 1, N'Birthday Party!')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (5, 7, CAST(N'2023-05-14T00:00:00.000' AS DateTime), CAST(N'2023-05-18T00:00:00.000' AS DateTime), 0, 1, N'')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (13, 6, CAST(N'2023-04-19T00:00:00.000' AS DateTime), CAST(N'2023-04-24T00:00:00.000' AS DateTime), 1, 2, N'Going to be sick that week.')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (14, 9, CAST(N'2023-05-16T00:00:00.000' AS DateTime), CAST(N'2023-05-21T00:00:00.000' AS DateTime), 0, 1, N'Ded')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (22, 8, CAST(N'2023-03-29T00:00:00.000' AS DateTime), CAST(N'2023-03-30T00:00:00.000' AS DateTime), 1, 2, N'Tired')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (24, 8, CAST(N'2023-04-20T00:00:00.000' AS DateTime), CAST(N'2023-04-27T00:00:00.000' AS DateTime), 1, 2, N'Now I can put apostrophe''s and semicolons ; all throughout here and it won''t hurt anything')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (28, 2, CAST(N'2023-04-03T00:00:00.000' AS DateTime), CAST(N'2023-04-03T00:00:00.000' AS DateTime), 1, 1, N'This is for the schedule composer')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (29, 8, CAST(N'2023-04-05T00:00:00.000' AS DateTime), CAST(N'2023-04-05T00:00:00.000' AS DateTime), 0, 1, N'I am planning to be sick this day.')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (32, 8, CAST(N'2023-04-05T00:00:00.000' AS DateTime), CAST(N'2023-04-05T00:00:00.000' AS DateTime), 0, 1, N'Same as start')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (33, 8, CAST(N'2023-04-08T00:00:00.000' AS DateTime), CAST(N'2023-04-08T00:00:00.000' AS DateTime), 0, 1, N'')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (34, 8, CAST(N'2023-04-07T00:00:00.000' AS DateTime), CAST(N'2023-04-07T00:00:00.000' AS DateTime), 0, 1, N'')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (35, 8, CAST(N'2023-04-06T00:00:00.000' AS DateTime), CAST(N'2023-04-06T00:00:00.000' AS DateTime), 1, 1, N'')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (36, 8, CAST(N'2023-04-03T00:00:00.000' AS DateTime), CAST(N'2023-04-03T00:00:00.000' AS DateTime), 0, 1, N'''; DROP DATABASE ScheduleManager;')
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID], [Notes]) VALUES (37, 6, CAST(N'2023-04-12T00:00:00.000' AS DateTime), CAST(N'2023-04-15T00:00:00.000' AS DateTime), 1, 1, N'Because I will be sick')
GO
SET IDENTITY_INSERT [dbo].[TimeOffRequest] OFF
GO
ALTER TABLE [dbo].[Shift] ADD  CONSTRAINT [DF_Shift_EmployeeID]  DEFAULT ((0)) FOR [EmployeeID]
GO
ALTER TABLE [dbo].[Availability]  WITH CHECK ADD  CONSTRAINT [FK_Availability_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
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
ALTER TABLE [dbo].[PickupRequest]  WITH CHECK ADD  CONSTRAINT [FK_PickupRequest_Shift] FOREIGN KEY([ShiftID])
REFERENCES [dbo].[Shift] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PickupRequest] CHECK CONSTRAINT [FK_PickupRequest_Shift]
GO
ALTER TABLE [dbo].[Shift]  WITH NOCHECK ADD  CONSTRAINT [FK_Shift_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([ID])
ON UPDATE CASCADE
ON DELETE SET DEFAULT
GO
ALTER TABLE [dbo].[Shift] NOCHECK CONSTRAINT [FK_Shift_Employee]
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
/****** Object:  StoredProcedure [dbo].[SP_Update_Availability]    Script Date: 4/4/2023 7:15:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		James Kelly
-- Create date: 3/21/2023
-- Description:	Insert or update an Availability
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_Availability] 
	-- Add the parameters for the stored procedure here
	@ID int = 0, 
	@EmployeeID int = 0,
	@EffectiveDate datetime,
	@MondayStart datetime,
	@MondayEnd datetime,
	@TuesdayStart datetime,
	@TuesdayEnd datetime,
	@WednesdayStart datetime,
	@WednesdayEnd datetime,
	@ThursdayStart datetime,
	@ThursdayEnd datetime,
	@FridayStart datetime,
	@FridayEnd datetime,
	@SaturdayStart datetime,
	@SaturdayEnd datetime,
	@SundayStart datetime,
	@SundayEnd datetime,
	@NewID int output
AS
BEGIN
	SET NOCOUNT ON;
	
	IF @ID=0
	BEGIN
		INSERT INTO Availability (EmployeeID, EffectiveDate, MondayStart, 
		MondayEnd, TuesdayStart, TuesdayEnd, WednesdayStart, WednesdayEnd,
		ThursdayStart, ThursdayEnd, FridayStart, FridayEnd, SaturdayStart,
		SaturdayEnd, SundayStart, SundayEnd) OUTPUT INSERTED.ID VALUES (@EmployeeID, @EffectiveDate,
		@MondayStart, @MondayEnd, @TuesdayStart, @TuesdayEnd, @WednesdayStart,
		@WednesdayEnd, @ThursdayStart, @ThursdayEnd, @FridayStart, @FridayEnd,
		@SaturdayStart, @SaturdayEnd, @SundayStart, @SundayEnd);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE Availability SET
		EmployeeID=@EmployeeID,
		EffectiveDate=@EffectiveDate,
		MondayStart=@MondayStart,
		MondayEnd=@MondayEnd,
		TuesdayStart=@TuesdayStart,
		TuesdayEnd=@TuesdayEnd,
		WednesdayStart=@WednesdayStart,
		WednesdayEnd=@WednesdayEnd,
		ThursdayStart=@ThursdayStart,
		ThursdayEnd=@ThursdayEnd,
		FridayStart=@FridayStart,
		FridayEnd=@FridayEnd,
		SaturdayStart=@SaturdayStart,
		SaturdayEnd=@SaturdayEnd,
		SundayStart=@SundayStart,
		SundayEnd=@SundayEnd
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Employee]    Script Date: 4/4/2023 7:15:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		James Kelly
-- Create date: 3/21/2023
-- Description:	Insert or update an Employee
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_Employee]
	@ID int = 0, 
	@FirstName varchar(50),
	@LastName varchar(50),
	@RankID int = 1,
	@Password varchar(50),
	@Username varchar(50),
	@Phone varchar(10),
	@Email varchar(100),
	@DateOfBirth date,
	@NewID int output
AS
BEGIN
	SET NOCOUNT ON;
	
	IF @ID=0
	BEGIN
		INSERT INTO Employee (FirstName, LastName, RankID, Password, Username, Phone, Email, DateOfBirth) 
		VALUES (@FirstName, @LastName, @RankID, @Password, @Username, @Phone, @Email, @DateOfBirth);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE Employee SET
		FirstName=@FirstName, LastName=@LastName, RankID=@RankID, Password=@Password,
		Username=@Username, Phone=@Phone, Email=@Email,	DateOfBirth=@DateOfBirth
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_PickupRequest]    Script Date: 4/4/2023 7:15:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		James Kelly
-- Create date: 3/21/2023
-- Description:	Insert or update a PickupRequest
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_PickupRequest]
	@ShiftID int, 
	@EmployeeID int,
	@IsApproved bit,
	@ManagerID int
AS
BEGIN
	SET NOCOUNT ON;
	
	IF (SELECT COUNT(*) FROM PickupRequest WHERE ShiftID=@ShiftID AND EmployeeID=@EmployeeID)=0
	BEGIN
		INSERT INTO PickupRequest (ShiftID, EmployeeID, IsApproved, ManagerID) 
		VALUES (@ShiftID, @EmployeeID, @IsApproved, @ManagerID);
	END
	ELSE
	BEGIN
		UPDATE PickupRequest
		SET	IsApproved=@IsApproved,	ManagerID=@ManagerID
		WHERE ShiftID=@ShiftID AND EmployeeID=@EmployeeID;
	END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Rank]    Script Date: 4/4/2023 7:15:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		James Kelly
-- Create date: 3/21/2023
-- Description:	Insert or update a PickupRequest
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_Rank]
	@ID int, 
	@Title varchar(50),
	@NewID int output
AS
BEGIN
	SET NOCOUNT ON;
	
	IF @ID=0
	BEGIN
		INSERT INTO Rank (Title) 
		VALUES (@Title);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE Rank
		SET	Title=@Title
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Role]    Script Date: 4/4/2023 7:15:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		James Kelly
-- Create date: 3/21/2023
-- Description:	Insert or update a Role
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_Role]
	@ID int, 
	@Name varchar(50),
	@NewID int output
AS
BEGIN
	SET NOCOUNT ON;
	
	IF @ID=0
	BEGIN
		INSERT INTO Role (Name) 
		VALUES (@Name);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE Role
		SET	Name=@Name
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_Shift]    Script Date: 4/4/2023 7:15:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		James Kelly
-- Create date: 3/21/2023
-- Description:	Insert or update a Shift
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_Shift]
	@ID int, 
	@IsOpen bit,
	@EmployeeID int,
	@Date datetime,
	@StartTime datetime,
	@EndTime datetime,
	@Role varchar(50),
	@Notes text,
	@NewID int output
AS
BEGIN
	SET NOCOUNT ON;
	IF @ID=0
	BEGIN
		INSERT INTO Shift (IsOpen, EmployeeID, Date, StartTime, EndTime, Role, Notes) 
		VALUES (@IsOpen, @EmployeeID, @Date, @StartTime, @EndTime, @Role, @Notes);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE Shift
		SET	IsOpen=@IsOpen, EmployeeID=@EmployeeID, Date=@Date, 
		StartTime=@StartTime, EndTime=@EndTime, Role=@Role, Notes=@Notes
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Update_TimeOffRequest]    Script Date: 4/4/2023 7:15:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		James Kelly
-- Create date: 3/21/2023
-- Description:	Insert or update a TimeOffRequest
-- =============================================
CREATE PROCEDURE [dbo].[SP_Update_TimeOffRequest]
	@ID int, 
	@EmployeeID int,
	@StartDate datetime,
	@EndDate datetime,
	@IsApproved bit,
	@ManagerID int,
	@Notes text,
	@NewID int output
AS
BEGIN
	SET NOCOUNT ON;
	IF @ID=0
	BEGIN
		INSERT INTO TimeOffRequest (EmployeeID, StartDate, EndDate, IsApproved, ManagerID, Notes) 
		VALUES (@EmployeeID, @StartDate, @EndDate, @IsApproved, @ManagerID, @Notes);
		SET @NewID = SCOPE_IDENTITY();
	END
	ELSE
	BEGIN
		UPDATE TimeOffRequest
		SET	EmployeeID=@EmployeeID, StartDate=@StartDate, EndDate=@EndDate, 
		IsApproved=@IsApproved, ManagerID=@ManagerID, Notes=@Notes
		WHERE ID=@ID;
		SET @NewID = SCOPE_IDENTITY();
	END
END
GO
USE [master]
GO
ALTER DATABASE [ScheduleManager] SET  READ_WRITE 
GO
