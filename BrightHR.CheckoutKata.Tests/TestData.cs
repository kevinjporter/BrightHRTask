using System.Collections.Generic;

namespace BrightHR.CheckoutKata.Tests
{
    public static class TestData
    {
        public static List<Item> Products = new List<Item>
        {
            { new Item("A", new PriceInformation(50, new SpecialPriceInformation(3, 130))) },
            { new Item("B", new PriceInformation(30, new SpecialPriceInformation(2, 45))) },
            { new Item("C", new PriceInformation(20)) },
            { new Item("D", new PriceInformation(15)) }
        };
    }
}
