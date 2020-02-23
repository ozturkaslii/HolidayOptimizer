using System.Threading.Tasks;
using HolidayOptimizer.API.Clients;
using HolidayOptimizer.API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace HolidayOptimizer.UnitTest
{
    [TestClass]
    public class HolidayOptimizerControllerTest
    {
        private readonly HolidayOptimizerController _holidayOptimizerController;
        private readonly Mock<IMediator> _mediator;

        public HolidayOptimizerControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _holidayOptimizerController = new HolidayOptimizerController(_mediator.Object);
        }

        [TestMethod]
        public async Task HolidayOptimizerController_GetCountryWithMostHolidaysByYear_Should_Return_Ok_Result()
        {
            var actionResult = await _holidayOptimizerController.GetCountryWithMostHolidaysByYear(2020);
            Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)actionResult).StatusCode);
        }

        [TestMethod]
        public async Task HolidayOptimizerController_GetMonthWithMostHolidaysByYear_Should_Return_Ok_Result()
        {
            var actionResult = await _holidayOptimizerController.GetMonthWithMostHolidaysByYear(2020);
            Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)actionResult).StatusCode);
        }

        [TestMethod]
        public async Task HolidayOptimizerController_GetCountryWithMostUniqueHolidaysByYear_Should_Return_Ok_Result()
        {
            var actionResult = await _holidayOptimizerController.GetCountryWithMostUniqueHolidaysByYear(2020);
            Assert.AreEqual(StatusCodes.Status200OK, ((OkObjectResult)actionResult).StatusCode);
        }
    }
}
