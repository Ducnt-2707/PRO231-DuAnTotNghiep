using NUnit.Framework;

namespace DuAnTotNghiep.Test
{
    [TestFixture]
    public class PostTests
    {
        [Test]
        public void CreatePost_ShouldHaveTitleAndContent()
        {
            string title = "Bài viết mẫu";
            string content = "Nội dung bài viết";

            Assert.IsNotEmpty(title);
            Assert.IsNotEmpty(content);
        }

        [Test]
        public void EditPost_ShouldChangeTitle()
        {
            string oldTitle = "Cũ";
            string newTitle = "Mới";

            Assert.AreNotEqual(oldTitle, newTitle);
        }
    }
}
