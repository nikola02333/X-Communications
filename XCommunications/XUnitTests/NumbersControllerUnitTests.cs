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
    public class NumbersControllerUnitTests
    {
        private NumbersController numbersController;
        private Mock<IMapper> mapper;
        private Mock<IService<NumberServiceModel>> service;
        private Mock<IQuery<NumberServiceModel>> query;
        private Mock<ILog> log;
        private List<NumberControllerModel> controllersNumbers;
        private List<NumberControllerModel> availableNumbers;
        private List<NumberServiceModel> serviceNumbers;
        private NumberServiceModel numberService;
        private NumberControllerModel numberController;

        public NumbersControllerUnitTests()
        {
            service = new Mock<IService<NumberServiceModel>>();
            mapper = new Mock<IMapper>();
            log = new Mock<ILog>();
            query = new Mock<IQuery<NumberServiceModel>>();
            numbersController = new NumbersController(service.Object, mapper.Object, log.Object, query.Object);
            Arrange();
        }

        // prepares objects for tests
        private void Arrange()
        {
            numberService = new NumberServiceModel() { Id = 1, Ndc = 123, Cc = 456, Sn = 4219940, Status = false };
            numberController = new NumberControllerModel() { Id = numberService.Id, Ndc = numberService.Ndc, Cc = numberService.Cc, Sn = numberService.Sn, Status = numberService.Status };

            controllersNumbers = new List<NumberControllerModel>()
            {
                new NumberControllerModel() { Id=1, Ndc=123, Cc=456, Sn=4219940, Status=true },
                new NumberControllerModel() { Id=1, Ndc=123, Cc=456, Sn=4219940, Status=true },
                new NumberControllerModel() { Id=1, Ndc=123, Cc=456, Sn=4219940, Status=true }
            };

            serviceNumbers = new List<NumberServiceModel>()
            {
                new NumberServiceModel() { Id=1, Ndc=123, Cc=456, Sn=4219940, Status=true },
                new NumberServiceModel() { Id=1, Ndc=123, Cc=456, Sn=4219940, Status=true },
                new NumberServiceModel() { Id=1, Ndc=123, Cc=456, Sn=4219940, Status=true }
            };

            availableNumbers = new List<NumberControllerModel>()
            {
                new NumberControllerModel() { Id=1, Ndc=123, Cc=456, Sn=4219940, Status=true },
                new NumberControllerModel() { Id=1, Ndc=123, Cc=456, Sn=4219940, Status=true },
                new NumberControllerModel() { Id=1, Ndc=123, Cc=456, Sn=4219940, Status=true }
            };
        }

        [Fact]
        public void GetNumber_WhenCalled_ReturnsAllItems()
        {
            int calls = 0;

            service.Setup(x => x.GetAll())
                   .Returns(() => serviceNumbers);

            mapper.Setup(m => m.Map<NumberControllerModel>(It.IsAny<NumberServiceModel>()))
                  .Returns(() => controllersNumbers[calls])
                  .Callback(() => calls++);

            // Act
            var result = numbersController.GetNumber();

            // Assert
            var allNumbers = new List<NumberControllerModel>(result);

            for(int i=0; i<3; i++)
            {
                Assert.True(allNumbers[i] == controllersNumbers[i]);
            }
        }

        [Theory]
        [InlineData(2)]
        public void GetNumber_WhenCalled_ReturnsNotFound(int id)
        {
            // Arrange
            service.Setup(x => x.Get(id))
                   .Returns(numberService);

            // Act
            var result = numbersController.GetNumber(id);

            // Assert
            Assert.True(result.GetType().Equals(typeof(NotFoundObjectResult)));
        }

        [Theory]
        [InlineData(1)]
        public void GetNumber_WhenCalled_ReturnsOk(int id)
        {
            // Arrange
            int calls = 0;

            service.Setup(x => x.Get(id))
                   .Returns(numberService);

            mapper.Setup(m => m.Map<NumberControllerModel>(It.IsAny<NumberServiceModel>()))
                  .Returns(() => controllersNumbers[calls])
                  .Callback(() => calls++);

            //// Act
            var result = numbersController.GetNumber(id);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));
        }

        [Theory]
        [InlineData(1)]
        public void GetNumber_WhenCalled_ReturnsInternalError(int id)
        {
            // Arrange
            numbersController = new NumbersController(null, null, log.Object, query.Object);

            // Act
            var result = numbersController.GetNumber(id);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(500, response.StatusCode);
        }

        //[Fact]
        //public void GetAvailableNumber_WhenCalled_ReturnsAllItems()
        //{
        //    // Arrange
        //    int calls = 0;

        //    service.Setup(x => x.GetAll())
        //           .Returns(() => serviceNumbers);

        //    mapper.Setup(m => m.Map<NumberControllerModel>(It.IsAny<NumberServiceModel>()))
        //          .Returns(() => availableNumbers[calls])
        //          .Callback(() => calls++);

        //    // Act
        //    var result = numbersController.GetAvailableNumber();

        //    // Assert
        //    var available = new List<NumberControllerModel>(result);

        //    for (int i = 0; i < 3; i++)
        //    {
        //        Assert.True(available[i] == availableNumbers[i]);
        //    }
        //}

        [Theory]
        [InlineData(2)]
        public void PutNumber_WhenCalled_ReturnsNotFound(int id)
        {
            // Arrange
            service.Setup(x => x.Update(numberService))
                   .Returns(true);

            mapper.Setup(x => x.Map<NumberServiceModel>(It.IsAny<NumberControllerModel>()))
                  .Returns(numberService);

            // Act
            var result = numbersController.PutNumber(id, numberController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(400, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void PutNumber_WhenCalled__ReturnsOk(int id)
        {
            // Arrange
            service.Setup(x => x.Update(It.IsAny<NumberServiceModel>()))
                   .Returns(true);

            mapper.Setup(x => x.Map<NumberControllerModel>(numberService))
                  .Returns(numberController);

            // Act
            var result = numbersController.PutNumber(id, numberController);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkResult)));
        }

        [Theory]
        [InlineData(1)]
        public void PutNumber_WhenCalled__ReturnsInternalError(int id)
        {
            // Arrange
            numbersController = new NumbersController(null, null, log.Object, query.Object);

            // Act
            var result = numbersController.PutNumber(id, numberController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(500, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void PutNumber_WhenCalled__ReturnsBadRequestModelState(int id)
        {
            // Arrange
            service.Setup(x => x.Update(numberService))
                   .Returns(true);

            mapper.Setup(x => x.Map<NumberControllerModel>(numberService))
                  .Returns(numberController);

            numbersController.ModelState.AddModelError("key", "error mesaage");

            // Act
            var result = numbersController.PutNumber(id, numberController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(400, response.StatusCode);
        }

        [Theory]
        [InlineData(2)]
        public void PutNumber_WhenCalled__ReturnsBadRequest(int id)
        {
            // Arrange
            service.Setup(x => x.Update(It.IsAny<NumberServiceModel>()))
                   .Returns(true);

            mapper.Setup(x => x.Map<NumberControllerModel>(numberService))
                  .Returns(numberController);

            // Act
            var result = numbersController.PutNumber(id, numberController);

            // Assert
            Assert.True(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [Fact]
        public void PostNumber_WhenCalled_ReturnsOk()
        {
            // Arrange
            service.Setup(x => x.Add(It.IsAny<NumberServiceModel>()))
                   .Returns(true);

            mapper.Setup(x => x.Map<NumberControllerModel>(numberService))
                  .Returns(numberController);

            // Act
            var result = numbersController.PostNumber(numberController);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));
        }

        [Fact]
        public void PostNumber_WhenCalled_ReturnsInternalError()
        {
            // Arrange
            numbersController = new NumbersController(null, null, log.Object, query.Object);

            // Act
            var result = numbersController.PostNumber(numberController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public void PostNumber_WhenCalled_ReturnsBadRequestModelState()
        {
            // Arrange
            service.Setup(x => x.Add(numberService))
                   .Returns(true);

            mapper.Setup(x => x.Map<NumberControllerModel>(numberService))
                  .Returns(numberController);

            numbersController.ModelState.AddModelError("key", "error mesaage");

            // Act
            var result = numbersController.PostNumber(numberController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(400, response.StatusCode);
        }

        [Theory]
        [InlineData(2)]
        public void DeleteNumber_WhenCalled_ReturnsNotFound(int id)
        {
            // Arrange
            service.Setup(x => x.Delete(id))
                   .Returns(false);

            mapper.Setup(x => x.Map<NumberServiceModel>(It.IsAny<NumberControllerModel>()))
                  .Returns(numberService);

            // Act
            var result = numbersController.DeleteNumber(id);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(404, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void DeleteNumber_WhenCalled__ReturnsOk(int id)
        {
            // Arrange
            service.Setup(x => x.Delete(id))
                   .Returns(true);

            mapper.Setup(x => x.Map<NumberControllerModel>(numberService))
                  .Returns(numberController);

            // Act
            var result = numbersController.DeleteNumber(id);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkResult)));
        }

        [Theory]
        [InlineData(1)]
        public void DeleteNumber_WhenCalled__ReturnsInternalError(int id)
        {
            // Arrange
            numbersController = new NumbersController(null, null, log.Object, query.Object);

            // Act
            var result = numbersController.DeleteNumber(id);
            Assert.IsType<StatusCodeResult>(result);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(500, response.StatusCode);
        }
    }
}
