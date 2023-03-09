USE master;

DROP DATABASE IF EXISTS door2doordb;

CREATE DATABASE door2doordb;

USE door2doordb;

/*####################################################
			## Drop Tables ##
####################################################*/
DROP TABLE IF EXISTS `routes`;
DROP TABLE IF EXISTS `admin`;
DROP TABLE IF EXISTS `log`;
DROP TABLE IF EXISTS `logTypes`;

/*####################################################
			## Create Tables ##
####################################################*/

CREATE TABLE `routes` 
( 
`id` INT(255) NOT NULL AUTO_INCREMENT , 
`text` VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL , 
PRIMARY KEY (`id`)
) ;

CREATE TABLE `admin` 
( 
`id` INT(255) NOT NULL AUTO_INCREMENT , 
`username` VARCHAR(255) NOT NULL , 
`password` VARCHAR(255) NOT NULL , 
PRIMARY KEY (`id`)
) ;

CREATE TABLE `log` 
( 
`id` INT NOT NULL AUTO_INCREMENT , 
`type` INT NOT NULL , 
`description` VARCHAR(255) NOT NULL, 
`timestamp` TIMESTAMP(6) NOT NULL, 
PRIMARY KEY (`id`)
);

CREATE TABLE `logTypes` 
( 
`id` INT NOT NULL AUTO_INCREMENT , 
`errorCodes` VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL, 
PRIMARY KEY (`id`)
) ;

/*####################################################
			## Alter Tables ##
####################################################*/

ALTER TABLE `routes` ADD `videoUrl` INT NOT NULL AFTER `text`; 

ALTER TABLE `routes` CHANGE `videoUrl` `videoUrl` VARCHAR(255) NOT NULL; 

ALTER TABLE `log` ADD FOREIGN KEY (`type`) REFERENCES `logTypes`(`id`) ON DELETE RESTRICT ON UPDATE RESTRICT; 

ALTER TABLE `routes` ADD `name` VARCHAR(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci NOT NULL AFTER `id`;  

/*####################################################
			## Insert dummy/static data ##
####################################################*/