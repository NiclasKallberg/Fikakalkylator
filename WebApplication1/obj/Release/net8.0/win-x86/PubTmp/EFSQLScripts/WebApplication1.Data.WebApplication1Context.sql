IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114120822_InitialCreate'
)
BEGIN
    CREATE TABLE [Product] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(40) NOT NULL,
        [Points] int NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ModifiedDate] datetime2 NOT NULL,
        [DeletedDate] datetime2 NULL,
        CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114120822_InitialCreate'
)
BEGIN
    CREATE TABLE [User] (
        [Id] uniqueidentifier NOT NULL,
        [Username] nvarchar(100) NULL,
        [FirstName] nvarchar(40) NOT NULL,
        [LastName] nvarchar(40) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [ModifiedDate] datetime2 NOT NULL,
        [DeletedDate] datetime2 NULL,
        [EmailAddress] nvarchar(255) NULL,
        [PhoneNumber] nvarchar(20) NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114120822_InitialCreate'
)
BEGIN
    CREATE TABLE [PositionDuration] (
        [Id] uniqueidentifier NOT NULL,
        [Position] int NOT NULL,
        [PositionStartDate] datetime2 NOT NULL,
        [PositionEndDate] datetime2 NULL,
        [UserId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_PositionDuration] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PositionDuration_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114120822_InitialCreate'
)
BEGIN
    CREATE TABLE [Purchase] (
        [Id] uniqueidentifier NOT NULL,
        [Quantity] int NOT NULL,
        [TotalPoints] int NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [UserId] uniqueidentifier NOT NULL,
        [ProductId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Purchase] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Purchase_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Purchase_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114120822_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_PositionDuration_UserId] ON [PositionDuration] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114120822_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Purchase_ProductId] ON [Purchase] ([ProductId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114120822_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Purchase_UserId] ON [Purchase] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250114120822_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250114120822_InitialCreate', N'9.0.0');
END;

COMMIT;
GO

