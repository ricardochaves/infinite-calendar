using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using InfiniteCalendar.Business;
using InfiniteCalendar.Models;
using InfiniteCalendar.Test.Support;

using Xunit;

namespace InfiniteCalendar.Test.Unit.Business
{
    public class CalendarBuilderTests : TestingCaseFixture<TestingStartUp>
    {
        [Fact]
        public void shouldCreateCalendarWithOneHoliday()
        {
            // Arrange
            var holidays = new List<Holiday>()
            {
                new Holiday(null, new DateTime(2020,1,1), "holiday_1", null)
            };

            var calendarbuilder = new CalendarBuilder(2020, holidays);

            // Act
            var calendar = calendarbuilder.build();

            // Assert
            Assert.Equal(12, calendar.months.Count);
            Assert.Equal(31, calendar.months.First().days.Count());
            Assert.Equal(31, calendar.months.Last().days.Count());
            Assert.True(calendar.months.First().days.First().isHoliday);
            Assert.False(calendar.months.First().days.Last().isHoliday);
        }


    }


}
