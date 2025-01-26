# CanineConnect

# AlSO CONTRIBUTED BY KARSTEN LARSON, DANIEL PUTNAM, AND NASHAT KHAN

## How to Reset a Database's Migrations

### Delete the Migrations Folder

Delete the `Migrations` folder from the Solution Explorer.

### Drop the Database

Open the Nuget Package Manager Console.

This can be found at `Tools` > `NuGet Package Manager` > `Package Manager Console`.

Run the following command:

```
Drop-Database
```

This running this command will ask something along the lines of:

```
Are you sure you want to perform this action?
Performing the operation "Drop-Database" on target "database 'aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502' on server '(localdb)\mssqllocaldb'".
[Y] Yes  [A] Yes to All  [N] No  [L] No to All  [S] Suspend  [?] Help (default is "Y"):
```

Type in "**Y**" and hit enter. 

### Add the new Initial Migration

Continuing in the NuGet Console, run the following command:

```
Add-Migration Initial
```

The output should be:

```
Build started...
Build succeeded.
To undo this action, use Remove-Migration.
```

### Update the Database

Continuing in the Nuget Console, update the database with the new migration. Run the following command:

```
Update-Database
```

The output should be:

```
Build started...
Build succeeded.
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (156ms) [Parameters=[], CommandType='Text', CommandTimeout='60']
      CREATE DATABASE [aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502];
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (45ms) [Parameters=[], CommandType='Text', CommandTimeout='60']
      IF SERVERPROPERTY('EngineEdition') <> 5
      BEGIN
          ALTER DATABASE [aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502] SET READ_COMMITTED_SNAPSHOT ON;
      END;
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (8ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [__EFMigrationsHistory] (
          [MigrationId] nvarchar(150) NOT NULL,
          [ProductVersion] nvarchar(32) NOT NULL,
          CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
      );
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (5ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT OBJECT_ID(N'[__EFMigrationsHistory]');
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT [MigrationId], [ProductVersion]
      FROM [__EFMigrationsHistory]
      ORDER BY [MigrationId];
Microsoft.EntityFrameworkCore.Migrations[20402]
      Applying migration '20241205215023_Initial'.
Applying migration '20241205215023_Initial'.
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Address] (
          [Id] int NOT NULL IDENTITY,
          [Street] nvarchar(max) NOT NULL,
          [City] nvarchar(max) NOT NULL,
          [State] nvarchar(max) NOT NULL,
          [PostalCode] nvarchar(max) NOT NULL,
          [Country] nvarchar(max) NOT NULL,
          CONSTRAINT [PK_Address] PRIMARY KEY ([Id])
      );
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [User] (
          [Id] int NOT NULL IDENTITY,
          [FirstName] nvarchar(max) NOT NULL,
          [LastName] nvarchar(max) NOT NULL,
          [Email] nvarchar(max) NOT NULL,
          [Age] date NOT NULL,
          [Password] nvarchar(max) NOT NULL,
          [Phone] nvarchar(max) NULL,
          [HomeAddressId] int NOT NULL,
          CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_User_Address_HomeAddressId] FOREIGN KEY ([HomeAddressId]) REFERENCES [Address] ([Id]) ON DELETE CASCADE
      );
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Message] (
          [Id] int NOT NULL IDENTITY,
          [Text] nvarchar(max) NOT NULL,
          [Timestamp] datetime2 NOT NULL,
          [SenderId] int NOT NULL,
          [ReceiverId] int NOT NULL,
          CONSTRAINT [PK_Message] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_Message_User_ReceiverId] FOREIGN KEY ([ReceiverId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
          CONSTRAINT [FK_Message_User_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
      );
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Shelter] (
          [Id] int NOT NULL IDENTITY,
          [ShelterName] nvarchar(max) NOT NULL,
          [Description] nvarchar(max) NULL,
          [FacebookURL] nvarchar(max) NULL,
          [InstagramURL] nvarchar(max) NULL,
          [TwitterURL] nvarchar(max) NULL,
          [WebsiteURL] nvarchar(max) NULL,
          [UserId] int NOT NULL,
          CONSTRAINT [PK_Shelter] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_Shelter_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
      );
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (11ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [DogListing] (
          [Id] int NOT NULL IDENTITY,
          [Name] nvarchar(max) NOT NULL,
          [Sex] nvarchar(max) NOT NULL,
          [Weight] decimal(6,1) NOT NULL,
          [Age] date NULL,
          [Avaliable] bit NOT NULL,
          [Descripton] nvarchar(max) NULL,
          [MedicalDescription] nvarchar(max) NULL,
          [ShelterId] int NOT NULL,
          CONSTRAINT [PK_DogListing] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_DogListing_Shelter_ShelterId] FOREIGN KEY ([ShelterId]) REFERENCES [Shelter] ([Id]) ON DELETE CASCADE
      );
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Event] (
          [Id] int NOT NULL IDENTITY,
          [Name] nvarchar(max) NOT NULL,
          [Date] datetime2 NOT NULL,
          [Description] nvarchar(max) NOT NULL,
          [LocationId] int NOT NULL,
          [HostId] int NOT NULL,
          CONSTRAINT [PK_Event] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_Event_Address_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Address] ([Id]) ON DELETE CASCADE,
          CONSTRAINT [FK_Event_Shelter_HostId] FOREIGN KEY ([HostId]) REFERENCES [Shelter] ([Id]) ON DELETE CASCADE
      );
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (12ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [Application] (
          [Id] int NOT NULL IDENTITY,
          [Approved] bit NOT NULL,
          [Timestamp] datetime2 NOT NULL,
          [DogListingId] int NOT NULL,
          [UserId] int NOT NULL,
          CONSTRAINT [PK_Application] PRIMARY KEY ([Id]),
          CONSTRAINT [FK_Application_DogListing_DogListingId] FOREIGN KEY ([DogListingId]) REFERENCES [DogListing] ([Id]) ON DELETE CASCADE,
          CONSTRAINT [FK_Application_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
      );
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_Application_DogListingId] ON [Application] ([DogListingId]);
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (0ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_Application_UserId] ON [Application] ([UserId]);
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_DogListing_ShelterId] ON [DogListing] ([ShelterId]);
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_Event_HostId] ON [Event] ([HostId]);
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_Event_LocationId] ON [Event] ([LocationId]);
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_Message_ReceiverId] ON [Message] ([ReceiverId]);
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_Message_SenderId] ON [Message] ([SenderId]);
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE UNIQUE INDEX [IX_Shelter_UserId] ON [Shelter] ([UserId]);
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_User_HomeAddressId] ON [User] ([HomeAddressId]);
Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
      VALUES (N'20241205215023_Initial', N'8.0.11');
Done.
```
