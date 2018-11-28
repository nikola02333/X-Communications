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
    public class SimcardsControllerUnitTest
    {
        private SimcardsController simcardsController;
        private Mock<IMapper> mapper;
        private Mock<IService<SimcardServiceModel>> service;
        private Mock<IQuery<SimcardServiceModel>> query;
        private Mock<ILog> log;
        private List<SimcardControllerModel> controllersSimcards;
        private List<SimcardControllerModel> availableSimcards;
        private List<SimcardServiceModel> serviceSimcards;
        private SimcardServiceModel simcardService;
        private SimcardControllerModel simcardController;

        public SimcardsControllerUnitTest()
        {
            service = new Mock<IService<SimcardServiceModel>>();
            mapper = new Mock<IMapper>();
            log = new Mock<ILog>();
            query = new Mock<IQuery<SimcardServiceModel>>();
            simcardsController = new SimcardsController(service.Object, mapper.Object, log.Object, query.Object);
            Arrange();
        }

        // prepares objects for tests
        private void Arrange()
        {
            simcardService = new SimcardServiceModel() { Imsi=1, Iccid=2, Pin=3456, Puk=7891, Status=true };
            simcardController = new SimcardControllerModel() { Imsi = simcardService.Imsi, Iccid = simcardService.Iccid, Pin = simcardService.Pin, Puk = simcardService.Puk, Status = simcardService.Status };

            controllersSimcards = new List<SimcardControllerModel>()
            {
                new SimcardControllerModel() { Imsi=1, Iccid=2, Pin=3456, Puk=7891, Status=true },
                new SimcardControllerModel() { Imsi=1, Iccid=2, Pin=3456, Puk=7891, Status=true },
                new SimcardControllerModel() { Imsi=1, Iccid=2, Pin=3456, Puk=7891, Status=true }
            };

            serviceSimcards = new List<SimcardServiceModel>()
            {
                new SimcardServiceModel() { Imsi=1, Iccid=2, Pin=3456, Puk=7891, Status=true },
                new SimcardServiceModel() { Imsi=1, Iccid=2, Pin=3456, Puk=7891, Status=true },
                new SimcardServiceModel() { Imsi=1, Iccid=2, Pin=3456, Puk=7891, Status=true }
            };

            availableSimcards = new List<SimcardControllerModel>()
            {
                new SimcardControllerModel() { Imsi=1, Iccid=2, Pin=3456, Puk=7891, Status=true },
                new SimcardControllerModel() { Imsi=1, Iccid=2, Pin=3456, Puk=7891, Status=true },
                new SimcardControllerModel() { Imsi=1, Iccid=2, Pin=3456, Puk=7891, Status=true }
            };
        }

        [Fact]
        public void GetSimcard_WhenCalled_ReturnsAllItems()
        {
            int calls = 0;

            service.Setup(x => x.GetAll())
                   .Returns(() => serviceSimcards);

            mapper.Setup(m => m.Map<SimcardControllerModel>(It.IsAny<SimcardServiceModel>()))
                  .Returns(() => controllersSimcards[calls])
                  .Callback(() => calls++);

            // Act
            var result = simcardsController.GetSimcard();

            // Assert
            var allNumbers = new List<SimcardControllerModel>(result);

            for (int i = 0; i < 3; i++)
            {
                Assert.True(allNumbers[i] == controllersSimcards[i]);
            }
        }

        [Theory]
        [InlineData(2)]
        public void GetSimcard_WhenCalled_ReturnsNotFound(int id)
        {
            // Arrange
            service.Setup(x => x.Get(id))
                   .Returns(simcardService);

            // Act
            var result = simcardsController.GetSimcard(id);

            // Assert
            Assert.True(result.GetType().Equals(typeof(NotFoundResult)));
        }

        [Theory]
        [InlineData(1)]
        public void GetSimcard_WhenCalled_ReturnsOk(int id)
        {
            // Arrange
            int calls = 0;

            service.Setup(x => x.Get(id))
                   .Returns(simcardService);

            mapper.Setup(m => m.Map<SimcardControllerModel>(It.IsAny<SimcardServiceModel>()))
                  .Returns(() => controllersSimcards[calls])
                  .Callback(() => calls++);

            //// Act
            var result = simcardsController.GetSimcard(id);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));
        }

        [Theory]
        [InlineData(1)]
        public void GetSimcard_WhenCalled_ReturnsInternalError(int id)
        {
            // Arrange
            simcardsController = new SimcardsController(null, null, log.Object, query.Object);

            // Act
            var result = simcardsController.GetSimcard(id);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(500, response.StatusCode);
        }

        //[Fact]
        //public void GetAvailableSimcard_WhenCalled_ReturnsAllItems()
        //{
        //    // Arrange
        //    int calls = 0;

        //    service.Setup(x => x.GetAll())
        //           .Returns(() => serviceSimcards);

        //    mapper.Setup(m => m.Map<SimcardControllerModel>(It.IsAny<SimcardServiceModel>()))
        //          .Returns(() => availableSimcards[calls])
        //          .Callback(() => calls++);

        //    // Act
        //    var result = simcardsController.GetAvailableSimcard();

        //    // Assert
        //    var available = new List<SimcardControllerModel>(result);

        //    for (int i = 0; i < 3; i++)
        //    {
        //        Assert.True(available[i] == availableSimcards[i]);
        //    }
        //}

        [Theory]
        [InlineData(2)]
        public void PutSimcard_WhenCalled_ReturnsNotFound(int id)
        {
            // Arrange
            service.Setup(x => x.Update(simcardService))
                   .Returns(true);

            mapper.Setup(x => x.Map<SimcardServiceModel>(It.IsAny<SimcardControllerModel>()))
                  .Returns(simcardService);

            // Act
            var result = simcardsController.PutSimcard(id, simcardController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(400, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void PutSimcard_WhenCalled__ReturnsOk(int id)
        {
            // Arrange
            service.Setup(x => x.Update(It.IsAny<SimcardServiceModel>()))
                   .Returns(true);

            mapper.Setup(x => x.Map<SimcardControllerModel>(simcardService))
                  .Returns(simcardController);

            // Act
            var result = simcardsController.PutSimcard(id, simcardController);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkResult)));
        }

        [Theory]
        [InlineData(1)]
        public void PutSimcard_WhenCalled__ReturnsInternalError(int id)
        {
            // Arrange
            simcardsController = new SimcardsController(null, null, log.Object, query.Object);

            // Act
            var result = simcardsController.PutSimcard(id, simcardController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(500, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void PutSimcard_WhenCalled__ReturnsBadRequestModelState(int id)
        {
            // Arrange
            service.Setup(x => x.Update(simcardService))
                   .Returns(true);

            mapper.Setup(x => x.Map<SimcardControllerModel>(simcardService))
                  .Returns(simcardController);

            simcardsController.ModelState.AddModelError("key", "error mesaage");

            // Act
            var result = simcardsController.PutSimcard(id, simcardController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(400, response.StatusCode);
        }

        [Theory]
        [InlineData(2)]
        public void PutSimcard_WhenCalled__ReturnsBadRequest(int id)
        {
            // Arrange
            service.Setup(x => x.Update(It.IsAny<SimcardServiceModel>()))
                   .Returns(true);

            mapper.Setup(x => x.Map<SimcardControllerModel>(simcardService))
                  .Returns(simcardController);

            // Act
            var result = simcardsController.PutSimcard(id, simcardController);

            // Assert
            Assert.True(result.GetType().Equals(typeof(BadRequestResult)));
        }

        [Fact]
        public void PostSimcard_WhenCalled_ReturnsOk()
        {
            // Arrange
            service.Setup(x => x.Add(It.IsAny<SimcardServiceModel>()))
                   .Returns(true);

            mapper.Setup(x => x.Map<SimcardControllerModel>(simcardService))
                  .Returns(simcardController);

            // Act
            var result = simcardsController.PostSimcard(simcardController);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));
        }

        [Fact]
        public void PostSimcard_WhenCalled_ReturnsInternalError()
        {
            // Arrange
            simcardsController = new SimcardsController(null, null, log.Object, query.Object);

            // Act
            var result = simcardsController.PostSimcard(simcardController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public void PostSimcard_WhenCalled_ReturnsBadRequestModelState()
        {
            // Arrange
            service.Setup(x => x.Add(simcardService))
                   .Returns(true);

            mapper.Setup(x => x.Map<SimcardControllerModel>(simcardService))
                  .Returns(simcardController);

            simcardsController.ModelState.AddModelError("key", "error mesaage");

            // Act
            var result = simcardsController.PostSimcard(simcardController);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(400, response.StatusCode);
        }

        [Theory]
        [InlineData(2)]
        public void DeleteSimcard_WhenCalled_ReturnsNotFound(int id)
        {
            // Arrange
            service.Setup(x => x.Delete(id))
                   .Returns(false);

            mapper.Setup(x => x.Map<SimcardServiceModel>(It.IsAny<SimcardControllerModel>()))
                  .Returns(simcardService);

            // Act
            var result = simcardsController.DeleteSimcard(id);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(404, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public void DeleteSimcard_WhenCalled__ReturnsOk(int id)
        {
            // Arrange
            service.Setup(x => x.Delete(id))
                   .Returns(true);

            mapper.Setup(x => x.Map<SimcardControllerModel>(simcardService))
                  .Returns(simcardController);

            // Act
            var result = simcardsController.DeleteSimcard(id);

            // Assert
            Assert.True(result.GetType().Equals(typeof(OkResult)));
        }

        [Theory]
        [InlineData(1)]
        public void DeleteSimcard_WhenCalled__ReturnsInternalError(int id)
        {
            // Arrange
            simcardsController = new SimcardsController(null, null, log.Object, query.Object);

            // Act
            var result = simcardsController.DeleteSimcard(id);
            Assert.IsType<StatusCodeResult>(result);

            // Assert
            var response = result as StatusCodeResult;
            Assert.Equal(500, response.StatusCode);
        }
    }
}

