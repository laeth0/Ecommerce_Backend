using AutoMapper;
using Ecommerce.DAL;

namespace Ecommerce.PL
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser,UsersViewModel>().ReverseMap();
        }
    }
}
