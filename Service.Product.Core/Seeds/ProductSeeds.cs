using Service.Product.Core.Entities;

namespace Service.Product.Core.Seeds;

public static class ProductSeeds
{
	public static ICollection<ProductEntity> Seeds()
	{
		return new List<ProductEntity>
		{
			new ProductEntity
			{
				Id = 1,
				Name = "Samosa",
				Price = 15,
				Description = "Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
				ImageUrl = "https://placehold.co/603x403",
				CategoryName = "Aperitivo"
			},
			new ProductEntity
			{
				Id = 2,
				Name = "Paneer Tikka",
				Price = 13.99,
				Description = "Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
				ImageUrl = "https://placehold.co/602x402",
				CategoryName = "Aperitivo"
			},
			new ProductEntity
			{
				Id = 3,
				Name = "Torta Doce",
				Price = 10.99,
				Description = "Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
				ImageUrl = "https://placehold.co/601x401",
				CategoryName = "Sobremesa"
			},
			new ProductEntity
			{
				Id = 4,
				Name = "Pav Bhaji",
				Price = 15,
				Description = "Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
				ImageUrl = "https://placehold.co/600x400",
				CategoryName = "Prato Principal"
			}
		};
	}
}
