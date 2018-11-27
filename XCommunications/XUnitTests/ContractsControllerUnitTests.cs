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
    public class ContractsControllerUnitTests
    {
        private Mock<IService<ContractServiceModel>> _mockContainer;
        private Mock<IMapper> _mapper;
        private Mock<ILog> _logger;
        private int _conrtactId;
        ContractsController _contractController;
        ContractServiceModel _contactServiceModel;
        ContractControllerModel _contractControllerModel;
        List<ContractServiceModel> _contractSreviceModelList;
        List<ContractControllerModel> _custumerControllerModelList;

        public ContractsControllerUnitTests()
        {
            _mockContainer = new Mock<IService<ContractServiceModel>>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILog>();
            SetupModels();
            _contractController = new ContractsController(_mockContainer.Object, _mapper.Object, _logger.Object);
        }
        private void SetupModels()
        {
            _conrtactId = 1;

            _contactServiceModel = new ContractServiceModel { Id = _conrtactId, CustomerId = 1, Date = It.IsAny<DateTime>(), Tarif = "postpejd", WorkerId = 1 };
            _contractControllerModel = new ContractControllerModel
            {
                Id = _contactServiceModel.Id,
                CustomerId = _contactServiceModel.CustomerId,
                Tarif = _contactServiceModel.Tarif,
                WorkerId = _contactServiceModel.WorkerId
            };

            _contractSreviceModelList = new List<ContractServiceModel>
             {
               new ContractServiceModel  {  Id=1, CustomerId=1, WorkerId=1, Date=DateTime.Now, Tarif="pripejd" },
               new ContractServiceModel  {  Id=2, CustomerId=2, WorkerId=2, Date=DateTime.Now, Tarif="pripejd"  }};

            _custumerControllerModelList = new List<ContractControllerModel>
             {
               new ContractControllerModel  {  Id=1, CustomerId=1, WorkerId=1, Tarif="pripejd"  },
               new ContractControllerModel  {  Id=1, CustomerId=1, WorkerId=1, Tarif="pripejd"  }};
        }
        [Theory]
        [InlineData(9)]
        public void GetContractById_ContractIsNull_ReturnsNotFound(int id)
        {
            _mockContainer.Setup(x => x.Get(id)).Returns(_contactServiceModel);
            

            var result = _contractController.GetContract(id);

            Assert.True(result.GetType().Equals(typeof(NotFoundObjectResult)));
        }
        [Theory]
        [InlineData(1)]
        public void GetContractById_ContractIsNull_ReturnsOkContract(int id)
        {
            _mapper
               .Setup(x => x.Map<ContractControllerModel>(It.IsAny<ContractServiceModel>()))
               .Returns(_contractControllerModel);

            _mockContainer
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(_contactServiceModel);



            var result = _contractController.GetContract(id);

            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));
        }
        [Theory]
        [InlineData(1)]
        public void GetContractById_ContractIsNull_ReturnsInternalError(int id)
        {

            _contractController = new ContractsController(null, null, _logger.Object);

            var result = _contractController.GetContract(id);
            Assert.IsType<StatusCodeResult>(result);

            var objectResponse = result as StatusCodeResult;

            Assert.Equal(500, objectResponse.StatusCode);
        }

        [Fact]
        public void GetAllContract_Return_AllContract()
        {
            int calls = 0;
            _mockContainer
               .Setup(x => x.GetAll())
               .Returns(() => _contractSreviceModelList);

            _mapper.Setup(m => m.Map<ContractControllerModel>(It.IsAny<ContractServiceModel>()))
              .Returns(() => _custumerControllerModelList[calls])
              .Callback(() => calls++);

            var result = _contractController.GetContract();

            var _returnList = new List<ContractControllerModel>(result);

            for (int i = 0; i < 2; i++)
            {
                Assert.True(_returnList[i] == _custumerControllerModelList[i]);
            }
    }


        [Theory]
        [InlineData(9)]
        public void PutContract_ContractIsNotNull_ReturnNotFound(int id)
        {
            _mockContainer
              .Setup(x => x.Update(_contactServiceModel))
              .Returns(true);

              _mapper
                .Setup(x => x.Map<ContractServiceModel>(It.IsAny<ContractControllerModel>()))
                .Returns(_contactServiceModel);


            var result = _contractController.PutContract(id, _contractControllerModel);

            Assert.True(result.GetType().Equals(typeof(NotFoundResult)));
        }
        [Theory]
        [InlineData(1)]
        public void PutContract_ContractIsNotNull_ReturnOk(int id)
        {
            _mockContainer
              .Setup(x => x.Update(It.IsAny<ContractServiceModel>()))
              .Returns(true);


            _mapper
              .Setup(x => x.Map<ContractControllerModel>(_contactServiceModel))
              .Returns(_contractControllerModel);
           
            var result = _contractController.PutContract(id, _contractControllerModel);

            Assert.True(result.GetType().Equals(typeof(OkResult)));
        }
        [Theory]
        [InlineData(1)]
        public void PutContract_ContractIsNotNull_ReturnInternalError(int id)
        {
            _contractController = new ContractsController(null, null, _logger.Object);

            var result = _contractController.PutContract(It.IsAny<int>(), It.IsAny<ContractControllerModel>());
            Assert.IsType<StatusCodeResult>(result);

            var objectResponse = result as StatusCodeResult;

            Assert.Equal(500, objectResponse.StatusCode);
        }
       

        [Theory]
        [InlineData(1)]
        public void PutContract_ContractIsInvalid_ReturnBadRequest(int id)
        {
            _mockContainer
              .Setup(x => x.Update(_contactServiceModel))
              .Returns(true);

            _mapper
              .Setup(x => x.Map<ContractControllerModel>(_contactServiceModel))
              .Returns(_contractControllerModel);

            _contractController.ModelState.AddModelError("key", "error mesaage");

            var result = _contractController.PutContract(id, _contractControllerModel);

            Assert.True(result.GetType().Equals(typeof(BadRequestResult)));
        }
        [Theory]
        [InlineData(1)]
        public void DeleteContractById_ContractExists_Return_Ok(int id)
        {
            _mapper
           .Setup(x => x.Map<ContractsController>(It.IsAny<ContractServiceModel>()))
           .Returns(_contractController);

            _mockContainer
             .Setup(x => x.Delete(id))
             .Returns(true);

            var result = _contractController.DeleteContract(id);

            Assert.True(result.GetType().Equals(typeof(OkResult)));
        }
        [Theory]
        [InlineData(9)]
        public void DeleteContractById_ContractNotExists_Return_NotFound(int id)
        {
            _mapper
           .Setup(x => x.Map<ContractsController>(It.IsAny<ContractServiceModel>()))
           .Returns(_contractController);

            _mockContainer
             .Setup(x => x.Delete(id))
             .Returns(false);

            var result = _contractController.DeleteContract(id);

            Assert.True(result.GetType().Equals(typeof(NotFoundResult)));
        }
        [Fact]
        public void DeleteContact_ContractExists_ReturnInternalError()
        {
            _contractController = new ContractsController(null, null, _logger.Object);

            var result = _contractController.DeleteContract(_conrtactId);
            Assert.IsType<StatusCodeResult>(result);

            var objectResponse = result as StatusCodeResult;

            Assert.Equal(500, objectResponse.StatusCode);
        }


    }
}


