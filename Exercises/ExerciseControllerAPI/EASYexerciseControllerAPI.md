### **Easy: Create a Product CRUD API in ASP.NET Core**

#### **Step 1: Create a New ASP.NET Core Web API Project**
1. Open a terminal or command prompt.
2. Navigate to the folder where you want to create the project.
3. Run the following command to scaffold a new Web API project:
   ```bash
   dotnet new webapi -n ProductCrudApi
   ```
   - This creates a new folder named `ProductCrudApi` with the default ASP.NET Core Web API setup.
4. Navigate into the project folder:
   ```bash
   cd ProductCrudApi
   ```

---

#### **Step 2: Install Swagger**
1. Add the Swagger package for API documentation:
   ```bash
   dotnet add package Swashbuckle.AspNetCore --version 6.5.0
   ```
   - Swagger will generate an interactive interface for testing and documenting the API.

---

#### **Step 3: Configure Swagger in `Program.cs`**
1. Open the `Program.cs` file (located in the root of the project).
2. Enable Swagger by adding the following lines:
   ```csharp
   builder.Services.AddSwaggerGen();
   if (app.Environment.IsDevelopment())
   {
       app.UseSwagger();
       app.UseSwaggerUI();
   }
   ```
   - This configures Swagger to run when the application is in development mode.

---

#### **Step 4: Create the `ProductsController`**
1. In the `Controllers` folder (path: `ProductCrudApi/Controllers`), delete the `WeatherForecastController.cs` file.
2. Create a new file named `ProductDTO.cs` in a folder called models
3. Add the following code:

```csharp
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
```
4. Create a new file named `ProductsController.cs` in the Controllers folder.
5. Add the following code to define basic CRUD operations:

   ```csharp
   using Microsoft.AspNetCore.Mvc;
   using System.ComponentModel.DataAnnotations;

   namespace ProductCrudApi.Controllers
   {
       [ApiController]
       [Route("api/[controller]")]
       public class ProductsController : ControllerBase
       {
           private static readonly List<ProductDto> Products = new();

           [HttpGet]
           public IActionResult GetProducts()
           {
               return Ok(Products);
           }

           [HttpGet("{id}")]
           public IActionResult GetProductById(int id)
           {
               var product = Products.FirstOrDefault(p => p.Id == id);
               if (product == null) return NotFound();
               return Ok(product);
           }

           [HttpPost]
           public IActionResult CreateProduct([FromBody] ProductDto product)
           {
               if (!ModelState.IsValid) return BadRequest(ModelState);

               product.Id = Products.Count + 1;
               Products.Add(product);
               return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
           }

           [HttpPut("{id}")]
           public IActionResult UpdateProduct(int id, [FromBody] ProductDto product)
           {
               var existingProduct = Products.FirstOrDefault(p => p.Id == id);
               if (existingProduct == null) return NotFound();

               existingProduct.Name = product.Name;
               existingProduct.Price = product.Price;
               existingProduct.Description = product.Description;
               return NoContent();
           }

           [HttpDelete("{id}")]
           public IActionResult DeleteProduct(int id)
           {
               var product = Products.FirstOrDefault(p => p.Id == id);
               if (product == null) return NotFound();

               Products.Remove(product);
               return NoContent();
           }
       }

   }
   ```

6. Save the file.

---

#### **Step 5: Test the API**
1. Run the application:
   ```bash
   dotnet run
   ```
2. Open your browser and navigate to `http://localhost:<port>/swagger` (replace `<port>` with the actual port number displayed in the terminal).
3. Test the API endpoints (`GET`, `POST`, `PUT`, and `DELETE`) using Swagger UI.

---

#### **Bonus Steps**
1. **Add Example Values and Descriptions**:
   - Install the Swagger Annotations package:
     ```bash
     dotnet add package Swashbuckle.AspNetCore.Annotations
     ```
   - Update the `ProductDto` class with annotations:
     ```csharp
     [SwaggerSchema("The name of the product", Example = "Sample Product")]
     public string Name { get; set; }
     ```

2. **Add File Upload Endpoint**:
   - Add the following method to `ProductsController`:
     ```csharp
     [HttpPost("upload")]
     public IActionResult UploadFile(IFormFile file)
     {
         if (file == null || file.Length == 0)
             return BadRequest("File is missing");

         return Ok(new { FileName = file.FileName, Size = file.Length });
     }
     ```

3. **Explore Validation**:
   - Try adding invalid data in the `POST` or `PUT` requests and observe the error messages returned by the API.

4. **Mix Parameters**:
   - Modify an endpoint to accept a combination of query, path, and body parameters. Example:
     ```csharp
     [HttpGet("search")]
     public IActionResult SearchProducts([FromQuery] string name, [FromQuery] decimal? minPrice)
     {
         var filtered = Products.Where(p => 
             (string.IsNullOrEmpty(name) || p.Name.Contains(name)) &&
             (!minPrice.HasValue || p.Price >= minPrice)
         ).ToList();

         return Ok(filtered);
     }
     ```

---

