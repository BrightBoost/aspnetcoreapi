using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var products = new List<Product>(){
    new Product("1", "HDMI cable", 10, true)
};

app.MapGet("/products", () => products);

app.MapGet("/products/{id}", Results<Ok<Product>, NotFound> (string id) => {
    Test();
    Product product = products.SingleOrDefault(p => p.id == id);
    if(product != null) {
        return TypedResults.Ok(product);
    } else {
        return TypedResults.NotFound();
    }
});

app.MapPost("/products", (Product product) => {
    products.Add(product);
    return TypedResults.Created($"/products/{product.id}", product);
});

app.Run();

record Product(string id, string name, decimal price, bool inStock);

