using AutoMapper;
using ProductsService.DTOs;
using ProductsService.Models;

namespace ProductsService.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductGetDTO>();
        }
    }
}
