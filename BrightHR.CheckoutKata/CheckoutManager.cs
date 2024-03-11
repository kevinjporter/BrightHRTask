using BrightHR.CheckoutKata.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace BrightHR.CheckoutKata
{
    public class CheckoutManager : ICheckout
    {
        private IList<Item> _cart = new List<Item>();

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void ScanProduct(ScanProductRequest scanProductRequest)
        {
            if (scanProductRequest == null) throw new ArgumentNullException("Scan product request is empty");
            if (string.IsNullOrEmpty(scanProductRequest.ItemSku)) throw new ArgumentNullException("Item sku is required");
        }

        public IList<Item> GetItems()
        {
            return _cart;
        }
    }
}
