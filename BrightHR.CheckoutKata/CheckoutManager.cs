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

            // group by SKU to get quantities 
            var itemsAndQuantities = _basket.GroupBy(x => x).Select(x => new { ItemSku = x.Key, Quantity = x.Count() }).ToList();

            var totalPrice = 0;

            // calculate total including using special offers if applicable
            itemsAndQuantities.ForEach(x =>
            {
                // find the item to get price information
                var product = _products.SingleOrDefault(p => p.Sku == x.ItemSku);

                if (product != null)
                {
                    var hasSpecialOffer = product.PriceInformation.SpecialPrice != null 
                        && product.PriceInformation.SpecialPrice.Quantity > 0 
                        && product.PriceInformation.SpecialPrice.DiscountedPrice > 0;
                    
                    var applySpecialOffer = hasSpecialOffer && x.Quantity >= product.PriceInformation.SpecialPrice.Quantity;

                    totalPrice += applySpecialOffer 
                        ? product.PriceInformation.SpecialPrice.DiscountedPrice
                        : product.PriceInformation.UnitPrice;
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
