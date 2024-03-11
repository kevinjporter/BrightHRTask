using BrightHR.CheckoutKata.Interfaces;
using System;
using System.Collections.Generic;

namespace BrightHR.CheckoutKata
{
    public class CheckoutManager : ICheckout
    {
        private static IList<Item> _cart = new List<Item>();

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void ScanProduct(ScanProductRequest scanProductRequest)
        {
            if (scanProductRequest == null) throw new ArgumentNullException("Scan product request is empty");
            if (string.IsNullOrEmpty(scanProductRequest.ItemSku)) throw new ArgumentNullException("Item sku is required");
        }
    }
}
