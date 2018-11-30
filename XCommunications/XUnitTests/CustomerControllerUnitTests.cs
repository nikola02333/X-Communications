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
    public  class CustomerControllerUnitTests
    {
        private Mock<IService<CustomerServiceModel>> _mockContainer;
        private Mock<IMapper> _mapper;
        private Mock<ILog> _logger;
        private int _customerId;
        CustomersController custController;
        CustomerServiceModel _customerServiceModel;
        CustomerControllerModel _customerControllerModel;
        CustomerServiceModel _customerServiceModelInvalid;
        CustomerControllerModel _customerControllerModelInvalid;
        List<CustomerServiceModel> _custumerSreviceModelList;
        List<CustomerControllerModel> _custumerControllerModelList;
      
        public CustomerControllerUnitTests()
        {
            _mockContainer = new Mock<IService<CustomerServiceModel>>();
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILog>();

            SetupModels();

            custController = new CustomersController(_mockContainer.Object, _mapper.Object, _logger.Object);
        }

        private void SetupModels()
        {
            _customerId = 1;

            _customerServiceModel = new CustomerServiceModel { Id = _customerId, Name = "pera", LastName = "Peric" };
            _customerServiceModelInvalid = new CustomerServiceModel { Id = _customerId, Name = "pera" };
            _customerControllerModelInvalid = new CustomerControllerModel
            {
                Id = _customerServiceModelInvalid.Id, Name = _customerServiceModelInvalid.Name
            };
            _customerControllerModel = new CustomerControllerModel
            {
                Id = _customerServiceModel.Id,
                Name = _customerServiceModel.Name,
                LastName = _customerServiceModel.LastName
            };

            _custumerSreviceModelList = new List<CustomerServiceModel>
             {
               new CustomerServiceModel  { Id = 2, Name = "pera", LastName = "Peric" },
               new CustomerServiceModel  { Id = 3, Name = "pera", LastName = "Peric" }};

            _custumerControllerModelList = new List<CustomerControllerModel>
             {
               new CustomerControllerModel  { Id = 2, Name = "pera", LastName = "Peric" },
               new CustomerControllerModel  { Id = 3, Name = "pera", LastName = "Peric" }};
        }
       

        [Theory]
        [InlineData(1)]
        public void GetCustomerById_CustomerIsNull_ReturnsNotFound(int id)
        {
            _mockContainer.Setup(x => x.Get(id)).Returns(_customerServiceModel);
           

            var result = custController.GetCustomer(id);

            Assert.True(result.GetType().Equals(typeof(NotFoundResult)));
        }

        [Theory]
        [InlineData(1)]
        public void GetCustomerById_CustomerIsNotNull_ReturnsCustomer(int id)
        {
            _mapper
                .Setup(x => x.Map<CustomerControllerModel>(It.IsAny<CustomerServiceModel>()))
                .Returns(_customerControllerModel);
        
            _mockContainer
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(_customerServiceModel);

            var result = custController.GetCustomer(id);
            
            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));

        }
        [Theory]
        [InlineData(1)]
        public void GetCustomerById_CustomerIsNull_ReturnsInternalError(int id)
        {
         
            custController = new CustomersController(null, null, _logger.Object);
            var result = custController.GetCustomer(id);
            Assert.IsType<StatusCodeResult>(result);

            var objectResponse = result as StatusCodeResult; 

            Assert.Equal(500, objectResponse.StatusCode);

        }
        [Fact]
        public void GetAllCustomers_ReturnAllCustomers()
        {
            int calls = 0;
            _mockContainer
               .Setup(x => x.GetAll())
               .Returns( () =>_custumerSreviceModelList);

                _mapper.Setup(m => m.Map<CustomerControllerModel>(It.IsAny<CustomerServiceModel>()))
                  .Returns(() => _custumerControllerModelList[calls]) 
                  .Callback(() => calls++);

           var result = custController.GetCustomer();

           var _returnList= new List<CustomerControllerModel>(result);

            for (int i = 0; i <2; i++)
            {
                Assert.True(_returnList[i] == _custumerControllerModelList[i]);
            }
        }
       
        [Fact]
        
        public void PostCustomer_CustomerIsNotNull_ReturnsOkObject()
        {
            _mapper
                  .Setup(x => x.Map<CustomerControllerModel>(It.IsAny<CustomerServiceModel>()))
                  .Returns(_customerControllerModel);

            _mockContainer
                .Setup(x => x.Add(It.IsAny<CustomerServiceModel>()))
                .Returns(true);

            var result = custController.PostCustomer(_customerControllerModel);

            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));
        }
        [Fact]

        public void PostCustomer_ModelStateInvalid_ReturnsBadRequest()
        {
            _mapper
                .Setup(x => x.Map<CustomerControllerModel>(It.IsAny<CustomerServiceModel>()))
                .Returns(_customerControllerModelInvalid);

            _mockContainer
                .Setup(x => x.Add(It.IsAny<CustomerServiceModel>()))
                .Returns(true);

            custController.ModelState.AddModelError("key", "error message");
            var result = custController.PostCustomer(_customerControllerModelInvalid);
           
            var objectResponse = result as StatusCodeResult;
            Assert.Equal(400, objectResponse.StatusCode);
        }
        [Fact]
        public void PostCustomer_CustomerIsNull_ReturnInternalError()
        {

            custController = new CustomersController(null, null, _logger.Object);
            var result = custController.PostCustomer(new CustomerControllerModel());
            Assert.IsType<StatusCodeResult>(result);

            var objectResponse = result as StatusCodeResult;

            Assert.Equal(500, objectResponse.StatusCode);

        }

        [Theory]
        [InlineData(1)]
        public void DeleteCustomerById_CustomerExists_ReturnOk(int id)
        {
            _mapper
            .Setup(x => x.Map<CustomerControllerModel>(It.IsAny<CustomerServiceModel>()))
            .Returns(_customerControllerModel);

            _mockContainer
             .Setup(x => x.Delete(id))
             .Returns(true);

            var result = custController.DeleteCustomer(id);

            Assert.True(result.GetType().Equals(typeof(OkResult)));

        }
        [Theory]
        [InlineData(9)]
        public void DeleteCustomerById_CustomerExists_ReturnBadRequest(int id)
        {
            _mapper
            .Setup(x => x.Map<CustomerControllerModel>(It.IsAny<CustomerServiceModel>()))
            .Returns(_customerControllerModel);

            _mockContainer
             .Setup(x => x.Delete(6))
             .Returns(true);

            var result = custController.DeleteCustomer(id);

            Assert.True(result.GetType().Equals(typeof(NotFoundResult)));
        }
        [Fact]
        public void DeleteCustomerById_CustomerExists_ReturnInternalError()
        {

            custController = new CustomersController(null, null, _logger.Object);
            var result = custController.DeleteCustomer(_customerId);
            Assert.IsType<StatusCodeResult>(result);

            var objectResponse = result as StatusCodeResult;

            Assert.Equal(500, objectResponse.StatusCode);
        }
       [Fact]
        public void PutCustomer_CustomerNotNull_ReturnOKCustomer()
        {



            
            _mockContainer
          .Setup(x => x.Update(_customerServiceModel))
          .Returns(true);

            _mapper
            .Setup(x => x.Map<CustomerServiceModel>(It.IsAny<CustomerControllerModel>()))
            .Returns(_customerServiceModel);

            var result = custController.PutCustomer(_customerId,_customerControllerModel);

            Assert.True(result.GetType().Equals(typeof(OkObjectResult)));
        }
        [Fact]
        public void PutCustomer_InvalidModelCustomer_ReturnBadRequest()
        {
            _mapper
            .Setup(x => x.Map<CustomerControllerModel>(It.IsAny<CustomerServiceModel>()))
            .Returns(_customerControllerModelInvalid);

            _mockContainer
             .Setup(x => x.Update(It.IsAny<CustomerServiceModel>()))
             .Returns(true);
             custController.ModelState.AddModelError("key", "error message");
            var result = custController.PutCustomer(_customerId,_customerControllerModel);

            Assert.True(result.GetType().Equals(typeof(BadRequestResult)));
        }
       [Fact]
        public void PutCustomer_CustomerNotNull_ReturnInternalError()
        {
            custController = new CustomersController(null, null, _logger.Object);
            var result = custController.PutCustomer(It.IsAny<int>(),It.IsAny<CustomerControllerModel>());
            Assert.IsType<StatusCodeResult>(result);

            var objectResponse = result as StatusCodeResult;

            Assert.Equal(500, objectResponse.StatusCode);
        }
        [Theory]
        [InlineData(9)]
        public void PutCustomer_CustomerNotNull_ReturnNotFound(int id)
        {
            _mapper
            .Setup(x => x.Map<CustomerControllerModel>(It.IsAny<CustomerServiceModel>()))
            .Returns(_customerControllerModel);

            _mockContainer
             .Setup(x => x.Update(_customerServiceModel))
             .Returns(true);

            var result = custController.PutCustomer(id, _customerControllerModel);

            Assert.True(result.GetType().Equals(typeof(NotFoundResult)));
        }

    }
}
