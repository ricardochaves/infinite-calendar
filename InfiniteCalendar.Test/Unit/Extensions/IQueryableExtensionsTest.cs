using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using InfiniteCalendar.Extensions;
using InfiniteCalendar.Parameters;
using InfiniteCalendar.Test.Support;

using Xunit;

namespace InfiniteCalendar.Test.Unit.Extensions
{
    public class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {1, 1, new List<string>{"h_1"}},
            new object[] {1, 2, new List<string>{"h_1", "h_2"}},
            new object[] {1, 3, new List<string>{"h_1", "h_2", "h_3"}},
            new object[] {2, 1, new List<string>{"h_2"}},
            new object[] {2, 2, new List<string>{"h_3", "h_4"}},
            new object[] {5, 1, new List<string>{"h_5"}},
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class QueryableExtensionsTest : TestingCaseFixture<TestingStartUp>
    {
        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public async Task getShouldReturnCorrectPage(int pageNumber, int pageSize, List<string> expectedNames)
        {
            // Arrange
            await new TestingScenarioBuilder(_context).buildScenarioWithFiveHolidaysWithSequenceName();
            var pagination = new PaginationParameters { pageNumber = pageNumber, pageSize = pageSize };

            // Act
            var holidaysNames = _context.Holidays.OrderBy(h => h.Id)
                                                             .getPage(pagination)
                                                             .Select(h => h.Name)
                                                             .ToList();

            // Assert
            Assert.Equal(expectedNames, holidaysNames);
        }
    }
}
