namespace BrightHR.CheckoutKata.Tests;

public class ItemBuilder
{
    private readonly Item item;

    public static ItemBuilder Start() => new ItemBuilder();

    public ItemBuilder()
    {
        item = new Item();
    }

    public ItemBuilder WithSku(string sku)
    {
        item.Sku = sku;
        return this;
    }

    public ItemBuilder WithUnitPrice(int unitPrice)
    {
        item.PriceInformation = new PriceInformation
        {
            UnitPrice = unitPrice
        };
        return this;
    }

    public ItemBuilder WithUnitPriceAndOffer(int unitPrice, int offerQuantity, int offerPrice)
    {
        item.PriceInformation = new PriceInformation
        {
            UnitPrice = unitPrice,
            SpecialPrice = new SpecialPriceInformation
            {
                Quantity = offerQuantity,
                DiscountedPrice = offerPrice
            }
        };
        return this;
    }

    internal Item Build() => item;
}
