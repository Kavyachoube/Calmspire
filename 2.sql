-- DROP (child tables first) if exist
IF OBJECT_ID('dbo.AssessmentResults', 'U') IS NOT NULL DROP TABLE dbo.AssessmentResults;
IF OBJECT_ID('dbo.MoodEntries', 'U') IS NOT NULL DROP TABLE dbo.MoodEntries;
IF OBJECT_ID('dbo.JournalEntries', 'U') IS NOT NULL DROP TABLE dbo.JournalEntries;
IF OBJECT_ID('dbo.ChatMessages', 'U') IS NOT NULL DROP TABLE dbo.ChatMessages;
IF OBJECT_ID('dbo.GratitudeEntries', 'U') IS NOT NULL DROP TABLE dbo.GratitudeEntries;
IF OBJECT_ID('dbo.Assessments', 'U') IS NOT NULL DROP TABLE dbo.Assessments;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;

-- Create Users
CREATE TABLE dbo.Users (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT (GETUTCDATE()),
    LastLoginAt DATETIME2 NULL
);
CREATE UNIQUE INDEX IX_Users_Email ON dbo.Users (Email);

-- Create Assessments
CREATE TABLE dbo.Assessments (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    QuestionsJson NVARCHAR(MAX) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT (GETUTCDATE())
);

-- Create AssessmentResults
CREATE TABLE dbo.AssessmentResults (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserId INT NOT NULL,
    AssessmentId INT NOT NULL,
    ResponsesJson NVARCHAR(MAX) NOT NULL,
    Score INT NULL,
    Interpretation NVARCHAR(MAX) NULL,
    CompletedAt DATETIME2 NOT NULL DEFAULT (GETUTCDATE()),
    CONSTRAINT FK_AssessmentResults_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(Id) ON DELETE CASCADE,
    CONSTRAINT FK_AssessmentResults_Assessments FOREIGN KEY (AssessmentId) REFERENCES dbo.Assessments(Id) ON DELETE CASCADE
);

-- Create MoodEntries
CREATE TABLE dbo.MoodEntries (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserId INT NOT NULL,
    MoodScore INT NOT NULL,
    Notes NVARCHAR(MAX) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT (GETUTCDATE()),
    CONSTRAINT FK_MoodEntries_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(Id) ON DELETE CASCADE
);
CREATE INDEX IX_MoodEntries_User_CreatedAt ON dbo.MoodEntries(UserId, CreatedAt);

-- Create JournalEntries
CREATE TABLE dbo.JournalEntries (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    UserId INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT (GETUTCDATE()),
    CONSTRAINT FK_JournalEntries_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(Id) ON DELETE CASCADE
);

-- Create ChatMessages
CREATE TABLE dbo.ChatMessages (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserId INT NOT NULL,
    Sender NVARCHAR(20) NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT (GETUTCDATE()),
    CONSTRAINT FK_ChatMessages_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(Id) ON DELETE CASCADE
);

-- Create GratitudeEntries
CREATE TABLE dbo.GratitudeEntries (
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserId INT NOT NULL,
    Content NVARCHAR(1000) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT (GETUTCDATE()),
    CONSTRAINT FK_GratitudeEntries_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(Id) ON DELETE CASCADE
);

-- Done
PRINT 'Schema created (Users, Assessments, AssessmentResults, MoodEntries, JournalEntries, ChatMessages, GratitudeEntries)';
