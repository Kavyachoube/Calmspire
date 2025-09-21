-- Drop existing table if it exists
IF OBJECT_ID('dbo.ChatMessages', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.ChatMessages;
END
GO

-- Create new ChatMessages table
CREATE TABLE dbo.ChatMessages
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserId INT NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    Response NVARCHAR(MAX) NOT NULL,
    CONSTRAINT FK_ChatMessages_User FOREIGN KEY (UserId)
        REFERENCES dbo.Users(Id)  -- <-- Use the correct column name from Users table
        ON DELETE CASCADE
);
GO
