USE door2doordb;

/*####################################################
			## Locations Section ##
####################################################*/
DROP PROCEDURE IF EXISTS spCreateLocation
DROP PROCEDURE IF EXISTS spUpdateLocation
DROP PROCEDURE IF EXISTS spDeleteLocation
DROP PROCEDURE IF EXISTS spGetLocationById
DROP PROCEDURE IF EXISTS spGetLocationByName
DROP PROCEDURE IF EXISTS spGetAllLocations
GO

-- Create operation
CREATE PROCEDURE spCreateLocation(
	@locationId BIGINT = NULL,
	@newName NVARCHAR(255),
	@newIconUrl NVARCHAR(255))
AS
BEGIN
	

	IF @locationId IS NOT NULL
	BEGIN
		SET IDENTITY_INSERT [locations] ON;
		INSERT INTO locations (id, name, iconUrl)
		VALUES (@locationId, @newName, @newIconUrl);
		SET IDENTITY_INSERT [locations] OFF;
	END
	ELSE
	BEGIN
		INSERT INTO locations (name, iconUrl)
		VALUES (@newName, @newIconUrl);
	END

END;
GO

-- Read operation
CREATE PROCEDURE spGetLocationById(
	@locationId BIGINT)
AS
BEGIN
	SELECT * FROM locations
	WHERE id = @locationId;
END;
GO

-- Read operation
CREATE PROCEDURE spGetLocationByName(
	@searchName NVARCHAR(255))
AS
BEGIN
	SELECT * FROM locations
	WHERE name = @searchName;
END;
GO

-- Read All operation
CREATE PROCEDURE spGetAllLocations
AS
BEGIN
	SELECT * FROM locations;
END;
GO

-- Update operation
CREATE PROCEDURE spUpdateLocation(
	@locationId BIGINT,
	@newName NVARCHAR(255),
	@newIconUrl NVARCHAR(255))
AS
BEGIN
	UPDATE locations SET
	name = @newName,
	iconUrl = @newIconUrl
	WHERE id = @locationId;
END;
GO

-- Delete operation
CREATE PROCEDURE spDeleteLocation(
	@locationId BIGINT)
AS
BEGIN
	DELETE FROM locations
	WHERE id = @locationId;
END;
GO

/*####################################################
			## Routes Section ##
####################################################*/
DROP PROCEDURE IF EXISTS spCreateRoute;
DROP PROCEDURE IF EXISTS spUpdateRoute;
DROP PROCEDURE IF EXISTS spDeleteRoute;
DROP PROCEDURE IF EXISTS spGetRouteById;
DROP PROCEDURE IF EXISTS spGetRouteByLocations;
DROP PROCEDURE IF EXISTS spGetAllRoutes;
GO

-- Create operation
CREATE PROCEDURE spCreateRoute(
	@routeId BIGINT = NULL,
	@startId BIGINT,
	@endId BIGINT,
	@newText NVARCHAR(255),
	@videourl NVARCHAR(255))
AS
BEGIN
	IF @routeId IS NOT NULL
	BEGIN
		SET IDENTITY_INSERT [routes] ON;
		INSERT INTO routes (id, startLocation, endLocation, text, videoUrl)
		VALUES (@routeId, @startId, @endId, @newText, @videourl);
		SET IDENTITY_INSERT [routes] OFF;
	END
	ELSE
	BEGIN
		INSERT INTO routes (startLocation, endLocation, text, videoUrl)
		VALUES (@startId, @endId, @newText, @videourl);
	END
END;
GO

-- Read operation
CREATE PROCEDURE spGetRouteById
(
@routeId BIGINT
)
AS
BEGIN
SELECT * FROM routes
WHERE id = @routeId;
END;
GO

-- Read operation
CREATE PROCEDURE spGetRouteByLocations
(
@startId BIGINT,
@endId BIGINT
)
AS
BEGIN
	SELECT * FROM routes
	WHERE startLocation = @startId AND endLocation = @endId;
END;
GO

-- Read All operation
CREATE PROCEDURE spGetAllRoutes
AS
BEGIN
	SELECT * FROM routes;
END;
GO

-- Update operation
CREATE PROCEDURE spUpdateRoute(
	@routeId BIGINT,
	@startId BIGINT,
	@endId BIGINT,
	@newText VARCHAR(255),
	@videourl VARCHAR(255))
AS
BEGIN
	UPDATE routes SET
	startLocation = @startId,
	endLocation = @endId,
	text = @newText,
	videoUrl = @videourl
	WHERE id = @routeId;
END;
GO

-- Delete operation
CREATE PROCEDURE spDeleteRoute(
@routeId BIGINT)
AS
BEGIN
	DELETE FROM routes
	WHERE id = @routeId;
END;
GO

/*####################################################
			## Admin Section ##
####################################################*/
DROP PROCEDURE IF EXISTS spCreateAdmin;
DROP PROCEDURE IF EXISTS spUpdateAdmin;
DROP PROCEDURE IF EXISTS spDeleteAdmin;
DROP PROCEDURE IF EXISTS spGetAdminById;
DROP PROCEDURE IF EXISTS spGetAdminByName;
DROP PROCEDURE IF EXISTS spGetAdminByLocations;
DROP PROCEDURE IF EXISTS spGetAllAdmins;
GO

-- Create operation
CREATE PROCEDURE spCreateAdmin(
	@adminId BIGINT = NULL,
	@username VARCHAR(255),
	@password VARCHAR(255))
