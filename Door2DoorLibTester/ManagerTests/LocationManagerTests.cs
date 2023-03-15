using Door2DoorLib.DataModels;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Managers;
using Door2DoorLibTester.Setup;
using NUnit.Framework;

namespace Door2DoorLibTester.ManagerTests
{
    internal class LocationManagerTests
    {
        private ILocationManager _locationManager;

        [OneTimeSetUp]
        public void Setup()
        {
            var db = SqlConfigurationSetup.SetupDB();
            _locationManager = new LocationManager(db);
        }

        [Test]
        [Order(1)]
        public async Task GetAllAsync_HasData_IfCollectionIsNotNull()
        {
            //Act
            var locations = await _locationManager.GetAllAsync();
            AsyncTestDelegate getAllAction = async () => await _locationManager.GetAllAsync();

            //Assert
            Assert.IsNotNull(locations);
            Assert.IsNotEmpty(locations);
            Assert.Greater(locations.Count(), 0);
            Assert.DoesNotThrowAsync(getAllAction);
        }

        [Test]
        [Order(2)]
        public async Task CreateAsync_CreatesLocation_IfArgumentsAreValid()
        {
            //Arrange
            Location testLocation = CreateTestLocation();
            Admin testUser = CreateTestUser();

            //Act
            bool result = await _locationManager.CreateAsync(testLocation, testUser);

            //Cleanup
            await _locationManager.DeleteAsync(testLocation, testUser);

            //Arrange
            Assert.IsTrue(result);
        }

        [Test]
        [Order(3)]
        public async Task GetByIdAsync_ReturnsAValidObject_IfArgumentsAreValid()
        {
            //Arrange
            Location requestedLocation;
            Location testLocation = CreateTestLocation();
            Admin testUser = CreateTestUser();
            await _locationManager.CreateAsync(testLocation, testUser);

            //Act
            requestedLocation = await _locationManager.GetByIdAsync(testLocation.Id);

            //Cleanup
            await _locationManager.DeleteAsync(testLocation, testUser);

            //Assert
            Assert.AreEqual(testLocation.Id, requestedLocation.Id);
            Assert.AreEqual(testLocation.Name, requestedLocation.Name);
            Assert.AreEqual(testLocation.IconUrl, requestedLocation.IconUrl);
        }

        [Test]
        [Order(4)]
        public async Task UpdateAsync_UpdatesExisitngObject_IfArgumentsAreValid()
        {
            //Arrange
            Location testLocation = CreateTestLocation();
            Location updatedRoute = CreateUpdatedTestObject();
            Admin testUser = CreateTestUser();
            await _locationManager.CreateAsync(testLocation, testUser);

            //Act
            var result = await _locationManager.UpdateAsync(updatedRoute, testUser);

            //Cleanup
            await _locationManager.DeleteAsync(updatedRoute, testUser);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        [Order(5)]
        public async Task RemoveObjectAsync_RemovesExistingObject_IfArgumentsAreValid()
        {
            //Arrange
            Location testLocation = CreateTestLocation();
            Admin testUser = CreateTestUser();
            await _locationManager.CreateAsync(testLocation, testUser);

            //Act
            var result = await _locationManager.DeleteAsync(testLocation, testUser);

            //Cleanup
            await _locationManager.DeleteAsync(testLocation, testUser);

            //Assert
            Assert.IsTrue(result);
        }

        private int GetTestId()
        {
            List<Location> locations = (List<Location>)_locationManager.GetAllAsync().Result;

            return (int)locations.Last().Id + 1;
        }

        private Admin CreateTestUser()
        {
            Admin admin = new Admin("TestUser", "123");
            return admin;
        }

        private Location CreateTestLocation()
        {
            Location location = new Location("Test", "Test", GetTestId());
            return location;
        }

        private Location CreateUpdatedTestObject()
        {
            Location location = new Location("Testing", "Testing", GetTestId());
            return location;
        }
    }
}
