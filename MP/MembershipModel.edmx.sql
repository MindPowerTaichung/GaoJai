
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/08/2015 16:13:06
-- Generated from EDMX file: C:\Users\Administrator\Documents\GitHub\MPERP2015\MP\MembershipModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MPERP2015];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RoleFeatures_Menus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleFeatures] DROP CONSTRAINT [FK_RoleFeatures_Menus];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleFeatures_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleFeatures] DROP CONSTRAINT [FK_RoleFeatures_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_RoleUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Menus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Menus];
GO
IF OBJECT_ID(N'[dbo].[RoleFeatures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleFeatures];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Timestamp] timestamp  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserName] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Timestamp] timestamp  NOT NULL,
    [Role_Id] int  NULL
);
GO

-- Creating table 'Menus'
CREATE TABLE [dbo].[Menus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(50)  NOT NULL,
    [ContentUrl] varchar(150)  NULL,
    [ParentId] int  NOT NULL,
    [CssClass] varchar(50)  NULL,
    [Timestamp] timestamp  NOT NULL
);
GO

-- Creating table 'RoleFeatures'
CREATE TABLE [dbo].[RoleFeatures] (
    [Menus1_Id] int  NOT NULL,
    [Roles1_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserName] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserName] ASC);
GO

-- Creating primary key on [Id] in table 'Menus'
ALTER TABLE [dbo].[Menus]
ADD CONSTRAINT [PK_Menus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Menus1_Id], [Roles1_Id] in table 'RoleFeatures'
ALTER TABLE [dbo].[RoleFeatures]
ADD CONSTRAINT [PK_RoleFeatures]
    PRIMARY KEY CLUSTERED ([Menus1_Id], [Roles1_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Role_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_RoleUser]
    FOREIGN KEY ([Role_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleUser'
CREATE INDEX [IX_FK_RoleUser]
ON [dbo].[Users]
    ([Role_Id]);
GO

-- Creating foreign key on [Menus1_Id] in table 'RoleFeatures'
ALTER TABLE [dbo].[RoleFeatures]
ADD CONSTRAINT [FK_RoleFeatures_Menu]
    FOREIGN KEY ([Menus1_Id])
    REFERENCES [dbo].[Menus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Roles1_Id] in table 'RoleFeatures'
ALTER TABLE [dbo].[RoleFeatures]
ADD CONSTRAINT [FK_RoleFeatures_Role]
    FOREIGN KEY ([Roles1_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleFeatures_Role'
CREATE INDEX [IX_FK_RoleFeatures_Role]
ON [dbo].[RoleFeatures]
    ([Roles1_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------