using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using XCommunications.Business.Interfaces;
using XCommunications.Business.Models;
using XCommunications.Controllers;
using XCommunications.WebAPI.Models;
using Xunit;

namespace XUnitTests
{
    public class WorkersControllerUnitTest
    {
        private WorkersController usersController;
        private Mock<IMapper> mapper;
        private Mock<IService<WorkerServiceModel>> service;
        private Mock<ILog> log;
        private List<WorkerControllerModel> controllersUsers;
        private List<WorkerServiceModel> serviceUsers;
        private WorkerServiceModel userService;
        private WorkerControllerModel userController;

        public WorkersControllerUnitTest()
        {
            service = new Mock<IService<WorkerServiceModel>>();
            mapper = new Mock<IMapper>();
            log = new Mock<ILog>();
            usersController = new WorkersController(service.Object, mapper.Object, log.Object);
            Arrange();
        }

        // prepares objects for tests
        private void Arrange()
        {
            userService = new WorkerServiceModel() { Id = 1, Name="name", LastName="lastName", Email="email@em.ail", Operater="operater" };
            userController = new WorkerControllerModel() { Id = userService.Id, Name=userService.Name, LastName = userService.LastName, Email = userService.Email, Operater = userService.Operater};

            controllersUsers = new List<WorkerControllerModel>()
            {
                new WorkerControllerModel() { Id = 1, Name="name", LastName="lastName", Email="email@em.ail", Operater="operater" },
                new WorkerControllerModel() { Id = 1, Name="name", LastName="lastName", Email="email@em.ail", Operater="operater" },
                new WorkerControllerModel() { Id = 1, Name="name", LastName="lastName", Email="email@em.ail", Operater="operater" }
            };

            serviceUsers = new List<WorkerServiceModel>()
            {
                new WorkerServiceModel() { Id = 1, Name="name", LastName="lastName", Email="email@em.ail", Operater="operater" },
                new WorkerServiceModel() { Id = 1, Name="name", LastName="lastName", Email="email@em.ail", Operater="operater" },
                new WorkerServiceModel() { Id = 1, Name="name", LastName="lastName", Email="email@em.ail", Operater="operater" }
            };
        }

        [Fact]
        public void GetWorker_WhenCalled_ReturnsAllItems()
        {
            int calls = 0;

            service.Setup(x => x.GetAll())
                   .Returns(() => serviceUsers);

            mapper.Setup(m => m.Map<WorkerControllerModel>(It.IsAny<WorkerServiceModel>()))
                  .Returns(() => controllersUsers[calls])
                  .Callback(() => calls++);

            // Act
            var result = usersController.GetWorker();

            // Assert
            var allNumbers = new List<WorkerControllerModel>(result);

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
            var result = usersController.GetWorker(id);

            // Assert
            Assert.True(result.GetType().Equals(typeof(NotFoundResult)));
        }

        [Theory]
        [InlineData(1)]
        public void GetRegistrated_WhenCalled_ReturnsOk(int id)
        {
            // Arrange
            int calls = 0;

            service.Setup(x => x.Get(id))
                   .Returns(userService);

            mapper.Setup(m => m.Map<WorkerControllerModel>(It.IsAny<WorkerServiceModel>()))
                  .Returns(() => controllersUsers[calls])
                  .Callback(() => calls++);

            //// Act
            var result = usersController.GetWorker(id);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));
        }

        [Theory]
        [InlineData(1)]
        public void GetRegistrated_WhenCalled_ReturnsInternalError(int id)
        {
            // Arrange
            usersController = new WorkersController(null, null, log.Object);

            // Act
            var result = usersController.GetWorker(id);

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

            mapper.Setup(x => x.Map<WorkerServiceModel>(It.IsAny<WorkerControllerModel>()))
                  .Returns(userService);

            // Act
            var result = usersController.PutWorker(id, userController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(400, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void PutRegistrated_WhenCalled__ReturnsOk(int id)
        {
            // Arrange
            service.Setup(x => x.Update(It.IsAny<WorkerServiceModel>()))
                   .Returns(true);

            mapper.Setup(x => x.Map<WorkerControllerModel>(userService))
                  .Returns(userController);

            // Act
            var result = usersController.PutWorker(id, userController);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkResult)));
        }

        [Theory]
        [InlineData(1)]
        public void PutRegistrated_WhenCalled__ReturnsInternalError(int id)
        {
            // Arrange
            usersController = new WorkersController(null, null, log.Object);

            // Act
            var result = usersController.PutWorker(id, userController);

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

            mapper.Setup(x => x.Map<WorkerControllerModel>(userService))
                  .Returns(userController);

            usersController.ModelState.AddModelError("key", "error mesaage");

            // Act
            var result = usersController.PutWorker(id, userController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(400, response.StatusCode);
        }

        [Theory]
        [InlineData(2)]
        public void PutRegistrated_WhenCalled__ReturnsBadRequest(int id)
        {
            // Arrange
            service.Setup(x => x.Update(It.IsAny<WorkerServiceModel>()))
                   .Returns(true);

            mapper.Setup(x => x.Map<WorkerControllerModel>(userService))
                  .Returns(userController);

            // Act
            var result = usersController.PutWorker(id, userController);

            // Assert
            Assert.True(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [Fact]
        public void PostRegistrated_WhenCalled_ReturnsOk()
        {
            // Arrange
            service.Setup(x => x.Add(It.IsAny<WorkerServiceModel>()))
                   .Returns(true);

            mapper.Setup(x => x.Map<WorkerControllerModel>(userService))
                  .Returns(userController);

            // Act
            var result = usersController.PostWorker(userController);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));
        }

        [Fact]
        public void PostRegistrated_WhenCalled_ReturnsInternalError()
        {
            // Arrange
            usersController = new WorkersController(null, null, log.Object);

            // Act
            var result = usersController.PostWorker(userController);

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

            mapper.Setup(x => x.Map<WorkerControllerModel>(userService))
                  .Returns(userController);

            usersController.ModelState.AddModelError("key", "error mesaage");

            // Act
            var result = usersController.PostWorker(userController);

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

            mapper.Setup(x => x.Map<WorkerServiceModel>(It.IsAny<WorkerControllerModel>()))
                  .Returns(userService);

            // Act
            var result = usersController.DeleteWorker(id);

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

            mapper.Setup(x => x.Map<WorkerControllerModel>(userService))
                  .Returns(userController);

            // Act
            var result = usersController.DeleteWorker(id);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkResult)));
        }

        [Theory]
        [InlineData(1)]
        public void DeleteRegistrated_WhenCalled__ReturnsInternalError(int id)
        {
            // Arrange
            usersController = new WorkersController(null, null, log.Object);

            // Act
            var result = usersController.DeleteWorker(id);
            Assert.IsType<StatusCodeResult>(result);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(500, response.StatusCode);
        }
    }
}
