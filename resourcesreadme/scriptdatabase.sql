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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240209211639_Initial')
BEGIN
    CREATE TABLE [Doctors] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Especiality] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [LastModifiedBy] nvarchar(max) NULL,
        [LastModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_Doctors] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240209211639_Initial')
BEGIN
    CREATE TABLE [Patients] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [LastModifiedBy] nvarchar(max) NULL,
        [LastModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_Patients] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240209211639_Initial')
BEGIN
    CREATE TABLE [OperationalHours] (
        [Id] int NOT NULL IDENTITY,
        [DoctorId] int NOT NULL,
        [Day] int NOT NULL,
        [Available] bit NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [LastModifiedBy] nvarchar(max) NULL,
        [LastModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_OperationalHours] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OperationalHours_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [Doctors] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240209211639_Initial')
BEGIN
    CREATE TABLE [Appointments] (
        [Id] int NOT NULL IDENTITY,
        [DoctorId] int NOT NULL,
        [PatientId] int NOT NULL,
        [ScheduledDate] datetime2 NOT NULL,
        [ScheduleTime] int NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedDate] datetime2 NOT NULL,
        [LastModifiedBy] nvarchar(max) NULL,
        [LastModifiedDate] datetime2 NULL,
        CONSTRAINT [PK_Appointments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Appointments_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [Doctors] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Appointments_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patients] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240209211639_Initial')
BEGIN
    CREATE TABLE [Hours] (
        [Id] int NOT NULL IDENTITY,
        [OperationalHourId] int NOT NULL,
        [Schedule] int NOT NULL,
        [Available] bit NOT NULL,
        CONSTRAINT [PK_Hours] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Hours_OperationalHours_OperationalHourId] FOREIGN KEY ([OperationalHourId]) REFERENCES [OperationalHours] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240209211639_Initial')
BEGIN
    CREATE INDEX [IX_Appointments_DoctorId] ON [Appointments] ([DoctorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240209211639_Initial')
BEGIN
    CREATE INDEX [IX_Appointments_PatientId] ON [Appointments] ([PatientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240209211639_Initial')
BEGIN
    CREATE INDEX [IX_Hours_OperationalHourId] ON [Hours] ([OperationalHourId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240209211639_Initial')
BEGIN
    CREATE UNIQUE INDEX [IX_Hours_Schedule_OperationalHourId] ON [Hours] ([Schedule], [OperationalHourId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240209211639_Initial')
BEGIN
    CREATE UNIQUE INDEX [IX_OperationalHours_DoctorId_Day] ON [OperationalHours] ([DoctorId], [Day]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20240209211639_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240209211639_Initial', N'7.0.12');
END;
GO

COMMIT;
GO

