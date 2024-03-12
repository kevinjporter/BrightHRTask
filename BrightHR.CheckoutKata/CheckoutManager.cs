using BrightHR.CheckoutKata.Interfaces;
using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public void ScanProduct(ScanProductRequest scanProductRequest)
        {
            if (scanProductRequest == null) throw new ArgumentNullException("Scan product request is empty");
            if (string.IsNullOrEmpty(scanProductRequest.ItemSku)) throw new ArgumentNullException("Item sku is required");

            _basket.Add(scanProductRequest.ItemSku);
        }

        public IList<string> GetBasket()
        {
            return _basket;
        }
    }
}
