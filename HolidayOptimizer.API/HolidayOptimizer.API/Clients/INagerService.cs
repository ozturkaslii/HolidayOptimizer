using System.Collections.Generic;
using System.Threading.Tasks;
using HolidayOptimizer.API.Models;

namespace HolidayOptimizer.API.Clients
{
    public interface INagerService
    {
        Task<string> GetCountryWithMostHolidayByYear(int year);
        Task<string> GetMonthWithMostHolidays(int year);
        Task<string> GetCountryWithMostUniqueHolidays(int year);
    }
}