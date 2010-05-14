ALTER DATABASE [$(DatabaseName)]
    ADD FILE (NAME = [WhoCanHelpMe], FILENAME = '$(DefaultDataPath)$(DatabaseName).mdf', MAXSIZE = UNLIMITED, FILEGROWTH = 1024 KB) TO FILEGROUP [PRIMARY];

