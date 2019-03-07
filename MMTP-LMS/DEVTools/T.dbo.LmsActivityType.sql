USE [aspnet-MMTP_LMS-5EEECC18-AFA0-44E1-99CE-4107B79B1534]
GO

/****** Object: Table [dbo].[LmsActivityType] Script Date: 2019-03-07 10:55:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[LmsActivityType];


GO
CREATE TABLE [dbo].[LmsActivityType] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL
);


