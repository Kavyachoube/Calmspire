-- ========================
-- DROP EXISTING TABLES
-- ========================
IF OBJECT_ID('dbo.AssessmentResults', 'U') IS NOT NULL DROP TABLE dbo.AssessmentResults;
IF OBJECT_ID('dbo.ChatMessages', 'U') IS NOT NULL DROP TABLE dbo.ChatMessages;
IF OBJECT_ID('dbo.MoodEntries', 'U') IS NOT NULL DROP TABLE dbo.MoodEntries;
IF OBJECT_ID('dbo.GratitudeEntries', 'U') IS NOT NULL DROP TABLE dbo.GratitudeEntries;
IF OBJECT_ID('dbo.JournalEntries', 'U') IS NOT NULL DROP TABLE dbo.JournalEntries;
IF OBJECT_ID('dbo.Assessments', 'U') IS NOT NULL DROP TABLE dbo.Assessments;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;

-- ========================
-- CREATE USERS
-- ========================
CREATE TABLE [dbo].[Users] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Username] NVARCHAR(100) NOT NULL UNIQUE,
    [PasswordHash] NVARCHAR(255) NOT NULL,
    [Email] NVARCHAR(150) NOT NULL UNIQUE,
    [CreatedAt] DATETIME2 DEFAULT GETDATE() NOT NULL
);

-- ========================
-- CREATE CHAT MESSAGES
-- ========================
CREATE TABLE [dbo].[ChatMessages] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [Sender] NVARCHAR(50) NOT NULL, -- "user" / "bot"
    [Message] NVARCHAR(MAX) NOT NULL,
    [CreatedAt] DATETIME2 DEFAULT GETDATE() NOT NULL,
    CONSTRAINT FK_ChatMessages_Users FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);

-- ========================
-- CREATE ASSESSMENTS
-- ========================
CREATE TABLE [dbo].[Assessments] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Title] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(500) NOT NULL,
    [QuestionsJson] NVARCHAR(MAX) NOT NULL,
    [IsActive] BIT DEFAULT 1 NOT NULL,
    [CreatedAt] DATETIME2 DEFAULT GETDATE() NOT NULL
);

-- ========================
-- CREATE ASSESSMENT RESULTS
-- ========================
CREATE TABLE [dbo].[AssessmentResults] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [AssessmentId] INT NOT NULL,
    [ResponsesJson] NVARCHAR(MAX) NOT NULL,
    [Score] INT NULL,
    [Interpretation] NVARCHAR(500) NULL,
    [CompletedAt] DATETIME2 DEFAULT GETDATE() NOT NULL,
    CONSTRAINT FK_AssessmentResults_Users FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_AssessmentResults_Assessments FOREIGN KEY ([AssessmentId]) REFERENCES [dbo].[Assessments]([Id]) ON DELETE CASCADE
);

-- ========================
-- CREATE MOOD ENTRIES
-- ========================
CREATE TABLE [dbo].[MoodEntries] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [Mood] NVARCHAR(100) NOT NULL,
    [Note] NVARCHAR(500) NULL,
    [CreatedAt] DATETIME2 DEFAULT GETDATE() NOT NULL,
    CONSTRAINT FK_MoodEntries_Users FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);

-- ========================
-- CREATE GRATITUDE ENTRIES
-- ========================
CREATE TABLE [dbo].[GratitudeEntries] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [Content] NVARCHAR(MAX) NOT NULL,
    [CreatedAt] DATETIME2 DEFAULT GETDATE() NOT NULL,
    CONSTRAINT FK_GratitudeEntries_Users FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);

-- ========================
-- CREATE JOURNAL ENTRIES
-- ========================
CREATE TABLE [dbo].[JournalEntries] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Title] NVARCHAR(200) NOT NULL,
    [Content] NVARCHAR(MAX) NOT NULL,
    [Author] NVARCHAR(150) NULL,
    [CreatedAt] DATETIME2 DEFAULT GETDATE() NOT NULL
);
