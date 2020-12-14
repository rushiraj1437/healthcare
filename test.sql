USE [master]
GO
/****** Object:  Database [PatientMangement]    Script Date: 12/14/2020 12:25:04 AM ******/
CREATE DATABASE [PatientMangement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PatientMangement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PatientMangement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PatientMangement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PatientMangement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PatientMangement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PatientMangement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PatientMangement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PatientMangement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PatientMangement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PatientMangement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PatientMangement] SET ARITHABORT OFF 
GO
ALTER DATABASE [PatientMangement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PatientMangement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PatientMangement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PatientMangement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PatientMangement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PatientMangement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PatientMangement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PatientMangement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PatientMangement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PatientMangement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PatientMangement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PatientMangement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PatientMangement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PatientMangement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PatientMangement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PatientMangement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PatientMangement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PatientMangement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PatientMangement] SET  MULTI_USER 
GO
ALTER DATABASE [PatientMangement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PatientMangement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PatientMangement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PatientMangement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PatientMangement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PatientMangement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PatientMangement] SET QUERY_STORE = OFF
GO
USE [PatientMangement]
GO
/****** Object:  Table [dbo].[Appointment]    Script Date: 12/14/2020 12:25:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointment](
	[AppointmentID] [int] IDENTITY(1,1) NOT NULL,
	[PatientID] [int] NOT NULL,
	[DoctorID] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[StartTime] [time](7) NULL,
	[EndTime] [time](7) NULL,
	[ContactNumber] [bigint] NULL,
	[Address] [varchar](50) NULL,
	[Disease] [varchar](max) NULL,
	[Advice] [varchar](max) NULL,
	[Reply] [varchar](max) NULL,
	[Fees] [int] NULL,
	[Status] [smallint] NULL,
 CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED 
(
	[AppointmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 12/14/2020 12:25:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[Gender] [varchar](20) NOT NULL,
	[ContactNum] [bigint] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Appointment] ON 
GO
INSERT [dbo].[Appointment] ([AppointmentID], [PatientID], [DoctorID], [Date], [StartTime], [EndTime], [ContactNumber], [Address], [Disease], [Advice], [Reply], [Fees], [Status]) VALUES (1, 3, 1, CAST(N'1998-09-09' AS Date), CAST(N'01:00:00' AS Time), CAST(N'02:00:00' AS Time), 32433242, N'Nashik', N'Yes', N'Testing', N'test', 233, 1)
GO
INSERT [dbo].[Appointment] ([AppointmentID], [PatientID], [DoctorID], [Date], [StartTime], [EndTime], [ContactNumber], [Address], [Disease], [Advice], [Reply], [Fees], [Status]) VALUES (2, 3, 1, CAST(N'1998-09-07' AS Date), CAST(N'01:00:00' AS Time), CAST(N'02:00:00' AS Time), 2332332, N'Nashik', N'Yes', N'hii', N'byeeeee', 700, 1)
GO
SET IDENTITY_INSERT [dbo].[Appointment] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([ID], [Name], [Username], [Password], [Role], [Gender], [ContactNum]) VALUES (1, N'aditi', N'aditi@gmail.com', N'123', N'doctor', N'female', 849484848)
GO
INSERT [dbo].[User] ([ID], [Name], [Username], [Password], [Role], [Gender], [ContactNum]) VALUES (3, N'rushi', N'rushi@gmail.com', N'111', N'user', N'Male', 23323232)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Appointment] ADD  DEFAULT ((0)) FOR [Status]
GO
USE [master]
GO
ALTER DATABASE [PatientMangement] SET  READ_WRITE 
GO
