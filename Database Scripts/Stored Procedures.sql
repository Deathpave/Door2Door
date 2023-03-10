USE door2doordb;

/*####################################################
			## Locations Section ##
####################################################*/
DROP PROCEDURE IF EXISTS `d2d.spCreateLocation`;
DROP PROCEDURE IF EXISTS `d2d.spUpdateLocation`;
DROP PROCEDURE IF EXISTS `d2d.spDeleteLocation`;
DROP PROCEDURE IF EXISTS `d2d.spGetLocationById`;
DROP PROCEDURE IF EXISTS `d2d.spGetLocationByName`;
DROP PROCEDURE IF EXISTS `d2d.spGetAllLocations`;

-- Create operation
DELIMITER //
CREATE PROCEDURE `d2d.spCreateLocation` (IN newName VARCHAR(255), IN newIconUrl VARCHAR(255))
BEGIN
INSERT INTO locations (name, iconUrl) VALUES (newName, newIconUrl);
END //
DELIMITER ;

-- Read operation
DELIMITER //
CREATE PROCEDURE `d2d.spGetLocationById` (IN locationId INT)
BEGIN
SELECT * FROM locations WHERE id = locatoinId;
END //
DELIMITER ;

-- Read operation 
DELIMITER //
CREATE PROCEDURE `d2d.spGetLocationByName` (IN searchName VARCHAR(255))
BEGIN
SELECT * FROM locations WHERE name = searchName;
END //
DELIMITER ;

-- Read All operation
DELIMITER //
CREATE PROCEDURE `d2d.spGetAllLocations` ()
BEGIN
SELECT * FROM locations;
END //
DELIMITER ;

-- Update operation
DELIMITER //
CREATE PROCEDURE `d2d.spUpdateLocation` (IN locationId INT, IN newName VARCHAR(255), IN newIconUrl VARCHAR(255))
BEGIN
UPDATE locations SET name = newName, iconUrl = newIconUrl WHERE id = locationId;
END //
DELIMITER ;

-- Delete operation
DELIMITER //
CREATE PROCEDURE `d2d.spDeleteLocation` (IN locationId INT)
BEGIN
DELETE FROM locations WHERE id = locationId;
END //
DELIMITER ;


/*####################################################
			## Routes Section ##
####################################################*/
DROP PROCEDURE IF EXISTS `d2d.spCreateRoute`;
DROP PROCEDURE IF EXISTS `d2d.spUpdateRoute`;
DROP PROCEDURE IF EXISTS `d2d.spDeleteRoute`;
DROP PROCEDURE IF EXISTS `d2d.spGetRouteById`;
DROP PROCEDURE IF EXISTS `d2d.spGetRouteByLocations`;
DROP PROCEDURE IF EXISTS `d2d.spGetAllRoutes`;

-- Create operation
DELIMITER //
CREATE PROCEDURE `d2d.spCreateRoute` (IN startId INT, IN endId INT, IN newText VARCHAR(255), IN videourl VARCHAR(255))
BEGIN
INSERT INTO routes (start, end, text, videoUrl) VALUES (newStart, newEnd, newText, videourl);
END //
DELIMITER ;

-- Read operation
DELIMITER //
CREATE PROCEDURE `d2d.spGetRouteById` (IN routeId INT)
BEGIN
SELECT * FROM routes WHERE id = routeId;
END //
DELIMITER ;

-- Read operation
DELIMITER //
CREATE PROCEDURE `d2d.spGetRouteByLocations` (IN startId INT, IN endId INT)
BEGIN
SELECT * FROM routes WHERE start = startId && end = endId;
END //
DELIMITER ;

-- Read All operation
DELIMITER //
CREATE PROCEDURE `d2d.spGetAllRoutes` ()
BEGIN
SELECT * FROM routes;
END //
DELIMITER ;

-- Update operation
DELIMITER //
CREATE PROCEDURE `d2d.spUpdateRoute` (IN routeId INT, IN startId INT, IN endID INT, IN newText VARCHAR(255), IN videourl VARCHAR(255))
BEGIN
UPDATE routes SET start = newStart, end = newEnd, text = newText, videoUrl = videourl WHERE id = routeId;
END //
DELIMITER ;

-- Delete operation
DELIMITER //
CREATE PROCEDURE `d2d.spDeleteRoute` (IN routeId INT)
BEGIN
DELETE FROM routes WHERE id = routeId;
END //
DELIMITER ;

/*####################################################
			## Admin Section ##
####################################################*/
DROP PROCEDURE IF EXISTS `d2d.spCreateAdmin`;
DROP PROCEDURE IF EXISTS `d2d.spUpdateAdmin`;
DROP PROCEDURE IF EXISTS `d2d.spDeleteAdmin`;
DROP PROCEDURE IF EXISTS `d2d.spGetAdminById`;
DROP PROCEDURE IF EXISTS `d2d.spGetAllAdmins`;

