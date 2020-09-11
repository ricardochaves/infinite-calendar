using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

using InfiniteCalendar.Models;

namespace InfiniteCalendar.Business
{
    public class CalendarBuilder
    {
        private readonly int _year;
        private readonly List<Holiday> _holidays;
        private readonly List<Month> _months;
        private readonly DateTime _firstDayOfTheYear;
        private readonly DateTime _lastDayOfTheYear;

        public CalendarBuilder(int year, List<Holiday> holidays)
        {
            _year = year;
            _holidays = holidays;
            _firstDayOfTheYear = new DateTime(year, 1, 1);
            _lastDayOfTheYear = new DateTime(year, 12, 31);
            _months = new List<Month>();
        }

        public Calendar build()
        {
            buildMonthsList();
            return new Calendar(_year, _months);
        }

        private void buildMonthsList()
        {
            var nextDay = _firstDayOfTheYear;

            while (nextDay <= _lastDayOfTheYear)
            {
                var d = createDay(nextDay);
                addDayForCorrectMonth(d, nextDay);
                nextDay = nextDay.AddDays(1);
            }
        }

        private Day createDay(DateTime dt)
        {
            var holiday = _holidays.FirstOrDefault(h => h.DateTime.Month == dt.Month && h.DateTime.Day == dt.Day);
            return new Day(dt.Day, (holiday != null), holiday, new DateTime(dt.Year, dt.Month, dt.Day));
        }

        private void addDayForCorrectMonth(Day day, DateTime dt)
        {
            var month = _months.FirstOrDefault(m => m.number == dt.Month);

            if (month == null)
            {
                var days = new List<Day> { day };
                _months.Add(new Month(dt.Month.ToString(), dt.Month, dt.Month.ToString(), days));
            }
            else
            {
                month.days.Add(day);
            }
        }
    }
}
