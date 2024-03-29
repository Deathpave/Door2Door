﻿using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Managers;
using Door2DoorLibTester.Setup;
using NUnit.Framework;

namespace Door2DoorLibTester.ManagerTests
{
    internal class LocationManagerTests
    {
        private ILocationManager _manager;

        [SetUp]
        public void Setup()
        {
            var db = SqlConfigurationSetup.SetupDB();
            _manager = new LocationManager(db);
        }

        /// <summary>
        /// Tests the GetAllAsync method of the LocationManager class
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(1)]
        public async Task GetAllAsync_HasData_IfCollectionIsNotNull()
        {
            //Act
            var locations = await _manager.GetAllAsync();
            AsyncTestDelegate getAllAction = async () => await _manager.GetAllAsync();

            //Assert
            Assert.IsNotNull(locations);
            Assert.IsNotEmpty(locations);
            Assert.Greater(locations.Count(), 0);
            Assert.DoesNotThrowAsync(getAllAction);
        }

        /// <summary>
        /// Tests the CreateAsync method of the LocationManager class
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(2)]
        public async Task CreateAsync_CreatesLocation_IfArgumentsAreValid()
        {
            //Arrange
            Location testLocation = CreateTestLocation();
            Admin testUser = CreateTestUser();

            //Act
            bool result = await _manager.CreateAsync(testLocation, testUser);

            //Cleanup
            await _manager.DeleteAsync(testLocation, testUser);

            //Arrange
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the GetByIdAsync method of the LocationManager class
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(3)]
        public async Task GetByIdAsync_ReturnsAValidObject_IfArgumentsAreValid()
        {
            //Arrange
            Location requestedLocation;
            Location testLocation = CreateTestLocation();
            Admin testUser = CreateTestUser();
            await _manager.CreateAsync(testLocation, testUser);

            //Act
            requestedLocation = await _manager.GetByIdAsync(testLocation.Id);

            //Cleanup
            await _manager.DeleteAsync(testLocation, testUser);

            //Assert
            Assert.AreEqual(testLocation.Id, requestedLocation.Id);
            Assert.AreEqual(testLocation.Name, requestedLocation.Name);
            Assert.AreEqual(testLocation.IconUrl, requestedLocation.IconUrl);
        }

        /// <summary>
        /// Tests the UpdateAsync method of the LocationManager class
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(4)]
        public async Task UpdateAsync_UpdatesExisitngObject_IfArgumentsAreValid()
        {
            //Arrange
            Location testLocation = CreateTestLocation();
            Location updatedRoute = CreateUpdatedTestObject();
            Admin testUser = CreateTestUser();
            await _manager.CreateAsync(testLocation, testUser);

            //Act
            var result = await _manager.UpdateAsync(updatedRoute, testUser);

            //Cleanup
            await _manager.DeleteAsync(updatedRoute, testUser);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the RemoveAsync method of the LocationManager class
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(5)]
        public async Task RemoveAsync_RemovesExistingObject_IfArgumentsAreValid()
        {
            //Arrange
            Location testLocation = CreateTestLocation();
            Admin testUser = CreateTestUser();
            await _manager.CreateAsync(testLocation, testUser);

            //Act
            var result = await _manager.DeleteAsync(testLocation, testUser);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Returns an Admin object for testing
        /// </summary>
        /// <returns></returns>
        private Admin CreateTestUser()
        {
            Admin admin = AdminFactory.CreateAdmin("TestUser", "123");
            return admin;
        }

        /// <summary>
        /// Returns a location object
        /// </summary>
        /// <returns></returns>
        private Location CreateTestLocation()
        {
            Location location = LocationFactory.CreateLocation("Test", "Test", 1000);

            return location;
        }

        /// <summary>
        /// Returns a location object with different properties
        /// </summary>
        /// <returns></returns>
        private Location CreateUpdatedTestObject()
        {
            Location location = LocationFactory.CreateLocation("Testing", "Testing", 1000);
            return location;
        }
    }
}
