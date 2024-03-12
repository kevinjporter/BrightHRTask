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
            var checkout = new CheckoutManager(TestData.Products);

            // ACT

            // ASSERT
            Assert.That(() => checkout.ScanProduct(null), Throws.TypeOf<ArgumentNullException>().With.Message.Contain("Scan product request is empty"));
        }

        [Test]
        public void Test_ScanItems_RequestWithNoItemSku_ExpectException()
        {
            // ARRANGE
            var checkout = new CheckoutManager(TestData.Products);
            var request = new ScanProductRequest("");

            // ACT

            // ASSERT
            Assert.That(() => checkout.ScanProduct(request), Throws.TypeOf<ArgumentNullException>().With.Message.Contain("Item sku is required"));
        }

        [Test]
        public void Test_ScanItems_ValidRequest_CheckCart()
        {
            // ARRANGE
            var checkoutManager = new CheckoutManager(TestData.Products);
            var scan1 = new ScanProductRequest("A");
            var scan2 = new ScanProductRequest("B");
            var scan3 = new ScanProductRequest("A");

            // ACT
            checkoutManager.ScanProduct(scan1);
            checkoutManager.ScanProduct(scan2);
            checkoutManager.ScanProduct(scan3);

            // ASSERT
            var cart = checkoutManager.GetBasket();
            Assert.That(cart.Count, Is.EqualTo(3));
        }

        #endregion

        [Test]
        public void Test_AddSingleItem_CheckTotal()
        {
            // ARRANGE
            var checkout = new CheckoutManager(TestData.Products);

            // ACT
            checkout.ScanProduct(new ScanProductRequest("B"));

            // ASSERT
            var total = checkout.GetTotalPrice();
            Assert.That(total, Is.EqualTo(30));
        }

        [Test]
        public void Test_AddOneOfEachItem_CheckTotal()
        {
            // ARRANGE
            var checkout = new CheckoutManager(TestData.Products);

            // ACT
            checkout.ScanProduct(new ScanProductRequest("A"));
            checkout.ScanProduct(new ScanProductRequest("B"));
            checkout.ScanProduct(new ScanProductRequest("C"));
            checkout.ScanProduct(new ScanProductRequest("D"));

            // ASSERT
            var total = checkout.GetTotalPrice();
            Assert.That(total, Is.EqualTo(115));
        }

        [Test]
        public void Test_AddMultipliesOfSomeItems_CheckTotal()
        {
            // ARRANGE
            var checkout = new CheckoutManager(TestData.Products);

            // ACT
            checkout.ScanProduct(new ScanProductRequest("C"));
            checkout.ScanProduct(new ScanProductRequest("A"));
            checkout.ScanProduct(new ScanProductRequest("C"));

            // ASSERT
            var total = checkout.GetTotalPrice();
            Assert.That(total, Is.EqualTo(90));
        }

        [Test]
        public void Test_TriggerItemOfferOnce_ForOneProduct_CheckTotal()
        {
            // ARRANGE
            var checkout = new CheckoutManager(TestData.Products);

            // ACT
            checkout.ScanProduct(new ScanProductRequest("B"));
            checkout.ScanProduct(new ScanProductRequest("A"));
            checkout.ScanProduct(new ScanProductRequest("B"));

            // ASSERT
            var total = checkout.GetTotalPrice();
            Assert.That(total, Is.EqualTo(95));
        }

        [Test]
        public void Test_TriggerItemOfferTwice_ForOneProduct_CheckTotal()
        {
            // ARRANGE
            var checkout = new CheckoutManager(TestData.Products);

            // ACT
            checkout.ScanProduct(new ScanProductRequest("B"));
            checkout.ScanProduct(new ScanProductRequest("B"));
            checkout.ScanProduct(new ScanProductRequest("B"));
            checkout.ScanProduct(new ScanProductRequest("B"));

            // ASSERT
            var total = checkout.GetTotalPrice();
            Assert.That(total, Is.EqualTo(90));
        }

        [Test]
        public void Test_TriggerItemOffersForDifferentProducts_CheckTotal()
        {
            // ARRANGE
            var checkout = new CheckoutManager(TestData.Products);

            // ACT
            checkout.ScanProduct(new ScanProductRequest("B"));
            checkout.ScanProduct(new ScanProductRequest("B"));
            checkout.ScanProduct(new ScanProductRequest("B"));

            checkout.ScanProduct(new ScanProductRequest("A"));
            checkout.ScanProduct(new ScanProductRequest("A"));
            checkout.ScanProduct(new ScanProductRequest("A"));

            // ASSERT
            var total = checkout.GetTotalPrice();
            Assert.That(total, Is.EqualTo(205));
        }
    }
}
