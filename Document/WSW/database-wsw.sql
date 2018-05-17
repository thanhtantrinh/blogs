USE [WSW]
GO
/****** Object:  Table [dbo].[Club]    Script Date: 17/05/2018 9:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Club](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[NickName] [nvarchar](50) NULL,
	[Logo] [varchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_Club_created_date]  DEFAULT (getdate()),
	[UpdatedDate] [datetime] NULL CONSTRAINT [DF_Club_UpdatedDate]  DEFAULT (getdate()),
	[Style] [nvarchar](4000) NULL,
 CONSTRAINT [PK_Club] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ClubInfo]    Script Date: 17/05/2018 9:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClubInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Body] [ntext] NULL,
	[ClubID] [int] NULL,
	[Path] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ClubInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Group]    Script Date: 17/05/2018 9:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Group](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Note] [text] NULL,
	[Status] [varchar](20) NULL,
	[GroupType] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Member]    Script Date: 17/05/2018 9:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Member](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[MemberID] [varchar](50) NOT NULL,
	[Password] [varchar](250) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[ClubID] [int] NOT NULL,
	[ApprovalDate] [datetime] NULL,
	[LastLoginDate] [datetime] NULL,
	[IsLocked] [bit] NOT NULL CONSTRAINT [DF_Member_IsLocked]  DEFAULT ((0)),
	[PasswordQuestion] [nvarchar](max) NULL,
	[PasswordAnswer] [nvarchar](max) NULL,
	[ActivationToken] [nvarchar](200) NULL,
	[EmailConfirmed] [bit] NOT NULL CONSTRAINT [DF_Member_EmailConfirmed]  DEFAULT ((0)),
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL CONSTRAINT [DF_Member_PhoneNumberConfirmed]  DEFAULT ((0)),
	[TwoFactorEnabled] [bit] NOT NULL CONSTRAINT [DF_Member_TwoFactorEnabled]  DEFAULT ((0)),
	[LockoutEndDateUtc] [datetime2](7) NULL,
	[LockoutEnabled] [bit] NOT NULL CONSTRAINT [DF_Member_LockoutEnabled]  DEFAULT ((0)),
	[AccessFailedCount] [int] NOT NULL CONSTRAINT [DF_Member_AccessFailedCount]  DEFAULT ((0)),
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_Member_CreatedDate]  DEFAULT (getdate()),
	[UpdatedDate] [datetime] NOT NULL CONSTRAINT [DF_Member_UpdatedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_Member] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MemberClaim]    Script Date: 17/05/2018 9:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberClaim](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MemberId] [bigint] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_MemberClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MemberDetail]    Script Date: 17/05/2018 9:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MemberDetail](
	[ID] [bigint] NOT NULL,
	[Address] [nvarchar](250) NULL,
	[Phone] [varchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Postcode] [nvarchar](50) NULL,
 CONSTRAINT [PK_MemberDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MemberGroup]    Script Date: 17/05/2018 9:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MemberGroup](
	[ID] [int] NOT NULL,
	[MemberID] [bigint] NULL,
	[GroupID] [int] NULL,
	[SeatingPrefID] [int] NULL,
	[SpecialRequirementID] [int] NULL,
	[IsGroupManager] [bit] NULL,
	[Priority] [int] NULL,
	[Attachment] [varchar](100) NULL,
	[OtherRequirement] [ntext] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MemberGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MemberLogin]    Script Date: 17/05/2018 9:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberLogin](
	[MemberId] [bigint] NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_MemberLogin] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC,
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MemberRegistrationToken]    Script Date: 17/05/2018 9:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberRegistrationToken](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MemberId] [bigint] NULL,
	[Token] [nchar](10) NOT NULL,
 CONSTRAINT [PK_SecurityToken] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MemberRole]    Script Date: 17/05/2018 9:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MemberRole](
	[MemberId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_MemberRole] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 17/05/2018 9:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SeatingPref]    Script Date: 17/05/2018 9:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeatingPref](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[ClubID] [int] NOT NULL,
 CONSTRAINT [PK_SeatingPrefence] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SpecialRequirement]    Script Date: 17/05/2018 9:16:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SpecialRequirement](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Type] [varchar](20) NULL CONSTRAINT [DF_SpecialRequirement_Type]  DEFAULT ('None'),
	[ClubID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [DF_SpecialRequirement_CreatedDate]  DEFAULT (getdate()),
	[UpdatedDate] [datetime] NULL CONSTRAINT [DF_SpecialRequirement_UpdatedDate]  DEFAULT (getdate()),
 CONSTRAINT [PK_SpecialRequirement] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Club] ON 

INSERT [dbo].[Club] ([ID], [Name], [NickName], [Logo], [CreatedDate], [UpdatedDate], [Style]) VALUES (1, N'Western Sydney Wanderers', N'WSW', NULL, CAST(N'2018-05-11 18:14:42.570' AS DateTime), CAST(N'2018-05-11 18:14:42.570' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Club] OFF
SET IDENTITY_INSERT [dbo].[Member] ON 

INSERT [dbo].[Member] ([ID], [MemberID], [Password], [FirstName], [LastName], [Email], [ClubID], [ApprovalDate], [LastLoginDate], [IsLocked], [PasswordQuestion], [PasswordAnswer], [ActivationToken], [EmailConfirmed], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [CreatedDate], [UpdatedDate]) VALUES (4, N'123456789', N'APqLGpQBkHfu9zHhLDc+kf9jKziJZ7Y0vI1dqv+NkV7oxpiwcnPB0NxsxURHHHvV/Q==', N'Tan', N'Trinh', N'tantrinh@tpfvietnam.vn', 1, NULL, NULL, 0, NULL, NULL, NULL, 1, N'ad2015d5-4eea-4b1f-8270-38b841c9c3c2', NULL, 0, 0, NULL, 0, 0, CAST(N'2018-05-16 07:27:28.950' AS DateTime), CAST(N'2018-05-16 07:27:28.950' AS DateTime))
SET IDENTITY_INSERT [dbo].[Member] OFF
SET IDENTITY_INSERT [dbo].[SpecialRequirement] ON 

INSERT [dbo].[SpecialRequirement] ([ID], [Name], [Type], [ClubID], [CreatedDate], [UpdatedDate]) VALUES (1, N'Wheelchair bay required', NULL, 1, CAST(N'2018-05-11 18:22:29.340' AS DateTime), CAST(N'2018-05-11 18:22:29.340' AS DateTime))
INSERT [dbo].[SpecialRequirement] ([ID], [Name], [Type], [ClubID], [CreatedDate], [UpdatedDate]) VALUES (2, N'Carers seat required', NULL, 1, CAST(N'2018-05-11 18:23:26.753' AS DateTime), CAST(N'2018-05-11 18:23:26.753' AS DateTime))
INSERT [dbo].[SpecialRequirement] ([ID], [Name], [Type], [ClubID], [CreatedDate], [UpdatedDate]) VALUES (3, N'No stairs (elevator/escalator required)', NULL, 1, CAST(N'2018-05-11 18:23:34.793' AS DateTime), CAST(N'2018-05-11 18:23:34.793' AS DateTime))
INSERT [dbo].[SpecialRequirement] ([ID], [Name], [Type], [ClubID], [CreatedDate], [UpdatedDate]) VALUES (4, N'Out of the sun', NULL, 1, CAST(N'2018-05-11 18:23:41.460' AS DateTime), CAST(N'2018-05-11 18:23:41.460' AS DateTime))
INSERT [dbo].[SpecialRequirement] ([ID], [Name], [Type], [ClubID], [CreatedDate], [UpdatedDate]) VALUES (5, N'Close to an aisle', NULL, 1, CAST(N'2018-05-11 18:23:47.183' AS DateTime), CAST(N'2018-05-11 18:23:47.183' AS DateTime))
INSERT [dbo].[SpecialRequirement] ([ID], [Name], [Type], [ClubID], [CreatedDate], [UpdatedDate]) VALUES (6, N'Fear of heights', NULL, 1, CAST(N'2018-05-11 18:23:52.643' AS DateTime), CAST(N'2018-05-11 18:23:52.643' AS DateTime))
INSERT [dbo].[SpecialRequirement] ([ID], [Name], [Type], [ClubID], [CreatedDate], [UpdatedDate]) VALUES (7, N'Other (Please specify)', NULL, 1, CAST(N'2018-05-11 18:23:58.807' AS DateTime), CAST(N'2018-05-11 18:23:58.807' AS DateTime))
INSERT [dbo].[SpecialRequirement] ([ID], [Name], [Type], [ClubID], [CreatedDate], [UpdatedDate]) VALUES (8, N'None of the above', NULL, 1, CAST(N'2018-05-11 18:24:06.387' AS DateTime), CAST(N'2018-05-11 18:24:06.387' AS DateTime))
SET IDENTITY_INSERT [dbo].[SpecialRequirement] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [UX_MemberRegistrationToken_Token]    Script Date: 17/05/2018 9:16:26 PM ******/
ALTER TABLE [dbo].[MemberRegistrationToken] ADD  CONSTRAINT [UX_MemberRegistrationToken_Token] UNIQUE NONCLUSTERED 
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClubInfo] ADD  CONSTRAINT [DF_ClubInfo_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ClubInfo] ADD  CONSTRAINT [DF_ClubInfo_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_GroupType]  DEFAULT ('Group') FOR [GroupType]
GO
ALTER TABLE [dbo].[Group] ADD  CONSTRAINT [DF_Group_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MemberGroup] ADD  CONSTRAINT [DF_MemberGroup_IsGroupManager]  DEFAULT ((0)) FOR [IsGroupManager]
GO
ALTER TABLE [dbo].[MemberGroup] ADD  CONSTRAINT [DF_MemberGroup_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MemberGroup] ADD  CONSTRAINT [DF_MemberGroup_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[SeatingPref] ADD  CONSTRAINT [DF_SeatingPrefence_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[SeatingPref] ADD  CONSTRAINT [DF_SeatingPref_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[ClubInfo]  WITH CHECK ADD  CONSTRAINT [FK_ClubInfo_Club] FOREIGN KEY([ClubID])
REFERENCES [dbo].[Club] ([ID])
GO
ALTER TABLE [dbo].[ClubInfo] CHECK CONSTRAINT [FK_ClubInfo_Club]
GO
ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Member_Club] FOREIGN KEY([ClubID])
REFERENCES [dbo].[Club] ([ID])
GO
ALTER TABLE [dbo].[Member] CHECK CONSTRAINT [FK_Member_Club]
GO
ALTER TABLE [dbo].[MemberClaim]  WITH CHECK ADD  CONSTRAINT [FK_MemberClaim_Member] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MemberClaim] CHECK CONSTRAINT [FK_MemberClaim_Member]
GO
ALTER TABLE [dbo].[MemberDetail]  WITH CHECK ADD  CONSTRAINT [FK_MemberDetail_Member] FOREIGN KEY([ID])
REFERENCES [dbo].[Member] ([ID])
GO
ALTER TABLE [dbo].[MemberDetail] CHECK CONSTRAINT [FK_MemberDetail_Member]
GO
ALTER TABLE [dbo].[MemberGroup]  WITH CHECK ADD  CONSTRAINT [FK_MemberGroup_Group] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([ID])
GO
ALTER TABLE [dbo].[MemberGroup] CHECK CONSTRAINT [FK_MemberGroup_Group]
GO
ALTER TABLE [dbo].[MemberGroup]  WITH CHECK ADD  CONSTRAINT [FK_MemberGroup_Member] FOREIGN KEY([MemberID])
REFERENCES [dbo].[Member] ([ID])
GO
ALTER TABLE [dbo].[MemberGroup] CHECK CONSTRAINT [FK_MemberGroup_Member]
GO
ALTER TABLE [dbo].[MemberLogin]  WITH CHECK ADD  CONSTRAINT [FK_MemberLogin_Member] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MemberLogin] CHECK CONSTRAINT [FK_MemberLogin_Member]
GO
ALTER TABLE [dbo].[MemberRole]  WITH CHECK ADD  CONSTRAINT [FK_MemberRole_Member] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MemberRole] CHECK CONSTRAINT [FK_MemberRole_Member]
GO
ALTER TABLE [dbo].[MemberRole]  WITH CHECK ADD  CONSTRAINT [FK_MemberRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MemberRole] CHECK CONSTRAINT [FK_MemberRole_Role]
GO
ALTER TABLE [dbo].[SeatingPref]  WITH CHECK ADD  CONSTRAINT [FK_SeatingPrefence_Club] FOREIGN KEY([ClubID])
REFERENCES [dbo].[Club] ([ID])
GO
ALTER TABLE [dbo].[SeatingPref] CHECK CONSTRAINT [FK_SeatingPrefence_Club]
GO
ALTER TABLE [dbo].[SpecialRequirement]  WITH CHECK ADD  CONSTRAINT [FK_SpecialRequirement_Club] FOREIGN KEY([ClubID])
REFERENCES [dbo].[Club] ([ID])
GO
ALTER TABLE [dbo].[SpecialRequirement] CHECK CONSTRAINT [FK_SpecialRequirement_Club]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Saved custom style for each club' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Club', @level2type=N'COLUMN',@level2name=N'Logo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ex: /login' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ClubInfo', @level2type=N'COLUMN',@level2name=N'Path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Pending or Submited' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Single or Group' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Group', @level2type=N'COLUMN',@level2name=N'GroupType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: not manager, 1: manager' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MemberGroup', @level2type=N'COLUMN',@level2name=N'IsGroupManager'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'None or Attachment or Other' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SpecialRequirement', @level2type=N'COLUMN',@level2name=N'Type'
GO
