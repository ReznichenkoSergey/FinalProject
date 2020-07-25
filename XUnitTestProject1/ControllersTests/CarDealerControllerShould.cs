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
    public class CarDealerControllerShould
    {
        [Theory]
        [InlineData("Name1")]
        public void ReturnAllDealersByName(string name)
        {
            //Arrange
            var mock = new Mock<ICommonActions<Dealer>>();
            mock.Setup(x => x.GetAllAsync()).Returns(GetDealers());
            var controller = new CarDealerController(mock.Object);

            //Act
            var result = controller.GetAllDealersAsync(name).Result;

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Dealer>>(viewResult.Model);
            var dealers = GetDealers()
                .Result
                .Where(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
            Assert.Equal(dealers.Count, model.Count);
        }

        [Theory]
        [InlineData("US")]
        public void ReturnAllDealersByCountryCode(string countryCode)
        {
            //Arrange
            var mock = new Mock<ICommonActions<Dealer>>();
            mock.Setup(x => x.GetAllAsync()).Returns(GetDealers);
            var controller = new CarDealerController(mock.Object);

            //Act
            var result = controller.GetAllDealersByCountryAsync(countryCode).Result;

            //Assert
            var modelResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Dealer>>(modelResult.Model);
            var dealers = GetDealers()
                .Result
                .Where(x => x.Country.Code.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
            Assert.Equal(dealers.Count, model.Count);

        }

        private Task<List<Dealer>> GetDealers() => Task.Run(() => new List<Dealer>()
        {
            new Dealer()
            {
                Id = 1,
                Name = "Name1",
                Country = new Country(){ Code="US", Name="USA" }
            },
            new Dealer()
            {
                Id = 1,
                Name = "Name2",
                Country = new Country(){ Code="CA", Name="Canada" }
            }
        });

    }
}
