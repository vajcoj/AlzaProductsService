using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductsService.Data;
using ProductsService.Helpers;
using ProductsService.Services;
using System;
using System.Linq;

namespace ProductsService.UnitTests
{
    internal static class InfrastructureBuilder
    {
        public static ProductsContext GetContext(bool seed = true)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductsContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            var context = new ProductsContext(optionsBuilder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (seed) Seed.SeedProducts(context);

            foreach (var entry in context.ChangeTracker.Entries().ToList())
            {
                context.Entry(entry.Entity).State = EntityState.Detached;
            }

            return context;
        }

        public static IMapper GetMapper()
        {
            var mapper = new MapperConfiguration(c => c.AddProfile<MappingProfiles>()).CreateMapper();
            return mapper;
        }

        public static ProductService GetProductService(bool seed = true)
        {
            var context = GetContext(seed);
            var mapper = GetMapper();
            return new ProductService(context, mapper);
        }

        public static ProductService GetProductService(ProductsContext context)
        {
            var mapper = GetMapper();
            return new ProductService(context, mapper);
        }

    }
}
