/* ============================
   STEP 1: DROP FOREIGN KEYS (Safe)
============================ */

-- Drop FK in ChatMessages
IF OBJECT_ID('FK_ChatMessages_Users', 'F') IS NOT NULL
    ALTER TABLE dbo.ChatMessages DROP CONSTRAINT FK_ChatMessages_Users;

-- Drop FK in GratitudeEntries
IF OBJECT_ID('FK_GratitudeEntries_Users', 'F') IS NOT NULL
    ALTER TABLE dbo.GratitudeEntries DROP CONSTRAINT FK_GratitudeEntries_Users;

-- Drop FK in JournalEntries
IF OBJECT_ID('FK_JournalEntries_Users', 'F') IS NOT NULL
    ALTER TABLE dbo.JournalEntries DROP CONSTRAINT FK_JournalEntries_Users;

-- Drop FK in MoodEntries
IF OBJECT_ID('FK_MoodEntries_Users', 'F') IS NOT NULL
    ALTER TABLE dbo.MoodEntries DROP CONSTRAINT FK_MoodEntries_Users;

-- Drop FK in AssessmentResults
IF OBJECT_ID('FK_AssessmentResults_Users', 'F') IS NOT NULL
    ALTER TABLE dbo.AssessmentResults DROP CONSTRAINT FK_AssessmentResults_Users;

IF OBJECT_ID('FK_AssessmentResults_Assessments', 'F') IS NOT NULL
    ALTER TABLE dbo.AssessmentResults DROP CONSTRAINT FK_AssessmentResults_Assessments;


/* ============================
   STEP 2: DROP TABLES (Child First, Parent Last)
============================ */
IF OBJECT_ID('dbo.AssessmentResults', 'U') IS NOT NULL DROP TABLE dbo.AssessmentResults;
IF OBJECT_ID('dbo.ChatMessages', 'U') IS NOT NULL DROP TABLE dbo.ChatMessages;
IF OBJECT_ID('dbo.GratitudeEntries', 'U') IS NOT NULL DROP TABLE dbo.GratitudeEntries;
IF OBJECT_ID('dbo.JournalEntries', 'U') IS NOT NULL DROP TABLE dbo.JournalEntries;
IF OBJECT_ID('dbo.MoodEntries', 'U') IS NOT NULL DROP TABLE dbo.MoodEntries;
IF OBJECT_ID('dbo.Assessments', 'U') IS NOT NULL DROP TABLE dbo.Assessments;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;


/* ============================
   STEP 3: CREATE TABLES (Parent First)
============================ */

-- 1. Users table
CREATE TABLE [dbo].[Users] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(255) NOT NULL UNIQUE,
    [PasswordHash] NVARCHAR(MAX) NOT NULL,
    [CreatedAt] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    [LastLoginAt] DATETIME2(7) NULL
);

-- 2. Assessments table
CREATE TABLE [dbo].[Assessments] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Title] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(MAX) NOT NULL,
    [QuestionsJson] NVARCHAR(MAX) NOT NULL,
    [CreatedAt] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    [IsActive] BIT DEFAULT 1 NOT NULL
);

-- 3. MoodEntries table
CREATE TABLE [dbo].[MoodEntries] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId] INT NOT NULL,
    [MoodScore] INT NOT NULL,
    [Notes] NVARCHAR(500) NULL,
    [EntryDate] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    [CreatedAt] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    CONSTRAINT [UQ_MoodEntries_User_EntryDate] UNIQUE NONCLUSTERED ([UserId], [EntryDate]),
    CONSTRAINT [FK_MoodEntries_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);

-- 4. JournalEntries table
CREATE TABLE [dbo].[JournalEntries] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId] INT NOT NULL,
    [Title] NVARCHAR(200) NOT NULL,
    [Content] NVARCHAR(MAX) NOT NULL,
    [CreatedAt] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    [UpdatedAt] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    CONSTRAINT [FK_JournalEntries_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);

-- 5. GratitudeEntries table
CREATE TABLE [dbo].[GratitudeEntries] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId] INT NOT NULL,
    [Content] NVARCHAR(500) NOT NULL,
    [EntryDate] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    [CreatedAt] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    CONSTRAINT [UQ_GratitudeEntries_User_EntryDate] UNIQUE NONCLUSTERED ([UserId], [EntryDate]),
    CONSTRAINT [FK_GratitudeEntries_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);

-- 6. ChatMessages table
CREATE TABLE [dbo].[ChatMessages] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId] INT NOT NULL,
    [Message] NVARCHAR(MAX) NOT NULL,
    [Response] NVARCHAR(MAX) NOT NULL,
    [CreatedAt] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    CONSTRAINT [FK_ChatMessages_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);

-- 7. AssessmentResults table
CREATE TABLE [dbo].[AssessmentResults] (
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId] INT NOT NULL,
    [AssessmentId] INT NOT NULL,
    [ResponsesJson] NVARCHAR(MAX) NOT NULL,
    [Score] INT NULL,
    [Interpretation] NVARCHAR(MAX) NULL,
    [CompletedAt] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    CONSTRAINT [FK_AssessmentResults_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AssessmentResults_Assessments] FOREIGN KEY ([AssessmentId]) REFERENCES [dbo].[Assessments]([Id]) ON DELETE CASCADE
);
