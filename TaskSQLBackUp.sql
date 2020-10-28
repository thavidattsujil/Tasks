USE [Task]
GO
/****** Object:  Table [dbo].[MunicipalityInfo]    Script Date: 28-10-2020 10:38:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MunicipalityInfo](
	[Municipality_Id] [int] NULL,
	[Municipality_Name] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MunicipalityTaxScheduled ]    Script Date: 28-10-2020 10:38:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MunicipalityTaxScheduled ](
	[DetailsId] [int] NULL,
	[MunicipalityId] [int] NULL,
	[TaxId] [int] NULL,
	[TaxType] [nvarchar](50) NULL,
	[Date] [datetime] NULL,
	[Result] [float] NULL,
	[UpdatedDateTime] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxTypeRule]    Script Date: 28-10-2020 10:38:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxTypeRule](
	[TaxId] [int] NULL,
	[TaxName] [varchar](50) NULL,
	[Result] [nchar](10) NULL,
	[DateInput] [datetime] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[MunicipalityInfo] ([Municipality_Id], [Municipality_Name]) VALUES (1, N'Copenhagen')
GO
INSERT [dbo].[MunicipalityInfo] ([Municipality_Id], [Municipality_Name]) VALUES (2, N'Test')
GO
INSERT [dbo].[MunicipalityTaxScheduled ] ([DetailsId], [MunicipalityId], [TaxId], [TaxType], [Date], [Result], [UpdatedDateTime]) VALUES (1, 1, 1, N'Yearly', CAST(N'2016-01-01T00:00:00.000' AS DateTime), 0.1, CAST(N'2020-10-28T05:04:34.273' AS DateTime))
GO
INSERT [dbo].[TaxTypeRule] ([TaxId], [TaxName], [Result], [DateInput]) VALUES (1, N'Yearly', N'0.2       ', NULL)
GO
INSERT [dbo].[TaxTypeRule] ([TaxId], [TaxName], [Result], [DateInput]) VALUES (2, N'Monthly ', N'0.4       ', NULL)
GO
INSERT [dbo].[TaxTypeRule] ([TaxId], [TaxName], [Result], [DateInput]) VALUES (3, N'Daily', N'0.1       ', NULL)
GO
