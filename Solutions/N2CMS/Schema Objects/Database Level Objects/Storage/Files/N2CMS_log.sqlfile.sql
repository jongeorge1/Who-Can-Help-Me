ALTER DATABASE [$(DatabaseName)]
    ADD LOG FILE (NAME = [N2CMS_log], FILENAME = '$(DefaultDataPath)$(DatabaseName)_log.ldf', MAXSIZE = 2097152 MB, FILEGROWTH = 10 %);

