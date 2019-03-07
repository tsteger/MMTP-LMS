USE [aspnet-MMTP_LMS-5EEECC18-AFA0-44E1-99CE-4107B79B1534]
GO

/****** Object: Table [dbo].[Document] Script Date: 2019-03-07 10:56:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Document];


GO
CREATE TABLE [dbo].[Document] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (MAX) NULL,
    [Description]   NVARCHAR (MAX) NULL,
    [TimeStamp]     DATETIME2 (7)  NOT NULL,
    [Url]           NVARCHAR (MAX) NULL,
    [PersonId]      NVARCHAR (450) NULL,
    [UserName]      NVARCHAR (MAX) NULL,
    [CourseId]      INT            NULL,
    [LmsActivityId] INT            NULL,
    [ModuleId]      INT            NULL
);


