using dambrosio.pretest.api.Models;
using dambrosio.pretest.api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataAccess, DataAccess>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// CARTS

// POST - Cart creation
app.MapPost("/api/Carts", (Cart cart, IDataAccess data) =>
{
    data.CreateCart(cart);

}).WithName("CreateCart")
  .WithDisplayName("Create Cart")
  .WithDescription("POST - Create new Cart");

// GET - Get products from a cart
app.MapGet("/api/Carts/{cart_id}", (int Id_, IDataAccess data) =>
{
    var itemsInCart = data.GetProductsFromCart(Id_);
    return itemsInCart;

}).WithName("GetProductsFromCart")
  .WithDisplayName("Get Products From Cart")
  .WithDescription("GET - Getting all products from a Cart with his id");

//GET - Get cart infos
app.MapGet("/api/Carts/{cartId}", (int cartId, IDataAccess data) =>
{
    data.GetCart(cartId);

}).WithName("GetCart")
  .WithDisplayName("Get Cart")
  .WithDescription("GET - Get Cart");



// PRODUCTS

// POST - Add items to a certain cart
app.MapPost("/api/Products/{cartId}", (int cartId, CartItem cartItem, IDataAccess data) =>
{
    data.AddItemsToCart(cartItem, cartId);

}).WithName("AddProductToCart")
  .WithDisplayName("Add Product To Cart")
  .WithDescription("POST - Add product to Cart");

//POST - Adding new product to DB
app.MapPost("/api/Products", (Product product, IDataAccess data) =>
{
    data.AddProduct(product);

}).WithName("AddProduct")
  .WithDisplayName("Add Product")
  .WithDescription("POST - Create new product");

//GET - Product list
app.MapGet("/api/Products", (IDataAccess data) =>
{
    var products = data.GetProductList();
    return products;

}).WithName("GetAllProducts")
  .WithDisplayName("Get All Products")
  .WithDescription("GET - Getting all products available");



app.Run();