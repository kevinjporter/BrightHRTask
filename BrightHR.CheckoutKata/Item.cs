using System.Collections.Generic;

namespace BrightHR.CheckoutKata
{
    internal record Item(string Sku, decimal UnitPrice, IList<ItemOffer> ItemOffers = null)
    {
    }
}
