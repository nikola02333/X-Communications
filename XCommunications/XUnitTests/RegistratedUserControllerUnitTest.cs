using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using XCommunications.Business.Interfaces;
using XCommunications.Business.Models;
using XCommunications.Controllers;
using XCommunications.WebAPI.Models;
using Xunit;

namespace XUnitTests
{
    public class RegistratedUserControllerUnitTest
    {
        private RegistratedUsersController usersController;
        private Mock<IMapper> mapper;
        private Mock<IService<RegistratedUserServiceModel>> service;
        private Mock<ILog> log;
        private List<RegistratedUserControllerModel> controllersUsers;
        private List<RegistratedUserServiceModel> serviceUsers;
        private RegistratedUserServiceModel userService;
        private RegistratedUserControllerModel userController;

        public RegistratedUserControllerUnitTest()
        {
            service = new Mock<IService<RegistratedUserServiceModel>>();
            mapper = new Mock<IMapper>();
            log = new Mock<ILog>();
            usersController = new RegistratedUsersController(service.Object, mapper.Object, log.Object);
            Arrange();
        }

        // prepares objects for tests
        private void Arrange()
        {
            userService = new RegistratedUserServiceModel() { Id = 1, Imsi=123, CustomerId=1, WorkerId=2, NumberId=3 };
            userController = new RegistratedUserControllerModel() { Id = userService.Id, Imsi=userService.Imsi, CustomerId=userService.CustomerId, WorkerId=userService.WorkerId, NumberId=userService.NumberId };

            controllersUsers = new List<RegistratedUserControllerModel>()
            {
                new RegistratedUserControllerModel() { Id = 1, Imsi=123, CustomerId=1, WorkerId=2, NumberId=3 },
                new RegistratedUserControllerModel() { Id = 1, Imsi=123, CustomerId=1, WorkerId=2, NumberId=3 },
                new RegistratedUserControllerModel() { Id = 1, Imsi=123, CustomerId=1, WorkerId=2, NumberId=3 }
            };

            serviceUsers = new List<RegistratedUserServiceModel>()
            {
                new RegistratedUserServiceModel() { Id = 1, Imsi=123, CustomerId=1, WorkerId=2, NumberId=3 },
                new RegistratedUserServiceModel() { Id = 1, Imsi=123, CustomerId=1, WorkerId=2, NumberId=3 },
                new RegistratedUserServiceModel() { Id = 1, Imsi=123, CustomerId=1, WorkerId=2, NumberId=3 }
            };
        }

        [Fact]
        public void GetRegistrated_WhenCalled_ReturnsAllItems()
        {
            int calls = 0;

            service.Setup(x => x.GetAll())
                   .Returns(() => serviceUsers);

            mapper.Setup(m => m.Map<RegistratedUserControllerModel>(It.IsAny<RegistratedUserServiceModel>()))
                  .Returns(() => controllersUsers[calls])
                  .Callback(() => calls++);

            // Act
            var result = usersController.GetRegistrated();

            // Assert
            var allNumbers = new List<RegistratedUserControllerModel>(result);

            for (int i = 0; i < 3; i++)
            {
                Assert.True(allNumbers[i] == controllersUsers[i]);
            }
        }

        [Theory]
        [InlineData(2)]
        public void GetRegistrated_WhenCalled_ReturnsNotFound(int id)
        {
            // Arrange
            service.Setup(x => x.Get(id))
                   .Returns(userService);

            // Act
            var result = usersController.GetRegistrated(id);

            // Assert
            Assert.True(result.GetType().Equals(typeof(NotFoundObjectResult)));
        }