-- Create operation
DELIMITER //
CREATE PROCEDURE `d2d.spCreateAdmin` (IN username VARCHAR(255), IN password VARCHAR(255))
BEGIN
INSERT INTO admin (username, password) VALUES (username, password);
END //
DELIMITER ;

-- Read operation
DELIMITER //
CREATE PROCEDURE `d2d.spGetAdminById` (IN adminId INT)
BEGIN
SELECT * FROM admin WHERE id = adminId;
END //
DELIMITER ;

-- Read all operation
DELIMITER //
CREATE PROCEDURE `d2d.spGetAllAdmins` ()
BEGIN
SELECT * FROM admin;
END //
DELIMITER ;

-- Update operation
DELIMITER //
CREATE PROCEDURE `d2d.spUpdateAdmin` (IN adminId INT, IN username VARCHAR(255), IN password VARCHAR(255))
BEGIN
UPDATE admin SET username = username, password = password WHERE id = adminId;
END //
DELIMITER ;

-- Delete operation
DELIMITER //
CREATE PROCEDURE `d2d.spDeleteAdmin` (IN adminId INT)
BEGIN
DELETE FROM admin WHERE id = adminId;
END //
DELIMITER ;

/*####################################################
			## Log Section ##
####################################################*/
DROP PROCEDURE IF EXISTS `d2d.spCreateLog`;
DROP PROCEDURE IF EXISTS `d2d.spUpdateLog`;
DROP PROCEDURE IF EXISTS `d2d.spDeleteLog`;
DROP PROCEDURE IF EXISTS `d2d.spGetLogById`;
DROP PROCEDURE IF EXISTS `d2d.spGetAllLogs`;

-- Create operation
DELIMITER //
CREATE PROCEDURE `d2d.spCreateLog` (IN type INT, IN description VARCHAR(255), IN timestamp TIMESTAMP(6))
BEGIN
INSERT INTO log (type, description, timestamp) VALUES (type, description, timestamp);
END //
DELIMITER ;

-- Read operation
DELIMITER //
CREATE PROCEDURE `d2d.spGetLogById` (IN logId INT)
BEGIN
SELECT * FROM log WHERE id = logId;
END //
DELIMITER ;

-- Read all operation
DELIMITER //
CREATE PROCEDURE `d2d.spGetAllLogs` ()
BEGIN
SELECT * FROM log;
END //
DELIMITER ;

-- Update operation
DELIMITER //
CREATE PROCEDURE `d2d.spUpdateLog` (IN logId INT, IN type INT, IN description VARCHAR(255), IN timestamp TIMESTAMP(6))
BEGIN
UPDATE log SET type = type, description = description, timestamp = timestamp WHERE id = logId;
END //
DELIMITER ;

-- Delete operation
DELIMITER //
CREATE PROCEDURE `d2d.spDeleteLog` (IN logId INT)
BEGIN
DELETE FROM log WHERE id = logId;
END //
DELIMITER ;

/*####################################################
			## LogType Section ##
####################################################*/
DROP PROCEDURE IF EXISTS `d2d.spCreateLogType`;
DROP PROCEDURE IF EXISTS `d2d.spUpdateLogType`;
DROP PROCEDURE IF EXISTS `d2d.spDeleteLogType`;
DROP PROCEDURE IF EXISTS `d2d.spGetLogTypeById`;
DROP PROCEDURE IF EXISTS `d2d.spGetAllLogTypes`;

-- Create operation
DELIMITER //
CREATE PROCEDURE `d2d.spCreateLogType` (IN errorCodes VARCHAR(255))
BEGIN
INSERT INTO logTypes (errorCodes) VALUES (errorCodes);
END //
DELIMITER ;

-- Read operation
DELIMITER //
CREATE PROCEDURE `d2d.spGetLogTypeById` (IN logTypeId INT)
BEGIN
SELECT * FROM logTypes WHERE id = logTypeId;
END //
DELIMITER ;

-- Read all operation
DELIMITER //
CREATE PROCEDURE `d2d.spGetAllLogTypes` ()
BEGIN
SELECT * FROM logTypes;
END //
DELIMITER ;

-- Update operation
DELIMITER //
CREATE PROCEDURE `d2d.spUpdateLogType` (IN logTypeId INT, IN errorCodes VARCHAR(255))
BEGIN
UPDATE logTypes SET errorCodes = errorCodes WHERE id = logTypeId;
END //
DELIMITER ;

-- Delete operation
DELIMITER //
CREATE PROCEDURE `d2d.spDeleteLogType` (IN logTypeId INT)
BEGIN
DELETE FROM logTypes WHERE id = logTypeId;
END //
DELIMITER ;