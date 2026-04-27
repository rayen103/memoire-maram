CREATE TABLE [Users] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(150) NOT NULL,
    [Email] NVARCHAR(255) NOT NULL UNIQUE,
    [PasswordHash] NVARCHAR(500) NOT NULL,
    [Role] NVARCHAR(20) NOT NULL
);

CREATE TABLE [StudentProfiles] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL UNIQUE,
    [Points] INT NOT NULL DEFAULT 0,
    [Level] INT NOT NULL DEFAULT 1,
    CONSTRAINT [FK_StudentProfiles_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
);

CREATE TABLE [ParentProfiles] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [UserId] INT NOT NULL UNIQUE,
    CONSTRAINT [FK_ParentProfiles_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
);

CREATE TABLE [Quizzes] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Title] NVARCHAR(200) NOT NULL,
    [Level] INT NOT NULL
);

CREATE TABLE [Questions] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [QuizId] INT NOT NULL,
    [Content] NVARCHAR(1000) NOT NULL,
    CONSTRAINT [FK_Questions_Quizzes] FOREIGN KEY ([QuizId]) REFERENCES [Quizzes]([Id]) ON DELETE CASCADE
);

CREATE TABLE [Answers] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [QuestionId] INT NOT NULL,
    [Content] NVARCHAR(1000) NOT NULL,
    [IsCorrect] BIT NOT NULL,
    CONSTRAINT [FK_Answers_Questions] FOREIGN KEY ([QuestionId]) REFERENCES [Questions]([Id]) ON DELETE CASCADE
);

CREATE TABLE [StudentAnswers] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [StudentProfileId] INT NOT NULL,
    [QuestionId] INT NOT NULL,
    [SelectedAnswerId] INT NOT NULL,
    [IsCorrect] BIT NOT NULL,
    [AnsweredAt] DATETIME2 NOT NULL,
    CONSTRAINT [FK_StudentAnswers_StudentProfiles] FOREIGN KEY ([StudentProfileId]) REFERENCES [StudentProfiles]([Id]),
    CONSTRAINT [FK_StudentAnswers_Questions] FOREIGN KEY ([QuestionId]) REFERENCES [Questions]([Id]),
    CONSTRAINT [FK_StudentAnswers_Answers] FOREIGN KEY ([SelectedAnswerId]) REFERENCES [Answers]([Id])
);

CREATE TABLE [Badges] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(150) NOT NULL,
    [Description] NVARCHAR(500) NOT NULL,
    [MinPoints] INT NOT NULL
);

CREATE TABLE [StudentBadges] (
    [StudentProfileId] INT NOT NULL,
    [BadgeId] INT NOT NULL,
    [EarnedAt] DATETIME2 NOT NULL,
    CONSTRAINT [PK_StudentBadges] PRIMARY KEY ([StudentProfileId], [BadgeId]),
    CONSTRAINT [FK_StudentBadges_StudentProfiles] FOREIGN KEY ([StudentProfileId]) REFERENCES [StudentProfiles]([Id]),
    CONSTRAINT [FK_StudentBadges_Badges] FOREIGN KEY ([BadgeId]) REFERENCES [Badges]([Id])
);

CREATE TABLE [Videos] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Title] NVARCHAR(200) NOT NULL,
    [Url] NVARCHAR(1000) NOT NULL,
    [Description] NVARCHAR(1000) NOT NULL
);

CREATE TABLE [SafetyTips] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [Content] NVARCHAR(2000) NOT NULL
);

CREATE TABLE [ParkingZones] (
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [SchoolName] NVARCHAR(200) NOT NULL,
    [Type] NVARCHAR(20) NOT NULL,
    [Location] NVARCHAR(500) NOT NULL
);
