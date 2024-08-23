using System;
namespace HarBourBackEnd.ViewModels.Comment
{
	public class CommentAdminVM
	{
        public int Id { get; set; }
        public string PostDate { get; set; }
        public string BlogTitle { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string Comment { get; set; }
    }
}

