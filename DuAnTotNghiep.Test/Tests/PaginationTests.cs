using NUnit.Framework;

namespace DuAnTotNghiep.Test
{
    [TestFixture]
    public class PaginationTests
    {
        [Test]
        public void Pagination_ShouldCalculateTotalPages()
        {
            int totalItems = 25;
            int pageSize = 10;
            int totalPages = (totalItems + pageSize - 1) / pageSize;

            Assert.AreEqual(3, totalPages);
        }

        [Test]
        public void Pagination_Page1_ShouldStartAtIndex0()
        {
            int pageSize = 10;
            int pageIndex = 1;
            int startIndex = (pageIndex - 1) * pageSize;

            Assert.AreEqual(0, startIndex);
        }
    }
}
