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
    public void Test_ScanItems_NullRequest_ExpectErrorResponse()
    {
        // ARRANGE
        var checkout = new CheckoutManager(_itemData);

        // ACT
        var response = checkout.ScanProduct(null);

        // ASSERT
        Assert.That(response.ItemScanned, Is.EqualTo(false));
        Assert.That(response.ErrorMessage, Is.EqualTo(CheckoutErrors.RequestIsNull));
    }

    [Test]
    public void Test_ScanItems_RequestWithNoItemSku_ExpectErrorResponse()
    {
        // ARRANGE
        var checkout = new CheckoutManager(_itemData);
        var request = new ScanProductRequest("");

        // ACT
        var response = checkout.ScanProduct(request);

        // ASSERT
        Assert.That(response.ItemScanned, Is.EqualTo(false));
        Assert.That(response.ErrorMessage, Is.EqualTo(CheckoutErrors.ItemSkuIsRequired));
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
        var scan1Response = checkoutManager.ScanProduct(scan1);
        var scan2Response = checkoutManager.ScanProduct(scan2);
        var scan3Response = checkoutManager.ScanProduct(scan3);

        // ASSERT
        Assert.That(scan1Response.ItemScanned, Is.EqualTo(true));
        Assert.That(scan1Response.ErrorMessage, Is.Null);
        
        Assert.That(scan2Response.ItemScanned, Is.EqualTo(true));
        Assert.That(scan2Response.ErrorMessage, Is.Null);
        
        Assert.That(scan3Response.ItemScanned, Is.EqualTo(true));
        Assert.That(scan3Response.ErrorMessage, Is.Null);

        var cart = checkoutManager.GetBasket();
        Assert.That(cart.Count, Is.EqualTo(3));
    }

    [Test]
    public void Test_ScanItem_SkuNotFound_ExpectErrorResponse()
    {
        // ARRANGE
        var checkout = new CheckoutManager(_itemData);
        var request = new ScanProductRequest("ZZ");

        // ACT
        var response = checkout.ScanProduct(request);

        // ASSERT
        Assert.That(response.ItemScanned, Is.EqualTo(false));
        Assert.That(response.ErrorMessage, Is.EqualTo(CheckoutErrors.ItemNotFound("ZZ")));
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
