using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

public class ProductDto
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    [SwaggerSchema("The name of the product", Description = "Sample Product")]
    public required string Name { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    [SwaggerSchema("The price of the product", Description = "19.99")]
    public decimal Price { get; set; }

    [StringLength(500)]
    [SwaggerSchema("Optional description of the product", Description = "A very useful product.")]
    public string Description { get; set; } = "";
}
