using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using InfiniteCalendar.Extensions;
using InfiniteCalendar.Models;
using InfiniteCalendar.Models.DTOs;
using InfiniteCalendar.Parameters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InfiniteCalendar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HolidayController : ControllerBase
    {
        private readonly ILogger<HolidayController> _logger;
        private readonly InfiniteCalendarContext _context;
        private readonly IMapper _mapper;

        public HolidayController(ILogger<HolidayController> logger, InfiniteCalendarContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<HolidayDto>> Get([FromQuery] PaginationParameters pagination)
        {

            return await _mapper.ProjectTo<HolidayDto>(_context.Holidays
                                                                       .OrderBy(x => x.Id)
                                                                       .getPage(pagination)
                                                        ).ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HolidayDto>> Get(int id)
        {
            var dt = await _context.Holidays.Where(h => h.Id == id)
                                                        .ProjectTo<HolidayDto>(_mapper.ConfigurationProvider)
                                                        .FirstOrDefaultAsync();

            if (dt == null)
                return NotFound();

            return dt;

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] HolidayDto holidayDto)
        {
            if (id != holidayDto.Id)
                return BadRequest();

            if (!_context.Holidays.Any(e => e.Id == id))
                return NotFound();

            var holiday = _mapper.Map<Holiday>(holidayDto);
            _context.Entry(holiday).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<HolidayDto>> Post([FromBody] HolidayDto holidayDto)
        {
            var holiday = _mapper.Map<Holiday>(holidayDto);

            await _context.Holidays.AddAsync(holiday);
            await _context.SaveChangesAsync();

            var addedHolidayDto = _mapper.Map<HolidayDto>(holiday);
            return CreatedAtAction(nameof(Get), new { id = addedHolidayDto.Id }, addedHolidayDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            var holiday = await _context.Holidays.FindAsync(id);
            if (holiday == null)
            {
                return NotFound();
            }

            _context.Holidays.Remove(holiday);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
