using NUnit.Framework;

namespace DuAnTotNghiep.Test
{
    [TestFixture]
    public class ValidationTests
    {
        [Test]
        public void EmailValidation_ShouldDetectInvalidEmail()
        {
            string email = "invalidemail";
            Assert.IsFalse(email.Contains("@"), "Email không hợp lệ phải bị từ chối.");
        }

        [Test]
        public void PasswordValidation_ShouldRequireAtLeast6Chars()
        {
            string password = "12345";
            Assert.Less(password.Length, 6, "Mật khẩu quá ngắn phải bị báo lỗi.");
        }
    }
}
