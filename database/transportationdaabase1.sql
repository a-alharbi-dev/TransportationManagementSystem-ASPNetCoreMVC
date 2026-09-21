USE [master]
GO
/****** Object:  Database [TransportationDB]    Script Date: 9/21/2026 11:29:21 AM ******/
CREATE DATABASE [TransportationDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TransportationDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\DATA\TransportationDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TransportationDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\DATA\TransportationDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TransportationDB] SET COMPATIBILITY_LEVEL = 170
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TransportationDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TransportationDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TransportationDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TransportationDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TransportationDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TransportationDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TransportationDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TransportationDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TransportationDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TransportationDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TransportationDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TransportationDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TransportationDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TransportationDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TransportationDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TransportationDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TransportationDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TransportationDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TransportationDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TransportationDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TransportationDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TransportationDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TransportationDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TransportationDB] SET RECOVERY FULL 
GO
ALTER DATABASE [TransportationDB] SET  MULTI_USER 
GO
ALTER DATABASE [TransportationDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TransportationDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TransportationDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TransportationDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TransportationDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TransportationDB] SET OPTIMIZED_LOCKING = OFF 
GO
ALTER DATABASE [TransportationDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [TransportationDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [TransportationDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TransportationDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/21/2026 11:29:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assignments]    Script Date: 9/21/2026 11:29:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignments](
	[AssignmentID] [int] IDENTITY(1,1) NOT NULL,
	[TripID] [int] NOT NULL,
	[DriverID] [int] NOT NULL,
	[VehicleID] [int] NOT NULL,
	[AssignedDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Assignments] PRIMARY KEY CLUSTERED 
(
	[AssignmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 9/21/2026 11:29:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drivers](
	[DriverID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[LicenseNumber] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Drivers] PRIMARY KEY CLUSTERED 
(
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmergencyRequests]    Script Date: 9/21/2026 11:29:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmergencyRequests](
	[RequestID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[PickupLocation] [nvarchar](max) NOT NULL,
	[Destination] [nvarchar](max) NOT NULL,
	[Priority] [nvarchar](max) NOT NULL,
	[RequestTime] [datetime2](7) NOT NULL,
	[Reason] [nvarchar](max) NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NOT NULL,
	[DriverID] [int] NULL,
	[VehicleID] [int] NULL,
 CONSTRAINT [PK_EmergencyRequests] PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 9/21/2026 11:29:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trips]    Script Date: 9/21/2026 11:29:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trips](
	[TripID] [int] IDENTITY(1,1) NOT NULL,
	[PickupLocation] [nvarchar](max) NOT NULL,
	[Destination] [nvarchar](max) NOT NULL,
	[DepartureTime] [time](7) NOT NULL,
	[ArrivalTime] [time](7) NOT NULL,
 CONSTRAINT [PK_Trips] PRIMARY KEY CLUSTERED 
(
	[TripID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/21/2026 11:29:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 9/21/2026 11:29:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[VehicleID] [int] IDENTITY(1,1) NOT NULL,
	[PlateNumber] [nvarchar](max) NOT NULL,
	[VehicleType] [nvarchar](max) NOT NULL,
	[Model] [nvarchar](max) NOT NULL,
	[Capacity] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260809084429_initialCreationDB', N'10.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260813010057_AddEmergencyRequestAndNotifications', N'10.0.10')
GO
SET IDENTITY_INSERT [dbo].[Assignments] ON 

INSERT [dbo].[Assignments] ([AssignmentID], [TripID], [DriverID], [VehicleID], [AssignedDate], [Status]) VALUES (1, 1, 1, 1, CAST(N'2026-08-12T09:58:27.4933333' AS DateTime2), N'Assigned')
INSERT [dbo].[Assignments] ([AssignmentID], [TripID], [DriverID], [VehicleID], [AssignedDate], [Status]) VALUES (2, 2, 2, 2, CAST(N'2026-08-12T09:58:27.4933333' AS DateTime2), N'Assigned')
INSERT [dbo].[Assignments] ([AssignmentID], [TripID], [DriverID], [VehicleID], [AssignedDate], [Status]) VALUES (3, 3, 3, 3, CAST(N'2026-08-12T09:58:27.4933333' AS DateTime2), N'Assigned')
SET IDENTITY_INSERT [dbo].[Assignments] OFF
GO
SET IDENTITY_INSERT [dbo].[Drivers] ON 

INSERT [dbo].[Drivers] ([DriverID], [FullName], [Phone], [LicenseNumber], [Status]) VALUES (1, N'Ahmed Ali', N'0501111111', N'LIC-1001', N'Available')
INSERT [dbo].[Drivers] ([DriverID], [FullName], [Phone], [LicenseNumber], [Status]) VALUES (2, N'Mohammed Saleh', N'0502222222', N'LIC-1002', N'Available')
INSERT [dbo].[Drivers] ([DriverID], [FullName], [Phone], [LicenseNumber], [Status]) VALUES (3, N'Fahad Omar', N'0503333333', N'LIC-1003', N'Available')
SET IDENTITY_INSERT [dbo].[Drivers] OFF
GO
SET IDENTITY_INSERT [dbo].[EmergencyRequests] ON 

INSERT [dbo].[EmergencyRequests] ([RequestID], [UserID], [PickupLocation], [Destination], [Priority], [RequestTime], [Reason], [Notes], [Status], [DriverID], [VehicleID]) VALUES (1, 3, N'Obhur Housing', N'Armed Forces Hospital', N'High', CAST(N'2026-08-13T08:17:00.0000000' AS DateTime2), N'Medical Emergency', NULL, N'Approved', 3, 2)
INSERT [dbo].[EmergencyRequests] ([RequestID], [UserID], [PickupLocation], [Destination], [Priority], [RequestTime], [Reason], [Notes], [Status], [DriverID], [VehicleID]) VALUES (2, 3, N'Obhur Housing', N'Main Administration', N'High', CAST(N'2026-08-14T13:03:00.0000000' AS DateTime2), N'Medical Emergency', NULL, N'Rejected', NULL, NULL)
INSERT [dbo].[EmergencyRequests] ([RequestID], [UserID], [PickupLocation], [Destination], [Priority], [RequestTime], [Reason], [Notes], [Status], [DriverID], [VehicleID]) VALUES (3, 3, N'Obhur Housing', N'Medical City', N'High', CAST(N'2026-08-14T13:58:00.0000000' AS DateTime2), N'Urgent Work Assignment', NULL, N'Approved', 2, 1)
INSERT [dbo].[EmergencyRequests] ([RequestID], [UserID], [PickupLocation], [Destination], [Priority], [RequestTime], [Reason], [Notes], [Status], [DriverID], [VehicleID]) VALUES (4, 3, N'Jafali Housing', N'Medical City', N'High', CAST(N'2026-08-14T14:59:00.0000000' AS DateTime2), N'Patient Transfer', NULL, N'Rejected', NULL, NULL)
SET IDENTITY_INSERT [dbo].[EmergencyRequests] OFF
GO
SET IDENTITY_INSERT [dbo].[Notifications] ON 

INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [CreatedAt], [Status]) VALUES (1, 3, N'Your emergency request has been approved. Driver: Mohammed Saleh. Vehicle: BUS-101.', CAST(N'2026-08-14T23:57:19.0107150' AS DateTime2), N'Read')
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [CreatedAt], [Status]) VALUES (2, 3, N'Your emergency request has been rejected.', CAST(N'2026-08-14T23:57:23.0885649' AS DateTime2), N'Read')
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO
SET IDENTITY_INSERT [dbo].[Trips] ON 

INSERT [dbo].[Trips] ([TripID], [PickupLocation], [Destination], [DepartureTime], [ArrivalTime]) VALUES (1, N'Obhur Housing', N'King Fahd Armed Forces Hospital', CAST(N'08:00:00' AS Time), CAST(N'08:30:00' AS Time))
INSERT [dbo].[Trips] ([TripID], [PickupLocation], [Destination], [DepartureTime], [ArrivalTime]) VALUES (2, N'Al Hamra', N'King Fahd Armed Forces Hospital', CAST(N'09:00:00' AS Time), CAST(N'09:30:00' AS Time))
INSERT [dbo].[Trips] ([TripID], [PickupLocation], [Destination], [DepartureTime], [ArrivalTime]) VALUES (3, N'Al Andalus', N'King Fahd Armed Forces Hospital', CAST(N'10:00:00' AS Time), CAST(N'10:30:00' AS Time))
INSERT [dbo].[Trips] ([TripID], [PickupLocation], [Destination], [DepartureTime], [ArrivalTime]) VALUES (4, N'Al Hamra', N'King Fahd Road', CAST(N'00:09:00' AS Time), CAST(N'00:09:00' AS Time))
SET IDENTITY_INSERT [dbo].[Trips] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [FullName], [Username], [Password], [Role], [IsActive]) VALUES (1, N'System Manager', N'manager', N'123456', N'Manager', 1)
INSERT [dbo].[Users] ([UserID], [FullName], [Username], [Password], [Role], [IsActive]) VALUES (2, N'Movement Officer', N'movement', N'123456', N'Movement Officer', 1)
INSERT [dbo].[Users] ([UserID], [FullName], [Username], [Password], [Role], [IsActive]) VALUES (3, N'Nurse', N'nurse', N'123456', N'Nurse', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehicles] ON 

INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [VehicleType], [Model], [Capacity], [Status]) VALUES (1, N'BUS-101', N'Bus', N'Toyota Coaster', 20, N'Available')
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [VehicleType], [Model], [Capacity], [Status]) VALUES (2, N'BUS-102', N'Bus', N'Toyota Coaster', 20, N'Available')
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [VehicleType], [Model], [Capacity], [Status]) VALUES (3, N'VAN-205', N'Van', N'Toyota Hiace', 10, N'Available')
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
GO
/****** Object:  Index [IX_Assignments_DriverID]    Script Date: 9/21/2026 11:29:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Assignments_DriverID] ON [dbo].[Assignments]
(
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Assignments_TripID]    Script Date: 9/21/2026 11:29:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Assignments_TripID] ON [dbo].[Assignments]
(
	[TripID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Assignments_VehicleID]    Script Date: 9/21/2026 11:29:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Assignments_VehicleID] ON [dbo].[Assignments]
(
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmergencyRequests_DriverID]    Script Date: 9/21/2026 11:29:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_EmergencyRequests_DriverID] ON [dbo].[EmergencyRequests]
(
	[DriverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmergencyRequests_UserID]    Script Date: 9/21/2026 11:29:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_EmergencyRequests_UserID] ON [dbo].[EmergencyRequests]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EmergencyRequests_VehicleID]    Script Date: 9/21/2026 11:29:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_EmergencyRequests_VehicleID] ON [dbo].[EmergencyRequests]
(
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Notifications_UserID]    Script Date: 9/21/2026 11:29:23 AM ******/
CREATE NONCLUSTERED INDEX [IX_Notifications_UserID] ON [dbo].[Notifications]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Assignments]  WITH CHECK ADD  CONSTRAINT [FK_Assignments_Drivers_DriverID] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[Assignments] CHECK CONSTRAINT [FK_Assignments_Drivers_DriverID]
GO
ALTER TABLE [dbo].[Assignments]  WITH CHECK ADD  CONSTRAINT [FK_Assignments_Trips_TripID] FOREIGN KEY([TripID])
REFERENCES [dbo].[Trips] ([TripID])
GO
ALTER TABLE [dbo].[Assignments] CHECK CONSTRAINT [FK_Assignments_Trips_TripID]
GO
ALTER TABLE [dbo].[Assignments]  WITH CHECK ADD  CONSTRAINT [FK_Assignments_Vehicles_VehicleID] FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([VehicleID])
GO
ALTER TABLE [dbo].[Assignments] CHECK CONSTRAINT [FK_Assignments_Vehicles_VehicleID]
GO
ALTER TABLE [dbo].[EmergencyRequests]  WITH CHECK ADD  CONSTRAINT [FK_EmergencyRequests_Drivers_DriverID] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([DriverID])
GO
ALTER TABLE [dbo].[EmergencyRequests] CHECK CONSTRAINT [FK_EmergencyRequests_Drivers_DriverID]
GO
ALTER TABLE [dbo].[EmergencyRequests]  WITH CHECK ADD  CONSTRAINT [FK_EmergencyRequests_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[EmergencyRequests] CHECK CONSTRAINT [FK_EmergencyRequests_Users_UserID]
GO
ALTER TABLE [dbo].[EmergencyRequests]  WITH CHECK ADD  CONSTRAINT [FK_EmergencyRequests_Vehicles_VehicleID] FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([VehicleID])
GO
ALTER TABLE [dbo].[EmergencyRequests] CHECK CONSTRAINT [FK_EmergencyRequests_Vehicles_VehicleID]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_Users_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_Users_UserID]
GO
USE [master]
GO
ALTER DATABASE [TransportationDB] SET  READ_WRITE 
GO
