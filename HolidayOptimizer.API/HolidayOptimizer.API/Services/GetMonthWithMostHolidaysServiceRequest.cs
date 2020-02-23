using System.Threading;
using System.Threading.Tasks;
using HolidayOptimizer.API.Clients;
using MediatR;

namespace HolidayOptimizer.API.Services
{
    public class GetMonthWithMostHolidaysServiceRequest : IRequest<string>
    {
        public GetMonthWithMostHolidaysServiceRequest(int year)
        {
            Year = year;
        }

        public int Year { get; set; }
    }

    public class GetMonthWithMostHolidaysServiceRequestHandler : IRequestHandler<GetMonthWithMostHolidaysServiceRequest, string>
    {
        private readonly INagerService _client;

        public GetMonthWithMostHolidaysServiceRequestHandler(INagerService client)
        {
            _client = client;
        }

        public async Task<string> Handle(GetMonthWithMostHolidaysServiceRequest request, CancellationToken cancellationToken)
        {
            var response = await _client.GetMonthWithMostHolidays(request.Year);
            return response;
        }
    }
}