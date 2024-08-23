using System;
namespace HarBourBackEnd.ViewModels.About
{
	public class AboutEditImageVM
	{
        public int Id { get; set; }
        public int AboutId { get; set; }
        public string Image { get; set; }
        public bool IsMain { get; set; }
    }
}

