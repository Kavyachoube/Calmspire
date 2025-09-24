-- Users Table
CREATE TABLE [dbo].[Users] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(255) NOT NULL UNIQUE,
    [PasswordHash] NVARCHAR(255) NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE()
);

-- MoodEntries Table
CREATE TABLE [dbo].[MoodEntries] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [Mood] NVARCHAR(50) NOT NULL,
    [EntryDate] DATE NOT NULL DEFAULT GETDATE(),
    [Notes] NVARCHAR(MAX) NULL,
    CONSTRAINT FK_MoodEntries_Users FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE,
    CONSTRAINT UQ_User_Date UNIQUE ([UserId], [EntryDate])
);

-- JournalEntries Table
CREATE TABLE [dbo].[JournalEntries] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [Title] NVARCHAR(200) NOT NULL,
    [Content] NVARCHAR(MAX) NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_JournalEntries_Users FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);

-- Assessments Table
CREATE TABLE [dbo].[Assessments] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Title] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [QuestionsJson] NVARCHAR(MAX) NOT NULL
);

-- AssessmentResults Table
CREATE TABLE [dbo].[AssessmentResults] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [AssessmentId] INT NOT NULL,
    [Score] INT NOT NULL,
    [ResultJson] NVARCHAR(MAX) NULL,
    [TakenAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_AssessmentResults_Users FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE,
    CONSTRAINT FK_AssessmentResults_Assessments FOREIGN KEY ([AssessmentId]) REFERENCES [dbo].[Assessments]([Id]) ON DELETE CASCADE
);

-- ChatMessages Table
CREATE TABLE [dbo].[ChatMessages] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [Sender] NVARCHAR(20) NOT NULL, -- "User" / "Bot"
    [Message] NVARCHAR(MAX) NOT NULL,
    [SentAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ChatMessages_Users FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);

-- GratitudeEntries Table
CREATE TABLE [dbo].[GratitudeEntries] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL,
    [Entry] NVARCHAR(MAX) NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_GratitudeEntries_Users FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);
