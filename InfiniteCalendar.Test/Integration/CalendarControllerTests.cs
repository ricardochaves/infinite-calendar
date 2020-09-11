using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using InfiniteCalendar.Models;
using InfiniteCalendar.Test.Support;
using Newtonsoft.Json;
using Xunit;

namespace InfiniteCalendar.Test.Integration
{
    public class CalendarControllerTests: TestingCaseFixture<TestingStartUp>
    {
        [Fact]
        public async Task getShouldReturnStatusCodeOK()
        {
            // Arrange
            var holidays = new List<Holiday>()
            {
                new Holiday(null,dateTime:new DateTime(1891,1,1), name:"New Year's Day", null),
                new Holiday(null,dateTime:new DateTime(1891,4,10), name:"Good Friday", null),
                new Holiday(null,dateTime:new DateTime(1891,4,21), name:"Tiradentes Day", null),
                new Holiday(null,dateTime:new DateTime(1891,5,1), name:"Labour Day", null),
                new Holiday(null,dateTime:new DateTime(1891,6,11), name:"Corpus Christi", null),
                new Holiday(null,dateTime:new DateTime(1891,9,7), name:"Independence Day", null),
                new Holiday(null,dateTime:new DateTime(1891,10,12), name:"Lady of Aparecida", null),
                new Holiday(null,dateTime:new DateTime(1891,11,2), name:"All Souls' Day", null),
                new Holiday(null,dateTime:new DateTime(1891,11,15), name:"Republic Day", null),
                new Holiday(null,dateTime:new DateTime(1891,12,25), name:"Christmas Day", null),

                new Holiday(null,dateTime:new DateTime(1891,11,12), name:"Fake", 1999),

            };

            await _context.Holidays.AddRangeAsync(holidays);
            await _context.SaveChangesAsync();
            
            // Act
            var response = await _client.GetAsync($"/calendar/2020");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var calendar =  JsonConvert.DeserializeObject<Calendar>(await response.Content.ReadAsStringAsync());
            Assert.True(calendar.months.First().days.First().isHoliday);
            
            Assert.Equal(12, calendar.months.Count);
            
            var day = calendar.months.First(m => m.number == 11).days.First(d => d.day == 12);
            Assert.False(day.isHoliday);
        }
    }
}
