CREATE LOGIN [SA] WITH PASSWORD = 'Password123';
USE PersonalWebsite;
CREATE USER [SA] FOR LOGIN [SA];
EXEC sp_addrolemember 'db_datareader', 'SA';
EXEC sp_addrolemember 'db_datawriter', 'SA';