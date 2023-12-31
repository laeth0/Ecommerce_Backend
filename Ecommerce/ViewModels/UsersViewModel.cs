using System.ComponentModel.DataAnnotations;

namespace Ecommerce.PL
{
    public class UsersViewModel
    {
        public string? Id { get; set; } //we make it string because it is a GUID
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
