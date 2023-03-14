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
        private int testId = 0;

        [OneTimeSetUp]
        public void Setup()
        {
            var db = SqlConfigurationSetup.SetupDB();
            _routeManager = new RouteManager(db);

            testId = GetTestId();
        }

        [Test]
        [Order(1)]
        public async Task GetAllAsync_HasData_IfCollectionIsNotNull()
        {
            //Act
            var routes = await _routeManager.GetAllAsync();
            AsyncTestDelegate getAllAction = async () => await _routeManager.GetAllAsync();

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
            Route testRoute = CreateTestRoute();

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
        public async Task GetByLocationsAsync_ReturnsAValidObject_IFArgumentsAreValid()
        {
            //Arrange
            Route requestedRoute;
            Route testRoute = CreateTestRoute();

            //Act
            requestedRoute = await _routeManager.GetByLocationsAsync(testRoute.StartLocation, testRoute.EndLocation);

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

            //Act
            var result = await _routeManager.UpdateAsync(updatedRoute, testUser);

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

            //Act
            var result = await _routeManager.DeleteAsync(testRoute, testUser);

            //Assert
            Assert.IsTrue(result);
        }

        private int GetTestId()
        {
            List<Route> routes = (List<Route>)_routeManager.GetAllAsync().Result;
            
            return (int)routes.Last().Id + 1;
        }

        private static Admin CreateTestUser()
        {
            Admin admin = new Admin("TestUser", "123");
            return admin;
        }

        private Route CreateTestRoute()
        {
            Route route = new Route("Test", "Test", 6, 7, testId);
            return route;
        }

        private Route CreateUpdatedTestObject()
        {
            Route route = new Route("https://www.youtube.com/watch?v=dQw4w9WgXcQ", "Lav en U-vending", 10, 12, testId);
            return route;
        }

    }
}
