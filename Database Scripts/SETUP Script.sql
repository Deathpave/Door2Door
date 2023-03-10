USE sys;
DROP DATABASE IF EXISTS door2doordb;
CREATE DATABASE door2doordb;
USE door2doordb;

/*####################################################
			## Drop Tables ##
####################################################*/
DROP TABLE IF EXISTS `locations`;
DROP TABLE IF EXISTS `routes`;
DROP TABLE IF EXISTS `admin`;
DROP TABLE IF EXISTS `log`;
DROP TABLE IF EXISTS `logTypes`;

/*####################################################
			## Create Tables ##
####################################################*/

CREATE TABLE `locations`
(
`id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
`name` VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
`iconUrl` VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci
);

CREATE TABLE `routes` 
( 
`id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY, 
`start` INT NOT NULL,
`end` INT NOT NULL,
`text` VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL,
`videoUrl` VARCHAR(255) NOT NULL
) ;

CREATE TABLE `admin` 
( 
`id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY, 
`username` VARCHAR(255) NOT NULL , 
`password` VARCHAR(255) NOT NULL
) ;

CREATE TABLE `log` 
( 
`id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY, 
`type` INT NOT NULL , 
`description` VARCHAR(255) NOT NULL, 
`timestamp` TIMESTAMP(6) NOT NULL
);

CREATE TABLE `logTypes` 
( 
`id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY, 
`errorCodes` VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL
) ;

/*####################################################
			## Alter Tables ##
####################################################*/

ALTER TABLE `routes` ADD FOREIGN KEY (`start`) REFERENCES `locations`(`id`) ON UPDATE CASCADE; 
ALTER TABLE `routes` ADD FOREIGN KEY (`end`) REFERENCES `locations`(`id`) ON UPDATE CASCADE; 
ALTER TABLE `log` ADD FOREIGN KEY (`type`) REFERENCES `logTypes`(`id`) ON DELETE RESTRICT ON UPDATE RESTRICT; 


/*####################################################
			## Insert dummy/static data ##
####################################################*/