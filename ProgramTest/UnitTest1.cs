using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using IdezetGeneratorNS;
using static IdezetGeneratorNS.Program;
using IdezetGenerator.Modell;

namespace ProgramTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ExistingMood_InvalidMood_ReturnsFalse()
        {
            //Arrange
            var mood = "invalid_mood";

            //Act
            var result = existingMood(mood);


            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ExistingMood_ValidMood_ReturnsTrue()
        {
            //Arrange
            var mood = "boldog";

            //Act
            var result = existingMood(mood);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task GetQuote_InvalidFilePath_ThrowsException()
        {
            //Arrange
            var filePath = "invalid_path.json";

            //Act and Assert
            await Assert.ThrowsExceptionAsync<FileNotFoundException>(async () => await GetQuote(filePath));
        }
    }
}