USE Door2DoorDB;
GO

/*####################################################
		## Insert Static data ##
####################################################*/
EXEC spCreateLocation 1, 'D.16', ' ';
EXEC spCreateLocation 2, 'AdministrationIndgang', ' ';
EXEC spCreateLocation 3, 'HovedIndgang', ' ';
EXEC spCreateLocation NULL, 'E.3', ' ';
EXEC spCreateLocation NULL, 'HandicapWC', ' ';
GO

EXEC spCreateRoute NULL, 3, 4, 'Begynd at gå ned af den store gang. Gå ca 200 meter til du ser et blåt skilt med "E" over en gang på din højre hånd. Drej til højre ned af gangen. Forsæt ca 20m ned af E gangen. Drej til højre ved anden dør på højre hånd. Du er nu ankommet', 'HTTP://10.13.0.125//Videos//Walk.mp4';

/*####################################################
## Insert Setup User ##
####################################################*/
/* This user is for starting the system on setup. After setup a new user can be created and this one can be manually deleted through the admin portal
Username: TestUser
Password: 123
*/
EXEC spCreateAdmin 1, '21Pxpy7gkhZNAPSv4WH4UQ==', '8uZl5s+QkTaRW5n8dhXlh6eT9AZc1FWxqISxEWCa5OE=';