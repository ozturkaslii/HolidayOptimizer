using HolidayOptimizer.API.Clients;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace HolidayOptimizer.API.Services
{
    public class GetCountryWithMostHolidaysServiceRequest : IRequest<string>
    {
        public GetCountryWithMostHolidaysServiceRequest(int year)
        {
            Year = year;
        }
            
        public int Year { get; set; }
    }

    public class GetCountryWithMostHolidaysServiceRequestHandler : IRequestHandler<GetCountryWithMostHolidaysServiceRequest, string>
    {
        private readonly INagerService _client;

        public GetCountryWithMostHolidaysServiceRequestHandler(INagerService client)
        {
            _client = client;
        }

        public async Task<string> Handle(GetCountryWithMostHolidaysServiceRequest request, CancellationToken cancellationToken)
        {
            var response = await _client.GetCountryWithMostHolidayByYear(request.Year);
            return response;
        }
    }
}