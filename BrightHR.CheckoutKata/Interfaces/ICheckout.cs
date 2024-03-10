namespace BrightHR.CheckoutKata.Interfaces;

internal interface ICheckout
{
    void ScanProduct(string productSku);
    int GetTotalPrice();
}