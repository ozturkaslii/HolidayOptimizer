using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HolidayOptimizer.API.Cache;
using HolidayOptimizer.API.Enums;
using HolidayOptimizer.API.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace HolidayOptimizer.API.Clients
{
    public class NagerService : INagerService
    {
        private static readonly string[] CountryCodes = {
            "AD", "AR", "AT", "AU", "AX", "BB", "BE", "BG", "BO", "BR", "BS", "BW", "BY", "BZ", "CA", "CH", "CL", "CN",
            "CO", "CR", "CU", "CY", "CZ", "DE", "DK", "DO", "EC", "EE", "EG", "ES", "FI", "FO", "FR", "GA", "GB", "GD",
            "GL", "GR", "GT", "GY", "HN", "HR", "HT", "HU", "IE", "IM", "IS", "IT", "JE", "JM", "LI", "LS", "LT", "LU",
            "LV", "MA", "MC", "MD", "MG", "MK", "MT", "MX", "MZ", "NA", "NI", "NL", "NO", "NZ", "PA", "PE", "PL", "PR",
            "PT", "PY", "RO", "RS", "RU", "SE", "SI", "SJ", "SK", "SM", "SR", "SV", "TN", "TR", "UA", "US", "UY", "VA",
            "VE", "ZA"
        };

        private const string Endpoint = "/Api/v2/PublicHolidays";
        private readonly HttpClient _httpClient;
        private readonly IHolidayCache _cache;

        /// <inheritdoc />
        public NagerService(HttpClient httpClient, IHolidayCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        /// <summary>
        /// Get country that has the most holidays in this specific year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<string> GetCountryWithMostHolidayByYear(int year)
        {
            var holidays = new List<HolidayModel>();

            foreach (var countryCode in CountryCodes)
            {
                var publicHolidays = await await _cache.GetOrCreateCacheAsync(
                    () => _httpClient.GetStringAsync($"{Endpoint}/{year}/{countryCode}")
                    , $"GetCountryWithMostHolidayByYear_{year}_{countryCode}");

               
                var publicHolidaysClientModels = JsonConvert.DeserializeObject<List<PublicHolidayClientModel>>(publicHolidays);

                holidays.Add(new HolidayModel
                {
                    CountryCode = countryCode,
                    PublicHolidayClientModels = publicHolidaysClientModels
                });
            }

            return holidays.OrderByDescending(l => l.TotalCount).First().CountryCode;
        }

        /// <summary>
        /// Get month which has most holiday
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<string> GetMonthWithMostHolidays(int year)
        {
            var holidayMonthsDictionary = new Dictionary<int, int>();

            foreach (var countryCode in CountryCodes)
            {
                var publicHolidays = await await _cache.GetOrCreateCacheAsync(
                    () => _httpClient.GetStringAsync($"{Endpoint}/{year}/{countryCode}")
                    , $"GetMonthWithMostHolidays_{year}_{countryCode}");

                var publicHolidayClientModels = JsonConvert.DeserializeObject<List<PublicHolidayClientModel>>(publicHolidays);

                //Calculate months based on holidays
                foreach (var model in publicHolidayClientModels)
                {
                    var holidayMonth = model.Date.Month;
                    if (!holidayMonthsDictionary.TryGetValue(holidayMonth, out int count))
                    {
                        holidayMonthsDictionary.Add(holidayMonth, 1);
                    }
                    else
                    {
                        holidayMonthsDictionary[holidayMonth] = count + 1;
                    }
                }
            }

            var month = holidayMonthsDictionary.OrderByDescending(q => q.Value).FirstOrDefault().Key;

            return Enum.GetName(typeof(Months), month);
        }

        /// <summary>
        /// Get days that no other country had a holiday
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public async Task<string> GetCountryWithMostUniqueHolidays(int year)
        {
            var holidayDictionary = new Dictionary<string, string>();

            foreach (var countryCode in CountryCodes)
            {
                var publicHolidays = await await _cache.GetOrCreateCacheAsync(
                    () => _httpClient.GetStringAsync($"{Endpoint}/{year}/{countryCode}")
                    , $"GetCountryWithMostUniqueHolidays_{year}_{countryCode}");

                var publicHolidaysClientModels = JsonConvert.DeserializeObject<List<PublicHolidayClientModel>>(publicHolidays);

                foreach (var model in publicHolidaysClientModels)
                {
                    var day = $"{model.Date.Month}-{model.Date.Day}";
                    
                    if (!holidayDictionary.ContainsKey(day))
                    {
                        holidayDictionary.Add(day, countryCode);
                    }
                    else
                    {
                        holidayDictionary.Remove(day);
                    }
                }
            }

            var uniqueHolidayCountry = holidayDictionary.GroupBy(p => p.Value)
                .ToDictionary(q => q.Key, r => r.Select(s => s.Key).ToList()).OrderByDescending(q => q.Value.Count)
                .FirstOrDefault();

            return uniqueHolidayCountry.Key;
        }
    }
}