        [Theory]
        [InlineData(1)]
        public void GetRegistrated_WhenCalled_ReturnsOk(int id)
        {
            // Arrange
            int calls = 0;

            service.Setup(x => x.Get(id))
                   .Returns(userService);

            mapper.Setup(m => m.Map<RegistratedUserControllerModel>(It.IsAny<RegistratedUserServiceModel>()))
                  .Returns(() => controllersUsers[calls])
                  .Callback(() => calls++);

            //// Act
            var result = usersController.GetRegistrated(id);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));
        }

        [Theory]
        [InlineData(1)]
        public void GetRegistrated_WhenCalled_ReturnsInternalError(int id)
        {
            // Arrange
            usersController = new RegistratedUsersController(null, null, log.Object);

            // Act
            var result = usersController.GetRegistrated(id);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(500, response.StatusCode);
        }

        [Theory]
        [InlineData(2)]
        public void PutRegistrated_WhenCalled_ReturnsNotFound(int id)
        {
            // Arrange
            service.Setup(x => x.Update(userService))
                   .Returns(true);

            mapper.Setup(x => x.Map<RegistratedUserServiceModel>(It.IsAny<RegistratedUserControllerModel>()))
                  .Returns(userService);

            // Act
            var result = usersController.PutRegistrated(id, userController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(400, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void PutRegistrated_WhenCalled__ReturnsOk(int id)
        {
            // Arrange
            service.Setup(x => x.Update(It.IsAny<RegistratedUserServiceModel>()))
                   .Returns(true);

            mapper.Setup(x => x.Map<RegistratedUserControllerModel>(userService))
                  .Returns(userController);

            // Act
            var result = usersController.PutRegistrated(id, userController);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkResult)));
        }

        [Theory]
        [InlineData(1)]
        public void PutRegistrated_WhenCalled__ReturnsInternalError(int id)
        {
            // Arrange
            usersController = new RegistratedUsersController(null, null, log.Object);

            // Act
            var result = usersController.PutRegistrated(id, userController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(500, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void PutRegistrated_WhenCalled__ReturnsBadRequestModelState(int id)
        {
            // Arrange
            service.Setup(x => x.Update(userService))
                   .Returns(true);

            mapper.Setup(x => x.Map<RegistratedUserControllerModel>(userService))
                  .Returns(userController);

            usersController.ModelState.AddModelError("key", "error mesaage");

            // Act
            var result = usersController.PutRegistrated(id, userController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(400, response.StatusCode);
        }

        [Theory]
        [InlineData(2)]
        public void PutRegistrated_WhenCalled__ReturnsBadRequest(int id)
        {
            // Arrange
            service.Setup(x => x.Update(It.IsAny<RegistratedUserServiceModel>()))
                   .Returns(true);

            mapper.Setup(x => x.Map<RegistratedUserControllerModel>(userService))
                  .Returns(userController);

            // Act
            var result = usersController.PutRegistrated(id, userController);

            // Assert
            Assert.True(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [Fact]
        public void PostRegistrated_WhenCalled_ReturnsOk()
        {
            // Arrange
            service.Setup(x => x.Add(It.IsAny<RegistratedUserServiceModel>()))
                   .Returns(true);

            mapper.Setup(x => x.Map<RegistratedUserControllerModel>(userService))
                  .Returns(userController);

            // Act
            var result = usersController.PostRegistrated(userController);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));
        }

        [Fact]
        public void PostRegistrated_WhenCalled_ReturnsInternalError()
        {
            // Arrange
            usersController = new RegistratedUsersController(null, null, log.Object);

            // Act
            var result = usersController.PostRegistrated(userController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public void PostNumber_WhenCalled_ReturnsBadRequestModelState()
        {
            // Arrange
            service.Setup(x => x.Add(userService))
                   .Returns(true);

            mapper.Setup(x => x.Map<RegistratedUserControllerModel>(userService))
                  .Returns(userController);

            usersController.ModelState.AddModelError("key", "error mesaage");

            // Act
            var result = usersController.PostRegistrated(userController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(400, response.StatusCode);
        }

        [Theory]
        [InlineData(2)]
        public void DeleteRegistrated_WhenCalled_ReturnsNotFound(int id)
        {
            // Arrange
            service.Setup(x => x.Delete(id))
                   .Returns(false);

            mapper.Setup(x => x.Map<RegistratedUserServiceModel>(It.IsAny<RegistratedUserControllerModel>()))
                  .Returns(userService);

            // Act
            var result = usersController.DeleteRegistrated(id);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(404, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void DeleteRegistrated_WhenCalled__ReturnsOk(int id)
        {
            // Arrange
            service.Setup(x => x.Delete(id))
                   .Returns(true);

            mapper.Setup(x => x.Map<RegistratedUserControllerModel>(userService))
                  .Returns(userController);

            // Act
            var result = usersController.DeleteRegistrated(id);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkResult)));
        }

        [Theory]
        [InlineData(1)]
        public void DeleteRegistrated_WhenCalled__ReturnsInternalError(int id)
        {
            // Arrange
            usersController = new RegistratedUsersController(null, null, log.Object);

            // Act
            var result = usersController.DeleteRegistrated(id);
            Assert.IsType<StatusCodeResult>(result);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(500, response.StatusCode);
        }
    }
}
