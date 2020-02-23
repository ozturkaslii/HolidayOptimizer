using System.Collections.Generic;
using System.Linq;

namespace HolidayOptimizer.API.Models
{
    /// <summary>
    /// Holiday model
    /// </summary>
    public class HolidayModel
    {
        /// <summary>
        /// Country code
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Public holidays
        /// </summary>
        public IEnumerable<PublicHolidayClientModel> PublicHolidayClientModels { get; set; }

        /// <summary>
        /// Total holiday count for country
        /// </summary>
        public int TotalCount => PublicHolidayClientModels.Count();
    }
}