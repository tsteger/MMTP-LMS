USE [aspnet-MMTP_LMS-5EEECC18-AFA0-44E1-99CE-4107B79B1534]
GO

/****** Object: Table [dbo].[Module] Script Date: 2019-03-07 10:55:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Module];


GO
CREATE TABLE [dbo].[Module] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [Description] NVARCHAR (MAX) NULL,
    [StartDate]   DATETIME2 (7)  NOT NULL,
    [EndDate]     DATETIME2 (7)  NOT NULL,
    [CourseId]    INT            NOT NULL
);


