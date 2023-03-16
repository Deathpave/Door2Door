using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Managers;
using Door2DoorLibTester.Setup;
using NUnit.Framework;

namespace Door2DoorLibTester.ManagerTests
{
    internal class RouteManagerTests
    {
        private IRouteManager _manager;

        [SetUp]
        public void Setup()
        {
            var db = SqlConfigurationSetup.SetupDB();
            _manager = new RouteManager(db);
        }

        [Test]
        [Order(1)]
        public async Task GetAllAsync_HasData_IfCollectionIsNotNull()
        {
            //Act
            var routes = await _manager.GetAllAsync();
            AsyncTestDelegate getAllAction = async () => await _manager.GetAllAsync();

            //Assert
            Assert.IsNotNull(routes);
            Assert.IsNotEmpty(routes);
            Assert.Greater(routes.Count(), 0);
            Assert.DoesNotThrowAsync(getAllAction);
        }

        [Test]
        [Order(2)]
        public async Task CreateAsync_CreatesARoute_IfArgumentsAreValid()
        {
            //Arrange
            Route testRoute = CreateTestRoute();
            Admin testUser = CreateTestUser();

            //Act
            bool result = await _manager.CreateAsync(testRoute, testUser);

            //Cleanup
            await _manager.DeleteAsync(testRoute, testUser);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        [Order(3)]
        public async Task GetByIdAsync_ReturnsAValidObject_IfArgumentAreValid()
        {
            //Arrange
            Route requestedRoute;
            Route testRoute = CreateTestRoute();
            Admin testUser = CreateTestUser();
            await _manager.CreateAsync(testRoute, testUser);

            //Act
            requestedRoute = await _manager.GetByIdAsync(testRoute.Id);

            //Cleanup
            await _manager.DeleteAsync(testRoute, testUser);

            //Assert
            Assert.AreEqual(testRoute.Id, requestedRoute.Id);
            Assert.AreEqual(testRoute.Description, requestedRoute.Description);
            Assert.AreEqual(testRoute.VideoUrl, requestedRoute.VideoUrl);
            Assert.AreEqual(testRoute.StartLocation, requestedRoute.StartLocation);
            Assert.AreEqual(testRoute.EndLocation, requestedRoute.EndLocation);
        }

        [Test]
        [Order(4)]
        public async Task GetByLocationsAsync_ReturnsAValidObject_IFArgumentsAreValid()
        {
            //Arrange
            Route requestedRoute;
            Route testRoute = CreateTestRoute();
            Admin testUser = CreateTestUser();
            await _manager.CreateAsync(testRoute, testUser);

            //Act
            requestedRoute = await _manager.GetByLocationsAsync(testRoute.StartLocation, testRoute.EndLocation);

            //cleanup
            await _manager.DeleteAsync(testRoute, testUser);

            //Assert
            Assert.AreEqual(testRoute.Id, requestedRoute.Id);
            Assert.AreEqual(testRoute.Description, requestedRoute.Description);
            Assert.AreEqual(testRoute.VideoUrl, requestedRoute.VideoUrl);
            Assert.AreEqual(testRoute.StartLocation, requestedRoute.StartLocation);
            Assert.AreEqual(testRoute.EndLocation, requestedRoute.EndLocation);
        }

        [Test]
        [Order(5)]
        public async Task UpdateAsync_UpdatesExistingObject_IfArgumentsAreValid()
        {
            //Arrange
            Route testRoute = CreateTestRoute();
            Route updatedRoute = CreateUpdatedTestObject();
            Admin testUser = CreateTestUser();
            await _manager.CreateAsync(testRoute, testUser);

            //Act
            var result = await _manager.UpdateAsync(updatedRoute, testUser);

            //Cleanup
            await _manager.DeleteAsync(updatedRoute, testUser);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        [Order(6)]
        public async Task RemoveObjectAsync_RemovesExistingObject_IfArgumentsAreValid()
        {
            //Arrange
            Route testRoute = CreateTestRoute();
            Admin testUser = CreateTestUser();
            await _manager.CreateAsync(testRoute, testUser);

            //Act
            var result = await _manager.DeleteAsync(testRoute, testUser);

            //Assert
            Assert.IsTrue(result);
        }

        private Admin CreateTestUser()
        {
            Admin admin = AdminFactory.CreateAdmin("TestUser", "123");
            return admin;
        }

        private Route CreateTestRoute()
        {
            Route route = new("Test", "Test", 6, 7, 1000);
            return route;
        }

        private Route CreateUpdatedTestObject()
        {
            Route route = new("https://www.youtube.com/watch?v=dQw4w9WgXcQ", "Lav en U-vending", 10, 12, 1000);
            return route;
        }

    }
}
