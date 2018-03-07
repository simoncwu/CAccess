EXECUTE sp_addrolemember @rolename = N'cfb_caccess_user', @membername = N'NYCCFB\CAAPI';


GO
EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'NYCCFB\CAAPI';
