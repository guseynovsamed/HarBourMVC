using System;
namespace HarBourBackEnd.ViewModels.User
{
	public class UserVM
	{
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public List<string> UserRoles { get; set; }
        public string UserId { get; set; }
    }
}

