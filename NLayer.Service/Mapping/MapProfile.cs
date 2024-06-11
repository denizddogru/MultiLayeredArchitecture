using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        // product'ı productmap'e dönüştürdük
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
        CreateMap<ProductUpdateDto, Product>();
        CreateMap<Product, ProductWithCategoryDto>().ReverseMap();

    }
}
