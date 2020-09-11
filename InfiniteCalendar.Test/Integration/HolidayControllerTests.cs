using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using InfiniteCalendar.Models;
using InfiniteCalendar.Models.DTOs;
using InfiniteCalendar.Test.Support;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using Xunit;

namespace InfiniteCalendar.Test.Integration
{
    public class HolidayControllerTests : TestingCaseFixture<TestingStartUp>
    {
        [Theory]
        [InlineData("/holiday")]
        [InlineData("/holiday?pageSize=1&pageNumber=3")]
        public async Task getShouldReturnFirstPage(string url)
        {
            // Arrange
            await new TestingScenarioBuilder(_context).buildScenarioWithTwentyHolidaysAsync();

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task getShouldReturnSingleHoliday()
        {
            // Arrange
            var holidays = await new TestingScenarioBuilder(_context).buildScenarioWithTwentyHolidaysAsync();

            // Act
            var response = await _client.GetAsync($"/holiday/{holidays.First().Id}");

            // Assert
            var holiday = JsonConvert.DeserializeObject<Holiday>(await response.Content.ReadAsStringAsync());
            Assert.Equal(holidays.First().Id, holiday.Id);
        }

        [Fact]
        public async Task getShouldReturnNotFoundWhenIdNotExists()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync($"/holiday/99999");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task putShouldUpdateNameAndDate()
        {
            // Arrange
            var holidays = await new TestingScenarioBuilder(_context).buildScenarioWithTwentyHolidaysAsync();

            var holidayForUpdate = holidays.First();
            holidayForUpdate.Name = "New Nome";
            holidayForUpdate.DateTime = DateTime.MaxValue;

            var putData = JsonConvert.SerializeObject(holidayForUpdate);
            var payload = new StringContent(putData, Encoding.UTF8, "application/json");

            _context.Entry(holidayForUpdate).State = EntityState.Detached;

            // Act
            var response = await _client.PutAsync($"/holiday/{holidayForUpdate.Id}", payload);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            var updatedHoliday = await _context.FindAsync<Holiday>(holidayForUpdate.Id);
            Assert.Equal(DateTime.MaxValue, updatedHoliday.DateTime);
            Assert.Equal("New Nome", updatedHoliday.Name);

        }

        [Fact]
        public async Task putShouldReturnBadRequestWhenUrlIdIsDifferentOfBodyId()
        {
            // Arrange
            var holidays = await new TestingScenarioBuilder(_context).buildScenarioWithTwentyHolidaysAsync();

            var holidayForUpdate = holidays.First();

            var putData = JsonConvert.SerializeObject(holidayForUpdate);
            var payload = new StringContent(putData, Encoding.UTF8, "application/json");

            _context.Entry(holidayForUpdate).State = EntityState.Detached;

            // Act
            var response = await _client.PutAsync($"/holiday/99999", payload);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Fact]
        public async Task putShouldReturnNotFoundWhenIdNotExists()
        {
            // Arrange
            var holidayForUpdate = new Holiday(99999, DateTime.Now, "test_1", null);

            var putData = JsonConvert.SerializeObject(holidayForUpdate);
            var payload = new StringContent(putData, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/holiday/99999", payload);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        }

        [Fact]
        public async Task postShouldCreateNewHoliday()
        {
            // Arrange
            var newHolidayDto = new HolidayDto(null, DateTime.Now, "new_holiday", null);

            var putData = JsonConvert.SerializeObject(newHolidayDto);
            var payload = new StringContent(putData, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync($"/holiday", payload);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var addedHolidayDto = JsonConvert.DeserializeObject<HolidayDto>(await response.Content.ReadAsStringAsync());
            var holidayFromDb = await _context.FindAsync<Holiday>(addedHolidayDto.Id);
            Assert.NotNull(holidayFromDb);
            Assert.Equal(addedHolidayDto.Name, holidayFromDb.Name);
            var x = response.Headers.Location.AbsolutePath; // /Holiday/765
        }

        [Fact]
        public async Task postShouldBadRequestWhenDataIsInvalid()
        {
            // Arrange
            var newHolidayDto = new HolidayDto(null, DateTime.Now, "new_holiday", null);

            var putData = "{'Id':null,'DateTime':'2020-09-06T14:03:08.87279-03:00'}";
            var payload = new StringContent(putData, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync($"/holiday", payload);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task deleteShouldRemoveHoliday()
        {
            // Arrange
            var holidays = await new TestingScenarioBuilder(_context).buildScenarioWithTwentyHolidaysAsync();
            var holidayForUpdate = holidays.First();
            // Act
            var response = await _client.DeleteAsync($"/holiday/{holidayForUpdate.Id}");

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal(19, _context.Holidays.Count());
        }
    }
}
