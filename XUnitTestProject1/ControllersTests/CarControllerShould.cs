using FinalProject.Controllers;
using FinalProject.Models.CarMarket;
using FinalProject.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTest.ControllersTests
{
    public class CarControllerShould
    {
        [Fact]
        public void ReturnAllCars()
        {
            //Arrange
            var mock = new Mock<ICommonActions<Car>>();
            mock.Setup(x => x.GetAllAsync()).Returns(GetTestCars());
            var controller = new CarController(mock.Object);

            //Act
            var result = controller.GetAllCarsAsync().Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Car>>(viewResult.Model);
            var counter = GetTestCars()
                .Result;
            Assert.Equal(counter.Count, model.Count);
        }

        [Theory]
        [InlineData(1)]
        public void ReturnCarById(int id)
        {
            //Arrange
            var mock = new Mock<ICommonActions<Car>>();
            mock.Setup(x => x.GetByIdAsync(id))
                .Returns(Task.Run(() => GetTestCars().Result.FirstOrDefault(x => x.Id == id)));
            var controller = new CarController(mock.Object);

            //Act
            var result = controller.GetCarByIdAsync(id).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Car>(viewResult.Model);
            var counter = GetTestCars()
                .Result
                .FirstOrDefault(x => x.Id == id);
            Assert.Equal(counter.Id, model.Id);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ReturnAllCarsByDealerId(int dealerid)
        {
            //Arrange
            var mock = new Mock<ICommonActions<Car>>();
            mock.Setup(x => x.GetAllAsync()).Returns(GetTestCars());
            var controller = new CarController(mock.Object);

            //Act
            var result = controller.GetCarsByDealerIdAsync(dealerid).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Car>>(viewResult.Model);
            var counter = GetTestCars()
                .Result
                .Where(x => x.DealerId == dealerid).ToList();
            Assert.Equal(counter.Count, model.Count);
        }

        [Theory]
        [InlineData("VinCode")]
        [InlineData("VinCode2")]
        public void ReturnCarByVinCode(string vinCode)
        {
            //Arrange
            var mock = new Mock<ICommonActions<Car>>();
            mock.Setup(repo => repo.GetAllAsync()).Returns(GetTestCars());
            var controller = new CarController(mock.Object);

            //Act
            var result = controller.GetSingleCarAsync(vinCode).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Car>(viewResult.Model);
            var counter = GetTestCars()
                .Result
                .FirstOrDefault(x => x.VinCode.Equals(vinCode, StringComparison.InvariantCultureIgnoreCase));
            Assert.Equal(counter.Id, model.Id);
        }

        private Task<List<Car>> GetTestCars() => Task.Run(() => new List<Car>(){
                new Car {
                    Id = 1,
                    VinCode="VinCode",
                    Price = 0,
                    UrlPage="UrlPage",
                    CarState = CarState.New,
                    ColorExterior = "ColorExterior",
                    CarStatus = CarStatus.Active,
                    ColorInterior = "ColorInterior",
                    DateUpdateInfo = DateTime.Now.Date,
                    Name = "Name",
                    NativeId= "NativeId",
                    DealerId = 1,
                    Dealer = new Dealer()
                    {
                         Id = 1,
                         Name = "Name"
                    }
                },
                new Car {
                    Id = 1,
                    VinCode="VinCode2",
                    Price = 1,
                    UrlPage="UrlPage2",
                    CarState = CarState.IsStock,
                    ColorExterior = "ColorExterior2",
                    CarStatus = CarStatus.Saled,
                    ColorInterior = "ColorInterior2",
                    DateUpdateInfo = DateTime.Now.Date.AddDays(-1),
                    Name = "Name2",
                    NativeId= "NativeId2",
                    DealerId = 2,
                    Dealer = new Dealer()
                    {
                         Id = 2,
                         Name = "Name2"
                    }
                }
        });
    }
}
