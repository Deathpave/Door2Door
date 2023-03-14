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
        private Route testRoute = null;
        private Admin testUser = null;


        [SetUp]
        public async Task SetupAsync()
        {
            var db = SqlConfigurationSetup.SetupDB();
            _routeManager = new RouteManager(db);

            testRoute = await CreateTestObject();
            testUser = CreateTestUser();
        }

        [Test]
        [Order(1)]
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
        [Order(2)]
        public async Task CreateAsync_CreatesARoute_IfArgumentsAreValid()
        {
            //Act
            bool result = await _routeManager.CreateAsync(testRoute, testUser);

            //Assert
            Assert.IsTrue(result);
        }

        

        [Test]
        [Order(3)]
        public async Task GetByIdAsync_ReturnsAValidObject_IfArgumentIsValid()
        {
            //Arrange
            Route requestedRoute;

            //Act
            requestedRoute = await _routeManager.GetByIdAsync(testRoute.Id);

            //Assert
            Assert.AreEqual(testRoute.Id, requestedRoute.Id);
            Assert.AreEqual(testRoute.Description, requestedRoute.Description);
            Assert.AreEqual(testRoute.VideoUrl, requestedRoute.VideoUrl);
            Assert.AreEqual(testRoute.StartLocation, requestedRoute.StartLocation);
            Assert.AreEqual(testRoute.EndLocation,requestedRoute.EndLocation);
        }

        [Test]
        [Order(4)]
        public async Task UpdateAsync_UpdatesExistingObject_IfArgumentsAreValid()
        {
            Route updatedRoute = await CreateUpdatedTestObject();

            await _routeManager.CreateAsync(testRoute, testUser);

            //Act
            var result = await _routeManager.UpdateAsync(updatedRoute, testUser);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        [Order(5)]
        public async Task RemoveObjectAsync_RemovesExistingObject_IfArgumentsAreValid()
        {
            await _routeManager.CreateAsync(testRoute, testUser);

            //Act
            var result = await _routeManager.DeleteAsync(testRoute, testUser);

            //Assert
            Assert.IsTrue(result);
        }

        private static Admin CreateTestUser()
        {
            Admin admin = new Admin("TestUser", "123");
            return admin;
        }

        private async Task<Route> CreateTestObject()
        {
            List<Route> routes = (List<Route>)await _routeManager.GetAllAsync();

            Route route = new Route("Test", "Test", 1, 2, routes.Count + 1);
            return route;
        }

        private async Task<Route> CreateUpdatedTestObject()
        {
            List<Route> routes = (List<Route>)await _routeManager.GetAllAsync();

            Route route = new Route("https://www.youtube.com/watch?v=dQw4w9WgXcQ", "Lav en U-vending", 1, 2, routes.Count + 1);
            return route;
        }

    }
}
