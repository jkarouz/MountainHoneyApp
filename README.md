# MountainHoneyApp
 
 CREATE TABLE [dbo].[MountainHoney](
       [Id] [int] IDENTITY(1,1) NOT NULL,
       [Name] [nvarchar](50) NULL,
       [Surname] [nvarchar](50) NULL,
       [FullName] [nvarchar](200) NULL,
       [IdNumber] [int] NULL,
       [ContactNumber] [nvarchar](50) NULL,
       [RentAmount] [int] NULL,
       [DateOnlyTime] [datetime] NULL,
       [DateOnly]  AS (CONVERT([varchar](10),[DateOnlyTime],(105))),
CONSTRAINT [PK_MountainHoney] PRIMARY KEY CLUSTERED 
(
       [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Sunrise](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Surname] [nvarchar](50) NULL,
	[FullName] [nvarchar](200) NULL,
	[Comments] [nvarchar](500) NULL,
	[IdNumber] [nvarchar](50) NULL,
	[ContactNumber] [nvarchar](50) NULL,
	[Place] [nvarchar](50) NULL,
	[Payment] [nvarchar](50) NULL,
	[Method] [nvarchar](50) NULL,
	[RentAmount] [int] NULL,
	[OldAmount] [int] NULL,
	[FullAmount] [int] NULL,
	[DateOnlyTime] [datetime] NULL,
	[DateOnly]  AS (CONVERT([varchar](10),[DateOnlyTime],(105))),
 CONSTRAINT [PK_Sunrise] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[userdetails](
       [Id] [int] IDENTITY(1,1) NOT NULL,
       [Email] [nvarchar](50) NULL,
       [Mobile] [nvarchar](50) NULL,
       [Name] [nvarchar](50) NULL,
       [Password] [nvarchar](50) NULL
) ON [PRIMARY]
GO
