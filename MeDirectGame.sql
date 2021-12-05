USE [MeDirect]
GO
/****** Object:  Table [dbo].[GameLights]    Script Date: 5.12.2021 13:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameLights](
	[Id] [uniqueidentifier] NOT NULL,
	[GameSettingId] [uniqueidentifier] NULL,
	[LightOpenX] [int] NULL,
	[LightOpenY] [int] NULL,
 CONSTRAINT [PK_GameLights] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameSettings]    Script Date: 5.12.2021 13:14:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameSettings](
	[Id] [uniqueidentifier] NOT NULL,
	[Size] [int] NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[GameLights] ([Id], [GameSettingId], [LightOpenX], [LightOpenY]) VALUES (N'ad22950a-0c63-4096-9669-2f17d7df08e0', N'3fa85f64-5717-4562-b3fc-2c963f66afa6', 0, 0)
GO
INSERT [dbo].[GameLights] ([Id], [GameSettingId], [LightOpenX], [LightOpenY]) VALUES (N'2abf4231-120b-4a84-8944-c6020c618bae', N'3fa85f64-5717-4562-b3fc-2c963f66afa6', 2, 2)
GO
INSERT [dbo].[GameSettings] ([Id], [Size]) VALUES (N'3fa85f64-5717-4562-b3fc-2c963f66afa6', 4)
GO
