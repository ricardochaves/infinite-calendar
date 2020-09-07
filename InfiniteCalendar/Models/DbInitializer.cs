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
            context.Database.EnsureCreated();

            // Look for any holiday.
            if (context.Holidays.Any())
            {
                return;   // DB has been seeded
            }


            var holidays = new List<Holiday>()
            {
                new Holiday(null,dateTime:DateTime.Now, name:"hl_01", null)
            };

            holidays.ForEach(delegate (Holiday holiday) { context.Holidays.Add(holiday); });

            context.SaveChanges();

        }
    }
}
