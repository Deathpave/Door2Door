/*####################################################
			## Insert Static data ##
####################################################*/
INSERT INTO `locations` (name) VALUES ('A1.17');
INSERT INTO `locations` (name) VALUES ('A1.22');
INSERT INTO `locations` (name) VALUES ('A1.23');
INSERT INTO `locations` (name) VALUES ('A1.27');
INSERT INTO `locations` (name) VALUES ('B.01');
INSERT INTO `locations` (name) VALUES ('B.11');
INSERT INTO `locations` (name) VALUES ('B.14');
INSERT INTO `locations` (name) VALUES ('B.16');
INSERT INTO `locations` (name) VALUES ('B.21');
INSERT INTO `locations` (name) VALUES ('B.23');
INSERT INTO `locations` (name) VALUES ('B.24');
INSERT INTO `locations` (name) VALUES ('B.25');
INSERT INTO `locations` (name) VALUES ('B.26');
INSERT INTO `locations` (name) VALUES ('C.08');
INSERT INTO `locations` (name) VALUES ('C.15');
INSERT INTO `locations` (name) VALUES ('C.17');
INSERT INTO `locations` (name) VALUES ('C.19');
INSERT INTO `locations` (name) VALUES ('C.20');
INSERT INTO `locations` (name) VALUES ('C.21');
INSERT INTO `locations` (name) VALUES ('C.23');
INSERT INTO `locations` (name) VALUES ('C.29');
INSERT INTO `locations` (name) VALUES ('C.30');
INSERT INTO `locations` (name) VALUES ('C.31');
INSERT INTO `locations` (name) VALUES ('C.32');
INSERT INTO `locations` (name) VALUES ('C.33');
INSERT INTO `locations` (name) VALUES ('C.35');
INSERT INTO `locations` (name) VALUES ('D.05');
INSERT INTO `locations` (name) VALUES ('D.08');
INSERT INTO `locations` (name) VALUES ('D.09');
INSERT INTO `locations` (name) VALUES ('D.19');
INSERT INTO `locations` (name) VALUES ('D.11');
INSERT INTO `locations` (name) VALUES ('D.12');
INSERT INTO `locations` (name) VALUES ('D.13');
INSERT INTO `locations` (name) VALUES ('D.14');
INSERT INTO `locations` (name) VALUES ('D.15');
INSERT INTO `locations` (name) VALUES ('D.16');
INSERT INTO `locations` (name) VALUES ('D.17');
INSERT INTO `locations` (name) VALUES ('D.18');
INSERT INTO `locations` (name) VALUES ('D.27');
INSERT INTO `locations` (name) VALUES ('D.28');
INSERT INTO `locations` (name) VALUES ('D.29');
INSERT INTO `locations` (name) VALUES ('D.30');
INSERT INTO `locations` (name) VALUES ('D.31');
INSERT INTO `locations` (name) VALUES ('D.32');
INSERT INTO `locations` (name) VALUES ('D.35');
INSERT INTO `locations` (name) VALUES ('E.01');
INSERT INTO `locations` (name) VALUES ('E.02');
INSERT INTO `locations` (name) VALUES ('E.03');
INSERT INTO `locations` (name) VALUES ('E.04');
INSERT INTO `locations` (name) VALUES ('E.05');
INSERT INTO `locations` (name) VALUES ('E.06');
INSERT INTO `locations` (name) VALUES ('E.07');
INSERT INTO `locations` (name) VALUES ('E.08');
INSERT INTO `locations` (name) VALUES ('E.09');
INSERT INTO `locations` (name) VALUES ('F.01');
INSERT INTO `locations` (name) VALUES ('F.02');
INSERT INTO `locations` (name) VALUES ('F.03');
INSERT INTO `locations` (name) VALUES ('F.04');
INSERT INTO `locations` (name) VALUES ('F.05');
INSERT INTO `locations` (name) VALUES ('F.07');
INSERT INTO `locations` (name) VALUES ('F.22');
INSERT INTO `locations` (name) VALUES ('F.34');
INSERT INTO `locations` (name) VALUES ('G.01');
INSERT INTO `locations` (name) VALUES ('G.02');
INSERT INTO `locations` (name) VALUES ('G.03');
INSERT INTO `locations` (name) VALUES ('G.04');
INSERT INTO `locations` (name) VALUES ('G.05');
INSERT INTO `locations` (name) VALUES ('G.07');
INSERT INTO `locations` (name) VALUES ('G.10');
INSERT INTO `locations` (name) VALUES ('H.01');
INSERT INTO `locations` (name) VALUES ('H.07');
INSERT INTO `locations` (name) VALUES ('H.11');
INSERT INTO `locations` (name) VALUES ('I.04');
INSERT INTO `locations` (name) VALUES ('I.06');

/*####################################################
			## Insert Setup User ##
####################################################*/
/* This user is for starting the system on setup. After setup a new user can be created and this one can be manually deleted through the admin portal
Username: TestUser
Password: 123
*/
CALL `door2doordb`.`spCreateAdmin`(1,  '21Pxpy7gkhZNAPSv4WH4UQ==', '8uZl5s+QkTaRW5n8dhXlh6eT9AZc1FWxqISxEWCa5OE=');

/*####################################################
			## Insert Test data ##
####################################################*/
CALL `door2doordb`.`spCreateRoute`(0, 1, 2, 'Kør til Cph', 'C:/Videos/download.jpg');
CALL `door2doordb`.`spCreateRoute`(0, 1, 5, 'HEJ', 'http://10.13.0.125//Videos/IMG_3358.mp4');