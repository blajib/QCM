
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/29/2020 13:25:11
-- Generated from EDMX file: C:\Users\dastolfi\source\repos\QCMApp\QCMApp\ModelQCM2.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [QCMAppBDD];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Elements_Choixes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Elements] DROP CONSTRAINT [FK_Elements_Choixes];
GO
IF OBJECT_ID(N'[dbo].[FK_Elements_Questionnaires]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Elements] DROP CONSTRAINT [FK_Elements_Questionnaires];
GO
IF OBJECT_ID(N'[dbo].[FK_ElementTypeElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Elements] DROP CONSTRAINT [FK_ElementTypeElement];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupeSiteQuestionnaire_Questionnaires]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupeSiteQuestionnaire] DROP CONSTRAINT [FK_GroupeSiteQuestionnaire_Questionnaires];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupeSiteQuestionnaire_Site]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupeSiteQuestionnaire] DROP CONSTRAINT [FK_GroupeSiteQuestionnaire_Site];
GO
IF OBJECT_ID(N'[dbo].[FK_UtilisateurReponse]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Reponses] DROP CONSTRAINT [FK_UtilisateurReponse];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Choixes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Choixes];
GO
IF OBJECT_ID(N'[dbo].[Elements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Elements];
GO
IF OBJECT_ID(N'[dbo].[GroupeSiteQuestionnaire]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupeSiteQuestionnaire];
GO
IF OBJECT_ID(N'[dbo].[Questionnaires]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Questionnaires];
GO
IF OBJECT_ID(N'[dbo].[Reponses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Reponses];
GO
IF OBJECT_ID(N'[dbo].[Site]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Site];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TypeElements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeElements];
GO
IF OBJECT_ID(N'[dbo].[Utilisateurs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Utilisateurs];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Choixes'
CREATE TABLE [dbo].[Choixes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [intitule] varchar(200)  NOT NULL,
    [statut] bit  NOT NULL,
    [media] varchar(300)  NULL
);
GO

-- Creating table 'Elements'
CREATE TABLE [dbo].[Elements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [intitule] varchar(max)  NOT NULL,
    [reponses] varchar(150)  NULL,
    [media] varchar(300)  NULL,
    [couleur] varchar(50)  NULL,
    [doc] varchar(max)  NULL,
    [ChoixId] int  NULL,
    [TypeElement_Id] int  NULL,
    [questionnaire_id] int  NULL,
    [texte] varchar(max)  NULL,
    [ordre] int  NULL
);
GO

-- Creating table 'GroupeSiteQuestionnaire'
CREATE TABLE [dbo].[GroupeSiteQuestionnaire] (
    [Id] int  NOT NULL,
    [site_id] int  NULL,
    [questionnaire_id] int  NULL
);
GO

-- Creating table 'Questionnaires'
CREATE TABLE [dbo].[Questionnaires] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [intitule] varchar(100)  NOT NULL,
    [couleur] varchar(50)  NULL,
    [note] int  NULL,
    [date] datetime  NULL,
    [actif] int  NULL
);
GO

-- Creating table 'Reponses'
CREATE TABLE [dbo].[Reponses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [idQuestionnaire] int  NOT NULL,
    [intituleQuestionnaire] varchar(100)  NOT NULL,
    [idElement] int  NOT NULL,
    [intituleElement] varchar(100)  NOT NULL,
    [idChoix] int  NOT NULL,
    [intituleChoix] varchar(200)  NOT NULL,
    [statut] bit  NOT NULL,
    [date] datetime  NOT NULL,
    [UtilisateurId] int  NOT NULL
);
GO

-- Creating table 'Site'
CREATE TABLE [dbo].[Site] (
    [Id] int  NOT NULL,
    [nom] varchar(100)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'TypeElements'
CREATE TABLE [dbo].[TypeElements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nom] varchar(50)  NOT NULL
);
GO

-- Creating table 'Utilisateurs'
CREATE TABLE [dbo].[Utilisateurs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [nom] varchar(100)  NOT NULL,
    [prenom] varchar(100)  NOT NULL,
    [note] float  NULL,
    [validation] bigint  NULL,
    [role] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Choixes'
ALTER TABLE [dbo].[Choixes]
ADD CONSTRAINT [PK_Choixes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Elements'
ALTER TABLE [dbo].[Elements]
ADD CONSTRAINT [PK_Elements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GroupeSiteQuestionnaire'
ALTER TABLE [dbo].[GroupeSiteQuestionnaire]
ADD CONSTRAINT [PK_GroupeSiteQuestionnaire]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Questionnaires'
ALTER TABLE [dbo].[Questionnaires]
ADD CONSTRAINT [PK_Questionnaires]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Reponses'
ALTER TABLE [dbo].[Reponses]
ADD CONSTRAINT [PK_Reponses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Site'
ALTER TABLE [dbo].[Site]
ADD CONSTRAINT [PK_Site]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'TypeElements'
ALTER TABLE [dbo].[TypeElements]
ADD CONSTRAINT [PK_TypeElements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Utilisateurs'
ALTER TABLE [dbo].[Utilisateurs]
ADD CONSTRAINT [PK_Utilisateurs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ChoixId] in table 'Elements'
ALTER TABLE [dbo].[Elements]
ADD CONSTRAINT [FK_Elements_Choixes]
    FOREIGN KEY ([ChoixId])
    REFERENCES [dbo].[Choixes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Elements_Choixes'
CREATE INDEX [IX_FK_Elements_Choixes]
ON [dbo].[Elements]
    ([ChoixId]);
GO

-- Creating foreign key on [questionnaire_id] in table 'Elements'
ALTER TABLE [dbo].[Elements]
ADD CONSTRAINT [FK_Elements_Questionnaires]
    FOREIGN KEY ([questionnaire_id])
    REFERENCES [dbo].[Questionnaires]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Elements_Questionnaires'
CREATE INDEX [IX_FK_Elements_Questionnaires]
ON [dbo].[Elements]
    ([questionnaire_id]);
GO

-- Creating foreign key on [TypeElement_Id] in table 'Elements'
ALTER TABLE [dbo].[Elements]
ADD CONSTRAINT [FK_ElementTypeElement]
    FOREIGN KEY ([TypeElement_Id])
    REFERENCES [dbo].[TypeElements]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ElementTypeElement'
CREATE INDEX [IX_FK_ElementTypeElement]
ON [dbo].[Elements]
    ([TypeElement_Id]);
GO

-- Creating foreign key on [questionnaire_id] in table 'GroupeSiteQuestionnaire'
ALTER TABLE [dbo].[GroupeSiteQuestionnaire]
ADD CONSTRAINT [FK_GroupeSiteQuestionnaire_Questionnaires]
    FOREIGN KEY ([questionnaire_id])
    REFERENCES [dbo].[Questionnaires]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupeSiteQuestionnaire_Questionnaires'
CREATE INDEX [IX_FK_GroupeSiteQuestionnaire_Questionnaires]
ON [dbo].[GroupeSiteQuestionnaire]
    ([questionnaire_id]);
GO

-- Creating foreign key on [site_id] in table 'GroupeSiteQuestionnaire'
ALTER TABLE [dbo].[GroupeSiteQuestionnaire]
ADD CONSTRAINT [FK_GroupeSiteQuestionnaire_Site]
    FOREIGN KEY ([site_id])
    REFERENCES [dbo].[Site]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupeSiteQuestionnaire_Site'
CREATE INDEX [IX_FK_GroupeSiteQuestionnaire_Site]
ON [dbo].[GroupeSiteQuestionnaire]
    ([site_id]);
GO

-- Creating foreign key on [UtilisateurId] in table 'Reponses'
ALTER TABLE [dbo].[Reponses]
ADD CONSTRAINT [FK_UtilisateurReponse]
    FOREIGN KEY ([UtilisateurId])
    REFERENCES [dbo].[Utilisateurs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UtilisateurReponse'
CREATE INDEX [IX_FK_UtilisateurReponse]
ON [dbo].[Reponses]
    ([UtilisateurId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------