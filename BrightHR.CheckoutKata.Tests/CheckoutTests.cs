using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrightHR.CheckoutKata.Tests
{
    [TestFixture]
    internal class CheckoutTests
    {
        #region Scan Items 
        
        [Test]
        public void Test_ScanItems_NullRequest_ExpectException()
        {
            // ARRANGE

            // ACT

            // ASSERT
        }

        [Test]
        public void Test_ScanItems_RequestWithNoItemSku_ExpectException()
        {
            // ARRANGE

            // ACT

            // ASSERT
        }

        [Test]
        public void Test_ScanItems_ValidRequest_CheckCart()
        {
            // ARRANGE

            // ACT

            // ASSERT
        }

        #endregion

        [Test]
        public void Test_AddSingleItem_CheckTotal()
        {
            // ARRANGE

            // ACT

            // ASSERT
        }

        [Test]
        public void Test_AddOneOfEachItem_CheckTotal()
        {
            // ARRANGE

            // ACT

            // ASSERT
        }

        [Test]
        public void Test_AddMultipliesOfSomeItems_CheckTotal()
        {
            // ARRANGE

            // ACT

            // ASSERT
        }

        [Test]
        public void Test_AddSingleItemWithItemOffer_CheckTotal()
        {
            // ARRANGE

            // ACT

            // ASSERT
        }

        [Test]
        public void Test_AddMixtureOfItemsWithItemOffers_CheckTotal()
        {
            // ARRANGE

            // ACT

            // ASSERT
        }
    }
}
