namespace BrightHR.CheckoutKata.Interfaces;

internal interface ICheckout
{
    void ScanProduct(ScanProductRequest scanProductRequest);
    int GetTotalPrice();
}