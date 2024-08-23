using System;
namespace HarBourBackEnd.ViewModels.Comment
{
	public class CommentVM
	{
        public string UserId { get; set; }
        public int BlogId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
    }
}

