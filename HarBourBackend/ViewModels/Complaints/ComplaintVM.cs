using System;
namespace HarBourBackEnd.ViewModels.Complaints
{
	public class ComplaintVM
	{
        public int Id { get; set; }
        public string UserFullName { get; set; }
        public string UserPhone { get; set; }
        public string Subject { get; set; }
        public string UserEmail { get; set; }
        public string UserSuggest { get; set; }
    }
}

