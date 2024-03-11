using System.Collections.Generic;

namespace BrightHR.CheckoutKata;

public record Item(string Sku, decimal UnitPrice, IList<ItemOffer> ItemOffers = null)
{
}
