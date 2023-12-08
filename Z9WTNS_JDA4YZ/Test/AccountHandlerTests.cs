using System;
using System.IO;
using NUnit.Framework;
using Z9WTNS_JDA4YZ.Xml;

namespace Z9WTNS_JDA4YZ.Test
{
    [TestFixture]
    internal class AccountHandlerTests
    {
        private const string TestUsersPath = "test_users.xml";

        [SetUp]
        public void SetUp()
        {
            
            File.WriteAllText(TestUsersPath, "<?xml version=\"1.0\" encoding=\"utf-8\"?><Users></Users>");
        }
        [Test]
        public void Register_CorrectInput_CreatesUser()
        {
            Console.SetIn(new StringReader("John\npassword\nigen\n"));
            var result = AccountHandler.Register();

            Assert.That(result.Username, Is.EqualTo("John"));
            Assert.That(result.HashedPassword, Is.EqualTo(HashPassword("password"))); // Assuming HashPassword is a function
            Assert.That(result.isUnder25, Is.True);
            Assert.That(result.Id, Is.GreaterThanOrEqualTo(0));
        }

        private object? HashPassword(string v)
        {
            throw new NotImplementedException();
        }

        [TearDown]
        public void TearDown()
        {

            File.Delete(TestUsersPath);
        }

  

       
       
    
    }
}
