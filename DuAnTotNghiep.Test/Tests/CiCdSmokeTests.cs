using NUnit.Framework;

namespace DuAnTotNghiep.Test
{
    [TestFixture]
    public class CiCdSmokeTests
    {
        [Test]
        public void BuildSmokeTest_ShouldAlwaysPass()
        {
            Assert.IsTrue(true, "Kiểm thử CI/CD cơ bản luôn pass.");
        }

        [Test]
        public void SimpleMath_ShouldAddCorrectly()
        {
            int sum = 2 + 3;
            Assert.AreEqual(5, sum);
        }
    }
}
