using Door2DoorLib.DataModels;
using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Managers;
using Door2DoorLibTester.Setup;
using NUnit.Framework;

namespace Door2DoorLibTester.ManagerTests
{
    internal class AdminManagerTests
    {
        private IAdminManager _manager;

        [SetUp]
        public void Setup()
        {
            var db = SqlConfigurationSetup.SetupDB();
            _manager = new AdminManager(db);
        }

        /// <summary>
        /// Tests the CreateAsync method of the AdminManager class
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(1)]
        public async Task CreateAsync_CreatesAUser_IfArgumentsAreValid()
        {
            //Arrange
            Admin testAdmin = CreateTestUser();

            //Act
            bool result = await _manager.CreateAsync(testAdmin, testAdmin);

            //Cleanup
            await _manager.DeleteAsync(testAdmin, testAdmin);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the ChecLoginAsync method of the AdminManager class
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(2)]
        public async Task ChecLoginAsync_ReturnsTrue_IfAdminInfoIsValid()
        {
            //Arrange
            Admin testAdmin = CreateTestUser();
            await _manager.CreateAsync(testAdmin, testAdmin);

            //Act
            bool result = await _manager.CheckLoginAsync(testAdmin.UserName, testAdmin.Password);

            //Cleanup
            await _manager.DeleteAsync(testAdmin, testAdmin);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the UpdateAsync method of the AdminManager class
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(3)]
        public async Task UpdateAsync_UpdatesExistingUser_IfArgumentsAreValid()
        {
            //Arrange
            Admin testAdmin = CreateTestUser();
            Admin updatedAdmin = CreateUpdatedTestUser();
            await _manager.CreateAsync(testAdmin, testAdmin);

            //Act
            bool result = await _manager.UpdateAsync(updatedAdmin, updatedAdmin);

            //Cleanup
            await _manager.DeleteAsync(updatedAdmin, updatedAdmin);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the RemoveAsync method of the AdminManager class
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(4)]
        public async Task RemoveAsync_RemovesExistingObject_IfArgumentsAreValid()
        {
            //Arrange
            Admin testAdmin = CreateTestUser();
            await _manager.CreateAsync(testAdmin, testAdmin);

            //Act
            var result = await _manager.DeleteAsync(testAdmin, testAdmin);

            //Assert
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Returns a test Admin object
        /// </summary>
        /// <returns></returns>
        private Admin CreateTestUser()
        {
            Admin admin = AdminFactory.CreateAdmin("TestUser", "123", 1000);
            return admin;
        }

        /// <summary>
        /// Returns a test Admin object with different properties
        /// </summary>
        /// <returns></returns>
        private Admin CreateUpdatedTestUser()
        {
            Admin admin = AdminFactory.CreateAdmin("updatedTestUser", "321", 1000);
            return admin;
        }
    }
}
