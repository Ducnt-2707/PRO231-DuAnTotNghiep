using NUnit.Framework;
using System.Collections.Generic;

namespace DuAnTotNghiep.Test
{
    [TestFixture]
    public class SearchTests
    {
        [Test]
        public void Search_ReturnsResults_WhenKeywordExists()
        {
            var data = new List<string> { "apple", "banana", "test" };
            var results = data.FindAll(x => x.Contains("test"));

            Assert.IsNotEmpty(results, "Kết quả tìm kiếm phải có khi từ khóa tồn tại.");
        }

        [Test]
        public void Search_ReturnsEmpty_WhenNoMatch()
        {
            var data = new List<string> { "apple", "banana" };
            var results = data.FindAll(x => x.Contains("xyz"));

            Assert.IsEmpty(results, "Không có kết quả khi không khớp từ khóa.");
        }
    }
}
