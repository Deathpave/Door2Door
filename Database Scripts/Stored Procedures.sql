USE door2doordb;

/*####################################################
			## Locations Section ##
####################################################*/
DROP PROCEDURE IF EXISTS `spCreateLocation`;
DROP PROCEDURE IF EXISTS `spUpdateLocation`;
DROP PROCEDURE IF EXISTS `spDeleteLocation`;
DROP PROCEDURE IF EXISTS `spGetLocationById`;
DROP PROCEDURE IF EXISTS `spGetLocationByName`;
DROP PROCEDURE IF EXISTS `spGetAllLocations`;

-- Create operation
DELIMITER //
CREATE PROCEDURE `spCreateLocation` (IN newName VARCHAR(255), IN newIconUrl VARCHAR(255))
BEGIN
	INSERT INTO locations (name, iconUrl) 
    VALUES (newName, newIconUrl);
END //
DELIMITER ;

-- Read operation
DELIMITER //
CREATE PROCEDURE `spGetLocationById` (IN locationId INT)
BEGIN
SELECT * FROM locations WHERE id = locatoinId;
END //
DELIMITER ;

-- Read operation 
DELIMITER //
CREATE PROCEDURE `spGetLocationByName` (IN searchName VARCHAR(255))
BEGIN
	SELECT * FROM locations 
    WHERE name = searchName;
END //
DELIMITER ;

-- Read All operation
DELIMITER //
CREATE PROCEDURE `spGetAllLocations` ()
BEGIN
	SELECT * FROM locations;
END //
DELIMITER ;

-- Update operation
DELIMITER //
CREATE PROCEDURE `spUpdateLocation` (IN locationId INT, IN newName VARCHAR(255), IN newIconUrl VARCHAR(255))
BEGIN
	UPDATE locations SET 
    name = newName, 
    iconUrl = newIconUrl 
    WHERE id = locationId;
END //
DELIMITER ;

-- Delete operation
DELIMITER //
CREATE PROCEDURE `spDeleteLocation` (IN locationId INT)
BEGIN
	DELETE FROM locations 
    WHERE id = locationId;
END //
DELIMITER ;


/*####################################################
			## Routes Section ##
####################################################*/
DROP PROCEDURE IF EXISTS `spCreateRoute`;
DROP PROCEDURE IF EXISTS `spUpdateRoute`;
DROP PROCEDURE IF EXISTS `spDeleteRoute`;
DROP PROCEDURE IF EXISTS `spGetRouteById`;
DROP PROCEDURE IF EXISTS `spGetRouteByLocations`;
DROP PROCEDURE IF EXISTS `spGetAllRoutes`;

-- Create operation
DELIMITER //
CREATE PROCEDURE `spCreateRoute` (IN startId INT, IN endId INT, IN newText VARCHAR(255), IN videourl VARCHAR(255))
BEGIN
	INSERT INTO routes (startLocation, endLocation, text, videoUrl) 
    VALUES (startId, endID, newText, videourl);
END //
DELIMITER ;

-- Read operation
DELIMITER //
CREATE PROCEDURE `spGetRouteById` (IN routeId INT)
BEGIN
	SELECT * FROM routes 
    WHERE id = routeId;
END //
DELIMITER ;

-- Read operation
DELIMITER //
CREATE PROCEDURE `spGetRouteByLocations` (IN startId INT, IN endId INT)
BEGIN
	SELECT * FROM routes 
	WHERE startLocation = startId && endLocation = endId;
END //
DELIMITER ;

-- Read All operation
DELIMITER //
CREATE PROCEDURE `spGetAllRoutes` ()
BEGIN
SELECT * FROM routes;
END //
DELIMITER ;

-- Update operation
DELIMITER //
CREATE PROCEDURE `spUpdateRoute` (IN routeId INT, IN startId INT, IN endID INT, IN newText VARCHAR(255), IN videourl VARCHAR(255))
BEGIN
	UPDATE routes SET 
		startLocation = newStart, 
		endLocation = newEnd, 
		text = newText, 
		videoUrl = videourl 
		WHERE id = routeId;
END //
DELIMITER ;

-- Delete operation
DELIMITER //
CREATE PROCEDURE `spDeleteRoute` (IN routeId INT)
BEGIN
	DELETE FROM routes 
    WHERE id = routeId;
END //
DELIMITER ;


/*####################################################
			## Admin Section ##
####################################################*/
DROP PROCEDURE IF EXISTS `spCreateAdmin`;
DROP PROCEDURE IF EXISTS `spUpdateAdmin`;
DROP PROCEDURE IF EXISTS `spDeleteAdmin`;
DROP PROCEDURE IF EXISTS `spGetAdminById`;
DROP PROCEDURE IF EXISTS `spGetAllAdmins`;

