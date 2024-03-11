using BrightHR.CheckoutKata.Interfaces;
using NSubstitute;
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
            var checkout = new CheckoutManager();

            // ACT

            // ASSERT
            Assert.That(() => checkout.ScanProduct(null), Throws.TypeOf<ArgumentNullException>().With.Message.Contain("Scan product request is empty"));
        }

        [Test]
        public void Test_ScanItems_RequestWithNoItemSku_ExpectException()
        {
            // ARRANGE
            var checkout = new CheckoutManager();
            var request = new ScanProductRequest("");

            // ACT

            // ASSERT
            Assert.That(() => checkout.ScanProduct(request), Throws.TypeOf<ArgumentNullException>().With.Message.Contain("Item sku is required"));
        }

        [Test]
        public void Test_ScanItems_ValidRequest_CheckCart()
        {
            // ARRANGE
            var checkoutManager = new CheckoutManager();
            var scan1 = new ScanProductRequest("A");
            var scan2 = new ScanProductRequest("B");
            var scan3 = new ScanProductRequest("A");

            // ACT
            checkoutManager.ScanProduct(scan1);
            checkoutManager.ScanProduct(scan2);
            checkoutManager.ScanProduct(scan3);

            // ASSERT
            var cart = checkoutManager.GetItems();
            Assert.That(cart.Count, Is.EqualTo(3));
        }

        #endregion

        [Test]
        public void Test_AddSingleItem_CheckTotal()
        {
            Assert.Inconclusive();

            // ARRANGE

            // ACT

            // ASSERT
        }

        [Test]
        public void Test_AddOneOfEachItem_CheckTotal()
        {
            Assert.Inconclusive();

            // ARRANGE

            // ACT

            // ASSERT
        }

        [Test]
        public void Test_AddMultipliesOfSomeItems_CheckTotal()
        {
            Assert.Inconclusive();

            // ARRANGE

            // ACT

            // ASSERT
        }

        [Test]
        public void Test_AddSingleItemWithItemOffer_CheckTotal()
        {
            Assert.Inconclusive();

            // ARRANGE

            // ACT

            // ASSERT
        }

        [Test]
        public void Test_AddMixtureOfItemsWithItemOffers_CheckTotal()
        {
            Assert.Inconclusive();

            // ARRANGE

            // ACT

            // ASSERT
        }
    }
}
