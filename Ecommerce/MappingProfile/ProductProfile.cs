using AutoMapper;
using Ecommerce.DAL;

namespace Ecommerce.PL
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, Product>().ReverseMap();
        }
    }
}