-- Create operation
DELIMITER //
CREATE PROCEDURE `spCreateAdmin` (IN username VARCHAR(255), IN password VARCHAR(255))
BEGIN
	INSERT INTO admin (username, password) 
    VALUES (username, password);
END //
DELIMITER ;

-- Read operation
DELIMITER //
CREATE PROCEDURE `spGetAdminById` (IN adminId INT)
BEGIN
	SELECT * FROM admin 
    WHERE id = adminId;
END //
DELIMITER ;

-- Read all operation
DELIMITER //
CREATE PROCEDURE `spGetAllAdmins` ()
BEGIN
	SELECT * FROM admin;
END //
DELIMITER ;

-- Update operation
DELIMITER //
CREATE PROCEDURE `spUpdateAdmin` (IN adminId INT, IN newUsername VARCHAR(255), IN newPassword VARCHAR(255))
BEGIN
	UPDATE admin SET 
    username = newUsername, 
    password = newPassword 
    WHERE id = adminId;
END //
DELIMITER ;

-- Delete operation
DELIMITER //
CREATE PROCEDURE `spDeleteAdmin` (IN adminId INT)
BEGIN
	DELETE FROM admin WHERE id = adminId;
END //
DELIMITER ;


/*####################################################
			## Log Section ##
####################################################*/
DROP PROCEDURE IF EXISTS `spCreateLog`;
DROP PROCEDURE IF EXISTS `spUpdateLog`;
DROP PROCEDURE IF EXISTS `spDeleteLog`;
DROP PROCEDURE IF EXISTS `spGetLogById`;
DROP PROCEDURE IF EXISTS `spGetAllLogs`;

-- Create operation
DELIMITER //
CREATE PROCEDURE `spCreateLog` (IN type INT, IN description VARCHAR(255), IN timestamp TIMESTAMP(6))
BEGIN
	INSERT INTO log (type, description, timestamp) 
    VALUES (type, description, timestamp);
END //
DELIMITER ;

-- Read operation
DELIMITER //
CREATE PROCEDURE `spGetLogById` (IN logId INT)
BEGIN
	SELECT * FROM log 
    WHERE id = logId;
END //
DELIMITER ;

-- Read all operation
DELIMITER //
CREATE PROCEDURE `spGetAllLogs` ()
BEGIN
	SELECT * FROM log;
END //
DELIMITER ;

-- Update operation
DELIMITER //
CREATE PROCEDURE `spUpdateLog` (IN logId INT, IN type INT, IN description VARCHAR(255), IN timestamp TIMESTAMP(6))
BEGIN
	UPDATE log SET 
    type = type, 
    description = description, 
    timestamp = timestamp 
    WHERE id = logId;
END //
DELIMITER ;

-- Delete operation
DELIMITER //
CREATE PROCEDURE `spDeleteLog` (IN logId INT)
BEGIN
	DELETE FROM log 
    WHERE id = logId;
END //
DELIMITER ;


/*####################################################
			## LogType Section ##
####################################################*/
DROP PROCEDURE IF EXISTS `spCreateLogType`;
DROP PROCEDURE IF EXISTS `spUpdateLogType`;
DROP PROCEDURE IF EXISTS `spDeleteLogType`;
DROP PROCEDURE IF EXISTS `spGetLogTypeById`;
DROP PROCEDURE IF EXISTS `spGetAllLogTypes`;

-- Create operation
DELIMITER //
CREATE PROCEDURE `spCreateLogType` (IN errorCodes VARCHAR(255))
BEGIN
	INSERT INTO logTypes (errorCodes) 
    VALUES (errorCodes);
END //
DELIMITER ;

-- Read operation
DELIMITER //
CREATE PROCEDURE `spGetLogTypeById` (IN logTypeId INT)
BEGIN
	SELECT * FROM logTypes 
    WHERE id = logTypeId;
END //
DELIMITER ;

-- Read all operation
DELIMITER //
CREATE PROCEDURE `spGetAllLogTypes` ()
BEGIN
	SELECT * FROM logTypes;
END //
DELIMITER ;

-- Update operation
DELIMITER //
CREATE PROCEDURE `spUpdateLogType` (IN logTypeId INT, IN errorCodes VARCHAR(255))
BEGIN
	UPDATE logTypes SET 
    errorCodes = errorCodes 
    WHERE id = logTypeId;
END //
DELIMITER ;

-- Delete operation
DELIMITER //
CREATE PROCEDURE `spDeleteLogType` (IN logTypeId INT)
BEGIN
	DELETE FROM logTypes 
    WHERE id = logTypeId;
END //
DELIMITER ;