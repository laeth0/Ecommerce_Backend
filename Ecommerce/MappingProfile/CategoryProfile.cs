using AutoMapper;
using Ecommerce.DAL;

namespace Ecommerce.PL
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerViewModel, Customer>().ReverseMap();
        }
    }
}
