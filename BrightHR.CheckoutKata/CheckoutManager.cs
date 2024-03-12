using BrightHR.CheckoutKata.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace BrightHR.CheckoutKata
{
    public class CheckoutManager : ICheckout
    {
        private IList<string> _basket = new List<string>();
        private IList<Item> _products = new List<Item>();

        public CheckoutManager(IList<Item> products)
        {
            _products = products;
        }

        public int GetTotalPrice()
        {
            if (_basket.Count == 0) return 0;

            var itemsAndQuantities = _basket.GroupBy(x => x).Select(x => new { ItemSku = x.Key, Quantity = x.Count() }).ToList();

            var totalPrice = 0;

            itemsAndQuantities.ForEach(x =>
            {
                var product = _products.SingleOrDefault(p => p.Sku == x.ItemSku);

                if (product != null)
                {
                    var hasSpecialOffer = product.PriceInformation.SpecialPrice != null 
                        && product.PriceInformation.SpecialPrice.Quantity > 0 
                        && product.PriceInformation.SpecialPrice.DiscountedPrice > 0;
                    
                    var applySpecialOffer = hasSpecialOffer && x.Quantity >= product.PriceInformation.SpecialPrice.Quantity;

                    if (applySpecialOffer)
                    {
                        if (x.Quantity >= product.PriceInformation.SpecialPrice.Quantity)
                        {
                            // work out if the special price needs to be applied more than once (e.g. buying 4x"B" would result in (2x45)x2
                            // if there is a remainder (e.g. buying 3x"B" would result in special offer applied for 2 items
                            // then the remaining item would be charged at normal price)
                            var specialOfferQuantity = Math.DivRem(x.Quantity, product.PriceInformation.SpecialPrice.Quantity, out var remainingQuantiy);

                            totalPrice += (specialOfferQuantity * product.PriceInformation.SpecialPrice.DiscountedPrice);

                            if (remainingQuantiy > 0)
                                totalPrice += (remainingQuantiy * product.PriceInformation.UnitPrice);   
                        }
                    }
                    else
                    {
                        totalPrice += (x.Quantity * product.PriceInformation.UnitPrice);
                    }
                }
            });

            return totalPrice;
        }

        public void ScanProduct(ScanProductRequest scanProductRequest)
        {
            if (scanProductRequest == null) throw new ArgumentNullException("Scan product request is empty");
            if (string.IsNullOrEmpty(scanProductRequest.ItemSku)) throw new ArgumentNullException("Item sku is required");

            // TODO: Check if product is available

            _basket.Add(scanProductRequest.ItemSku);
        }

        public IList<string> GetBasket()
        {
            return _basket;
        }
    }
}
