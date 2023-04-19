using dambrosio.pretest.api.Models;
using Dapper;
using Npgsql;

namespace dambrosio.pretest.api.Services
{
    public class DataAccess : IDataAccess
    {
        private readonly string _connectionString;


        public DataAccess(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ecommerce-dotnet") ?? "";
            if (string.IsNullOrEmpty(_connectionString))
                throw new ArgumentException("Connection string ecommerce-dotnet cannot be null or empty.", nameof(_connectionString));
        }

        public void CreateCart(Cart cart)
        {
            using var connection = new NpgsqlConnection(this._connectionString);
            string query = """
                INSERT INTO Cart 
                    (User_, CreatedAt_ ) 
                VALUES 
                    (@User_, @CreatedAt_);
                """;

            connection.Execute(query, new { User_ = cart.User_, CreatedAt_ = cart.CreatedAt_ });

        }

        public void AddItemsToCart(CartItem cartItem, int CartId_)
        {
            using var connection = new NpgsqlConnection(this._connectionString);
            string query = """
                INSERT INTO CartItem 
                    (CartId_, ProductId_, Quantity_ ) 
                VALUES 
                    (@CartId_, @ProductId_, @Quantity_ ) ;
                """;

            connection.Execute(query, new { ProductId_ = cartItem.ProductId_, CartId_, Quantity_ = cartItem.Quantity_ });
        }


        public IEnumerable<Product> GetProductsFromCart(int CartId_)
        {
            using var connection = new NpgsqlConnection(this._connectionString);
            string query = """
                SELECT Product.Name_, Product.Price_, CartItem.Quantity_ 
                FROM 
                    Product
                        JOIN CartItem on (Product.Id_ = CartItem.ProductId_)
                WHERE CartItem.CartId_ = @CartId_;
                """;

            return connection.Query<Product>(query, new { CartId_ });
        }

        public Cart GetCart(int CartId_)
        {
            using var connection = new NpgsqlConnection(this._connectionString);
            string query = """
                SELECT * FROM Cart WHERE CartId_ = @CartId_;
                """;

            return connection.QueryFirstOrDefault<Cart>(query, new { CartId_ });
        }

        public IEnumerable<Product>? GetProductList()
        {
            using var connection = new NpgsqlConnection(this._connectionString);
            string query = """
                SELECT * FROM Product;
                """;

            return connection.Query<Product>(query);

        }

        public void AddProduct(Product product)
        {
            using var connection = new NpgsqlConnection(this._connectionString);
            string query = """
                INSERT INTO Product 
                    (Name_, Price_) 
                VALUES 
                    (@Name_, @Price_)                 
                """;

            connection.Execute(query, new { Name_ = product.Name_, Price_ = product.Price_ });
        }

        public void CloseCart(DateTime CompletedAt_,
                               Decimal TotalPrice_,
                               int CartId_)
        {
            using var connection = new NpgsqlConnection(this._connectionString);
            string query = """
                UPDATE Cart SET
                    (CompletedAt_, TotalPrice_)
                VALUES 
                    (@CompletedAt_, @TotalPrice_);                    
                """;

            connection.Execute(query);
        }

    }
}
