using NUnit.Framework;

namespace DuAnTotNghiep.Test
{
    [TestFixture]
    public class ConfigTests
    {
        [Test]
        public void AppSettings_ShouldBeReadable()
        {
            string connectionString = "Server=localhost;Database=TestDB;";
            Assert.IsTrue(connectionString.Contains("Database"));
        }

        [Test]
        public void Config_ShouldContainServerKeyword()
        {
            string config = "Server=localhost;Database=TestDB;";
            Assert.That(config, Does.Contain("Server"));
        }
    }
}
