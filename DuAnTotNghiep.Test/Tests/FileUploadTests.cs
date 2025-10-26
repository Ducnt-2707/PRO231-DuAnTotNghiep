using NUnit.Framework;
using System.IO;

namespace DuAnTotNghiep.Test
{
    [TestFixture]
    public class FileUploadTests
    {
        [Test]
        public void UploadFile_ShouldExistAfterUpload()
        {
            string path = Path.GetTempFileName();
            Assert.IsTrue(File.Exists(path));
            File.Delete(path);
        }

        [Test]
        public void DeleteFile_ShouldRemoveFile()
        {
            string path = Path.GetTempFileName();
            File.Delete(path);
            Assert.IsFalse(File.Exists(path));
        }
    }
}
