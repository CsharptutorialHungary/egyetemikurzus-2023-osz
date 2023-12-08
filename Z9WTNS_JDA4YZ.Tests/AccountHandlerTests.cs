using System;
using System.IO;
using NUnit.Framework;
using Z9WTNS_JDA4YZ.DataClasses;
using Z9WTNS_JDA4YZ.Xml;
using Z9WTNS_JDA4YZ;

namespace Z9WTNS_JDA4YZ.Test
{
    [TestFixture]
    internal class AccountHandlerTests
    {

        [Test]
        public void CalculateNetIncome_Under25_ReturnsCorrectNetIncome()
        {
       
            decimal grossIncome = 50000m;
            User under25User = new User { isUnder25 = true };

       
            decimal netIncome = AccountHandler.CalculateNetIncome(grossIncome, under25User);

      
            decimal expectedNetIncome = grossIncome / 1.226993865m;
            Assert.AreEqual(expectedNetIncome, netIncome, "Net income calculation is incorrect for users under 25.");
        }

        [Test]
        public void CalculateNetIncome_Above25_ReturnsCorrectNetIncome()
        {
      
            decimal grossIncome = 70000m;
            User above25User = new User { isUnder25 = false };

       
            decimal netIncome = AccountHandler.CalculateNetIncome(grossIncome, above25User);

        
            decimal expectedNetIncome = grossIncome / 1.5037593398m;
            Assert.AreEqual(expectedNetIncome, netIncome, "Net income calculation is incorrect for users above 25.");
        }
        [Test]
        public void SavedMoney_CalculatesNonNegativeSavings()
        {

            decimal grossIncome = 60000m;
            User under25User = new User { isUnder25 = true };

            decimal savings = AccountHandler.SavedMoney(grossIncome, under25User);
            Assert.GreaterOrEqual(savings, 0m, "The calculated savings should not be negative.");
        }

        [Test]
        public void SavedMoney_CalculatesCorrectSavings()
        {
      
            decimal grossIncome = 80000m;
            User under25User = new User { isUnder25 = true };


            decimal actualSavings = AccountHandler.SavedMoney(grossIncome, under25User);

            User tempOver25 = new User { isUnder25 = false };
            decimal expectedSavings = AccountHandler.CalculateNetIncome(grossIncome, under25User) - AccountHandler.CalculateNetIncome(grossIncome, tempOver25);
                                      

            Assert.That(actualSavings, Is.EqualTo(expectedSavings).Within(0.0001m),
                "The calculated savings do not match the expected value within the acceptable tolerance.");
        }
    }
}
