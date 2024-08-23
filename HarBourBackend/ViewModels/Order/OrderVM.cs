using System;
namespace HarBourBackEnd.ViewModels.Order
{
	public class OrderVM
	{
		public int YachtId { get; set; }
		public string UserName { get; set; }
		public string UserEmail { get; set; }
		public int Guest { get; set; }
		public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}

