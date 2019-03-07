USE [aspnet-MMTP_LMS-5EEECC18-AFA0-44E1-99CE-4107B79B1534]
GO

/****** Object: Table [dbo].[LmsActivity] Script Date: 2019-03-07 10:55:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[LmsActivity];


GO
CREATE TABLE [dbo].[LmsActivity] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Name]              NVARCHAR (MAX) NULL,
    [Description]       NVARCHAR (MAX) NULL,
    [StartDate]         DATETIME2 (7)  NOT NULL,
    [EndTime]           DATETIME2 (7)  NOT NULL,
    [LmsActivityTypeId] INT            NOT NULL,
    [ModuleId]          INT            NOT NULL
);


