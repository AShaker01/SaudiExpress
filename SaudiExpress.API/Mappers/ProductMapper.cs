using AutoMapper;
using SaudiExpress.API.ViewModels;
using SaudiExpress.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaudiExpress.API.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductViewModel, ProductDTO>().ReverseMap();
        }
    }
}
