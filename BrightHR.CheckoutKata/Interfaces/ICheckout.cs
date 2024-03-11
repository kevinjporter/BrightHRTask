namespace BrightHR.CheckoutKata.Interfaces;

public interface ICheckout
{
    void ScanProduct(ScanProductRequest scanProductRequest);
    int GetTotalPrice();
}