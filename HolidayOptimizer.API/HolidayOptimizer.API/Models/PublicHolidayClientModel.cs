using System;

namespace HolidayOptimizer.API.Models
{
    public class PublicHolidayClientModel
    {
        /// <summary>
        /// Date of the holiday
        /// </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Local name of the holiday
        /// </summary>
        public string LocalName { get; set; }

        /// <summary>
        /// English name of the holiday
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Country code
        /// </summary>
        public string CountryCode { get; set; }
    }

}