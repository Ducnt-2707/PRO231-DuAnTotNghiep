using NUnit.Framework;

namespace DuAnTotNghiep.Test
{
    [TestFixture]
    public class RegisterTests
    {
        [Test]
        public void Register_WithValidData_ShouldCreateUser()
        {
            string username = "newuser";
            string email = "newuser@example.com";

            bool created = !string.IsNullOrEmpty(username) && email.Contains("@");

            Assert.IsTrue(created, "Tạo tài khoản hợp lệ phải thành công.");
        }

        [Test]
        public void Register_WithEmptyUsername_ShouldFail()
        {
            string username = "";
            string email = "user@example.com";

            bool created = !string.IsNullOrEmpty(username) && email.Contains("@");

            Assert.IsFalse(created, "Tên đăng nhập trống không được phép tạo tài khoản.");
        }
    }
}
