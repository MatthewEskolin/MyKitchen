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
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210314165209_reset migrations', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210314165651_string length', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FileUploads]') AND [c].[name] = N'EntityType');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [FileUploads] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [FileUploads] ALTER COLUMN [EntityType] nvarchar(300) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210319003647_createdate', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [FileUploads] ADD [CreateDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210319004018_createdate1', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210704153714_favorite', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [GroceryListItems] (
    [GroceryListItemID] int NOT NULL IDENTITY,
    [Name] nvarchar(200) NULL,
    CONSTRAINT [PK_GroceryListItems] PRIMARY KEY ([GroceryListItemID])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220901024113_grocerylist', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220901024309_empty', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GroceryListItems]') AND [c].[name] = N'Name');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [GroceryListItems] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [GroceryListItems] DROP COLUMN [Name];
GO

ALTER TABLE [GroceryListItems] ADD [Item] nvarchar(max) NULL;
GO

ALTER TABLE [GroceryListItems] ADD [Shopped] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [GroceryListItems] ADD [UserID] nvarchar(450) NULL;
GO

CREATE INDEX [IX_GroceryListItems_UserID] ON [GroceryListItems] ([UserID]);
GO

ALTER TABLE [GroceryListItems] ADD CONSTRAINT [FK_GroceryListItems_AspNetUsers_UserID] FOREIGN KEY ([UserID]) REFERENCES [AspNetUsers] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220905163905_grocerylist2', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Meals]') AND [c].[name] = N'MealName');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Meals] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Meals] ALTER COLUMN [MealName] nvarchar(100) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[GroceryListItems]') AND [c].[name] = N'Item');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [GroceryListItems] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [GroceryListItems] ALTER COLUMN [Item] nvarchar(100) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220905164108_maxlengths', N'6.0.8');
GO

COMMIT;
GO

