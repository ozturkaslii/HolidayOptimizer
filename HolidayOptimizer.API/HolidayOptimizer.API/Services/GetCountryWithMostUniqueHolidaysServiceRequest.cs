using System.Threading;
using System.Threading.Tasks;
using HolidayOptimizer.API.Clients;
using MediatR;

namespace HolidayOptimizer.API.Services
{
    public class GetCountryWithMostUniqueHolidaysServiceRequest : IRequest<string>
    {
        public GetCountryWithMostUniqueHolidaysServiceRequest(int year)
        {
            Year = year;
        }

        public int Year { get; set; }
    }

    public class GetCountryWithMostUniqueHolidaysServiceRequestHandler : IRequestHandler<GetCountryWithMostUniqueHolidaysServiceRequest, string>
    {
        private readonly INagerService _client;

        public GetCountryWithMostUniqueHolidaysServiceRequestHandler(INagerService client)
        {
            _client = client;
        }

        public async Task<string> Handle(GetCountryWithMostUniqueHolidaysServiceRequest request, CancellationToken cancellationToken)
        {
            var response = await _client.GetCountryWithMostUniqueHolidays(request.Year);
            return response;
        }
    }
}