AS
BEGIN
	IF (@adminId IS NOT NULL AND @adminId <> 0)
	BEGIN
		SET IDENTITY_INSERT [admin] ON;
		INSERT INTO admin (id, username, password)
		VALUES (@adminId, @username, @password);
		SET IDENTITY_INSERT [Admin] OFF;
	END
	ELSE
	BEGIN
		INSERT INTO admin (username, password)
		VALUES (@username, @password);
	END;
END;
GO

-- Read operation
CREATE PROCEDURE spGetAdminById(
@adminId BIGINT)
AS
BEGIN
	SELECT * FROM admin
	WHERE id = @adminId;
END;
GO

-- Read operation
CREATE PROCEDURE spGetAdminByName(
@adminName VARCHAR(255))
AS
BEGIN
	SELECT * FROM admin
	WHERE username = @adminName;
END;
GO

-- Read all operation
CREATE PROCEDURE spGetAllAdmins
AS
BEGIN
	SELECT * FROM admin;
END;
GO

-- Update operation
CREATE PROCEDURE spUpdateAdmin(
	@adminId BIGINT,
	@newUsername VARCHAR(255),
	@newPassword VARCHAR(255))
AS
BEGIN
	UPDATE admin SET
	username = @newUsername,
	password = @newPassword
	WHERE id = @adminId;
END;
GO

-- Delete operation
CREATE PROCEDURE spDeleteAdmin(
	@adminId BIGINT)
AS
BEGIN
	DELETE FROM admin
	WHERE id = @adminId;
END;
GO

/*####################################################
			## Log Section ##
####################################################*/
DROP PROCEDURE IF EXISTS spCreateLog;
DROP PROCEDURE IF EXISTS spUpdateLog;
DROP PROCEDURE IF EXISTS spDeleteLog;
DROP PROCEDURE IF EXISTS spGetLogById;
DROP PROCEDURE IF EXISTS spGetLogByLocations;
DROP PROCEDURE IF EXISTS spGetAllLogs;
GO

-- Create operation
CREATE PROCEDURE spCreateLog(
	@logId BIGINT = NULL,
	@type BIGINT,
	@description VARCHAR(255),
	@timestamp DATETIME)
AS
BEGIN
	IF @logId IS NOT NULL OR @logId <> 0
	BEGIN
		SET IDENTITY_INSERT [log] ON;
		INSERT INTO log (id, type, description, timestamp)
		VALUES (@logId, @type, @description, @timestamp);
		SET IDENTITY_INSERT [log] OFF;
	END
	ELSE
	BEGIN
		INSERT INTO log (type, description, timestamp)
		VALUES (@type, @description, @timestamp);
	END;
END;
GO

-- Read operation
CREATE PROCEDURE spGetLogById(
	@logId BIGINT)
AS
BEGIN
	SELECT * FROM log
	WHERE id = @logId;
END;
GO

-- Read all operation
CREATE PROCEDURE spGetAllLogs
AS
BEGIN
	SELECT * FROM log;
END;
GO

-- Update operation
CREATE PROCEDURE spUpdateLog(
	@logId BIGINT,
	@type BIGINT,
	@description VARCHAR(255),
	@timestamp DATETIME)
AS
BEGIN
	UPDATE log SET
	type = @type,
	description = @description,
	timestamp = @timestamp
	WHERE id = @logId;
END;
GO

-- Delete operation
CREATE PROCEDURE spDeleteLog(
@logId BIGINT)
AS
BEGIN
	DELETE FROM log
	WHERE id = @logId;
END;
GO

/*####################################################
			## LogType Section ##
####################################################*/
DROP PROCEDURE IF EXISTS spCreateLogType;
DROP PROCEDURE IF EXISTS spUpdateLogType;
DROP PROCEDURE IF EXISTS spDeleteLogType;
DROP PROCEDURE IF EXISTS spGetLogTypeById;
DROP PROCEDURE IF EXISTS spGetLogTypeByLocations;
DROP PROCEDURE IF EXISTS spGetAllLogTypes;
GO

-- Create operation
CREATE PROCEDURE spCreateLogType
    @logTypeId BIGINT = NULL,
    @errorCodes VARCHAR(255)
AS
BEGIN
    IF @logTypeId IS NOT NULL
    BEGIN
		SET IDENTITY_INSERT [logTypes] ON;
        INSERT INTO logTypes (id, errorCodes) 
        VALUES (@logTypeId, @errorCodes);
		SET IDENTITY_INSERT [logTypes] OFF;
    END
    ELSE
    BEGIN
        INSERT INTO logTypes (errorCodes) 
        VALUES (@errorCodes);
    END
END
GO

-- Read operation
CREATE PROCEDURE spGetLogTypeById
    @logTypeId BIGINT
AS
BEGIN
    SELECT * FROM logTypes 
    WHERE id = @logTypeId;
END
GO

-- Read all operation
CREATE PROCEDURE spGetAllLogTypes
AS
BEGIN
    SELECT * FROM logTypes;
END
GO

-- Update operation
CREATE PROCEDURE spUpdateLogType
    @logTypeId BIGINT,
    @errorCodes VARCHAR(255)
AS
BEGIN
    UPDATE logTypes SET 
    errorCodes = @errorCodes
    WHERE id = @logTypeId;
END
GO

-- Delete operation
CREATE PROCEDURE spDeleteLogType
    @logTypeId BIGINT
AS
BEGIN
    DELETE FROM logTypes 
    WHERE id = @logTypeId;
END
GO
