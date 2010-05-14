EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'NT AUTHORITY\NETWORK SERVICE';


GO
EXECUTE sp_addrolemember @rolename = N'db_datawriter', @membername = N'NT AUTHORITY\NETWORK SERVICE';

