using System.Reflection.Metadata.Ecma335;

namespace BrightHR.CheckoutKata
{
    public static class CheckoutErrors
    {
        public const string RequestIsNull = "Request object is null";
        public const string ItemSkuIsRequired = "Item sku is required";
        
        public static string ItemNotFound(string itemSku) => $"Item with SKU '{itemSku}' not found and will not be added to checkout.";
    }
}
