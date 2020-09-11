using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InfiniteCalendar.Business;
using InfiniteCalendar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace InfiniteCalendar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController
    {
        private readonly ILogger<HolidayController> _logger;
        private readonly InfiniteCalendarContext _context;
        private readonly IMapper _mapper;

        public CalendarController(ILogger<HolidayController> logger, InfiniteCalendarContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{year}")]
        public ActionResult<Calendar> Get(int year)
        {
            var holidays = _context.Holidays.Where(h=>h.LimitYear>=year||h.LimitYear==null).ToList();
            var calendar = new CalendarBuilder(year, holidays).build();
            return calendar;
        }
    }
}
