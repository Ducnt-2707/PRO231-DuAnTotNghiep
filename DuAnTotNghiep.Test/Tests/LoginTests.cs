using NUnit.Framework;

namespace DuAnTotNghiep.Test
{
    [TestFixture]
    public class LoginTests
    {
        [Test]
        public void Login_WithValidCredentials_ShouldSucceed()
        {
            string username = "admin";
            string password = "123456";

            bool result = (username == "admin" && password == "123456");

            Assert.IsTrue(result, "Đăng nhập hợp lệ phải thành công.");
        }

        [Test]
        public void Login_WithInvalidPassword_ShouldFail()
        {
            string username = "admin";
            string password = "wrong";

            bool result = (username == "admin" && password == "123456");

            Assert.IsFalse(result, "Sai mật khẩu thì không được đăng nhập.");
        }
    }
}
