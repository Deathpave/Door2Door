using Door2DoorLib.DataModels;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Managers;
using Door2DoorLibTester.Setup;
using NUnit.Framework;

namespace Door2DoorLibTester.ManagerTests
{
    internal class RouteManagerTests
    {
        private IRouteManager? _routeManager;

        [SetUp]
        public void Setup()
        {
            var db = SqlConfigurationSetup.SetupDB();
            _routeManager = new RouteManager(db);
        }

        [Test]
        public async Task CreateAsync_CreatesARoute_IfArgumentsAreValid()
        {
            //Arrange
            Route testRoute = CreateTestObject();
            Admin testUser = CreateTestUser();

            //Act
            bool result = await _routeManager.CreateAsync(testRoute, testUser);

            bool cleanupRouteTest = await _routeManager.DeleteAsync(testRoute, testUser);

            //Assert
            Assert.IsTrue(result);
            //Assert.IsTrue(cleanupRouteTest);
        }

        [Test]
        public async Task GetAllAsync_HasData_IfCollectionIsNotNull()
        {
            //Arrange
            int collectionCount = 0;

            //Act
            var routes = await _routeManager.GetAllAsync();
            AsyncTestDelegate getAllAction = async () => await _routeManager.GetAllAsync();

            //Assert
            Assert.IsNotNull(routes);
            Assert.IsNotEmpty(routes);
            Assert.Greater(routes.Count(), collectionCount);
            Assert.DoesNotThrowAsync(getAllAction);
        }

        [Test]
        public async Task GetByIdAsync_ReturnsAValidObject_IfArgumentIsValid()
        {
            //Arrange
            Route requestedRoute;
            Route testRoute = CreateTestObject();
            Admin testUser = CreateTestUser();

            await _routeManager.CreateAsync(testRoute, testUser);

            //Act
            requestedRoute = await _routeManager.GetByIdAsync(testRoute.Id);

            bool cleanupRouteTest = await _routeManager.DeleteAsync(requestedRoute, testUser);

            //Assert
            Assert.AreEqual(testRoute.Id, requestedRoute.Id);
            Assert.AreEqual(testRoute.Description, requestedRoute.Description);
            Assert.AreEqual(testRoute.VideoUrl, requestedRoute.VideoUrl);
            Assert.AreEqual(testRoute.StartLocation, requestedRoute.StartLocation);
            Assert.AreEqual(testRoute.EndLocation,requestedRoute.EndLocation);
            //Assert.IsTrue(cleanupRouteTest);
        }

        [Test]
        public async Task UpdateAsync_UpdatesExistingObject_IfArgumentsAreValid()
        {
            //Arrange
            Route testRoute = CreateTestObject();
            Route updatedRoute = CreateUpdatedTestObject();
            Admin testUser = CreateTestUser();

            await _routeManager.CreateAsync(testRoute, testUser);

            //Act
            var result = await _routeManager.UpdateAsync(updatedRoute, testUser);

            var cleanupTestRoute = await _routeManager.DeleteAsync(updatedRoute, testUser);

            //Assert
            Assert.IsTrue(result);
            //Assert.IsTrue(cleanupTestRoute);
        }

        [Test]
        public async Task RemoveObjectAsync_RemovesExistingObject_IfArgumentsAreValid()
        {
            //Arrange
            Route testRoute = CreateTestObject();
            Admin testUser = CreateTestUser();

            await _routeManager.CreateAsync(testRoute, testUser);

            //Act
            var result = await _routeManager.DeleteAsync(testRoute, testUser);

            //Assert
            Assert.IsTrue(result);
        }

        private static Admin CreateTestUser()
        {
            Admin admin = new Admin(666, "TestUser", "123");
            return admin;
        }

        private static Route CreateTestObject()
        {
            Route route = new Route(101, "", "", 1, 2);
            return route;
        }

        private static Route CreateUpdatedTestObject()
        {
            Route route = new Route(101, "https://www.youtube.com/watch?v=dQw4w9WgXcQ", "Lav en U-vending", 1, 2);
            return route;
        }

    }
}
