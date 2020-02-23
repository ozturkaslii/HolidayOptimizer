using System.Threading.Tasks;
using HolidayOptimizer.API.Filters;
using HolidayOptimizer.API.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HolidayOptimizer.API.Controllers
{
    [ApiController]
    [Route("api/holidays")]
    public class HolidayOptimizerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HolidayOptimizerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get country with most holidays
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("country-with-most-holidays")]
        [RequestRateLimit(Name = "GetCountryWithMostHolidaysByYear", Seconds = 3)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountryWithMostHolidaysByYear([FromQuery] int year)
        {
            var response = await _mediator.Send(new GetCountryWithMostHolidaysServiceRequest(year));
            return Ok(response);
        }

        /// <summary>
        /// Get month with most holidays
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("month-with-most-holidays")]
        [RequestRateLimit(Name = "GetMonthWithMostHolidaysByYear", Seconds = 3)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMonthWithMostHolidaysByYear([FromQuery] int year)
        {
            var response = await _mediator.Send(new GetMonthWithMostHolidaysServiceRequest(year));
            return Ok(response);
        }

        /// <summary>
        /// Get country with most unique holidays
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("country-with-most-unique-holidays")]
        [RequestRateLimit(Name = "GetCountryWithMostUniqueHolidaysByYear", Seconds = 3)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountryWithMostUniqueHolidaysByYear([FromQuery] int year)
        {
            var response = await _mediator.Send(new GetCountryWithMostUniqueHolidaysServiceRequest(year));
            return Ok(response);
        }
    }
}