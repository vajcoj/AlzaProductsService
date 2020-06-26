using Newtonsoft.Json;
using ProductsService.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductsService.Data
{
    public class Seed
    {
        public static void SeedProducts(ProductsContext context)
        {
            if (!context.Products.Any())
            {
                var jsonData = File.ReadAllText("Data/ProductsSeedData.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(jsonData);

                context.Products.AddRange(products);

                context.SaveChanges();
            }
        }

    }
}
