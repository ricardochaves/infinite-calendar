using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using InfiniteCalendar.Models;

namespace InfiniteCalendar.Test.Support
{
    public class TestingScenarioBuilder
    {
        private readonly InfiniteCalendarContext _context;
        private static Random random = new Random();

        public TestingScenarioBuilder(InfiniteCalendarContext context)
        {
            _context = context;
        }

        public async Task<List<Holiday>> buildScenarioWithTwentyHolidaysAsync(string name1 = "Room 1", string name2 = "Room 2",
            string name3 = "Room 3")
        {

            var holidays = new List<Holiday>();

            for (var i = 0; i < 20; i++)
            {
                holidays.Add(createHoliday());
            }

            await _context.AddRangeAsync(holidays);
            await _context.SaveChangesAsync();

            return holidays;
        }

        public async Task buildScenarioWithFiveHolidaysWithSequenceName()
        {
            await _context.AddAsync(new Holiday(null, DateTime.Now, "h_1"));
            await _context.AddAsync(new Holiday(null, DateTime.Now, "h_2"));
            await _context.AddAsync(new Holiday(null, DateTime.Now, "h_3"));
            await _context.AddAsync(new Holiday(null, DateTime.Now, "h_4"));
            await _context.AddAsync(new Holiday(null, DateTime.Now, "h_5"));
            await _context.SaveChangesAsync();
        }

        private static Holiday createHoliday(string name) => new Holiday(null, DateTime.Now, name);

        private Holiday createHoliday() => createHoliday(randomString(10));


        public static string randomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
