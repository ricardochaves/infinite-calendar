using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper.Internal;

namespace InfiniteCalendar.Models
{
    public class DbInitializer
    {
        public static void Initialize(InfiniteCalendarContext context)
        {
            // context.Database.EnsureCreated();

            // // Look for any holiday.
            // if (context.Holidays.Any())
            // {
            //     return;   // DB has been seeded
            // }
            //
            //
            // var holidays = new List<Holiday>()
            // {
            //     new Holiday(null,dateTime:new DateTime(1891,1,1), name:"New Year's Day", null),
            //     new Holiday(null,dateTime:new DateTime(1891,4,10), name:"Good Friday", null),
            //     new Holiday(null,dateTime:new DateTime(1891,4,21), name:"Tiradentes Day", null),
            //     new Holiday(null,dateTime:new DateTime(1891,5,1), name:"Labour Day", null),
            //     new Holiday(null,dateTime:new DateTime(1891,6,11), name:"Corpus Christi", null),
            //     new Holiday(null,dateTime:new DateTime(1891,9,7), name:"Independence Day", null),
            //     new Holiday(null,dateTime:new DateTime(1891,10,12), name:"Lady of Aparecida", null),
            //     new Holiday(null,dateTime:new DateTime(1891,11,2), name:"All Souls' Day", null),
            //     new Holiday(null,dateTime:new DateTime(1891,11,15), name:"Republic Day", null),
            //     new Holiday(null,dateTime:new DateTime(1891,12,25), name:"Christmas Day", null)
            //
            // };
            //
            // context.Holidays.AddRange(holidays);
            // context.SaveChanges();

        }
    }
}
