USE [master]
GO
/****** Object:  Database [ScheduleManager]    Script Date: 3/1/2023 5:19:27 PM ******/
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
/****** Object:  Table [dbo].[Availability]    Script Date: 3/1/2023 5:19:27 PM ******/
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
/****** Object:  Table [dbo].[Employee]    Script Date: 3/1/2023 5:19:27 PM ******/
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
/****** Object:  Table [dbo].[PickupRequest]    Script Date: 3/1/2023 5:19:27 PM ******/
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
/****** Object:  Table [dbo].[Rank]    Script Date: 3/1/2023 5:19:27 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 3/1/2023 5:19:27 PM ******/
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
/****** Object:  Table [dbo].[Shift]    Script Date: 3/1/2023 5:19:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shift](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IsOpen] [bit] NOT NULL,
	[EmployeeID] [int] NULL,
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
/****** Object:  Table [dbo].[TimeOffRequest]    Script Date: 3/1/2023 5:19:27 PM ******/
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
 CONSTRAINT [PK_TimeOffRequest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Availability] ON 
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (1, 1, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T09:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T09:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T09:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T09:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T09:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T09:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T09:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (2, 2, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (5, 6, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T09:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T12:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T06:00:00.000' AS DateTime), CAST(N'2001-01-01T20:00:00.000' AS DateTime), CAST(N'2001-01-01T07:00:00.000' AS DateTime), CAST(N'2001-01-01T16:00:00.000' AS DateTime), CAST(N'2001-01-01T10:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (7, 7, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T11:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime), CAST(N'2001-01-01T23:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (9, 8, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T06:00:00.000' AS DateTime), CAST(N'2001-01-01T15:00:00.000' AS DateTime), CAST(N'2001-01-01T06:00:00.000' AS DateTime), CAST(N'2001-01-01T15:00:00.000' AS DateTime), CAST(N'2001-01-01T06:00:00.000' AS DateTime), CAST(N'2001-01-01T15:00:00.000' AS DateTime), CAST(N'2001-01-01T06:00:00.000' AS DateTime), CAST(N'2001-01-01T15:00:00.000' AS DateTime), CAST(N'2001-01-01T06:00:00.000' AS DateTime), CAST(N'2001-01-01T15:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[Availability] ([ID], [EmployeeID], [EffectiveDate], [MondayStart], [MondayEnd], [TuesdayStart], [TuesdayEnd], [WednesdayStart], [WednesdayEnd], [ThursdayStart], [ThursdayEnd], [FridayStart], [FridayEnd], [SaturdayStart], [SaturdayEnd], [SundayStart], [SundayEnd]) VALUES (11, 9, CAST(N'2001-01-01T00:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime), CAST(N'2001-01-01T13:00:00.000' AS DateTime), CAST(N'2001-01-01T22:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Availability] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (1, N'Genrale', N'Manjeri', 3, N'GM', N'GM', N'1234567890', N'gm@gm.com', CAST(N'2001-01-01' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (2, N'Smol', N'Manger', 2, N'M', N'M', N'1234567890', N'm@m.com', CAST(N'2002-01-01' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (6, N'Max', N'Connor', 1, N'Max', N'Max', N'1234567890', N'max@max.com', CAST(N'1992-12-05' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (7, N'William', N'White', 1, N'William', N'William', N'1234567890', N'william@william.com', CAST(N'1987-11-15' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (8, N'Kent', N'Shrock', 1, N'Kent', N'Kent', N'1234567890', N'kent@kent.com', CAST(N'1990-06-08' AS Date))
GO
INSERT [dbo].[Employee] ([ID], [FirstName], [LastName], [RankID], [Password], [Username], [Phone], [Email], [DateOfBirth]) VALUES (9, N'James', N'Kelly', 1, N'James', N'James', N'1234567890', N'james@james.com', CAST(N'1989-11-28' AS Date))
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (1, 1, 0, NULL)
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (2, 1, 0, 1)
GO
INSERT [dbo].[PickupRequest] ([ShiftID], [EmployeeID], [IsApproved], [ManagerID]) VALUES (8, 6, 0, 1)
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
INSERT [dbo].[Role] ([ID], [Name]) VALUES (1, N'General Manager')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (2, N'Manager')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (3, N'Cashier')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (4, N'Cook')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (5, N'Runner')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (6, N'Drive-Thru')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (7, N'Dining Room')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (8, N'Driver')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Shift] ON 
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (1, 0, 1, CAST(N'2023-03-06T00:00:00.000' AS DateTime), CAST(N'2023-03-06T06:00:00.000' AS DateTime), CAST(N'2023-03-06T15:00:00.000' AS DateTime), N'General Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (2, 0, 1, CAST(N'2023-03-07T00:00:00.000' AS DateTime), CAST(N'2023-03-07T06:00:00.000' AS DateTime), CAST(N'2023-03-07T15:00:00.000' AS DateTime), N'General Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (4, 0, 1, CAST(N'2023-03-08T00:00:00.000' AS DateTime), CAST(N'2023-03-08T06:00:00.000' AS DateTime), CAST(N'2023-03-08T15:00:00.000' AS DateTime), N'General Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (5, 0, 1, CAST(N'2023-03-09T00:00:00.000' AS DateTime), CAST(N'2023-03-09T08:00:00.000' AS DateTime), CAST(N'2023-03-08T17:00:00.000' AS DateTime), N'General Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (6, 0, 1, CAST(N'2023-03-10T00:00:00.000' AS DateTime), CAST(N'2023-03-10T06:00:00.000' AS DateTime), CAST(N'2023-03-10T15:00:00.000' AS DateTime), N'General Manager', N'')
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (7, 0, 2, CAST(N'2023-03-08T00:00:00.000' AS DateTime), CAST(N'2023-03-08T10:00:00.000' AS DateTime), CAST(N'2023-03-08T19:00:00.000' AS DateTime), N'Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (8, 0, 2, CAST(N'2023-03-09T00:00:00.000' AS DateTime), CAST(N'2023-03-09T11:00:00.000' AS DateTime), CAST(N'2023-03-09T20:00:00.000' AS DateTime), N'Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (10, 0, 2, CAST(N'2023-03-10T00:00:00.000' AS DateTime), CAST(N'2023-03-10T12:00:00.000' AS DateTime), CAST(N'2023-03-10T21:00:00.000' AS DateTime), N'Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (12, 0, 2, CAST(N'2023-03-11T00:00:00.000' AS DateTime), CAST(N'2023-03-11T13:00:00.000' AS DateTime), CAST(N'2023-03-10T22:00:00.000' AS DateTime), N'Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (13, 0, 2, CAST(N'2023-03-12T00:00:00.000' AS DateTime), CAST(N'2023-03-12T15:00:00.000' AS DateTime), CAST(N'2023-03-12T23:00:00.000' AS DateTime), N'Manager', NULL)
GO
INSERT [dbo].[Shift] ([ID], [IsOpen], [EmployeeID], [Date], [StartTime], [EndTime], [Role], [Notes]) VALUES (14, 0, 6, CAST(N'2023-03-07T00:00:00.000' AS DateTime), CAST(N'2023-03-07T17:00:00.000' AS DateTime), CAST(N'2023-03-07T22:00:00.000' AS DateTime), N'Cashier', NULL)
GO
SET IDENTITY_INSERT [dbo].[Shift] OFF
GO
SET IDENTITY_INSERT [dbo].[TimeOffRequest] ON 
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID]) VALUES (1, 1, CAST(N'2023-03-20T00:00:00.000' AS DateTime), CAST(N'2023-03-25T00:00:00.000' AS DateTime), 1, 1)
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID]) VALUES (2, 2, CAST(N'2023-05-16T00:00:00.000' AS DateTime), CAST(N'2023-05-31T00:00:00.000' AS DateTime), 0, NULL)
GO
INSERT [dbo].[TimeOffRequest] ([ID], [EmployeeID], [StartDate], [EndDate], [IsApproved], [ManagerID]) VALUES (4, 6, CAST(N'2023-09-30T00:00:00.000' AS DateTime), CAST(N'2023-09-30T00:00:00.000' AS DateTime), 1, 1)
GO
SET IDENTITY_INSERT [dbo].[TimeOffRequest] OFF
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
ALTER TABLE [dbo].[PickupRequest]  WITH CHECK ADD  CONSTRAINT [FK_PickupRequest_Shift] FOREIGN KEY([ShiftID])
REFERENCES [dbo].[Shift] ([ID])
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
