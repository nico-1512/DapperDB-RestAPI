using dambrosio.pretest.api.Models;

namespace dambrosio.pretest.api.Services
{
    public interface IDataAccess
    {
        void AddItemsToCart(CartItem cartItem, int CartId_);
        void AddProduct(Product product);
        void CloseCart(DateTime CompletedAt_, decimal TotalPrice_, int CartId_);
        void CreateCart(Cart cart);
        Cart GetCart(int CartId_);
        IEnumerable<Product> GetProductList();
        IEnumerable<Product> GetProductsFromCart(int CartId_);
    }
}