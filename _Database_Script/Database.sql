USE [bpta3817_db2]
GO
/****** Object:  Table [dbo].[About]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[About](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[MetaTitle] [varchar](250) NULL,
	[Description] [nvarchar](500) NULL,
	[Image] [nvarchar](250) NULL,
	[Detail] [ntext] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](250) NULL,
	[MetaDescriptions] [nchar](250) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_About] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[MetaTitle] [varchar](250) NULL,
	[ParentID] [bigint] NULL,
	[DisplayOrder] [int] NULL CONSTRAINT [DF_Category_DisplayOrder]  DEFAULT ((0)),
	[Image] [nvarchar](250) NULL,
	[SeoTitle] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_Category_CreatedDate]  DEFAULT (getdate()),
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](250) NULL,
	[MetaDescriptions] [nchar](250) NULL,
	[Status] [bit] NULL CONSTRAINT [DF_Category_Status]  DEFAULT ((1)),
	[ShowOnHome] [bit] NULL CONSTRAINT [DF_Category_ShowOnHome]  DEFAULT ((0)),
	[Language] [varchar](2) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Content] [ntext] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Content]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Content](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[MetaTitle] [varchar](250) NULL,
	[Description] [nvarchar](500) NULL,
	[Image] [nvarchar](250) NULL,
	[CategoryID] [bigint] NULL,
	[Detail] [ntext] NULL,
	[Warranty] [int] NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_Content_CreatedDate]  DEFAULT (getdate()),
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](250) NULL,
	[MetaDescriptions] [nchar](250) NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_Content_Status]  DEFAULT ((1)),
	[TopHot] [datetime] NULL,
	[ViewCount] [int] NULL CONSTRAINT [DF_Content_ViewCount]  DEFAULT ((0)),
	[Tags] [nvarchar](500) NULL,
	[Language] [varchar](2) NULL,
	[IsProduct] [bit] NULL,
	[File] [nvarchar](250) NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ContentTag]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContentTag](
	[ContentID] [bigint] NOT NULL,
	[TagID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ContentTag] PRIMARY KEY CLUSTERED 
(
	[ContentID] ASC,
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Credential]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Credential](
	[UserGroupID] [varchar](20) NOT NULL,
	[RoleID] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Credential] PRIMARY KEY CLUSTERED 
(
	[UserGroupID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Person] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[MobilePhone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Content] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_Feedback_CreatedDate]  DEFAULT (getdate()),
	[Status] [bit] NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Footer]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Footer](
	[ID] [varchar](50) NOT NULL,
	[Content] [ntext] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Footer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Language]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Language](
	[ID] [varchar](2) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[IsDefault] [bit] NOT NULL CONSTRAINT [DF_Language_IsDefault]  DEFAULT ((0)),
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](50) NULL,
	[Link] [nvarchar](250) NULL,
	[DisplayOrder] [int] NULL,
	[Target] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[TypeID] [int] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuType]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_MenuType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Order]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CustomerID] [bigint] NULL,
	[ShipName] [nvarchar](50) NULL,
	[ShipMobile] [varchar](50) NULL,
	[ShipAddress] [nvarchar](50) NULL,
	[ShipEmail] [nvarchar](50) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ProductID] [bigint] NOT NULL,
	[OrderID] [bigint] NOT NULL,
	[Quantity] [int] NULL CONSTRAINT [DF_OrderDetail_Quantity]  DEFAULT ((1)),
	[Price] [decimal](18, 0) NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[Code] [varchar](10) NULL,
	[MetaTitle] [varchar](250) NULL,
	[Description] [nvarchar](500) NULL,
	[Image] [nvarchar](250) NULL,
	[MoreImages] [xml] NULL,
	[Price] [decimal](18, 0) NULL CONSTRAINT [DF_Product_Price]  DEFAULT ((0)),
	[PromotionPrice] [decimal](18, 0) NULL,
	[IncludedVAT] [bit] NULL,
	[Quantity] [int] NOT NULL CONSTRAINT [DF_Product_Quantity]  DEFAULT ((0)),
	[CategoryID] [bigint] NULL,
	[Detail] [ntext] NULL,
	[Warranty] [int] NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_Product_CreatedDate]  DEFAULT (getdate()),
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](250) NULL,
	[MetaDescriptions] [nchar](250) NULL,
	[Status] [bit] NULL CONSTRAINT [DF_Product_Status]  DEFAULT ((1)),
	[TopHot] [datetime] NULL,
	[ViewCount] [int] NULL CONSTRAINT [DF_Product_ViewCount]  DEFAULT ((0)),
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NULL,
	[MetaTitle] [varchar](250) NULL,
	[ParentID] [bigint] NULL,
	[DisplayOrder] [int] NULL CONSTRAINT [DF_ProductCategory_DisplayOrder]  DEFAULT ((0)),
	[SeoTitle] [nvarchar](250) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_ProductCategory_CreatedDate]  DEFAULT (getdate()),
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[MetaKeywords] [nvarchar](250) NULL,
	[MetaDescriptions] [nchar](250) NULL,
	[Status] [bit] NULL CONSTRAINT [DF_ProductCategory_Status]  DEFAULT ((1)),
	[ShowOnHome] [bit] NULL CONSTRAINT [DF_ProductCategory_ShowOnHome]  DEFAULT ((0)),
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Slide]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Slide](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Image] [nvarchar](250) NULL,
	[DisplayOrder] [int] NULL CONSTRAINT [DF_Slide_DisplayOrder]  DEFAULT ((1)),
	[Link] [nvarchar](250) NULL,
	[Description] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_Slide_CreatedDate]  DEFAULT (getdate()),
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Slide] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SystemConfig]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemConfig](
	[ID] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Type] [varchar](50) NULL,
	[Value] [nvarchar](250) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_SystemConfig] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tag]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tag](
	[ID] [varchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](32) NULL,
	[GroupID] [varchar](20) NULL CONSTRAINT [DF_User_GroupID]  DEFAULT ('MEMBER'),
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[ProvinceID] [int] NULL,
	[DistrictID] [int] NULL,
	[CreatedDate] [datetime] NULL CONSTRAINT [DF_User_CreatedDate]  DEFAULT (getdate()),
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 7/17/2016 10:19:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserGroup](
	[ID] [varchar](20) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

GO
INSERT [dbo].[Category] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [Image], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome], [Language]) VALUES (1, N'Tin thế giới', N'tin-the-gioi', NULL, NULL, NULL, NULL, CAST(N'2015-08-15 07:49:20.183' AS DateTime), NULL, CAST(N'2016-01-16 18:31:48.357' AS DateTime), NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[Category] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [Image], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome], [Language]) VALUES (2, N'Tin trong nước', N'tin-trong-nuoc', NULL, NULL, NULL, NULL, CAST(N'2015-08-15 07:49:33.087' AS DateTime), NULL, CAST(N'2016-01-16 18:31:44.340' AS DateTime), NULL, NULL, NULL, 1, NULL, NULL)
GO
INSERT [dbo].[Category] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [Image], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome], [Language]) VALUES (4, N'About Us', N'about-us', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2015-12-27 12:29:24.660' AS DateTime), NULL, NULL, NULL, 1, NULL, N'vi')
GO
INSERT [dbo].[Category] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [Image], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome], [Language]) VALUES (5, N'News & Events', N'news-events', NULL, NULL, N'/Data/images/intro-product/news-events.jpg', NULL, NULL, NULL, CAST(N'2015-12-27 12:29:19.107' AS DateTime), NULL, NULL, NULL, 1, NULL, N'vi')
GO
INSERT [dbo].[Category] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [Image], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome], [Language]) VALUES (6, N'Festival Highlights', N'festival-highlights', NULL, NULL, NULL, NULL, NULL, NULL, CAST(N'2015-12-27 12:18:02.943' AS DateTime), NULL, NULL, NULL, 1, NULL, N'vi')
GO
INSERT [dbo].[Category] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [Image], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome], [Language]) VALUES (7, N'Products & Services', N'products-services', NULL, NULL, NULL, NULL, CAST(N'2015-12-28 20:12:06.913' AS DateTime), NULL, CAST(N'2016-01-16 18:31:36.280' AS DateTime), NULL, NULL, NULL, 1, NULL, N'vi')
GO
INSERT [dbo].[Category] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [Image], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome], [Language]) VALUES (9, N' Styrene Viscosity Modifiers ( SVM Series )', N'styrene-viscosity-modifiers-svm-series', 7, NULL, N'/Data/images/SVM-5(1).jpg', NULL, CAST(N'2016-02-21 10:02:50.740' AS DateTime), NULL, CAST(N'2016-06-26 20:44:28.140' AS DateTime), NULL, NULL, NULL, 1, NULL, N'vi')
GO
INSERT [dbo].[Category] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [Image], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome], [Language]) VALUES (10, N'Lubricant Oil Additives – OCP&PAMA Grafting Technology', N'lubricant-oil-additives-ocp-pama-grafting-technology', 7, 1, N'/Data/images/intro-product/Photo%20of%20VIS-BEST%20V%2020%20in%20POWDER%20FORM.jpg', NULL, CAST(N'2016-02-21 10:17:12.623' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1, NULL, N'vi')
GO
INSERT [dbo].[Category] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [Image], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome], [Language]) VALUES (11, N'Lubricant Oil Additives – OCP Viscosity Index Improvers', N'lubricant-oil-additives-ocp-viscosity-index-improvers', 7, 2, N'/Data/images/intro-product/BPT%20logo%20VISBEST%20V20.JPG', NULL, CAST(N'2016-02-21 10:18:23.810' AS DateTime), NULL, CAST(N'2016-02-23 20:34:05.833' AS DateTime), NULL, NULL, NULL, 1, NULL, N'vi')
GO
INSERT [dbo].[Category] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [Image], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome], [Language]) VALUES (12, N'PPDs (Pour Point Depressants)', N'ppds-pour-point-depressants', 7, NULL, N'/Data/images/intro-product/PPD%20P20%20nen%20vang.png', NULL, CAST(N'2016-02-21 10:19:05.777' AS DateTime), NULL, CAST(N'2016-03-31 12:49:26.203' AS DateTime), NULL, NULL, NULL, 1, NULL, N'vi')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Contact] ON 

GO
INSERT [dbo].[Contact] ([ID], [Content], [Status]) VALUES (1, N'<p>Công ty CP Online Shop</p><p>Địa chỉ: Số 1 Quang Trung Hà Đông </p> <p>Điện thoại: 04 6523 5342</p>', 1)
GO
SET IDENTITY_INSERT [dbo].[Contact] OFF
GO
SET IDENTITY_INSERT [dbo].[Content] ON 

GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (1, N'tin tức', N'tin-tuc-demo', N'424', N'/Data/images/14.PNG', 1, N'<p>Ch&agrave;o mọi người m&igrave;nh l&agrave; người mới</p>

<p>Ch&agrave;o mọi người m&igrave;nh l&agrave; người mới</p>
', 12, CAST(N'2015-09-20 08:01:57.590' AS DateTime), N'toanbn', CAST(N'2016-02-28 13:52:20.713' AS DateTime), NULL, N'313', N'13                                                                                                                                                                                                                                                        ', 1, NULL, 0, N'tin tức,thời sự', NULL, NULL, N'/Data/files/101%20LINQ%20Samples.zip')
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (2, N'About Us', N'About-Us', N'In the world of competitive global economy and in the shortage of raw materials worldwide such as chemical materials & additives..., ', NULL, 4, N'<p style="text-align:justify">In the world of competitive global economy and in the shortage of raw materials worldwide such as chemical materials &amp; additives..., specially for fast &ndash; developing oils &amp; lubricants industry, the key benchmark is that how to search for stable quality products with most competitive prices being vitally essential to many enterprises. With the huge of experiences in petroleum chemicals, BPT Chemicals Company has been building up very good relationships with most reliable suppliers in assurance of supplying to our customers: trusted qualified inputs in which always satisfied your demands with best offers.&nbsp;</p>

<p style="text-align:justify">Establishing representative office in UAE, where can guarantee to us full volume capability with on &ndash; time delivery, has been strengthening our position as one of the biggest suppliers in Oils &amp; Lubricants. <strong>BPT</strong> stands for &quot;<strong>Bringing You Profession &amp; Trust</strong>&quot; running as our motto to commit of companion with your business success.</p>

<p style="text-align:justify"><strong>OUR STRENGTH</strong></p>

<p>If you have previously had any issue with gaming addiction, financial difficulty, or any other such issue accounted for under our &ldquo;Responsible Gaming&rdquo; procedure, it is <a href="http://www.bellayoscura.com/">http://www.bellayoscura.com/</a>your responsibility to refrain from opening new accounts whilst such issue is in place.</p>

<p style="text-align:justify">People is the best decisive core in any business milestone.</p>

<p style="text-align:justify">We are so proud of inviting many young and talent employees. Their expertise in Petroleum Chemicals and active &ndash; thinking always drive us come up with many creative ideas in developing new products and strategies for new markets.</p>

<p style="text-align:justify">Large amount of employees working in main office and representative office under Sales &amp; Marketing, Import &amp; Export Department, Accounting Department, Administration, Forwarding Department, Warehouse which always satisfy clients with very professional &amp; enthusiastic attitude.</p>

<p style="text-align:justify">Therefore, our products have being crossed over Asia market and approached to many big markets like Middle East, South America, Africa,&hellip;.All these strengths make us completely different and trusted !</p>
', NULL, CAST(N'2015-09-20 08:01:57.000' AS DateTime), NULL, CAST(N'2015-12-27 11:50:25.017' AS DateTime), N'USER_SESSION', NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (3, N'Union Of The European Lubricants Industry (UEIL Congress in 2014) ', N'union-of-the-european-lubricants-industry-ueil-congress-in-2014-', N'Union of The European Lubricants Industry ( UEIL Congress 2014 )  Madrid Spain, 22nd – 24th Oct 2014.The Biggest & Most Premium Congress for Lubricant Industry in European Markets. Main Prestigious Sponsors: ', N'/Data/images/IMG_0148.JPG', 5, N'<p style="text-align:center"><strong>Union of The European Lubricants Industry ( UEIL Congress 2014 ) &nbsp;</strong></p>

<p style="text-align:center">Madrid Spain, 22<sup>nd</sup> &ndash; 24<sup>th</sup> Oct 2014.</p>

<p style="text-align:center"><span style="color:#0000ff; font-size:medium"><strong>The Biggest &amp; Most Premium Congress for Lubricant Industry in European Markets.</strong></span></p>

<p style="text-align:center"><strong>Main Prestigious Sponsors:</strong> <a href="http://www.ueil.org/en/EVENTS/Congress-2014/Sponsorship/">http://www.ueil.org/en/EVENTS/Congress-2014/Sponsorship/</a></p>

<p style="text-align:center"><u><u><img alt="" src="/Data/images/EUIL%20khc%20di.png" style="width:100%" /></u></u></p>

<p style="text-align:center"><u><u><img alt="" src="/Data/images/IMG_0148.JPG" style="height:413px; width:550px" /></u></u></p>

<p style="text-align:center"><u><u><img alt="" src="/Data/images/IMG_0156.JPG" style="height:413px; width:550px" /></u></u></p>
', NULL, CAST(N'2015-09-20 08:01:57.000' AS DateTime), NULL, CAST(N'2016-02-28 12:06:31.183' AS DateTime), N'USER_SESSION', NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (4, N'China ( Chongqing ) International Lubricanting Exhibition 2015', N'china-chongqing-international-lubricanting-exhibition-2015', N'Registration & Build up: Sept.9-10, 2015 (09:00–17:00)
Opening Ceremony: Sept.11, 2015 (09:20)
Exhibition & Trading: Sept.11-13, 2015 (09:00–16:30)
Closure & Dismantling: Sept.13, 2015(15:00)', N'/Data/images/news-event/China-Chongqing.jpg', 5, N'<p>Registration &amp; Build up: Sept.9-10, 2015 (09:00&ndash;17:00)</p>

<p>Opening Ceremony: Sept.11, 2015 (09:20)</p>

<p>Exhibition &amp; Trading: Sept.11-13, 2015 (09:00&ndash;16:30)</p>

<p>Closure &amp; Dismantling: Sept.13, 2015(15:00)</p>
', NULL, CAST(N'2015-12-27 11:09:47.340' AS DateTime), N'toanbn', CAST(N'2016-02-28 12:13:37.097' AS DateTime), NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (5, N'Styrene Star Solid - SVM 5 ( SSI 5 )', N'styrene-star-solid-svm-5-ssi-5', NULL, N'/Data/images/SVM-5(1).jpg', 9, N'<p><img alt="" src="/Data/images/TDS_SVM_05--%20(2).jpg" style="height:822px; width:635px" /></p>
', NULL, CAST(N'2015-12-28 20:14:23.290' AS DateTime), N'toanbn', CAST(N'2016-06-26 20:45:09.667' AS DateTime), NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, N'/Data/files/TDS%20SVM%2005.doc')
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (6, N'Olefin Copolymer Pellet/Granular Form ', N'Olefin-Copolymer-Pellet-Granular-Form', NULL, N'/Data/images/intro-product/EPDM%20V141%20OG.jpg', 11, N'<p><img alt="" src="/Data/images/TDS%203of%20Olefin%20Copolymer%20EPM%20Pellet%20Form%20logo%20new.jpg" style="height:902px; width:635px" /></p>
', NULL, CAST(N'2015-12-28 20:25:37.320' AS DateTime), N'toanbn', CAST(N'2016-02-24 05:41:50.357' AS DateTime), NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (7, N'Olefin Copolymer Bale/Block Form ', N'Olefin-Copolymer-Bale-Block-Form', NULL, N'/Data/images/intro-product/BALE%20FORM.JPG', 11, N'<p><img alt="" src="/Data/images/TDS%203%20Olefin%20Copolymer%20Bale%20Form%20new%20logo.jpg" style="height:907px; width:635px" /></p>
', NULL, CAST(N'2015-12-28 20:26:20.297' AS DateTime), N'toanbn', CAST(N'2016-02-24 05:42:10.590' AS DateTime), NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (8, N'VIS - BEST® PO - 20 Crumb / Powder Form', N'vis-best-po-20-crumb-powder-form', NULL, N'/Data/images/intro-product/Photo%20of%20VIS-BEST%20V%2020%20in%20POWDER%20FORM.jpg', 10, N'<p><img alt="" src="/Data/images/TDS%20VISBEST%20PO20%20POWDER%20FORM%201(1).jpg" style="height:903px; width:635px" /><img alt="" src="/Data/images/TDS%20VISBEST%20PO20%20POWDER%20FORM%202(1).jpg" /></p>

<p>&nbsp;</p>
', NULL, CAST(N'2015-12-28 20:27:31.287' AS DateTime), N'toanbn', CAST(N'2016-04-27 11:57:28.747' AS DateTime), NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, N'/Data/images/TDS%20VISBEST%20PO20%20POWDER%20FORM%202(1).jpg')
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (9, N'VIS - BEST® L - 20 Engine Oil VII ', N'vis-best®-l-20-engine-oil-vii', NULL, N'/Data/images/intro-product/hinh%20viscobest%20L20.png', 10, N'<p><img alt="" src="/Data/images/TDS%20of%20VIS%20BEST%20%20L20%20-%20NEW%20LOGO2.jpg" style="height:902px; width:635px" /></p>
<p><img alt="" src="/Data/images/TDS%20of%20VIS%20BEST%20%20L20%20--%20NEW%20LOGO1.jpg" style="height:902px; width:635px" /></p>
', NULL, CAST(N'2015-12-28 20:28:13.083' AS DateTime), N'toanbn', CAST(N'2016-02-23 21:14:33.220' AS DateTime), NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (11, N'VIS - BEST® V - 20 Bale/Block Form', N'vis-best-v-20-bale-block-form', NULL, N'/Data/images/intro-product/BPT%20logo%20VISBEST%20V20.JPG', 10, N'<p style="text-align:center"><img alt="" src="/Data/images/TDS%20of%20V20%20-%20file%20anh%201.jpg" style="height:891px; width:635px" /></p>

<p style="text-align:center"><img alt="" src="/Data/images/TDS%20VISBEST%20V20%20POWDER%20FORM%202.jpg" style="height:901px; width:635px" /></p>
', NULL, CAST(N'2015-12-28 20:31:22.473' AS DateTime), N'toanbn', CAST(N'2016-04-22 22:48:41.007' AS DateTime), NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (12, N'PPD P20', N'ppd-p20', NULL, N'/Data/images/intro-product/base-oils_20_1_t.jpg', 12, N'<p><img alt="" src="/Data/images/TDS%20of%20PPD%20NEW%20LOGO%20-%20%20EUIL.jpg" style="height:895px; width:635px" /></p>
', NULL, CAST(N'2016-01-16 17:53:13.757' AS DateTime), N'toanbn', CAST(N'2016-03-31 12:49:49.133' AS DateTime), NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, N'/Data/files/101%20LINQ%20Samples.zip')
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (13, N'Global Freights ', N'Global-Freights', NULL, NULL, 4, N'<p><strong>1/ UAE - Jebel Ali Port </strong><br />
<img alt="" src="http://bptchemicals.com/images/hinhanhtrinh/Jebel.jpg" style="display:block; margin-left:auto; margin-right:auto" /><br />
Jebel Ali is a deep port located in Jebel Ali Dubai, United Arab Emirates. Jebel Ali is the world&#39;s largest man-made harbor and the biggest port in the Middle East. Port Jebel Ali was constructed in the late 1970s to supplement the facilities at Port Rashid<br />
<br />
Jebel Ali port is one of DP World&#39;s flagship facilities and have been ranked as 9th in Top Container Port Worldwide having handled 7.62 million TEUs in 2005, which represents a 19% increase in throughput, over 2004. Jebel Ali Port was ranked 7th in the worlds largest ports in 2007.[4] Jebel Ali port is managed by state-owned Dubai Ports World.<br />
<br />
<strong>2/ India - Nhava Sheva Port</strong></p>

<p><img alt="" src="http://bptchemicals.com/images/hinhanhtrinh/nhavasheva.jpg" style="display:block; margin-left:auto; margin-right:auto" /><br />
The Port of Nhava Sheva is India&#39;s largest port handling almost half of the country&#39;s maritime traffic. The Port of Nhava Sheva, created to add capacity to Mumbai&#39;s port, is located on the west coast of India close to the country&#39;s commercial capital, Mumbai. DP World Nhava Sheva, India&#39;s first privately managed container terminal, is the company&#39;s flagship operation in the Subcontinent.<br />
<br />
Construction of Nhava Sheva port was originally planned in 1965. The original plan envisioned the port for use to import food grains, since India then faced a major food shortfall. Following the Green Revolution in India, India gained vast surplus food grain production capacity. So the plans for the port shifted focus to container traffic.<br />
<br />
<strong>3/ Germany - Hamburg Port</strong></p>

<p><img alt="" src="http://bptchemicals.com/images/hinhanhtrinh/hurmberg%20port1.jpg" style="display:block; margin-left:auto; margin-right:auto" /><br />
The Port of Hamburg is a port in Hamburg, Germany, on the river Elbe. The harbour is located 110 kilometres from the mouth of the Elbe into the North Sea.<br />
<br />
It is named Germany&#39;s &quot;Gateway to the World&quot; and is the largest port in Germany. In terms of TEU throughout, the port of Hamburg is the third-busiest port in Europe (after the ports of Rotterdam and Antwerp) and 15th-largest worldwide. In 2010, 7.895 million containers were handled in Hamburg.</p>

<p style="text-align:justify">The harbour covers an area of 73.99 km&sup2; (64.80 km&sup2; usable), of which 43.31 km&sup2; (34.12 km&sup2;) are land areas. The location is naturally advantaged by a branching Elbe, creating an ideal place for a port complex with warehousing and transshipment facilities. The extensive free port also enables toll-free shipping.<br />
<br />
<strong>4/ Colombia - Colombo Port</strong></p>

<p><img alt="" src="http://bptchemicals.com/images/hinhanhtrinh/colombo.jpg" style="display:block; margin-left:auto; margin-right:auto" /><br />
The Port of Colombo (known as Port of Kolomtota in the early 14th Century Kotte Kingdom) is the largest and busiest port in Sri Lanka as well as in South Asia. Located in Colombo, on the southwestern shores on the Kelani River, it serves as an important terminal in Asia due to its strategic location in the Indian Ocean. During the 1980s, the port underwent rapid modernization with the installation of Cranes, Gantries and other modern-day terminal requirements. Currently with a capacity of 4.1 million TEUs and a dredged depth of over 15 m (49 ft), the Colombo Harbour is one of the busiest ports in the world, and ranks among the top 35 ports. It is also one of the biggest artificial harbours in the world handling most of the country&#39;s foreign trade. It has an annual cargo tonnage of 30.9 million tons.<br />
<br />
<strong>5/USA - Houstan Port</strong></p>

<p><img alt="" src="http://bptchemicals.com/images/hinhanhtrinh/houston.jpg" style="display:block; margin-left:auto; margin-right:auto" /><br />
The Port of Houston is a port in Houston &mdash; the fourth-largest port in the United States. The Port is a 25-mile-long complex of diversified public and private facilities located a few hours&#39; sailing time from the Gulf of Mexico. It is the busiest port in the United States in terms of foreign tonnage, second-busiest in the United States in terms of overall tonnage, and thirteenth-busiest in the world. Though originally the port&#39;s terminals were primarily within the Houston city limits, the port has expanded to such a degree that today it has facilities in multiple communities in the surrounding area. In particular the port&#39;s busiest terminal, the Barbours Cut Terminal, is located in Morgan&#39;s Point.<br />
<br />
&nbsp;The Port of Houston is a cooperative entity consisting of both the port authority, which operates the major terminals along the Houston Ship Channel, and more than 150 private companies situated along Buffalo Bayou and Galveston Bay.<br />
<br />
<strong>6/ Italia - Genova Port</strong></p>

<p><img alt="" src="http://bptchemicals.com/images/hinhanhtrinh/genova.jpg" style="display:block; margin-left:auto; margin-right:auto" /><br />
The Port of Genoa is a major Italian seaport on the Mediterranean Sea. With a trade volume of 51.6 million tonnes, it is the busiest port of Italy by cargo tonnage and the second busiest in terms of twenty-foot equivalent units after the transshipment port of Gioia Tauro, with a trade volume of 1.85 million TEUs handled in 2011.<br />
<br />
The Port of Genoa covers an area of about 700 hectares of land and 500 hectares on water, stretching for over 22 kilometres along the coastline, with 47 km of maritime ways and 30 km of operative quays.<br />
<br />
<strong>7/ Ukraine - Odessa Port</strong></p>

<p><img alt="" src="http://bptchemicals.com/images/hinhanhtrinh/odessa.jpg" style="display:block; margin-left:auto; margin-right:auto" /><br />
The Port of Odessa is the largest Ukrainian seaport and one of the largest ports in the Black Sea basin, with a total annual traffic capacity of 40 million tonnes (15 million tonnes dry bulk and 25 million tonnes liquid bulk). Along with ports of Illichivsk and Yuzhny it serves as a major freight and passenger gateway to Ukraine.<br />
<br />
In 2007 the Port of Odessa handled 31,368,000 tonnes of cargo and 523,881 TEU&#39;s making it the busiest cargo and container port in Ukraine.<br />
&nbsp;<br />
The largest cargo volume was in 2002 when the port handled 13.2 million tons of dry cargo and 20.4 million tons of liquid products<br />
<br />
<strong>8/ Turkey - IZMIR Port</strong></p>

<p><img alt="" src="http://bptchemicals.com/images/hinhanhtrinh/izmir.jpg" style="display:block; margin-left:auto; margin-right:auto" /><br />
Izmir is situated at the east end of Izmir Bay. To reach the main facilities of the port of Izmir, a ship must enter Izmir Korfezi (inlet) from the Aegean Sea on a southeasterly course for about 20 nmi, then turn eastward through Izmir Bay for about 10 nmi.Izmir is Turkey&#39;s third largest port and has the best natural harbor. Traffic at the harbor is not congested; the ships transit the channel each day. Plenty ships can load/unload simultaneously, but Izmir is primarily a discharge port for tankers. The port is divided into several sections: outer harbor, middle harbor, inner harbor, and an explosives anchorage. The inner harbor has two primary port facilities: the old port which is sheltered by a breakwater and Alsancak.<br />
<br />
<strong>9/Spain - Valencia Port</strong></p>

<p><img alt="" src="http://bptchemicals.com/images/hinhanhtrinh/Valencia%20.jpg" style="display:block; margin-left:auto; margin-right:auto" /><br />
The Port of Valencia is the largest seaport in Spain and in the Mediterranean Sea basin, with an annual traffic capacity of around 57 million tonnes of cargo (2009) and 4,210,000 TEU (2010).<br />
<br />
The port is also an important employer in the area, with more than 15,000 employees who provide services to more than 7,500 ships every year.</p>
', NULL, CAST(N'2016-01-16 18:42:19.020' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 1, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (14, N'VI Modifiers OCP Liquid Form ', N'vi-modifiers-ocp-liquid-form', NULL, N'/Data/images/intro-product/liquid.PNG', 11, N'<p><img alt="" src="/Data/images/TDS%20of%20VII%20Liquid%20-%20new%20logo.png" style="height:898px; width:635px" /></p>
', NULL, CAST(N'2016-02-24 05:44:32.863' AS DateTime), N'toanbn', CAST(N'2016-02-28 12:48:20.227' AS DateTime), NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (17, N'Olefin Copolymer Crumb/Powder Form', N'olefin-copolymer-crumb-powder-form', NULL, N'/Data/images/intro-product/Photo%20of%20VIS-BEST%20V%2020%20in%20POWDER%20FORM.jpg', 11, N'<p style="text-align:center"><img alt="" src="/Data/images/TDS%20-%203POWDER%20%20CRUM%20FORM%20new%20logo.jpg" /></p>
', NULL, CAST(N'2016-03-18 22:47:57.393' AS DateTime), N'toanbn', CAST(N'2016-03-21 19:28:52.997' AS DateTime), NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (18, N'ExxonMobil Singapore Chemical Plant expansion in operation', N'exxonmobil-singapore-chemical-plant-expansion-in-operation', N'ExxonMobil’s Singapore Chemical Plant is now producing ethylene from the facility’s second world-scale steam cracker. The expansion is integrated with the existing petrochemical plant. Over the next few weeks, the petrochemical complex, powered by a 375-megawatt cogeneration plant, will increase production at its three polyethylene plants, two polypropylene plants, a specialty metallocene elastomers unit and the expanded oxo-alcohol and aromatics units.', N'/Data/images/news-event/exxon_resize.jpg', 5, N'<p style="text-align:justify"><img src="../images/exxon_resize.jpg" /></p>

<p style="text-align:justify"><strong>Friday, May 31, 2013</strong><br />
ExxonMobil&rsquo;s Singapore Chemical Plant is now producing ethylene from the facility&rsquo;s second world-scale steam cracker.</p>

<p style="text-align:justify">The expansion is integrated with the existing petrochemical plant. Over the next few weeks, the petrochemical complex, powered by a 375-megawatt cogeneration plant, will increase production at its three polyethylene plants, two polypropylene plants, a specialty metallocene elastomers unit and the expanded oxo-alcohol and aromatics units.</p>

<p style="text-align:justify">&quot;This expansion gives ExxonMobil unparalleled feedstock flexibility in the industry and positions the Singapore petrochemical complex well to serve growth markets from China to the Indian sub-continent and beyond,&quot; said Matthew Aguiar, chairman and managing director, ExxonMobil Asia Pacific Pte Ltd. &quot;We are committed to meeting the regional demand for petrochemical products and to contributing to Singapore&rsquo;s growth.&quot;</p>

<p style="text-align:justify">ExxonMobil completed construction of the expansion in December 2012 and is producing commercial grades of new products, such as specialty metallocene elastomers, for the first time in the Asia Pacific region. It also set an industry-leading record in construction safety with more than 83 million hours worked without an injury involving a lost day of work.</p>

<p style="text-align:justify">&quot;We successfully completed the commissioning of the steam cracker and we are now focused on ensuring that the plant operates safely and reliably,&quot; said Georges Grosliere, venture executive and manufacturing director of the Singapore Chemical Plant, ExxonMobil Chemical Company. &quot;The scale and complexity of this expansion project, which doubled the steam-cracking capacity, demanded a strong focus on safety, operational integrity and discipline.&quot;</p>

<p style="text-align:justify">ExxonMobil has operated in Singapore for 120 years and is one of Singapore&rsquo;s largest foreign manufacturing investors. The company has expanded refining and petrochemical production in Singapore to meet expected demand for transportation fuels and the chemicals used for plastics and other manufacturing across the Asia Pacific region.</p>

<p style="text-align:justify">About ExxonMobil Chemical Company</p>

<p style="text-align:justify">ExxonMobil Chemical Company is one of the world&rsquo;s premier petrochemical companies with manufacturing, technology, and marketing operations around the world. The company delivers a broad portfolio of products and solutions efficiently and responsibly, with a commitment to create outstanding customer and shareholder value. ExxonMobil Chemical Company endorses the principles of sustainable development, including the need to balance economic growth, social development and environmental considerations. To learn more, visit www.exxonmobilchemical.com.</p>
', NULL, CAST(N'2016-03-19 14:36:50.780' AS DateTime), N'toanbn', CAST(N'2016-03-19 14:44:14.197' AS DateTime), NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (19, N'U.S. Targets Iran’s Petrochemical Industry', N'us-targets-irans-petrochemical-industry', N'The Obama administration on Friday escalated efforts to isolate Iran economically, blacklisting Iranian companies in the petrochemical industry for the first time and punishing five businesses in four other countries for conspiring to evade American sanctions aimed at restricting Iranian oil sales and air transportation.', N'/Data/images/news-event/us-targets-irans-petrochemical-industry.jpg', 5, N'<p>The Obama administration on Friday escalated efforts to isolate Iran economically, blacklisting Iranian companies in the petrochemical industry for the first time and punishing five businesses in four other countries for conspiring to evade American sanctions aimed at restricting Iranian oil sales and air transportation.</p>

<p><img alt="" src="http://www.bptchemicals.com/images/news-events/us-targets-irans-petrochemical-industry.jpg" style="display:block; margin-left:auto; margin-right:auto" /><br />
The new steps came a day after the administration issued sanctions against a top aide of Iran&rsquo;s supreme leader, Ayatollah Ali Khamenei, and more than 50 other Iranian officials for what it called their efforts to repress dissent and free speech in Iran, where presidential elections are scheduled in two weeks.</p>

<p>Under the increasingly strict sanctions on Iran, the United States freezes any American assets of blacklisted companies and individuals and bans them from doing business with any American companies or citizens. In addition, companies and individuals in other countries who help Iran evade sanctions can be cut off from the American financial system and face other restrictions.</p>

<p>&ldquo;We are committed to intensifying the pressure against Iran, not only by adopting new sanctions, but also by actively enforcing our sanctions and preventing sanctions evasion,&rdquo; David S. Cohen, the Treasury Department under secretary whose office oversees the sanctions effort, said in a statement. &ldquo;Today&rsquo;s actions take aim at revenues from Iran&rsquo;s petrochemical sector, as well as deceptive schemes Iran has employed in an effort to evade sanctions on its oil sales and its airlines.&rdquo;</p>

<p>The United States has never before targeted Iran&rsquo;s petrochemicals industry, which Treasury officials said had become an important source of revenue to the Iranian government as sales of oil &mdash; its most important export &mdash; have fallen sharply in the past few years because of the effects of other sanctions, including a European Union embargo.</p>

<p>&quot;The administration is taking action today to target this revenue stream by both designating companies involved in transactions with the sector and identifying several petrochemical companies as subject to sanctions because they are controlled by the Iranian government,&rdquo; the Treasury statement said.</p>

<p>The blacklist announced Friday covers eight companies scattered throughout Iran, five of them in special economic zones designed in part to encourage foreign investment.</p>

<p>American and European Union sanctions on Iran are aimed largely at pressing the country to halt its uranium enrichment activities, which the Iranians say are peaceful but which Western governments suspect are a cover for achieving the ability to make nuclear weapons.</p>

<p>Negotiations over the uranium enrichment have stalled with little expectation of progress anytime soon, particularly with Iran&rsquo;s approaching elections. A leading presidential candidate, Saeed Jalili, the country&rsquo;s unyielding nuclear negotiator and a prot&eacute;g&eacute; of Ayatollah Khamenei, has vowed that Iran will never bend to Western economic pressure on the nuclear issue.</p>

<p>The non-Iranian companies targeted by the American sanctions include Ferland Company, which is based in Cyprus and Ukraine and is accused of helping Iran disguise an oil shipment to evade sanctions; and Kyrgyz Trans Avia of Kyrgyzstan, Ukrainian-Mediterranean Airlines and Bukovyna Airlines of Ukraine, and Sirjanco Trading L.L.C. of the United Arab Emirates, all accused of helping Iran procure aircraft used to move illicit cargo to the Syrian government in its war against a two-year-old insurgency. Iran is Syria&rsquo;s major regional ally in that conflict.</p>

<p>Despite rising inflation and a weakening currency caused in part by the sanctions, the Iranian government has said it is weathering the economic deprivations. On Friday, the deputy economic minister, Behrouz Alishiri, was quoted by the semiofficial Fars News Agency as saying said Iran enjoyed &ldquo;remarkable growth&rdquo; in foreign investment this past year.</p>
', NULL, CAST(N'2016-03-19 14:49:05.997' AS DateTime), N'toanbn', CAST(N'2016-03-19 14:50:43.727' AS DateTime), NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (20, N'When America Stops Importing Energy', N'when-america-stops-importing-energy', N'You’ve probably heard by now about the American energy revolution. Breakthroughs in drilling technology have opened access to enough new oil and gas reserves in the United States to dramatically reduce U.S. dependence on foreign oil.', N'/Data/images/news-event/when-america-stops-importing-energy.gif', 5, N'<p style="text-align:justify"><img src="http://graphics8.nytimes.com/images/2013/05/23/opinion/global/23iht-edbremmer23/23iht-edbremmer23-articleInline.gif" style="float:left; height:235px; margin-right:10px; width:190px" /> You&rsquo;ve probably heard by now about the American energy revolution. Breakthroughs in drilling technology have opened access to enough new oil and gas reserves in the United States to dramatically reduce U.S. dependence on foreign oil.<br />
The numbers tell the story: U.S. oil production has reversed its 30-plus year decline; U.S. imports from OPEC producers have fallen more than 20 percent in the past three years; U.S. natural gas reserves and production are up significantly and prices have dropped 75 percent in the past five years. The International Energy Agency forecasts that the United States could become the world&rsquo;s largest oil producer by 2020 and may be energy self-sufficient by 2035. That&rsquo;s a game changer.</p>

<p style="text-align:justify">While this is not a free lunch, it should not be feared. The production process is complicated and expensive, and if the industry is not careful there can be risks to the environment. But the potential is staggering. Significant domestic job growth and economic expansion has begun.</p>

<p style="text-align:justify">But let&rsquo;s look beyond the impact on the United States and consider a few of the more profound implications for the rest of the world, because this revolution is also a game changer for international politics and the global economy.</p>

<p style="text-align:justify">The axiom of &ldquo;resource scarcity&rdquo; has been one of the dominant forces shaping global geopolitics and economics since the end of World War II. Now, thanks to the U.S. oil and gas industry&rsquo;s technological and entrepreneurial savvy, we have ushered in an era in which &ldquo;resource abundance&rdquo; will be the norm. The technology will be used to turn the U.S. into an energy exporter and also unlock hidden reserves in other countries. The resulting surge in supply means that the global energy sector will begin to behave like a more &ldquo;normal&rdquo; market, one in which demand and supply are in better balance and less power is concentrated in the hands of select producers.<br />
<br />
Energy-rich countries like Russia, Saudi Arabia or Venezuela could be in serious trouble. Higher prices and market power have allowed their rulers to boost their domestic popularity with subsidies and other social spending projects, but when their customers produce more of their own energy or have other suppliers available, they will be forced to adapt quickly and intelligently or deal with the consequences.</p>

<p style="text-align:justify">The other great shift will come from a change in U.S. foreign policy. At the beginning of World War II, the United States supplied about 63 percent of the world&rsquo;s oil and Texas was the global supplier of last resort. The Arabian Peninsula, Iran and Iraq together produced less than 5 percent. But in 1960, new discoveries encouraged Saudi Arabia, Iran, Iraq, Kuwait and Venezuela to band together to form the Organization of the Petroleum Exporting Countries (OPEC). Over the next decade, Middle Eastern and North African countries ramped up production by 13 million barrels per day, and OPEC members began to claim a bigger share of profits from Western oil companies operating on their territory. Qatar, Indonesia, Algeria, Libya, the United Arab Emirates and Nigeria joined the cartel.</p>

<p style="text-align:justify">Then came the tipping point. In March 1971, Texas reached maximum productive capacity. Over the next five years, U.S. oil imports nearly doubled and other producers gained critical market power. The Yom Kippur War in October 1973 triggered the OPEC oil embargo and the market distortions we&rsquo;ve lived with ever since. That&rsquo;s when access to foreign oil became a central preoccupation of U.S. foreign and security policy.</p>

<p style="text-align:justify">Fast forward to 2013. As America drives toward a new era of energy self-sufficiency, Washington will be less willing to risk lives and spends billions on ensuring the free flow of oil and gas through dangerous places. That&rsquo;s especially important for the Middle East &mdash; a region where Ottomans, then Europeans, and lately Americans have, for better and for worse, helped keep the peace. The United States isn&rsquo;t about to abandon the region entirely, not with the global economy still so dependent on the flow of commerce through the Strait of Hormuz and Israel&rsquo;s security at risk. But it&rsquo;s natural that as America becomes less reliant on the Middle East for energy, Washington&rsquo;s willingness to accept risks and burdens there will diminish, or at least become harder to justify in a fiscally constrained era.</p>

<p style="text-align:justify">Interestingly, even as America becomes less vulnerable to that region&rsquo;s volatility, China will become more so &mdash; and more directly involved in its politics as a result. The International Energy Agency has forecast that China will import nearly 80 percent of its oil by 2030, and much of that crude will come from North Africa and the Middle East. Some estimates suggest that China holds larger shale gas deposits than even the United States, but for the moment it lacks the technology and know-how to exploit them.</p>

<p style="text-align:justify">Even if China gets up to speed quickly, it will take years for all the requisite infrastructure to be put in place and for its domestic oil and gas industry to mature to the point where it can deliver significant volumes year in and year out.</p>

<p style="text-align:justify">Thus as China&rsquo;s willingness to intervene in the politics of other countries rises, its leaders will want to extend their influence. And energy-rich Middle Eastern governments that have lost big customers in America and Europe will welcome all the deep-pocketed foreign friends they can find.</p>

<p style="text-align:justify">Like all revolutions, America&rsquo;s new energy bonanza raises some fascinating questions. How might a lighter U.S. presence and heavier Chinese involvement change the world&rsquo;s most volatile neighborhood? What can the next generation of Saudi leaders expect for their country&rsquo;s future in a world where OPEC has lost much of its market power? Will Qatar&rsquo;s support for Muslim Brotherhood governments in other Arab states and China&rsquo;s interest in using the United Arab Emirates as an offshore trading center for its currency leave the Saudis dangerously isolated? Can Iran&rsquo;s revolution survive the need to build a more modern economy?</p>

<p style="text-align:justify">A world in which the United States is less involved in answering these questions is a new world indeed.</p>

<p style="text-align:justify">Ian Bremmer is president of Eurasia Group and author of &ldquo;Every Nation for Itself: Winners and Losers in a G-Zero World.&rdquo; Kenneth A. Hersh is chief executive officer of NGP Energy Capital Management.</p>
', NULL, CAST(N'2016-03-19 14:52:32.473' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (21, N'Africa Energy Forum 2013', N'africa-energy-forum-2013', N'The Africa Energy Forum is the established meeting place for the African power sector. All the players in the industry are in the same place at the same time. As the International Finance Corporation noted in 2011 "AEF is the biggest and most interesting conference on African energy in the world.”
', N'/Data/images/news-event/africa-energy-forum-2013.jpg', 5, N'<p><strong>June 19, 2013 - June 21, 2013</strong><br />
<strong>Barcelona International Convention Centre (CCIB)</strong><br />
<strong>Barcelona</strong><br />
<strong>Spain</strong><br />
<br />
The Africa Energy Forum is the established meeting place for the African power sector. All the players in the industry are in the same place at the same time. As the International Finance Corporation noted in 2011 &quot;AEF is the biggest and most interesting conference on African energy in the world.&rdquo;</p>

<p><strong>Contact Information</strong><br />
Liz Owens<br />
EnergyNet Ltd<br />
Fulham Green<br />
London<br />
Phone: +44 (0)20 7384 7807<br />
http://www.energynet.co.uk</p>
', NULL, CAST(N'2016-03-19 14:54:36.007' AS DateTime), N'toanbn', CAST(N'2016-03-19 14:55:42.150' AS DateTime), NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (22, N'ADIPEC 2013', N'adipec-2013', N'Largest Gas and Oil Event for the Region.
10-13 November 2013 , Abu Dhabi National Exhibition Center, UAE
If, at any stage, you become concerned about your gambling behavior, you www.weberik.com can request one of the following.  ', N'/Data/images/news-event/adipec-2013.png', 5, N'<p><strong><strong><img src="http://www.oilandgaspages.com/images/exhibition/2013/14.png" style="height:149px; width:300px" /></strong></strong></p>

<p>Largest Gas and Oil Event for the Region</p>

<p>10-13 November 2013 , Abu Dhabi National Exhibition Center, UAE<br />
<br />
ADIPEC - the Abu Dhabi International Petroleum Exhibition and Conference - is the largest gas and oil event for the Middle East. At ADNEC, Abu Dhabi, UAE between 10 - 13 November 2013, ADIPEC is now an annual event that attracts more than 51,000 attendees, 1,600 international suppliers, NOCs, IOCs and IGOs across 38,500 square meters of net space ...... an event not to be missed!</p>
', NULL, CAST(N'2016-03-19 14:56:49.700' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (23, N'EPM V0010 has officially entered China market by launching in the Guangzhou ', N'epm-v0010-has-officially-entered-china-market-by-launching-in-the-guangzhou', N'23rd – 26th Sep 2013, BPT CHEMICALS CO., LTD successfully made a big amazing impression in The Interlubric 2013 held in Guangzhou China, well known as the special exhibition for petroleum chemicals in oils & lubricants industry, by launching new product named EPM V 0010 – the most advanced technology Viscosity Index Improver in bale form for lubricant additives.', N'/Data/images/news-event/DSCN2088.JPG', 5, N'<p><strong>23rd &ndash; 26th Sep 2013, BPT CHEMICALS CO., LTD successfully made a big amazing impression in The Interlubric 2013 held in Guangzhou China, well known as the special exhibition for petroleum chemicals in oils &amp; lubricants industry, by launching new product named EPM V 0010 &ndash; the most advanced technology Viscosity Index Improver in bale form for lubricant additives.</strong></p>

<p>EPM V 0010 is manufactured upon the world high-class innovative technology. Which take leading the worldwide market with the most competitive edge of low pour point VII - low SSI - high profit. Therefore it has been becoming the PERFECT replacement of old traditional raw materials (PARATONE 8900 and KELTAN 1200A, J0010, Lubrizol 7067...).</p>

<p>&ldquo;The Chinese market is extremely potential market to develop our products because we provide a perfect solution for lubricant additives by supplying Viscosity Index Improvers in Bale Form &amp; Liquid Form achieving the same quality as world &ndash; class brands but much lower costs, especially in nowadays economy difficulty situation. The exhibition is really a good chance for lubricant manufacturers to be offered the full technical advice, application instruction, benefit analysis, &hellip;Thus we have been receiving big number of positive feedbacks from clients accordingly. Upon this good signals, we are so confident to set sales target by 2014 upto 500 &ndash; 1000MT per month for only VII bale form&rdquo; said by Managing Director</p>

<p>As the most professional, authoritative and largest international exhibition in China lubricant industry, China International Lubricants and Technology Exhibition (Inter Lubric China) has received great support from organizers: SINOPEC Lubricant Company, PetroChina Lubricant Company, and Council for the Promotion of International Trade Shanghai, and co-organizer Shanghai Lubricant Trade Association. InterLubric China has been successfully held annually in Beijing, Guangzhou, Shanghai over the past 13 years, attracting over 1700 exhibitors, more than 60,000 professional attendees including manufactures, dealers, distributors and end users, with about 60 seminars and conferences in total. It is the wind vane of the China lubricants industry development and regarded as a must-attend annual event for lubricant professionals.</p>
', NULL, CAST(N'2016-03-19 14:58:56.907' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (24, N'Synthetic lubricant & base stock events in 2014', N'synthetic-lubricant-base-stock-events-in-2014', N'- 8th Asia-Pacific Base Oil, Lubricant & Grease Conference
- STLE Annual Meeting & Exhibition
- ACI European Base Oils & Lubricants Summit
-  ...', N'/Data/images/news-event/STLE%202014.jpg', 5, N'<table border="0">
	<tbody>
		<tr>
			<td><img alt="" src="../images/FL-Asia.gif" style="height:74px; width:213px" /></td>
			<td>
			<p><strong>8<sup>th</sup> Asia-Pacific Base Oil, Lubricant &amp; Grease Conference<br />
			Shangri La Hotel, Orange Grove Road, Singapore</strong><br />
			<strong>4 &ndash; 7 March 2014</strong></p>

			<p>F+L Week is the convergence of the 20th Annual Fuels &amp; Lubes Asia Conference and the 8th Asia-Pacific Base Oil, Lubricant &amp; Grease Conference, two of Asia&#39;s premier industry events. This event is the sounding board for the latest developments in base oils, greases, industrial and automotive lubricants , transportation fuels and petroleum additives.</p>

			<p>Visit Event Website : http://fuelsandlubes.com/conference/fl-week-2014-2</p>
			</td>
		</tr>
		<tr>
			<td colspan="2">
			<hr /></td>
		</tr>
		<tr>
			<td><img alt="" src="../images/STLE2.jpg" style="height:166px; width:233px" /></td>
			<td>
			<p><strong><a href="http://www.exxonmobilchemical.com/Chem-English-Micro/stle/default.aspx" target="_blank">STLE Annual Meeting &amp; Exhibition</a></strong></p>

			<p>Disney&rsquo;s Comteporary Resort, Buena Vista Lake, Florida, US<br />
			<em>May 18 &ndash; 22, 2013</em></p>

			<p>The five-day conference (Sunday-Thursday) showcases more than 300 technical papers, application- based case studies and best practice reports, and discussion panels on technical or market trends.<br />
			<br />
			Education courses support professional development needs and prepare people for one of STLE&#39;s four certification programs. Exhibits and commercial presentations spotlight the latest products and services of interest to lubrication professionals. Business networking is another invaluable part of the STLE meeting experience. For many people, their STLE relationships are the number one problem-solving resource that they use on the job every day. Typical attendance is 1300 people.</p>

			<p>Visit Event Website : http://www.stle.org/events/annual/details.aspx</p>
			</td>
		</tr>
		<tr>
			<td colspan="2">
			<hr /></td>
		</tr>
		<tr>
			<td><img alt="" src="../images/ACI.jpg" style="height:73px; width:241px" /></td>
			<td>
			<p><strong>ACI European Base Oils &amp; Lubricants Summit</strong><br />
			<em>17 &ndash; 18 Sep 2014</em></p>

			<p>Following the recent success of this year&rsquo;s event, we are delighted to announce that ACI&rsquo;s European Base Oils &amp; Lubricants Summit will take place on 17-18 September 2014 in Europe. Now on its 6<sup>th</sup> year, this platform will reach all key base oils and lubricants players from established Western Europe and US markets together with companies from emerging markets including Central and Eastern Europe, India and the Middle East.</p>

			<p>Visit event website: http://www.wplgroup.com/aci/conferences/eu-ebl6.asp</p>
			</td>
		</tr>
		<tr>
			<td colspan="2">
			<hr /></td>
		</tr>
		<tr>
			<td><img alt="" src="../images/images.jpg" style="height:175px; width:175px" /></td>
			<td>
			<p><strong><a href="https://www.exxonmobilchemical.com/Chem-English-Micro/inter-lubric/default.aspx" target="_blank">Inter Lubric China</a> </strong></p>

			<p>China International Exhibition Center (CIEC)<br />
			20 &ndash; 22 Nov 2014</p>

			<p><strong>The 15th China International Lubricants and Technology Exhibition (Inter Lubric China 2014)</strong>, a playform that showcase the direction of <strong>Lube industry</strong>&rsquo;s development and it is a must &ndash;attend trade show for <strong>lubricant </strong>professionals, will be staged at <strong>China International Exhibition Center (CIEC) </strong>during Nov.20-22, 2014.</p>

			<p>As the largest and most professional international <strong>lubricant industry exhibition</strong> in China, <strong>the 15th China International Lubricants and Technology Exhibition (Inter Lubric China 2014)</strong> has received great support from Sinopec Lubricant Company, PetroChina Lubricant Company, <strong>CCPIT</strong> Shanghai, Shanghai <strong>Lubricant Trade</strong> Association.</p>

			<p>Visit event website : <a href="http://www.interlubric.com">http://www.interlubric.com</a></p>
			</td>
		</tr>
		<tr>
			<td colspan="2">
			<hr /></td>
		</tr>
		<tr>
			<td><img alt="" src="../images/aea-logo-en_60x60.jpg" style="height:143px; width:143px" /></td>
			<td>
			<p><strong>AEA Symposium on Lubes and Additives</strong></p>

			<p>S&atilde;o Paulo, Brazil &ndash; Oct 24, 2014<br />
			<em>Knowledge and Technology. Competitiveness and productivity. Autonomy and impartiality. For AEA, these are the drives for the development of automotive engineering in Brazil. Much more than a centre of automotive engineering, the AEA is an entity that seeks to gather and use the various thoughts and ideas to provide solutions that drive not only automotive engineering, but also Brazil as a whole. To do this, we must always be in motion, seeking solutions increasingly modern and applicable . And that&rsquo;s what AEA have been doing.</em><br />
			<a href="http://aea.org.br/v1/vi-lubricants-additives-and-fluids-international-symposium/" target="_blank">Visit event website</a> : http://aea.org.br/v1/english/</p>
			</td>
		</tr>
		<tr>
			<td colspan="2">
			<hr /></td>
		</tr>
		<tr>
			<td><img alt="" src="../images/12.jpg" style="height:149px; width:149px" /></td>
			<td>
			<p><strong>ILMA Annual Meeting</strong></p>

			<p>October 18-21, 2014<br />
			<a href="http://www.indianwells.hyatt.com/hyatt/hotels-indianwells/index.jsp?null" target="_blank">Hyatt Regency Indian Wells Resort &amp; Spa</a><br />
			Indian Wells, CA</p>

			<p>The Independent Lubricant Manufacturers Association (ILMA) was established in 1948 in order to meet the needs of its <a href="http://www.ilma.org/about/membercompanies.cfm">Members</a> by providing advocacy, networking, and a collaborative effort to succeed in today&#39;s business world. The trade association is the principal <a href="http://www.ilma.org/resources/index.cfm">voice</a> for the industry before Congress, federal regulatory agencies and other industry groups. Through its enforceable <a href="http://www.ilma.org/about/codeofethics.cfm">Code of Ethics</a>, ILMA promotes integrity and quality in lubricant manufacturing and marketing.</p>

			<p><a href="http://www.ilma.org/events/annualmtg13/" target="_blank">Visit event website</a> : www.ilma.org</p>
			</td>
		</tr>
		<tr>
			<td colspan="2">
			<hr /></td>
		</tr>
		<tr>
			<td><img alt="" src="../images/795067028437b026b4c291.gif" style="height:135px; width:146px" /></td>
			<td>
			<p><strong>Lubricants Russia</strong><br />
			Renaissance Hotel, Moscow, Russia<br />
			<em>Nov 11-14, 2014</em></p>

			<p>Each year the most influential players and decision makers of the International lubricants industry meet at the International Lubricants Russia Conference in Moscow. We invite you to join them at this event, now in its 9th year.</p>

			<p>LUBRICANTS RUSSIA has a reputation as the leading conference in the Russian lubricants industry. The caliber of the attendees and the Sponsors, coupled with excellent networking opportunities, unrivalled evening receptions, and a high-level program have earned LUBRICANTS RUSSIA the reputation it deserves.</p>
			</td>
		</tr>
	</tbody>
</table>
', NULL, CAST(N'2016-03-19 15:01:25.213' AS DateTime), N'toanbn', CAST(N'2016-03-19 15:04:55.660' AS DateTime), NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (25, N'  USW blasts Tesoro over lack of refinery safety', N'usw-blasts-tesoro-over-lack-of-refinery-safety', N'The United Steelworkers (USW) has demanded that Tesoro Corp. develop a comprehensive, cohesive safety program after a sulfuric acid release at the alkylation unit of its 161,000-b/d Golden Eagle refinery near Martinez, Calif., in early February seriously injured two workers.', N'/Data/images/news-event/oil1.jpg', 5, N'<p style="text-align:justify">The United Steelworkers (USW) has demanded that Tesoro Corp. develop a comprehensive, cohesive safety program after a sulfuric acid release at the alkylation unit of its 161,000-b/d Golden Eagle refinery near Martinez, Calif., in early February seriously injured two workers.</p>

<p style="text-align:justify">&ldquo;Tesoro management trivialized the extent of the workers&rsquo; injuries to establish jurisdictional defense specifically to avoid the scrutiny of US Chemical Safety Board (CSB) and other agencies,&rdquo; USW Vice-Pres. Gary Beevers said in a Feb. 24 release.<br />
USW also applauded the California Division of Occupational Safety and Health (Cal/OSHA) for prohibiting Tesoro from restarting Golden Eagle&rsquo;s alkylation unit following the Feb. 12 incident until management meets certain conditions.</p>

<p style="text-align:justify">According to USW, refinery operators told Cal/OSHA investigators that they were afraid to operate the alkylation unit at the Martinez refinery and signed &ldquo;green sheets&rdquo; with the notation &ldquo;signed under duress&rdquo; for procedure changes.</p>

<p style="text-align:justify">Operators also informed investigators assigned to the case that Tesoro failed to conduct required management of organizational changes when they decided to reduce staffing for start-up and shutdown of the alkylation unit, USW said.</p>

<p style="text-align:justify"><br />
&ldquo;While the company continues to grow and its market share expands, Tesoro&rsquo;s corporate culture of safety has steadily diminished,&rdquo; Beevers said, noting that the refiner has withdrawn from USW&rsquo;s Triangle of Prevention program&mdash;which supports incident investigations&mdash;and stopped its quest for inclusion in OSHA&#39;s Voluntary Protection Program.</p>

<p style="text-align:justify">The union went on to criticize Tesoro for disputing a CSB report that cited deficient corporate-wide management culture of safety as a contributor to an explosion that killed seven workers in April 2010 at its Anacortes, Wash., refinery (OGJ Online, Apr. 5, 2010).aqaa</p>
', NULL, CAST(N'2016-03-19 15:12:01.727' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (26, N'18 – 22nd May 2014, BPT Chemicals Global was successfully organized their exhibition activity in STLE 69th Annual Meeting held in Buena Vista Lake, Orlando Florida', N'18-22nd-may-2014-bpt-chemicals-global-was-successfully-organized-their-exhibition-activity-in-stle-69th-annual-meeting-held-in-buena-vista-lake-orlando-florida', N'In the beautiful cool weather in Florida, many big giants in Oils, Lubricants & Biotribology Industry gathered together to share their most advanced technologies and latest products, showcased more than 300 technical papers, application-based case studies, best practice reports and discussion panels on technical and market trends along with huge exhibition hall with 88 exhibitors.', N'/Data/images/news-event/STLE%201.JPG', 5, N'<p><strong>In the beautiful cool weather in Florida, many big giants in Oils, Lubricants &amp; Biotribology Industry gathered together to share their most advanced technologies and latest products, showcased more than 300 technical papers, application-based case studies, best practice reports and discussion panels on technical and market trends along with huge exhibition hall with 88 exhibitors.</strong></p>
<p>As President of STLE – Mr Rob Heverly said : “ STLE 69th in this year is the biggest conference in modern history with more than 1,400 of our peers from lubrication – engineering and tribology - research communities” and “ Annual Exhibition Booth and Commercial Marketing Forum are sold out quickly for 6 month ago”</p>
<p>To be one of world-top exhibitors in this community is a great chance and honor for BPT CHEMICALS GLOBAL to introduce their outstanding technologies in Lubricant Additives in which their unique VISCOBEST brands ranking as the first &amp; the most innovative technology in Viscosity Index Improver Manufacturing.</p>
<p>“ Meanwhile we are also give detailed presentation to many Lubricant Manufacturers and International Traders about the difference of VISCOBEST VII against from Traditional Olefin Copolymer VII with real small samples for attendees if requested” happily shared by Mr David Le – Director of BPT Chemicals Co., Ltd.</p>
<p>VISCOBEST VII is famous as OCP &amp; PAMA Grafting Technology in Viscosity Index Improver, available for bale &amp; liquid form, allow no need to apply PPD at finished lubricants but still can get the excellent SSI ( 0 – 35 ) and stable Viscosity Index even in any cold weather markets such as US, North America, Canada, Ukraina &amp; Russia.</p>
<p>The STLE Annual Meeting at the Omni Hotel, May 17-21, 2015. will be held in Dallas, Texas (USA).</p>
<p><strong><em>For more information or photo of this event, please kindle visit : <a href="http://www.stle.org/" target="_blank"><span style="color: #0000ff;">www.stle.org</span></a> and <span style="color: #0000ff;"><a href="http://www.bptchemicals.com/" target="_blank"><span style="color: #0000ff;">www.bptchemicals.com</span></a></span></em></strong></p>
<p>****************************************************************************************************************************************</p>
<p><img src="../images/STLE 1.JPG" border="0" alt="" style="display: block; margin-left: auto; margin-right: auto;" /></p>
<p><img src="../images/STLE 2.JPG" border="0" alt="" style="display: block; margin-left: auto; margin-right: auto;" /></p>
<p><img src="../images/STLE 3.JPG" border="0" alt="" style="display: block; margin-left: auto; margin-right: auto;" /></p>
<p><img src="../images/STLE400.JPG" border="0" alt="" style="display: block; margin-left: auto; margin-right: auto;" /></p>
<p><img src="../images/STLE 5.JPG" border="0" alt="" style="display: block; margin-left: auto; margin-right: auto;" /></p>
<p><img src="../images/STLE 6.JPG" border="0" alt="" style="display: block; margin-left: auto; margin-right: auto;" /></p>
<p><img src="../images/STLE 7.JPG" border="0" alt="" style="display: block; margin-left: auto; margin-right: auto;" /></p>
<p><img src="../images/STLE 8.JPG" border="0" alt="" style="display: block; margin-left: auto; margin-right: auto;" /></p>', NULL, CAST(N'2016-03-19 15:14:09.403' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (27, N'The 8th ICIS Asian Base Oils & Lubricants Conference Held Marina Bay Sands, Singapore On 25 – 26th June 2014', N'the-8th-icis-asian-base-oils-lubricants-conference-held-marina-bay-sands-singapore-on-25-26th-june-2014', N'The conference is considered as one of the biggest conferences in Asia for Base Oils &amp; Lubricants Industry and will be organized in Singapore “The New Dragon of Asia Economy” upon the appraisal of Economists.', N'/Data/images/news-event/ICIS%20Sing.jpg', 5, N'
<p>The conference is considered as one of the biggest conferences in Asia for Base Oils &amp; Lubricants Industry and will be organized in Singapore “The New Dragon of Asia Economy” upon the appraisal of Economists.</p>
<p>Especially on 24th June, there will be an extremely important Pre – Conference Seminar focus on China huge market to provide the participants the opportunity to look in depth at what is influencing the second largest global automotive lubricant market.</p>
<p>Asia remains a critical geography for the base oils and lubricants industry. This conference will explore the supply and demand balances in the region, and highlight the growth products and new market opportunities. As new production comes on stream in other regions, the big question in 2014 is - how will the new supply impact Asia?</p>
<p>Thus, the first time in Asia launching the latest technology in manufacturing Viscosity Index Improver ( VII ) well known as OCP &amp; PAMA Grafting Copolymers under brand VIS - BEST® by BPT CHEMICALS GLOBAL. This technology challenges the traditional OCP technology in which VIS – BEST VII allows no need to apply any PPD into finished lubricants but still can reach the excellent SSI with stable Viscosity Index even in any low temperature performance. This technology has been becoming the best choice for US, EUROPE, Canada, Ukraina, Russia and North China.</p>
<p>“In 2013 the event was attended by more than 340 base oils and lubricants professionals from across the world and to be expected enlarging double in this year” said by President of BPT Chemicals Global.</p>
<p>BPT CHEMICALS GLOBAL has head office located in UAE and main factory in Asia, specialized in manufacturing Lubricant Additives ( Viscosity Index Improver ) with full package of VII from pellet form to bale/block form and liquid form. For more information, please visit our website www.bptchemicals.com or email directly to Ms Carol Zhang for arranging direct meeting and technical discussion with Director in this event : carol.zhang@bptchemicals.com</p>', NULL, CAST(N'2016-03-19 15:17:46.697' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (28, N'ICIS 4th Meet The South America Market Conference ( ICIS SA ) held in Rio De Janeiro, Brazil', N'icis-4th-meet-the-south-america-market-conference-icis-sa-held-in-rio-de-janeiro-brazil', N'On 28 – 30th May, in the hustle &amp; bustle atmosphere of World Cup 2014, there are many big events starting in Brazil - the biggest country in South America. As per Mr Gustavo Zamboni – Director of Agencia Virtual Ltda “ ,…”', N'/Data/images/news-event/STLE%201.JPG', 5, N'<p>On 28 &ndash; 30th May, in the hustle &amp; bustle atmosphere of World Cup 2014, there are many big events starting in Brazil - the biggest country in South America. As per Mr Gustavo Zamboni &ndash; Director of Agencia Virtual Ltda &ldquo; This year ICIS SA is considered as the largest event in Oils &amp; Lubricants in South America with the attendance of almost professionals, experts, government representatives with over 350 delegates from the most famous giants in Base Oils &amp; Lubricants worldwide such as Exxon Mobil, Dow, Nestle Oil, Chevron, Ipiranga, Petrobras, Cosan, NYNAS, Shell, Castrol, Evonik, Infenium,&hellip;&rdquo;</p>

<p>Along with huge exhibition sponsors like Exxon Mobil, Dow,&hellip; BPT CHEMICALS GLOBAL also gave big impression as by presenting the latest technology &ndash; OCP &amp; PAMA Grafting Copolymers with exclusive sample illustration.</p>

<p>This technology challenges the traditional OCP technology in the brand name VIS &ndash; BEST VII ( Block &amp; Liquid Form ) allows no need to apply any PPD into finished lubricants but still can reach the excellent SSI ( 0 &ndash; 35) with stable Viscosity Index even in any low temperature performance. This technology has been becoming the best choice for US, EUROPE, Canada, Ukraina, Russia and North China.</p>

<p>OCP &amp; PAMA Grafting Technology created the break-through milestone in Viscosity Index Improver Industry and attracted many attentions from Lubricant Manufacturers especially Brazil Lubricant Association immediately invites us to join their Annual Meeting held in Sao Paulo on May 2015.</p>

<p>Others, BPT CHEMICALS GLOBAL also introduced VII in Pellet form which being now exported to Venezuela stably and going to enter Chile, Brazil,&hellip;market within this year.</p>

<p>BPT CHEMICALS GLOBAL has head office located in UAE and main factory in Asia, specialized in manufacturing Lubricant Additives ( Viscosity Index Improver ) with full package of VII from pellet form to bale/block form and liquid form. For more information, please visit our website: www.bptchemicals.com</p>

<p><img alt="" src="../images/news-events/icis-4th/1513733_1509598245925765_1347518396874264231_n.jpg" style="display:block; margin-left:auto; margin-right:auto" /></p>

<p><img src="../images/news-events/icis-4th/DSCN2088.JPG" style="display:block; margin-left:auto; margin-right:auto; width:550px" /></p>

<p><img src="../images/news-events/icis-4th/DSCN2090.JPG" style="display:block; margin-left:auto; margin-right:auto; width:550px" /></p>

<p><img src="../images/news-events/icis-4th/DSCN2092.JPG" style="display:block; margin-left:auto; margin-right:auto; width:550px" /></p>
', NULL, CAST(N'2016-03-19 15:23:01.813' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (29, N'BPT Chemicals Global - Key Sponsor For UEIL Congress held in Madrid Spain from 22 - 24th Oct', N'bpt-chemicals-global-key-sponsor-for-ueil-congress-held-in-madrid-spain-from-22-24th-oct', N'Union of The European Lubricants Industry ( UEIL Congress 2014 ) Madrid Spain, 22nd – 24th Oct 2014.
Attracting over 250 key industry players from Europe and beyond, the Annual Congress of the European Lubricants Industry offers sponsors a unique opportunity to reach out and communicate with the decision makers in the Lubricants Industry.', N'/Data/images/news-event/bpt-chemicals-global-key-sponsor-for-ueil-congress-held-in-madrid-spain-from-22-24th-oct-1.jpg', 5, N'

<p align="center"><strong>Union of The European Lubricants Industry ( UEIL Congress 2014 ) </strong><strong><em>Madrid Spain, 22<sup>nd</sup> – 24<sup>th</sup> Oct 2014.</em></strong></p>
<p align="center"><strong>The Biggest Congress for Lubricant Industry in European Markets.</strong></p>
<p align="center"><strong>Main Prestigious Sponsors:</strong></p>
<p align="center"><a href="http://www.ueil.org/en/EVENTS/Congress-2014/Sponsorship/">http://www.ueil.org/en/EVENTS/Congress-2014/Sponsorship/</a></p>
<p align="center"><img src="http://www.bptchemicals.com/images/news-events/bpt-chemicals-global-key-sponsor-for-ueil-congress-held-in-madrid-spain-from-22-24th-oct-1.jpg" border="0" alt="" /></p>
<p><strong><em>Attracting over 250 key industry players from Europe and beyond, the Annual Congress of the European Lubricants Industry offers sponsors a unique opportunity to reach out and communicate with the decision makers in the Lubricants Industry.</em></strong></p>
<p><strong><em>BPT CHEMICALS GLOBAL is so proud to be along with AFTON, CHEVRON, ERGON, ILMA, REPSOL,…being  one of main Sponsors of this event to bring the most advanced technology in Lubricant Additive Industry well – known as OCP &amp; PAMA Grafting Technology to launch out VIS – BEST V 20 ( Drum &amp; Bale Form ) &amp; VIS – BEST L 20 ( liquid form ):</em></strong><strong><em> </em></strong></p>
<p><strong>√ Easily formulate with multi – grade engine oils, transmission fluids, hydraulic fluids, gear oils &amp; other industry lubricants.</strong></p>
<p><strong>√ Specially, no need to use more PPD into finished lubricants </strong></p>
<p><strong>√ Still can get excellent SSI, great pour point ( - 30 to - 40 degree ), </strong></p>
<p><strong>√ Perform perfectly in any low temperature conditions, most suitable for cold-weather markets East Europe ( Russia , Ukraine, Canada,, US &amp; Europe,..) </strong></p>
<p><strong> </strong></p>
<p><strong>With head quarter in Middle East, sales representative offices in Viet Nam &amp; Australia with the great cooperation of large distributor networks worldwide, BPT CHEMICALS GLOBAL promises to supply the best quality Viscosity Index Improvers in full package of pellet / granular form, bale / block form &amp; liquid form at the most competitive costs. </strong></p>
<p><strong> </strong></p>
<p><strong>“ Breakthrough in </strong><strong>Passion to be different in </strong><strong>T</strong><strong>hinking” leads us unstoppable innovation by launching many latest technologies in lubricant specialist groups such as ILMA, UEIL, ICIS, NLGI,… to meet any Customers’ Demand through our large professional Distributor Networks :</strong></p>
<p><img src="http://www.bptchemicals.com/images/news-events/bpt-chemicals-global-key-sponsor-for-ueil-congress-held-in-madrid-spain-from-22-24th-oct-2.png" border="0" width="680" /></p>', NULL, CAST(N'2016-03-19 15:26:15.177' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (30, N'Annual Client Visit in Europe as scheduled Belgium & Netherlands ( 20 - 21st Oct ), Spain ( 22 - 25th Oct ), Switzerland ( 26 - 27th Oct ), Germany ( 28-29th Oct ), France ( 30-31st Oct )', N'annual-client-visit-in-europe-as-scheduled-belgium-netherlands-20-21st-oct-spain-22-25th-oct-switzerland-26-27th-oct-germany-28-29th-oct-france-30-31st-oct', N'No Need To cut/Chop/Shred/Masticate
Fast Dissolving Capability
Dissolving Process At Extremely Low Temperature
Environmental - Friendly Use', N'/Data/images/news-event/Annual-Client-Visit-Plan.jpg', 5, N'<p><img alt="" src="http://www.bptchemicals.com/images/news-events/Annual-Client-Visit-Plan.jpg" style="display:block; margin-left:auto; margin-right:auto" /></p>
', NULL, CAST(N'2016-03-19 15:28:20.510' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (31, N'OCP & PAMA GRAFTING TECHNOLOGY THE MOST ADVANCED VISCOSITY INDEX ', N'ocp-pama-grafting-technology-the-most-advanced-viscosity-index', N'Excellent shear stability
Challenge Any Low Temperature markets like: EU, US, Ukraine, Russia, Canada.....
No Need To Apply Any PPD
Superior thickening Efficiency For formulating any Oils & Lubricants', N'/Data/images/news-event/OCP--PAMA-GRAFTING-TECHNOLOGY.jpg', 5, N'<p><img alt="" src="http://www.bptchemicals.com/images/news-events/OCP--PAMA-GRAFTING-TECHNOLOGY.jpg" /></p>
', NULL, CAST(N'2016-03-19 15:29:49.580' AS DateTime), N'toanbn', CAST(N'2016-03-19 15:30:00.473' AS DateTime), NULL, NULL, NULL, 0, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (32, N'Union Of The European Lubricants Industry (UEIL Congress in 2014)', N'union-of-the-european-lubricants-industry-ueil-congress-in-2014', N'Union of the Europe Lubricant Industry in Marid Spain .... ', N'/Data/images/news-event/IMG_0156.JPG', 5, N'<hr id="system-readmore" />
<p style="text-align: center;" align="center"><span style="color: #0000ff; font-size: medium;"><strong>Union of The European Lubricants Industry ( UEIL Congress 2014 )  </strong></span></p>
<p style="text-align: center;" align="center"><span style="color: #0000ff; font-size: medium;"><span style="color: #ff0000;"><strong>Madrid Spain, 22<sup>nd</sup> – 24<sup>th</sup> Oct 2014.</strong></span></span></p>
<p style="text-align: center;" align="center"><span style="color: #0000ff; font-size: medium;"><strong>The Biggest &amp; Most Premium Congress for Lubricant Industry in European Markets.</strong></span></p>
<p style="text-align: center;" align="center"><strong>Main Prestigious Sponsors:</strong> <a href="http://www.ueil.org/en/EVENTS/Congress-2014/Sponsorship/">http://www.ueil.org/en/EVENTS/Congress-2014/Sponsorship/</a></p>
<p style="text-align: center;" align="center"><span style="text-decoration: underline;"><span style="color: #0000ff; text-decoration: underline;">http://www.ueil.org/en/EVENTS/Congress-2014/Photos/</span></span></p>
<p align="center"><img src="http://bptchemicals.com/images/Events/UEIL/EUIL%20khc%20di.png" border="0" width="650" /></p>
<p align="center"><img src="http://bptchemicals.com/images/Events/UEIL/Union%20of%20The%20European%20Lubricants%20Industry-page0002%20new.jpg" border="0" width="650" /></p>
<p style="text-align: left;" align="center"><span style="text-decoration: underline; color: #0000ff;"><strong><em>Here are some pictures of our Directors in UEIL Congress 2014:</em></strong></span></p>
<p style="text-align: left;" align="center"><span style="text-decoration: underline; color: #0000ff;"><strong><em><img src="http://bptchemicals.com/images/Events/UEIL/IMG_0156.JPG" border="0" width="450" /></em></strong></span></p>
<p style="text-align: left;" align="center"> </p>
<p style="text-align: left;" align="center"><img src="http://bptchemicals.com/images/Events/UEIL/IMG_0148.JPG" border="0" width="450" /></p>
', NULL, CAST(N'2016-03-19 15:33:21.777' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (33, N'  Warmly Welcome To Our Booth B049 At INTERLUBRIC 2014 Held In Beijing From 20 - 22nd Nov', N'warmly-welcome-to-our-booth-b049-at-interlubric-2014-held-in-beijing-from-20-22nd-nov', N'The 15th China International Lubricants and Technology Exhibition (Inter Lubric China 2014), a playform that showcase the direction of Lube industry’s development.....', N'/Data/images/news-event/IMG_0591.JPG', 5, N'<p><span><strong>The 15th China International Lubricants and Technology Exhibition - Inter Lubric China 2014</strong></span></p>
<p>The 15th China International Lubricants and Technology Exhibition (Inter Lubric China 2014), a playform that showcase the direction of Lube industry’s development and it is a must –attend trade show for lubricant professionals, will be staged at China International Exhibition Center (CIEC) during Nov.20-22, 2014.</p>
<p> <img src="http://bptchemicals.com/images/Events/CHINA/IMG_0591.JPG" border="0" width="550" height="413" style="display: block; margin-left: auto; margin-right: auto;" /></p>
<p style="text-align: center;"><em>Picture No.1: Our Directors &amp; Partners in Exhibition in China</em></p>
<p>As the largest and most professional international lubricant industry exhibition in China, the 15th China International Lubricants and Technology Exhibition (Inter Lubric China 2014) has received great support from Sinopec Lubricant Company, PetroChina Lubricant Company, CCPIT Shanghai, Shanghai Lubricant Trade Association.</p>
<p><strong><em>In Inter Lubric China 2014, you will see:</em></strong></p>
<p>* New competition structure for lube enterprises in China</p>
<p>* Latest lubricant additive product launch from China and abroad</p>
<p>* New economical base oil products through technological innovation.</p>
<p>* Brake oil development in China with safety and standard requirement</p>
<p>* Engine oil upgrading with engine development trend</p>
<p>* Transformer oil performance progress by different base oil application</p>
<p>* Mental work fluids future development by metal working improvement</p>
<p>For more information, please kindly visit : <a href="http://www.interlubric.com/en/">http://www.interlubric.com/en/</a></p>
<p><img src="http://bptchemicals.com/images/Events/CHINA/IMG_0571.JPG" border="0" width="550" style="display: block; margin-left: auto; margin-right: auto;" /></p>
<p style="text-align: center;"><em>Picture No.2: Our Booth in China Exhibition</em></p>
<p style="text-align: center;"><em><img src="http://bptchemicals.com/images/Events/CHINA/IMG_0565.JPG" border="0" width="550" /></em></p>
<p style="text-align: center;"> </p>
<p style="text-align: center;"><em>Picture No.3: Visiting our Booth in China</em></p>
<p style="text-align: center;"><em><span>***********************************************************************************************************************************</span></em></p>', NULL, CAST(N'2016-03-19 15:35:30.093' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (34, N'ILMA (The Independent Lubricant Manufacturers Association ) Management Forum Will Be Held In Arizona US on 23 – 25th Apr 2015', N'ilma-the-independent-lubricant-manufacturers-association-management-forum-will-be-held-in-arizona-us-on-23-25th-apr-2015', N'The Independent Lubricant Manufacturers Association (ILMA) was established in 1948 in order to meet the needs of its Members by providing advocacy, networking, and a collaborative effort to succeed in today''s business world. The trade association is the principal voice for the industry before Congress, federal regulatory agencies and other industry groups. Through its enforceable Code of Ethics, ILMA promotes integrity and quality in lubricant manufacturing and marketing.
', N'/Data/images/news-event/12.jpg', 5, N'<p><img src="http://www.mediafire.com/convkey/de85/cj3glaxgba3s0l86g.jpg" style="height:187px; width:600px" /></p>

<p style="text-align:center"><strong><a href="https://www.ilma.org/events/mgmtforum15/sponsorship/thankyou.cfm">https://www.ilma.org/events/mgmtforum15/sponsorship/thankyou.cfm</a> </strong></p>

<p><span style="color:#ff0000"><strong>The Independent Lubricant Manufacturers Association (ILMA)</strong></span> was established in 1948 in order to meet the needs of its Members by providing advocacy, networking, and a collaborative effort to succeed in today&#39;s business world. The trade association is the principal voice for the industry before Congress, federal regulatory agencies and other industry groups. Through its enforceable Code of Ethics, ILMA promotes integrity and quality in lubricant manufacturing and marketing.</p>

<p>ILMA serves its members through:</p>

<ul>
	<li>Industry and Association Committees</li>
	<li>Meetings</li>
	<li>Publications</li>
</ul>

<p>ILMA is guided by its Strategic Plan, which reflects the needs of the ILMA members today. After 15 years of utilizing a &#39;Long Range Plan,&#39; in 2000 ILMA developed its first facilitated Strategic Plan with the guidance of Smock-Sterling, Strategic Management Consultants. A second plan was created in 2004, and now a new plan is being implemented this year. Click here to see the new ILMA Strategic Plan.</p>

<p>We measure success by the value received by our members and, ultimately, by our members&#39; continuing financial success.</p>

<p><em><strong>Who Are Our Members?</strong></em></p>

<p>Manufacturing Members are independent lubricant companies that produce over 25% of all lubricants and 80% or more of the metalworking fluids and other specialty industrial lubricants sold in North America.</p>

<p>International Members are independent companies manufacturing lubricants outside North America.</p>

<p>ILMA&#39;s membership also includes Supplier Member companies that are key suppliers of the independent lubricant manufacturing industry and Marketing Member companies which market and manufacture lubricants, own and operate a lubricant storage facility and maintain a finished product inventory or utilize a toll blender to do so, but which do not meet Manufacturing Member criteria.For information about membership click here, or email <a href="mailto:ilma@ilma.org">ilma@ilma.org</a>.</p>

<p>&nbsp;</p>
', NULL, CAST(N'2016-03-19 15:37:19.810' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (35, N'SEMIPETRO FAIR OF LUBRICANT 2015 WILL BE HELD IN SAO PAULO FROM 19 – 20TH MAY 2015', N'semipetro-fair-of-lubricant-2015-will-be-held-in-sao-paulo-from-19-20th-may-2015', N'The Feilub 2015 is being prepared to be the first exclusive trade show for the lubricants market. The Interstate Association of Industries Mixers and Vacuum Packing Machines Oil Derivative Products - Simepetro is planning the event for 19 to 20 May next year, the Exhibition Centre and Convention Center Norte, in São Paulo. The stands are already being marketed and the expectation of the organizers is that there is a high demand for spaces.', N'http://www.mediafire.com/convkey/cae5/ngbal6jbm0de2ba6g.jpg', 5, N'

<p><img src="http://www.mediafire.com/convkey/003c/3thrbaxe2185i2b6g.jpg" border="0" width="500" height="148" style="display: block; margin-left: auto; margin-right: auto;" /></p>
<p style="text-align: center;"><a href="http://www.feilub.com.br/menu-expositor/menu-posicao-stands.html">http://www.feilub.com.br/menu-expositor/menu-posicao-stands.html</a></p>
<p style="text-align: justify;"><span style="color: #ff0000;"><strong>The Feilub 2015</strong></span> is being prepared to be the first exclusive trade show for the lubricants market.<br /> The Interstate Association of Industries Mixers and Vacuum Packing Machines Oil Derivative Products - Simepetro is planning the event for 19 to 20 May next year, the Exhibition Centre and Convention Center Norte, in São Paulo. The stands are already being marketed and the expectation of the organizers is that there is a high demand for spaces.<br /> According to the president of the Union, Carlos Ristum, the proposal is to bring together in a single space the entire production chain of lubricating oils and greases, as well as equipment manufacturers, suppliers of products, services and laboratories.<br /> <img src="http://www.mediafire.com/convkey/cae5/ngbal6jbm0de2ba6g.jpg" border="0" width="350" height="350" style="display: block; margin-left: auto; margin-right: auto;" /><br /> To meet all kinds of exhibitors, the space of the fair was divided into booths measuring 12m2 to 30m2. The expectation is to create a business environment that offers all kinds of products and service interests of manufacturers, dealers and distributors of lubricating oils and greases. "It will be an opportunity for producers to find new distributors and distributors find new suppliers, as well as having contact with technologies in machinery and equipment, and laboratory services. We are confident that the fair will be a success," says Ristum.<br /> <br /> The good expectation of the Union is based on the start of sales. "The feedback has been very good. We have indoor ranges and various reserves, which shows the interest of the industry," said the president. Those interested in learning about the project and the exposure conditions can contact with the Union (11) 3207-0072</p>
<p> </p>', NULL, CAST(N'2016-03-19 15:40:19.000' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (36, N'BPT Chemicals honorably became Sponsor for ILMA Management Forum 2015 in Tucson US', N'bpt-chemicals-honorably-became-sponsor-for-ilma-management-forum-2015-in-tucson-us', N'Afton Chemical Corporation, Amtecol, Inc. Calumet Specialty Products Partners, L.P.', NULL, 5, N'<p style="text-align:start">GOLD</p>

<div class="indent-left" style="margin-left: 30px; color: #787878; font-family: ''Open Sans'', ''Helvetica Neue'', Helvetica, Arial, sans-serif; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 20px; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: #ffffff;">
<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.aftonchemical.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Afton Chemical Corporation</a></strong><br />
Friday Night Reception Bar</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.aftonchemical.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/aftonlogo.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.amtecol.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Amtecol, Inc.</a></strong><br />
Audio/Visual Support</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.amtecol.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/amtecol.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.calumetspecialty.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Calumet Specialty Products Partners, L.P.</a></strong><br />
Keynote Speaker</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.calumetspecialty.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/calumet.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.elcocorp.com/" style="color: #007ccc; text-decoration: none;" target="_blank">The Elco Corporation</a></strong><br />
Friday Dinner</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.elcocorp.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/elco.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.ergon.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Ergon, Inc.</a></strong><br />
Saturday Breakfast Buffet</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.ergon.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/ergon.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><a href="http://www.fhr.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><strong>Flint Hills Resources</strong></a><br />
Lanyards</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.fhr.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/flinthills.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.hollyfrontier.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Holly Frontier Lubricants &amp; Specialty Products</a></strong><br />
Hotel Key Cards</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.hollyfrontier.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/holly1.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.infineum.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Infineum USA L.P. </a></strong><br />
Friday Night Activities and Decorations</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.infineum.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/infineum.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.nanomech.com/" style="color: #007ccc; text-decoration: none;" target="_blank">NanoMech, Inc.</a></strong><br />
Video Station</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.nanomech.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/nanomech.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.petro-canada.ca/" style="color: #007ccc; text-decoration: none;" target="_blank">Petro-Canada Lubricants Inc.</a></strong><br />
Base Camp</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.petro-canada.ca/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/petrocanada.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.sealandchem.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Sea-Land Chemical Company</a></strong><br />
Friday Breakfast Buffet</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.sealandchem.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/sealand.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.yubase.com/" style="color: #007ccc; text-decoration: none;" target="_blank">SK Lubricants Americas Inc.</a></strong><br />
Opening Reception</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.yubase.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/sklubricants.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span5" style="float: left; min-height: 30px; margin-left: 0px; width: 269.140625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.vanderbiltchemicals.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Vanderbilt Chemicals, LLC</a></strong><br />
Scholarship Program Golf Tournament</p>
</div>

<div class="span4 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 211.875px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.vanderbiltchemicals.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/Vanderbilt_color_lg.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>
</div>

<p style="text-align:start">SILVER</p>

<div class="indent-left" style="margin-left: 30px; color: #787878; font-family: ''Open Sans'', ''Helvetica Neue'', Helvetica, Arial, sans-serif; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 20px; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: #ffffff;">
<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.oils.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Allegheny Petroleum Products Company</a></strong><br />
Friday Refreshment Break</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.oils.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/allegheny2.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span5" style="float: left; min-height: 30px; margin-left: 0px; width: 269.140625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.amref.com/" style="color: #007ccc; text-decoration: none;" target="_blank">American Refining Group Inc.</a></strong><br />
Thursday Refreshment Breaks</p>
</div>

<div class="span4 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 211.875px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.amref.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/american_refining_lg.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.argusmedia.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Argus Media</a></strong><br />
Friday Market Analysis Lunch Session</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.argusmedia.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/argus_media.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span5" style="float: left; min-height: 30px; margin-left: 0px; width: 269.140625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.chevronbaseoils.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Chevron Base Oils</a></strong><br />
Meeting App</p>
</div>

<div class="span4 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 211.875px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.chevronbaseoils.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/Chevron_Base_Oils.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.crossoil.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Cross Oil Refining &amp; Marketing</a></strong><br />
Note Pages in Registration Booklets</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.crossoil.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/crossoillogo.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span5" style="float: left; min-height: 30px; margin-left: 0px; width: 269.140625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.daubertchemical.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Daubert Chemical Company, Inc.</a></strong><br />
Wireless Internet Access</p>
</div>

<div class="span4 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 211.875px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.daubertchemical.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/Daubert_lg.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.crystal-clean.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Heritage-Crystal Clean</a></strong><br />
Friday Fun Night Entertainment</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.crystal-clean.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/crystalclean.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.mcchemical.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Midcontinental Chemical Company</a></strong><br />
General Sponsor</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.mcchemical.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/MidContinentalChemical.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.nexeosolutions.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Nexeo Solutions, LLC</a></strong><br />
Registration Lists in Registration Booklets</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.nexeosolutions.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/nexeo.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.palmerholland.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Palmer Holland Inc.</a></strong><br />
Schedule of Events in Registration Booklets</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.palmerholland.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/palmerholland2.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.petchemgroup.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Petroleum Chemicals</a></strong><br />
Agenda Signs</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.petchemgroup.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/petroleum_chemicals.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.phillips66.com/EN/Pages/index.aspx" style="color: #007ccc; text-decoration: none;" target="_blank">Phillips 66 Company</a></strong><br />
Brochure</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.phillips66.com/EN/Pages/index.aspx" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/phillips66.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.rheinchemie.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Rhein Chemie Additives</a></strong><br />
Registration Booklets</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.rheinchemie.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/RheinChemie.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>
</div>

<p style="text-align:start">BRONZE</p>

<div class="indent-left" style="margin-left: 30px; color: #787878; font-family: ''Open Sans'', ''Helvetica Neue'', Helvetica, Arial, sans-serif; font-size: 14px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 20px; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: #ffffff;">
<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.acme-hardesty.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Acme Hardesty Co.</a></strong><br />
General Sponsor</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.acme-hardesty.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/AcmeHardesty.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.additivesinternational.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Additives International</a></strong><br />
WILMA Book Club</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.additivesinternational.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/additives.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.advancedlubes.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Advanced Lubrication Specialties</a></strong><br />
<em>Compoundings</em> Delivery</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.advancedlubes.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/advancedlub.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span5" style="float: left; min-height: 30px; margin-left: 0px; width: 269.140625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.americhemsales.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Americhem Sales Corporation</a></strong><br />
General Sponsor</p>
</div>

<div class="span4 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 211.875px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.americhemsales.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/americhem_lg.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.avista-oil.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Avista Oil Refining &amp; Trading, USA</a></strong><br />
General Sponsor</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.avista-oil.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/avista.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><a href="http://www.bosslubricants.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><strong>Boss Lubricants</strong></a><br />
New Member &amp; First Time Attendee Reception</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.bosslubricants.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/boss.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><a href="http://bptchemicals.com/en.html" style="color: #007ccc; text-decoration: none;" target="_blank"><strong>BPT Chemicals Co., Ltd.</strong></a><br />
General Sponsor</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://bptchemicals.com/en.html" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/BPTChemicals_logo.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.chevron.com/products/oronite/" style="color: #007ccc; text-decoration: none;" target="_blank">Chevron Oronite Company LLC</a></strong><br />
Name Badges</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.chevron.com/products/oronite/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/chevron-oronite.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.chsinc.com/" style="color: #007ccc; text-decoration: none;" target="_blank">CHS Inc.</a></strong><br />
Saturday Refreshment Break</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.chsinc.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/chs.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.citgo.com/" style="color: #007ccc; text-decoration: none;" target="_blank">CITGO Petroleum Corporation</a></strong><br />
General Sponsor</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.citgo.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/CITGO_Petroleum.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.deltacogroup.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Delta Companies Group</a></strong><br />
General Sponsor</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.deltacogroup.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/delta_companies.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.rohmax.com/product/rohmax/en/Pages/default.aspx" style="color: #007ccc; text-decoration: none;" target="_blank">Evonik Oil Additives</a></strong><br />
Registration Snacks</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.rohmax.com/product/rohmax/en/Pages/default.aspx" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/evonik.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.exxonmobil.com/basestocks" style="color: #007ccc; text-decoration: none;" target="_blank">ExxonMobil Basestocks</a></strong><br />
General Sponsor</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.exxonmobil.com/basestocks" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/exxon-mobil.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.coastalbp.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Irving Blending &amp; Packaging</a></strong><br />
Meeting Website</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.coastalbp.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/Irving.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><a href="http://www.munzing.com/na-en/" style="color: #007ccc; text-decoration: none;" target="_blank"><strong>M&uuml;nzing</strong></a><br />
General Sponsor</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.munzing.com/na-en/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/munzing.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.orgroup.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Owner Resource Group</a></strong><br />
Pocket Guides</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.orgroup.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/ORG.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><a href="http://www.pbfenergy.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><strong>Paulsboro Refining Company, LLC</strong></a><br />
General Sponsor</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.pbfenergy.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/Paulsboro.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.renkertoil.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Renkert Oil, LLC</a></strong><br />
Shipment of Meeting Materials</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.renkertoil.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/renkert.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.speclubes.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Specialty Lubricants Corporation</a></strong><br />
General Sponsor</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.speclubes.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/specialt-lubricants-logo.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.tulstar.com/" style="color: #007ccc; text-decoration: none;" target="_blank">Tulstar Products, Inc.</a></strong><br />
General Sponsor</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.tulstar.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/tulstar.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>

<div class="row-fluid" style="width: 670px;">
<div class="span6" style="float: left; min-height: 30px; margin-left: 0px; width: 326.40625px; display: block; box-sizing: border-box;">
<p><strong><a href="http://www.unitedcolor.com/" style="color: #007ccc; text-decoration: none;" target="_blank">United Color Manufacturing, Inc.</a></strong><br />
General Sponsor</p>
</div>

<div class="span3 center" style="float: left; min-height: 30px; margin-left: 17.171875px; width: 154.609375px; text-align: center; display: block; box-sizing: border-box;">
<p><a href="http://www.unitedcolor.com/" style="color: #007ccc; text-decoration: none;" target="_blank"><img src="https://www.ilma.org/events/images/ucmlogo2.jpg" style="border:0px; height:auto; max-width:100%; vertical-align:middle" /></a></p>
</div>
</div>
</div>

<p style="text-align:start">&nbsp;</p>
', NULL, CAST(N'2016-03-19 15:44:47.087' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (37, N'China ( Chongqing ) International Lubricanting Exhibition 2015 ', N'china-chongqing-international-lubricanting-exhibition-2015', N'Registration & Build up: Sept.9-10, 2015 (09:00–17:00)
•Opening Ceremony:      Sept.11, 2015 (09:20)
•Exhibition & Trading:     Sept.11-13, 2015 (09:00–16:30)
•Closure & Dismantling:  Sept.13, 2015(15:00)', N'/Data/images/news-event/china-chongqing-international-lubricanting-exhibition-2015.jpg', 5, N'<p><span style="color:#333333; font-family:times new roman; font-size:14px">Registration &amp; Build up: Sept.9-10, 2015 (09:00&ndash;17:00)</span><br />
<span style="color:#333333; font-family:times new roman; font-size:14px">&bull;Opening Ceremony:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Sept.11, 2015 (09:20)</span><br />
<span style="color:#333333; font-family:times new roman; font-size:14px">&bull;Exhibition &amp; Trading:&nbsp;&nbsp;&nbsp;&nbsp; Sept.11-13, 2015 (09:00&ndash;16:30)</span><br />
<span style="color:#333333; font-family:times new roman; font-size:14px">&bull;Closure &amp; Dismantling:&nbsp; Sept.13, 2015(15:00)</span></p>
', NULL, CAST(N'2016-03-19 15:46:35.633' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (38, N'16th China International Lubricants and Technology Exhibition in Shanghai ', N'16th-china-international-lubricants-and-technology-exhibition-in-shanghai', N'2014 Chinese lubricating oil industry development peak BBS", once again, bring about the whole lubricating oil industry in China for industry present situation and the future development trend of global analysis', N'/Data/images/news-event/16th-china-international-lubricants-and-technology-exhibition-in-shanghai.png', 5, N'<p>2014 Chinese lubricating oil industry development peak BBS&quot;, once again, bring about the whole lubricating oil industry in China for industry present situation and the future development trend of global analysis</p>
', NULL, CAST(N'2016-03-19 15:51:34.370' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (39, N'ExxonMobil Singapore Chemical Plant Expansion In Operation', N'exxonmobil-singapore-chemical-plant-expansion-in-operation', N'ExxonMobil’s Singapore Chemical Plant is now producing ethylene from the facility’s second world-scale steam cracker.', NULL, NULL, NULL, NULL, CAST(N'2016-03-19 15:55:26.100' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1, NULL, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Content] ([ID], [Name], [MetaTitle], [Description], [Image], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount], [Tags], [Language], [IsProduct], [File]) VALUES (42, N'SIMEPETRO Feira de lubrificantes para 2015, São Paulo, de 19 - 20 de maio de 2015 ', N'simepetro-feira-de-lubrificantes-para-2015-sao-paulo-de-19-20-de-maio-de-2015', N'
A Feilub 2015 está sendo preparada para ser a primeira feira exclusiva para o mercado de lubrificantes. O Sindicato Interestadual das Indústrias Misturadoras e Envasilhadoras de Produtos Derivados de Petróleo – Simepetro está programando o evento para os dias 19 e 20 de maio do próximo ano, no Centro de Exposições e Convenções Center Norte, em São Paulo. Os estandes já estão sendo comercializados e a expectativa dos organizadores é de que haja uma grande procura por espaços. ', N'/Data/images/news-event/PLANTA_com_posicoes_28_ABRIL_2015_valeesta.jpg', 5, N'<div class="details">
<p>A Feilub 2015 est&aacute; sendo preparada para ser a primeira feira exclusiva para o mercado de lubrificantes.<br />
O Sindicato Interestadual das Ind&uacute;strias Misturadoras e Envasilhadoras de Produtos Derivados de Petr&oacute;leo &ndash; Simepetro est&aacute; programando o evento para os dias 19 e 20 de maio do pr&oacute;ximo ano, no Centro de Exposi&ccedil;&otilde;es e Conven&ccedil;&otilde;es Center Norte, em S&atilde;o Paulo. Os estandes j&aacute; est&atilde;o sendo comercializados e a expectativa dos organizadores &eacute; de que haja uma grande procura por espa&ccedil;os.</p>

<p>De acordo com o presidente do Sindicato, Carlos Ristum, a proposta &eacute; reunir em um &uacute;nico espa&ccedil;o toda a cadeia produtiva de &oacute;leos lubrificantes e graxas, al&eacute;m de fabricantes de equipamentos, fornecedores de produtos, servi&ccedil;os e laborat&oacute;rios.</p>

<p>Para atender aos mais variados tipos de expositores, o espa&ccedil;o da Feira foi dividido em estandes que medem de 12m2 a 30m2. A expectativa &eacute; criar um ambiente de neg&oacute;cios que ofere&ccedil;a todo tipo de produto e servi&ccedil;o de interesse dos produtores, revendedores e distribuidores de &oacute;leos lubrificantes e graxas. &quot;Ser&aacute; uma oportunidade para o produtor encontrar novos distribuidores, bem como dos distribuidores encontrarem novos fornecedores, al&eacute;m de ter contato com tecnologias em m&aacute;quinas e equipamentos, e servi&ccedil;os de laborat&oacute;rios. Temos certeza que a Feira ser&aacute; um sucesso&quot;, afirma Ristum.</p>

<p>A boa expectativa do Sindicato tem por base o in&iacute;cio das vendas. &quot;O retorno est&aacute; sendo muito bom. J&aacute; temos estandes fechados e v&aacute;rias reservas, o que demonstra o interesse do setor&quot;, destaca o presidente. Os interessados em conhecer o projeto e as condi&ccedil;&otilde;es de exposi&ccedil;&atilde;o podem entrar em contato com o Sindicato (11) 3207-0072.</p>

<p>SEMIPETRO FAIR OF LUBRICANT 2015 WILL BE HELD IN SAO PAULO FROM 19 &ndash; 20TH MAY 2015</p>

<p>The Feilub 2015 is being prepared to be the first exclusive trade show for the lubricants market.<br />
The Interstate Association of Industries Mixers and Vacuum Packing Machines Oil Derivative Products - Simepetro is planning the event for 19 to 20 May next year, the Exhibition Centre and Convention Center Norte, in S&atilde;o Paulo. The stands are already being marketed and the expectation of the organizers is that there is a high demand for spaces.</p>

<p>According to the president of the Union, Carlos Ristum, the proposal is to bring together in a single space the entire production chain of lubricating oils and greases, as well as equipment manufacturers, suppliers of products, services and laboratories.</p>

<p>To meet all kinds of exhibitors, the space of the fair was divided into booths measuring 12m2 to 30m2. The expectation is to create a business environment that offers all kinds of products and service interests of manufacturers, dealers and distributors of lubricating oils and greases. &quot;It will be an opportunity for producers to find new distributors and distributors find new suppliers, as well as having contact with technologies in machinery and equipment, and laboratory services. We are confident that the fair will be a success,&quot; says Ristum.</p>

<p>The good expectation of the Union is based on the start of sales. &quot;The feedback has been very good. We have indoor ranges and various reserves, which shows the interest of the industry,&quot; said the president. Those interested in learning about the project and the exposure conditions can contact with the Union (11) 3207-0072</p>
</div>
', NULL, CAST(N'2016-03-19 16:02:06.623' AS DateTime), N'toanbn', NULL, NULL, NULL, NULL, 0, NULL, 0, NULL, N'vi', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Content] OFF
GO
INSERT [dbo].[ContentTag] ([ContentID], [TagID]) VALUES (1, N'thoi-su')
GO
INSERT [dbo].[ContentTag] ([ContentID], [TagID]) VALUES (1, N'tin-tuc')
GO
SET IDENTITY_INSERT [dbo].[Feedback] ON 

GO
INSERT [dbo].[Feedback] ([ID], [Name], [Person], [Phone], [MobilePhone], [Email], [Address], [Content], [CreatedDate], [Status]) VALUES (1, N'dcm ca', N'francisco pena', NULL, N'584142555263', N'penafj@gmail.com', N'valencia,venezuela', NULL, CAST(N'2016-04-13 20:52:59.573' AS DateTime), NULL)
GO
INSERT [dbo].[Feedback] ([ID], [Name], [Person], [Phone], [MobilePhone], [Email], [Address], [Content], [CreatedDate], [Status]) VALUES (2, N'dcm', N'francisco pena', NULL, N'584142555263', N'penafj@gmail.com', N'caracas', NULL, CAST(N'2016-04-23 06:50:56.237' AS DateTime), NULL)
GO
INSERT [dbo].[Feedback] ([ID], [Name], [Person], [Phone], [MobilePhone], [Email], [Address], [Content], [CreatedDate], [Status]) VALUES (3, N'dcm', N'francisco pena', NULL, N'584142555263', N'penafj@gmail.com', N'caracas', NULL, CAST(N'2016-04-23 06:51:14.117' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[Feedback] OFF
GO
INSERT [dbo].[Footer] ([ID], [Content], [Status]) VALUES (N'footer', N'<div class="wrap">	
	     <div class="section group">
				<div class="col_1_of_4 span_1_of_4">
						<h4>Information</h4>
						<ul>
						<li><a href="about.html">About Us</a></li>
						<li><a href="contact.html">Customer Service</a></li>
						<li><a href="#">Advanced Search</a></li>
						<li><a href="delivery.html">Orders and Returns</a></li>
						<li><a href="contact.html">Contact Us</a></li>
						</ul>
					</div>
				<div class="col_1_of_4 span_1_of_4">
					<h4>Why buy from us</h4>
						<ul>
						<li><a href="about.html">About Us</a></li>
						<li><a href="contact.html">Customer Service</a></li>
						<li><a href="#">Privacy Policy</a></li>
						<li><a href="contact.html">Site Map</a></li>
						<li><a href="#">Search Terms</a></li>
						</ul>
				</div>
				<div class="col_1_of_4 span_1_of_4">
					<h4>My account</h4>
						<ul>
							<li><a href="contact.html">Sign In</a></li>
							<li><a href="index.html">View Cart</a></li>
							<li><a href="#">My Wishlist</a></li>
							<li><a href="#">Track My Order</a></li>
							<li><a href="contact.html">Help</a></li>
						</ul>
				</div>
				<div class="col_1_of_4 span_1_of_4">
					<h4>Contact</h4>
						<ul>
							<li><span>+91-123-456789</span></li>
							<li><span>+00-123-000000</span></li>
						</ul>
						<div class="social-icons">
							<h4>Follow Us</h4>
					   		  <ul>
							      <li><a href="#" target="_blank"><img src="images/facebook.png" alt="" /></a></li>
							      <li><a href="#" target="_blank"><img src="images/twitter.png" alt="" /></a></li>
							      <li><a href="#" target="_blank"><img src="images/skype.png" alt="" /> </a></li>
							      <li><a href="#" target="_blank"> <img src="images/dribbble.png" alt="" /></a></li>
							      <li><a href="#" target="_blank"> <img src="images/linkedin.png" alt="" /></a></li>
							      <div class="clear"></div>
						     </ul>
   	 					</div>
				</div>
			</div>			
        </div>
        <div class="copy_right">
				<p>Company Name © All rights Reseverd | Design by  <a href="http://w3layouts.com">W3Layouts</a> </p>
		   </div>', 1)
GO
INSERT [dbo].[Language] ([ID], [Name], [IsDefault]) VALUES (N'en', N'Tiếng Anh', 0)
GO
INSERT [dbo].[Language] ([ID], [Name], [IsDefault]) VALUES (N'vi', N'Tiếng Việt', 1)
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

GO
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [TypeID]) VALUES (1, N'Home', N'/', 1, N'_blank', 1, 1)
GO
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [TypeID]) VALUES (2, N'About Us', N'about-us', 2, N'_self', 1, 1)
GO
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [TypeID]) VALUES (3, N'News & Events', N'news-events', 3, N'_self', 1, 1)
GO
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [TypeID]) VALUES (4, N'Products & Services', N'products-services', 4, N'_self', 1, 1)
GO
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [TypeID]) VALUES (5, N'Contact us', N'contact-us', 3, N'_self', 1, 2)
GO
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [TypeID]) VALUES (6, N'Đăng nhập', N'/dang-nhap', 1, N'_self', 0, 2)
GO
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [TypeID]) VALUES (7, N'Đăng ký', N'/dang-ky', 2, N'_self', 0, 2)
GO
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [TypeID]) VALUES (8, N'Global Freights', N'global-freights', 7, N'_self', 1, 1)
GO
INSERT [dbo].[Menu] ([ID], [Text], [Link], [DisplayOrder], [Target], [Status], [TypeID]) VALUES (9, N'Inquiry', N'/Contact/Inquiry', 8, N'_self', 1, 2)
GO
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET IDENTITY_INSERT [dbo].[MenuType] ON 

GO
INSERT [dbo].[MenuType] ([ID], [Name]) VALUES (1, N'Menu chính')
GO
INSERT [dbo].[MenuType] ([ID], [Name]) VALUES (2, N'Menu top')
GO
SET IDENTITY_INSERT [dbo].[MenuType] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (1, CAST(N'2015-09-10 22:51:27.657' AS DateTime), NULL, N'toanbn', N'2312412', N'hn', N'ngoctoan89@gmail.com', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (2, CAST(N'2015-09-12 21:45:35.777' AS DateTime), NULL, N'toàn', N'0966036626', N'Xuân Đỉnh Từ Liêm Hà Nội', N'toanbn@esvn.com.vn', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (3, CAST(N'2015-09-12 21:46:10.357' AS DateTime), NULL, N'toàn', N'0966036626', N'Xuân Đỉnh Từ Liêm Hà Nội', N'toanbn@esvn.com.vn', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (4, CAST(N'2015-09-12 21:47:26.417' AS DateTime), NULL, N'toanbn', N'0966036626', N'Xuân Đỉnh Từ Liêm Hà Nội', N'toanbn@esvn.com.vn', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (5, CAST(N'2015-09-12 21:49:37.137' AS DateTime), NULL, N'eq', N'eqw', N'Xuân Đỉnh Từ Liêm Hà Nội', N'eqw', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (6, CAST(N'2015-09-12 21:55:54.457' AS DateTime), NULL, N'toanbn', N'0966036626', N'23', N'131', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (7, CAST(N'2015-09-12 21:57:49.660' AS DateTime), NULL, N'423', N'423', N'423', N'423', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (8, CAST(N'2015-09-12 21:58:08.873' AS DateTime), NULL, N'423', N'423', N'423', N'423', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (9, CAST(N'2015-09-12 21:59:23.833' AS DateTime), NULL, N'423', N'424', N'424', N'42', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (10, CAST(N'2015-09-12 22:01:38.170' AS DateTime), NULL, N'534', N'53', N'543', N'534', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (11, CAST(N'2015-09-13 06:58:46.767' AS DateTime), NULL, N'4', N'423', N'423', N'423', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (12, CAST(N'2015-09-13 07:00:01.937' AS DateTime), NULL, N'34', N'423', N'432', N'423', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (13, CAST(N'2015-09-13 07:07:01.167' AS DateTime), NULL, N'423', N'423', N'423', N'423', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (14, CAST(N'2015-09-13 07:18:25.970' AS DateTime), NULL, N'toanbn', N'423', N'423', N'423', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (15, CAST(N'2015-09-13 07:21:34.320' AS DateTime), NULL, N'34', N'342', N'424', N'42', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (16, CAST(N'2015-09-13 07:24:01.707' AS DateTime), NULL, N'342', N'432', N'423', N'432', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (17, CAST(N'2015-09-13 07:47:09.587' AS DateTime), NULL, N'342', N'234', N'424', N'423', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (18, CAST(N'2015-09-13 07:47:30.560' AS DateTime), NULL, N'342', N'234', N'424', N'423', NULL)
GO
INSERT [dbo].[Order] ([ID], [CreatedDate], [CustomerID], [ShipName], [ShipMobile], [ShipAddress], [ShipEmail], [Status]) VALUES (19, CAST(N'2015-09-13 07:47:42.443' AS DateTime), NULL, N'342', N'234', N'424', N'ngoctoan89@gmail.com', NULL)
GO
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (4, 15, 1, CAST(12535000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (4, 16, 1, CAST(12535000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (5, 1, 3, CAST(22535000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (5, 2, 1, CAST(22535000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (5, 3, 1, CAST(22535000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (5, 4, 1, CAST(22535000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (5, 5, 1, CAST(22535000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (5, 6, 1, CAST(22535000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (5, 11, 1, CAST(22535000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (5, 13, 1, CAST(22535000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (5, 17, 1, CAST(22535000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (5, 18, 1, CAST(22535000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (5, 19, 1, CAST(22535000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (8, 7, 1, CAST(4300000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (8, 8, 1, CAST(4300000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (8, 9, 1, CAST(4300000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (8, 10, 1, CAST(4300000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (8, 12, 1, CAST(4300000 AS Decimal(18, 0)))
GO
INSERT [dbo].[OrderDetail] ([ProductID], [OrderID], [Quantity], [Price]) VALUES (8, 14, 1, CAST(4300000 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

GO
INSERT [dbo].[Product] ([ID], [Name], [Code], [MetaTitle], [Description], [Image], [MoreImages], [Price], [PromotionPrice], [IncludedVAT], [Quantity], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount]) VALUES (1, N'Tivi Sony 21 inch', N'A01', N'tivi-sony-21', N'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.', N'/assets/client/images/feature-pic1.jpg', NULL, CAST(5770000 AS Decimal(18, 0)), NULL, NULL, 0, 2, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed tincidunt bibendum est, non interdum nulla sodales ac. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed varius mollis sodales. Curabitur ac ligula dolor', NULL, CAST(N'2015-08-26 19:24:20.953' AS DateTime), N'admin', NULL, NULL, NULL, NULL, 1, CAST(N'2015-08-30 00:00:00.000' AS DateTime), 0)
GO
INSERT [dbo].[Product] ([ID], [Name], [Code], [MetaTitle], [Description], [Image], [MoreImages], [Price], [PromotionPrice], [IncludedVAT], [Quantity], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount]) VALUES (2, N'Bộ loa 2.0 Microlab', N'A02', N'bo-loa-20-microlab', N'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.', N'/assets/client/images/feature-pic2.jpg', NULL, CAST(435000 AS Decimal(18, 0)), CAST(235000 AS Decimal(18, 0)), 0, 12, 5, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed tincidunt bibendum est, non interdum nulla sodales ac. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed varius mollis sodales. Curabitur ac ligula dolor', 12, CAST(N'2015-08-26 19:30:57.870' AS DateTime), N'admin', NULL, NULL, NULL, NULL, 1, NULL, 0)
GO
INSERT [dbo].[Product] ([ID], [Name], [Code], [MetaTitle], [Description], [Image], [MoreImages], [Price], [PromotionPrice], [IncludedVAT], [Quantity], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount]) VALUES (3, N'Máy ảnh Nikon', N'A03', N'may-anh-nikon', N'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.', N'/assets/client/images/feature-pic3.jpg', NULL, CAST(2535000 AS Decimal(18, 0)), CAST(2350000 AS Decimal(18, 0)), 0, 12, 1, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed tincidunt bibendum est, non interdum nulla sodales ac. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed varius mollis sodales. Curabitur ac ligula dolor', 12, CAST(N'2015-08-26 19:32:06.437' AS DateTime), N'admin', NULL, NULL, NULL, NULL, 1, NULL, 0)
GO
INSERT [dbo].[Product] ([ID], [Name], [Code], [MetaTitle], [Description], [Image], [MoreImages], [Price], [PromotionPrice], [IncludedVAT], [Quantity], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount]) VALUES (4, N'Tivi plasma siêu phẳng', N'A04', N'tivi-plasma-sieu-phang', N'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.', N'/assets/client/images/feature-pic4.jpg', NULL, CAST(12535000 AS Decimal(18, 0)), CAST(12350000 AS Decimal(18, 0)), 0, 12, 1, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed tincidunt bibendum est, non interdum nulla sodales ac. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed varius mollis sodales. Curabitur ac ligula dolor', 12, CAST(N'2015-08-26 19:33:18.800' AS DateTime), N'admin', NULL, NULL, NULL, NULL, 1, NULL, 0)
GO
INSERT [dbo].[Product] ([ID], [Name], [Code], [MetaTitle], [Description], [Image], [MoreImages], [Price], [PromotionPrice], [IncludedVAT], [Quantity], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount]) VALUES (5, N'Máy ảnh Sony', N'A05', N'may-anh-sony', N'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.', N'/assets/client/images/new-pic1.jpg', NULL, CAST(22535000 AS Decimal(18, 0)), NULL, 0, 23, 3, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed tincidunt bibendum est, non interdum nulla sodales ac. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed varius mollis sodales. Curabitur ac ligula dolor', 12, CAST(N'2015-08-26 19:33:18.800' AS DateTime), N'admin', NULL, NULL, NULL, NULL, 1, NULL, 0)
GO
INSERT [dbo].[Product] ([ID], [Name], [Code], [MetaTitle], [Description], [Image], [MoreImages], [Price], [PromotionPrice], [IncludedVAT], [Quantity], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount]) VALUES (6, N'Dàn karaoke 2.0', N'A06', N'dan-karaoke-20', N'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.', N'/assets/client/images/images/new-pic2.jpg', NULL, CAST(12535000 AS Decimal(18, 0)), NULL, 0, 44, 6, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed tincidunt bibendum est, non interdum nulla sodales ac. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed varius mollis sodales. Curabitur ac ligula dolor', 12, CAST(N'2015-08-26 19:33:18.800' AS DateTime), N'admin', NULL, NULL, NULL, NULL, 1, NULL, 0)
GO
INSERT [dbo].[Product] ([ID], [Name], [Code], [MetaTitle], [Description], [Image], [MoreImages], [Price], [PromotionPrice], [IncludedVAT], [Quantity], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount]) VALUES (7, N'Máy giặt Electrolux', N'A07', N'may-giat-electrolux', N'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.', N'/assets/client/images/images/new-pic2.jpg', NULL, CAST(7535000 AS Decimal(18, 0)), NULL, 0, 44, 6, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed tincidunt bibendum est, non interdum nulla sodales ac. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed varius mollis sodales. Curabitur ac ligula dolor', 12, CAST(N'2015-08-26 19:33:18.800' AS DateTime), N'admin', NULL, NULL, NULL, NULL, 1, NULL, 0)
GO
INSERT [dbo].[Product] ([ID], [Name], [Code], [MetaTitle], [Description], [Image], [MoreImages], [Price], [PromotionPrice], [IncludedVAT], [Quantity], [CategoryID], [Detail], [Warranty], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [TopHot], [ViewCount]) VALUES (8, N'Đồng hồ đeo tay thời trang', N'A08', N'dong-ho-deo-tay-thoi-trang', N'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore.', N'/assets/client/images/new-pic3.jpg', NULL, CAST(4300000 AS Decimal(18, 0)), NULL, NULL, 21, 7, N'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed tincidunt bibendum est, non interdum nulla sodales ac. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed varius mollis sodales. Curabitur ac ligula dolor', NULL, CAST(N'2015-08-26 19:37:06.083' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] ON 

GO
INSERT [dbo].[ProductCategory] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome]) VALUES (1, N'Lubricant Oil Additives - OCP VISCOSITY INDEX IMPROVERS ', N'lubricant-oil-additives-ocp-viscosity-index-improvers', NULL, 0, NULL, CAST(N'2015-08-26 19:00:44.567' AS DateTime), NULL, CAST(N'2015-12-27 13:22:47.570' AS DateTime), NULL, NULL, NULL, 1, 0)
GO
INSERT [dbo].[ProductCategory] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome]) VALUES (2, N'Oils & Lubricant Additives', N'oils-lubricant-additives', NULL, 1, NULL, CAST(N'2015-08-26 19:20:32.210' AS DateTime), N'admin', CAST(N'2015-12-27 13:23:10.497' AS DateTime), NULL, NULL, NULL, 1, 0)
GO
INSERT [dbo].[ProductCategory] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome]) VALUES (4, N'Diesel Fuel Additives', N'Diesel-Fuel-Additives', NULL, 1, NULL, CAST(N'2015-08-26 19:20:32.210' AS DateTime), N'admin', CAST(N'2015-12-27 13:23:55.223' AS DateTime), NULL, NULL, NULL, 1, 0)
GO
INSERT [dbo].[ProductCategory] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome]) VALUES (5, N'Lubricant Oil Additives - OCP & PAMA Grafting Technology', N'lubricant-oil-additives-ocp-pama-grafting-technology', NULL, 1, NULL, CAST(N'2015-08-26 19:20:32.210' AS DateTime), N'admin', CAST(N'2015-12-27 13:24:52.480' AS DateTime), NULL, NULL, NULL, 1, 0)
GO
INSERT [dbo].[ProductCategory] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome]) VALUES (6, N'Phần mềm', N'phan-mem', NULL, 1, NULL, CAST(N'2015-08-26 19:20:32.210' AS DateTime), N'admin', NULL, NULL, NULL, NULL, 1, 0)
GO
INSERT [dbo].[ProductCategory] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome]) VALUES (7, N'Thể thao', N'the-thao', NULL, 1, NULL, CAST(N'2015-08-26 19:20:32.210' AS DateTime), N'admin', NULL, NULL, NULL, NULL, 1, 0)
GO
INSERT [dbo].[ProductCategory] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome]) VALUES (8, N'Thời trang', N'thoi-trang', NULL, 1, NULL, CAST(N'2015-08-26 19:20:32.210' AS DateTime), N'admin', NULL, NULL, NULL, NULL, 1, 0)
GO
INSERT [dbo].[ProductCategory] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome]) VALUES (9, N'Trang sức', N'trang-suc', NULL, 1, NULL, CAST(N'2015-08-26 19:20:32.210' AS DateTime), N'admin', NULL, NULL, NULL, NULL, 1, 0)
GO
INSERT [dbo].[ProductCategory] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome]) VALUES (10, N'Đồ nội thất', N'do-noi-that', NULL, 1, NULL, CAST(N'2015-08-26 19:20:32.210' AS DateTime), N'admin', NULL, NULL, NULL, NULL, 1, 0)
GO
INSERT [dbo].[ProductCategory] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome]) VALUES (11, N'Làm đẹp', N'lam-dep', NULL, 1, NULL, CAST(N'2015-08-26 19:20:32.210' AS DateTime), N'admin', NULL, NULL, NULL, NULL, 1, 0)
GO
INSERT [dbo].[ProductCategory] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome]) VALUES (12, N'ABC', N'abc', 1, 1, NULL, CAST(N'2015-09-05 00:25:30.690' AS DateTime), NULL, NULL, NULL, NULL, NULL, 1, 0)
GO
INSERT [dbo].[ProductCategory] ([ID], [Name], [MetaTitle], [ParentID], [DisplayOrder], [SeoTitle], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [MetaKeywords], [MetaDescriptions], [Status], [ShowOnHome]) VALUES (13, N'PAMA Polymers', N'PAMA-Polymers', NULL, NULL, NULL, CAST(N'2015-12-27 12:57:34.513' AS DateTime), N'Admin', NULL, NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'ADD_CONTENT', N'Thêm tin tức')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'ADD_USER', N'Thêm user')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'DELETE_USER', N'Xoá user')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'EDIT_CONTENT', N'Sửa tin tức')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'EDIT_USER', N'Sửa user')
GO
INSERT [dbo].[Role] ([ID], [Name]) VALUES (N'VIEW_USER', N'Xem danh sách user')
GO
SET IDENTITY_INSERT [dbo].[Slide] ON 

GO
INSERT [dbo].[Slide] ([ID], [Image], [DisplayOrder], [Link], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (1, N'/data/images/slide/1.jpg', 4, N'http://www.feilub.com.br/menu-expositor/menu-posicao-stands.html', NULL, CAST(N'2015-08-26 19:21:44.440' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Slide] ([ID], [Image], [DisplayOrder], [Link], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (2, N'/data/images/slide/2.jpg', 5, N'https://www.ilma.org/events/mgmtforum15/sponsorship/thankyou.cfm', NULL, CAST(N'2015-08-26 19:22:01.173' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Slide] ([ID], [Image], [DisplayOrder], [Link], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (4, N'/data/images/slide/4.jpg', 2, N'http://www.ueil.org/en/EVENTS/Congress-2014/Sponsorship/', NULL, CAST(N'2016-02-28 13:13:32.767' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Slide] ([ID], [Image], [DisplayOrder], [Link], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (5, N'/data/images/slide/5.jpg', 5, N'http://www.interlubric.com/en/index.php', NULL, CAST(N'2016-02-28 13:13:32.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Slide] ([ID], [Image], [DisplayOrder], [Link], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (6, N'/data/images/slide/6.jpg', 6, N'#', NULL, CAST(N'2016-02-28 13:13:32.000' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [dbo].[Slide] ([ID], [Image], [DisplayOrder], [Link], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (7, N'/data/images/slide/7.jpg', 7, N'#', NULL, CAST(N'2016-02-28 13:13:32.000' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [dbo].[Slide] ([ID], [Image], [DisplayOrder], [Link], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (9, N'/data/images/slide/9.jpg', 0, N'http://www.rpi-conferences.com//en/lubricants-week', NULL, CAST(N'2016-02-28 13:13:32.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Slide] ([ID], [Image], [DisplayOrder], [Link], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (10, N'/data/images/slide/8.jpg', 1, N'http://isflindia.in/symposium.html', NULL, CAST(N'2016-02-28 13:13:32.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Slide] ([ID], [Image], [DisplayOrder], [Link], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (11, N'/data/images/slide/c1.jpg', 8, N'#', NULL, CAST(N'2016-02-28 13:13:32.000' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Slide] ([ID], [Image], [DisplayOrder], [Link], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (12, N'/data/images/slide/c2.jpg', 9, N'#', NULL, CAST(N'2016-02-28 13:13:32.000' AS DateTime), NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Slide] OFF
GO
INSERT [dbo].[Tag] ([ID], [Name]) VALUES (N'thoi-su', N'thời sự')
GO
INSERT [dbo].[Tag] ([ID], [Name]) VALUES (N'tin-tuc', N'tin tức')
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [GroupID], [Name], [Address], [Email], [Phone], [ProvinceID], [DistrictID], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (2, N'toanbn', N'da30467d45c5742cf201d1577ce7e150', N'ADMIN', N'toanf', N'hn', N'ngoctoan.dev@gmail.com', N'121', NULL, NULL, NULL, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [GroupID], [Name], [Address], [Email], [Phone], [ProvinceID], [DistrictID], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (8, N'admin1', N'da30467d45c5742cf201d1577ce7e150', N'MEMBER', N'admin 1', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-08-12 07:33:56.890' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [GroupID], [Name], [Address], [Email], [Phone], [ProvinceID], [DistrictID], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (9, N'admin2', N'da30467d45c5742cf201d1577ce7e150', N'MEMBER', N'admin 2', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-08-12 07:34:09.687' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [GroupID], [Name], [Address], [Email], [Phone], [ProvinceID], [DistrictID], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (11, N'admin3', N'da30467d45c5742cf201d1577ce7e150', N'MEMBER', N'admin 3', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-08-12 07:34:21.950' AS DateTime), NULL, NULL, NULL, 0)
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [GroupID], [Name], [Address], [Email], [Phone], [ProvinceID], [DistrictID], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (14, N'admin4', N'da30467d45c5742cf201d1577ce7e150', N'MEMBER', N'admin 123', NULL, NULL, NULL, NULL, NULL, CAST(N'2015-08-12 07:34:36.870' AS DateTime), NULL, CAST(N'2015-09-08 20:05:46.327' AS DateTime), NULL, 1)
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [GroupID], [Name], [Address], [Email], [Phone], [ProvinceID], [DistrictID], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (15, N'toanbn1', N'da30467d45c5742cf201d1577ce7e150', N'MEMBER', N'toan', NULL, N'ngoctoan89112@gmail.com', N'121', NULL, NULL, NULL, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [GroupID], [Name], [Address], [Email], [Phone], [ProvinceID], [DistrictID], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (16, N'toanbn2', N'121189', N'MEMBER', N'toan', N'a', N'toanbn@esvn.com.vn', N'12121', NULL, NULL, CAST(N'2015-09-14 07:28:09.073' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [GroupID], [Name], [Address], [Email], [Phone], [ProvinceID], [DistrictID], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (17, N'toanbn3', N'121189', N'MEMBER', N'toan', N'a', N'toanbn1@esvn.com.vn', N'12121', NULL, NULL, CAST(N'2015-09-14 07:29:12.310' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [GroupID], [Name], [Address], [Email], [Phone], [ProvinceID], [DistrictID], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (18, N'toanbn4', N'121189', N'MEMBER', N'toan', N'a', N'toanbn4@esvn.com.vn', N'12121', NULL, NULL, CAST(N'2015-09-14 07:29:56.410' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [GroupID], [Name], [Address], [Email], [Phone], [ProvinceID], [DistrictID], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (19, N'toanbn58', N'121189', N'MEMBER', N'Bạch Ngọc Toàn', N'312', N'ngoctoan2@gmail.com', N'1212', NULL, NULL, CAST(N'2015-09-14 07:40:31.923' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [GroupID], [Name], [Address], [Email], [Phone], [ProvinceID], [DistrictID], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (20, N'ngoctoan89@gmail.com', NULL, N'MEMBER', N'Bạch Ngọc Toàn', NULL, N'ngoctoan89@gmail.com', NULL, NULL, NULL, CAST(N'2015-09-16 23:03:00.050' AS DateTime), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[User] ([ID], [UserName], [Password], [GroupID], [Name], [Address], [Email], [Phone], [ProvinceID], [DistrictID], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [Status]) VALUES (21, N'toanbn6', N'da30467d45c5742cf201d1577ce7e150', N'MEMBER', N'toàn', N'hn', N'toanbn123@esvn.com.vn', N'121', 101, 10153, CAST(N'2015-09-19 08:58:14.070' AS DateTime), NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
INSERT [dbo].[UserGroup] ([ID], [Name]) VALUES (N'ADMIN', N'Quản trị')
GO
INSERT [dbo].[UserGroup] ([ID], [Name]) VALUES (N'MEMBER', N'Thành viên')
GO
INSERT [dbo].[UserGroup] ([ID], [Name]) VALUES (N'MOD', N'Moderatior')
GO
ALTER TABLE [dbo].[About] ADD  CONSTRAINT [DF_About_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[About] ADD  CONSTRAINT [DF_About_Status]  DEFAULT ((1)) FOR [Status]
GO
