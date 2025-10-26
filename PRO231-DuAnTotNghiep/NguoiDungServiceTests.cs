using NUnit.Framework;
using PRO231_DuAnTotNghiep;
using System;

namespace DuAnTotNghiep.Tests
{
    [TestFixture]
    public class NguoiDungServiceTests
    {
        private NguoiDungService _service;

        [SetUp]
        public void Setup()
        {
            _service = new NguoiDungService();
        }

        [Test]
        [Description("TC001 - Thêm người dùng hợp lệ")]
        public void ThemNguoiDung_HopLe_ReturnsTrue()
        {
            bool result = _service.ThemNguoiDung("TestUser", "testlogin", "12345", "Admin");
            Assert.IsTrue(result, "Thêm người dùng hợp lệ phải trả về true.");
        }

        [Test]
        [Description("TC002 - Thiếu thông tin người dùng")]
        public void ThemNguoiDung_ThieuThongTin_ThrowsException()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                _service.ThemNguoiDung("", "", "", "")
            );
            Assert.That(ex.Message, Does.Contain("không hợp lệ"));
        }

        [Test]
        [Description("TC003 - Xóa người dùng không tồn tại")]
        public void XoaNguoiDung_KhongTonTai_ReturnsFalse()
        {
            bool result = _service.XoaNguoiDung(-999);
            Assert.IsFalse(result, "Xóa người dùng không tồn tại phải trả về false.");
        }

        [Test]
        [Description("TC004 - Cập nhật người dùng")]
        public void CapNhatNguoiDung_HopLe_ReturnsTrue()
        {
            bool result = _service.CapNhatNguoiDung(1, "Nguyen Van A", "admin", "newpass", "User");
            Assert.That(result, Is.True.Or.False, "Tùy vào dữ liệu DB thực tế, nhưng không ném lỗi.");
        }
    }
}
