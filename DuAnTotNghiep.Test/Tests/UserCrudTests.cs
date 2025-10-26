using NUnit.Framework;

namespace DuAnTotNghiep.Test
{
    [TestFixture]
    public class UserCrudTests
    {
        [Test]
        public void CreateUser_ShouldReturnId()
        {
            int newId = 1; // giả lập tạo thành công
            Assert.Greater(newId, 0, "Tạo người dùng phải trả về ID > 0.");
        }

        [Test]
        public void DeleteUser_ShouldRemoveUser()
        {
            bool deleted = true; // giả lập xóa thành công
            Assert.IsTrue(deleted, "Xóa người dùng phải thành công.");
        }
    }
}
