USE master;
IF EXISTS(SELECT * FROM sys.sysdatabases WHERE name = 'KRYPT')
BEGIN
	SELECT 'YES'
END
ELSE
BEGIN
	SELECT 'NO'
END
