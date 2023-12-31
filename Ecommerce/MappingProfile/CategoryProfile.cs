using AutoMapper;
using Ecommerce.DAL;

namespace Ecommerce.PL
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryViewModel, Category>().ReverseMap();
        }
    }
}
