USE [QCMAppBDD]
GO
ALTER TABLE [dbo].[Reponses] DROP CONSTRAINT [FK_UtilisateurReponse]
GO
ALTER TABLE [dbo].[GroupeSiteQuestionnaire] DROP CONSTRAINT [FK_GroupeSiteQuestionnaire_Site]
GO
ALTER TABLE [dbo].[GroupeSiteQuestionnaire] DROP CONSTRAINT [FK_GroupeSiteQuestionnaire_Questionnaires]
GO
ALTER TABLE [dbo].[Elements] DROP CONSTRAINT [FK_ElementTypeElement]
GO
ALTER TABLE [dbo].[Elements] DROP CONSTRAINT [FK_Elements_Questionnaires]
GO
ALTER TABLE [dbo].[Choixes] DROP CONSTRAINT [FK_Choixes_Elements]
GO
/****** Object:  Table [dbo].[Utilisateurs]    Script Date: 08/10/2020 11:06:44 ******/
DROP TABLE [dbo].[Utilisateurs]
GO
/****** Object:  Table [dbo].[TypeElements]    Script Date: 08/10/2020 11:06:44 ******/
DROP TABLE [dbo].[TypeElements]
GO
/****** Object:  Table [dbo].[Site]    Script Date: 08/10/2020 11:06:44 ******/
DROP TABLE [dbo].[Site]
GO
/****** Object:  Table [dbo].[Reponses]    Script Date: 08/10/2020 11:06:44 ******/
DROP TABLE [dbo].[Reponses]
GO
/****** Object:  Table [dbo].[Questionnaires]    Script Date: 08/10/2020 11:06:44 ******/
DROP TABLE [dbo].[Questionnaires]
GO
/****** Object:  Table [dbo].[GroupeSiteQuestionnaire]    Script Date: 08/10/2020 11:06:44 ******/
DROP TABLE [dbo].[GroupeSiteQuestionnaire]
GO
/****** Object:  Table [dbo].[Elements]    Script Date: 08/10/2020 11:06:44 ******/
DROP TABLE [dbo].[Elements]
GO
/****** Object:  Table [dbo].[Choixes]    Script Date: 08/10/2020 11:06:44 ******/
DROP TABLE [dbo].[Choixes]
GO
/****** Object:  Table [dbo].[Choixes]    Script Date: 08/10/2020 11:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Choixes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[intitule] [varchar](200) NULL,
	[statut] [bit] NULL,
	[media] [varchar](300) NULL,
	[element_id] [int] NULL,
 CONSTRAINT [PK_Choixes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Elements]    Script Date: 08/10/2020 11:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Elements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[intitule] [varchar](max) NULL,
	[reponses] [varchar](150) NULL,
	[media] [varchar](300) NULL,
	[couleur] [varchar](50) NULL,
	[doc] [varchar](max) NULL,
	[ChoixId] [int] NULL,
	[TypeElement_Id] [int] NULL,
	[questionnaire_id] [int] NULL,
	[texte] [varchar](max) NULL,
	[ordre] [int] NULL,
 CONSTRAINT [PK_Elements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupeSiteQuestionnaire]    Script Date: 08/10/2020 11:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupeSiteQuestionnaire](
	[site_id] [int] NOT NULL,
	[questionnaire_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[site_id] ASC,
	[questionnaire_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questionnaires]    Script Date: 08/10/2020 11:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questionnaires](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[intitule] [varchar](100) NOT NULL,
	[couleur] [varchar](50) NULL,
	[note] [int] NULL,
	[date] [datetime] NULL,
	[actif] [bit] NULL,
 CONSTRAINT [PK_Questionnaires] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[intitule] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reponses]    Script Date: 08/10/2020 11:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reponses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idQuestionnaire] [int] NOT NULL,
	[intituleQuestionnaire] [varchar](100) NOT NULL,
	[idElement] [int] NOT NULL,
	[intituleElement] [varchar](100) NOT NULL,
	[idChoix] [int] NOT NULL,
	[intituleChoix] [varchar](200) NOT NULL,
	[statut] [bit] NOT NULL,
	[date] [datetime] NOT NULL,
	[UtilisateurId] [int] NOT NULL,
 CONSTRAINT [PK_Reponses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Site]    Script Date: 08/10/2020 11:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Site](
	[Id] [int] NOT NULL,
	[nom] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeElements]    Script Date: 08/10/2020 11:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeElements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TypeElements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utilisateurs]    Script Date: 08/10/2020 11:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utilisateurs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[nom] [varchar](100) NOT NULL,
	[prenom] [varchar](100) NOT NULL,
	[note] [float] NULL,
	[validation] [bigint] NULL,
	[role] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Utilisateurs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Choixes]  WITH CHECK ADD  CONSTRAINT [FK_Choixes_Elements] FOREIGN KEY([element_id])
REFERENCES [dbo].[Elements] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Choixes] CHECK CONSTRAINT [FK_Choixes_Elements]
GO
ALTER TABLE [dbo].[Elements]  WITH CHECK ADD  CONSTRAINT [FK_Elements_Questionnaires] FOREIGN KEY([questionnaire_id])
REFERENCES [dbo].[Questionnaires] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Elements] CHECK CONSTRAINT [FK_Elements_Questionnaires]
GO
ALTER TABLE [dbo].[Elements]  WITH CHECK ADD  CONSTRAINT [FK_ElementTypeElement] FOREIGN KEY([TypeElement_Id])
REFERENCES [dbo].[TypeElements] ([Id])
GO
ALTER TABLE [dbo].[Elements] CHECK CONSTRAINT [FK_ElementTypeElement]
GO
ALTER TABLE [dbo].[GroupeSiteQuestionnaire]  WITH CHECK ADD  CONSTRAINT [FK_GroupeSiteQuestionnaire_Questionnaires] FOREIGN KEY([questionnaire_id])
REFERENCES [dbo].[Questionnaires] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GroupeSiteQuestionnaire] CHECK CONSTRAINT [FK_GroupeSiteQuestionnaire_Questionnaires]
GO
ALTER TABLE [dbo].[GroupeSiteQuestionnaire]  WITH CHECK ADD  CONSTRAINT [FK_GroupeSiteQuestionnaire_Site] FOREIGN KEY([site_id])
REFERENCES [dbo].[Site] ([Id])
GO
ALTER TABLE [dbo].[GroupeSiteQuestionnaire] CHECK CONSTRAINT [FK_GroupeSiteQuestionnaire_Site]
GO
ALTER TABLE [dbo].[Reponses]  WITH CHECK ADD  CONSTRAINT [FK_UtilisateurReponse] FOREIGN KEY([UtilisateurId])
REFERENCES [dbo].[Utilisateurs] ([Id])
GO
ALTER TABLE [dbo].[Reponses] CHECK CONSTRAINT [FK_UtilisateurReponse]
GO

USE [QCMAppBDD]
GO
SET IDENTITY_INSERT [dbo].[Questionnaires] ON 
GO
INSERT [dbo].[Questionnaires] ([Id], [intitule], [couleur], [note], [date], [actif]) VALUES (1, N'Questionnaire hygiène', NULL, 16, CAST(N'2020-10-08T13:30:03.763' AS DateTime), 1)
GO
INSERT [dbo].[Questionnaires] ([Id], [intitule], [couleur], [note], [date], [actif]) VALUES (2, N'Questionnaire culture générale', NULL, NULL, CAST(N'2020-10-08T13:30:47.333' AS DateTime), 0)
GO
INSERT [dbo].[Questionnaires] ([Id], [intitule], [couleur], [note], [date], [actif]) VALUES (3, N'Question générale sur la SGT', NULL, 16, CAST(N'2020-10-08T13:34:51.417' AS DateTime), 1)
GO
INSERT [dbo].[Questionnaires] ([Id], [intitule], [couleur], [note], [date], [actif]) VALUES (5, N'Entrée dans l''entreprise', NULL, 16, CAST(N'2020-10-08T13:36:26.730' AS DateTime), NULL)
GO
INSERT [dbo].[Questionnaires] ([Id], [intitule], [couleur], [note], [date], [actif]) VALUES (6, N'Gérer les stocks', NULL, 16, CAST(N'2020-10-08T13:40:42.520' AS DateTime), 1)
GO
INSERT [dbo].[Questionnaires] ([Id], [intitule], [couleur], [note], [date], [actif]) VALUES (8, N'super questionnaire', NULL, NULL, CAST(N'2020-01-01T00:00:00.000' AS DateTime), 1)
GO
INSERT [dbo].[Questionnaires] ([Id], [intitule], [couleur], [note], [date], [actif]) VALUES (9, N'Sécurité', NULL, NULL, CAST(N'2020-02-03T00:00:00.000' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Questionnaires] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeElements] ON 
GO
INSERT [dbo].[TypeElements] ([Id], [nom]) VALUES (1, N'description')
GO
INSERT [dbo].[TypeElements] ([Id], [nom]) VALUES (2, N'question')
GO
SET IDENTITY_INSERT [dbo].[TypeElements] OFF
GO
SET IDENTITY_INSERT [dbo].[Elements] ON 
GO
INSERT [dbo].[Elements] ([Id], [intitule], [reponses], [media], [couleur], [doc], [ChoixId], [TypeElement_Id], [questionnaire_id], [texte], [ordre]) VALUES (1, N'Port du masque', NULL, NULL, NULL, NULL, NULL, 2, 1, N'Le port du masque est obligatoire ?', 3)
GO
INSERT [dbo].[Elements] ([Id], [intitule], [reponses], [media], [couleur], [doc], [ChoixId], [TypeElement_Id], [questionnaire_id], [texte], [ordre]) VALUES (2, N'Port du masque', NULL, NULL, NULL, NULL, NULL, 1, 1, N'Le port du masque est obligatoire dans les lieux publics en ville, au travail excepté dans un bureau privé, au cinéma.', 2)
GO
INSERT [dbo].[Elements] ([Id], [intitule], [reponses], [media], [couleur], [doc], [ChoixId], [TypeElement_Id], [questionnaire_id], [texte], [ordre]) VALUES (3, N'Symptomes', NULL, NULL, NULL, NULL, NULL, 1, 1, N'Si fièvre, toux, problème respiratoire, la personne doit absolument resté chez elle, prendre rdv avec un médecin et contacté son employeur', 4)
GO
INSERT [dbo].[Elements] ([Id], [intitule], [reponses], [media], [couleur], [doc], [ChoixId], [TypeElement_Id], [questionnaire_id], [texte], [ordre]) VALUES (4, N'Symptômes', NULL, NULL, NULL, NULL, NULL, 2, 1, N'Je ne me sens pas bien, je suis fatigué et je tousse', 5)
GO
INSERT [dbo].[Elements] ([Id], [intitule], [reponses], [media], [couleur], [doc], [ChoixId], [TypeElement_Id], [questionnaire_id], [texte], [ordre]) VALUES (5, N'Contact avec les autres', NULL, NULL, NULL, NULL, NULL, 1, 1, N'', 6)
GO
INSERT [dbo].[Elements] ([Id], [intitule], [reponses], [media], [couleur], [doc], [ChoixId], [TypeElement_Id], [questionnaire_id], [texte], [ordre]) VALUES (6, N'Contact avec les autres', NULL, NULL, NULL, NULL, NULL, 2, 1, NULL, 7)
GO
SET IDENTITY_INSERT [dbo].[Elements] OFF
GO
SET IDENTITY_INSERT [dbo].[Choixes] ON 
GO
INSERT [dbo].[Choixes] ([Id], [intitule], [statut], [media], [element_id]) VALUES (1, N'Au cinéma', 1, NULL, 1)
GO
INSERT [dbo].[Choixes] ([Id], [intitule], [statut], [media], [element_id]) VALUES (2, N'En ville', 1, NULL, 1)
GO
INSERT [dbo].[Choixes] ([Id], [intitule], [statut], [media], [element_id]) VALUES (3, N'Chez soi', 0, NULL, 1)
GO
INSERT [dbo].[Choixes] ([Id], [intitule], [statut], [media], [element_id]) VALUES (4, N'Je vais quand même au travail car il faut travailler.', 0, NULL, 4)
GO
INSERT [dbo].[Choixes] ([Id], [intitule], [statut], [media], [element_id]) VALUES (5, N'J''appelle mon docteur', 0, NULL, 4)
GO
INSERT [dbo].[Choixes] ([Id], [intitule], [statut], [media], [element_id]) VALUES (6, N'Je sors dans la rue sans masque', 0, NULL, 4)
GO
INSERT [dbo].[Choixes] ([Id], [intitule], [statut], [media], [element_id]) VALUES (7, N'Je reste chez moi et je ne contacte personne', 0, NULL, 4)
GO
SET IDENTITY_INSERT [dbo].[Choixes] OFF
GO
INSERT [dbo].[Site] ([Id], [nom]) VALUES (1, N'SGT')
GO
INSERT [dbo].[Site] ([Id], [nom]) VALUES (2, N'MGP')
GO
INSERT [dbo].[Site] ([Id], [nom]) VALUES (3, N'CHA')
GO
INSERT [dbo].[GroupeSiteQuestionnaire] ([site_id], [questionnaire_id]) VALUES (1, 3)
GO
INSERT [dbo].[GroupeSiteQuestionnaire] ([site_id], [questionnaire_id]) VALUES (2, 2)
GO
INSERT [dbo].[GroupeSiteQuestionnaire] ([site_id], [questionnaire_id]) VALUES (2, 6)
GO
INSERT [dbo].[GroupeSiteQuestionnaire] ([site_id], [questionnaire_id]) VALUES (3, 1)
GO
INSERT [dbo].[GroupeSiteQuestionnaire] ([site_id], [questionnaire_id]) VALUES (3, 5)
GO
INSERT [dbo].[GroupeSiteQuestionnaire] ([site_id], [questionnaire_id]) VALUES (3, 8)
GO
