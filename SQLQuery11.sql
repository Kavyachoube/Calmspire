-- Drop tables if exist
DROP TABLE IF EXISTS [dbo].[ChatMessages];
DROP TABLE IF EXISTS [dbo].[GratitudeEntries];
DROP TABLE IF EXISTS [dbo].[JournalEntries];
DROP TABLE IF EXISTS [dbo].[MoodEntries];
DROP TABLE IF EXISTS [dbo].[Users];

-- Users table
CREATE TABLE [dbo].[Users] (
    [Id]           INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [FirstName]    NVARCHAR(100) NOT NULL,
    [LastName]     NVARCHAR(100) NOT NULL,
    [Email]        NVARCHAR(255) NOT NULL UNIQUE,
    [PasswordHash] NVARCHAR(MAX) NOT NULL,
    [CreatedAt]    DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    [LastLoginAt]  DATETIME2(7) NULL
);

-- MoodEntries table
CREATE TABLE [dbo].[MoodEntries] (
    [Id]        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId]    INT NOT NULL,
    [MoodScore] INT NOT NULL,
    [Notes]     NVARCHAR(500) NULL,
    [EntryDate] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    [CreatedAt] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    CONSTRAINT [UQ_MoodEntries_User_EntryDate] UNIQUE NONCLUSTERED ([UserId], [EntryDate]),
    CONSTRAINT [FK_MoodEntries_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);

-- JournalEntries table
CREATE TABLE [dbo].[JournalEntries] (
    [Id]        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId]    INT NOT NULL,
    [Title]     NVARCHAR(200) NOT NULL,
    [Content]   NVARCHAR(MAX) NOT NULL,
    [CreatedAt] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    [UpdatedAt] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    CONSTRAINT [FK_JournalEntries_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);

-- GratitudeEntries table
CREATE TABLE [dbo].[GratitudeEntries] (
    [Id]        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId]    INT NOT NULL,
    [Content]   NVARCHAR(500) NOT NULL,
    [EntryDate] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    [CreatedAt] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    CONSTRAINT [UQ_GratitudeEntries_User_EntryDate] UNIQUE NONCLUSTERED ([UserId], [EntryDate]),
    CONSTRAINT [FK_GratitudeEntries_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);

-- ChatMessages table
CREATE TABLE [dbo].[ChatMessages] (
    [Id]        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId]    INT NOT NULL,
    [Message]   NVARCHAR(MAX) NOT NULL,
    [Response]  NVARCHAR(MAX) NOT NULL,
    [CreatedAt] DATETIME2(7) DEFAULT (GETUTCDATE()) NOT NULL,
    CONSTRAINT [FK_ChatMessages_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]) ON DELETE CASCADE
);
