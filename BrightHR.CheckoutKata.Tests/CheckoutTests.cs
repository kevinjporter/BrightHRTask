using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BrightHR.CheckoutKata.Tests;

[TestFixture]
internal class CheckoutTests
{
    private List<Item> _itemData;

    [SetUp]
    public void SetupItemData()
    {
        _itemData = new List<Item>
        {
            {
                ItemBuilder
                    .Start()
                        .WithSku("A")
                        .WithUnitPriceAndOffer(50, 3, 130)
                    .Build()
            },

            {
                ItemBuilder
                    .Start()
                        .WithSku("B")
                        .WithUnitPriceAndOffer(30, 2, 45)
                    .Build()
            },

            {
                 ItemBuilder
                    .Start()
                        .WithSku("C")
                        .WithUnitPrice(20)
                    .Build()
            },

            {
                ItemBuilder
                    .Start()
                        .WithSku("D")
                        .WithUnitPrice(15)
                    .Build()
            }
        };
    }

    #region Scan Items 
    
    [Test]
    public void Test_ScanItems_NullRequest_ExpectException()
    {
        // ARRANGE
        var checkout = new CheckoutManager(_itemData);

        // ACT

        // ASSERT
        Assert.That(() => checkout.ScanProduct(null), Throws.TypeOf<ArgumentNullException>().With.Message.Contain("Scan product request is empty"));
    }

    [Test]
    public void Test_ScanItems_RequestWithNoItemSku_ExpectException()
    {
        // ARRANGE
        var checkout = new CheckoutManager(_itemData);
        var request = new ScanProductRequest("");

        // ACT

        // ASSERT
        Assert.That(() => checkout.ScanProduct(request), Throws.TypeOf<ArgumentNullException>().With.Message.Contain("Item sku is required"));
    }

    [Test]
    public void Test_ScanItems_ValidRequest_CheckCart()
    {
        // ARRANGE
        var checkoutManager = new CheckoutManager(_itemData);
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

    [Test]
    public void Test_ScanItem_SkuNotFound_ExpectException()
    {
        // ARRANGE
        var checkout = new CheckoutManager(_itemData);
        var request = new ScanProductRequest("ZZ");

        // ACT

        // ASSERT
        Assert.That(() => checkout.ScanProduct(request), Throws.TypeOf<Exception>().With.Message.Contain("Item with SKU 'ZZ' not found and will not be added to checkout"));
    }

    #endregion

    #region Get Total Price

    [Test]
    public void Test_AddSingleItem_CheckTotal()
    {
        // ARRANGE
        var checkout = new CheckoutManager(_itemData);

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
        var checkout = new CheckoutManager(_itemData);

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
        var checkout = new CheckoutManager(_itemData);

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
        var checkout = new CheckoutManager(_itemData);

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
        var checkout = new CheckoutManager(_itemData);

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
        var checkout = new CheckoutManager(_itemData);

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

    #endregion
}
