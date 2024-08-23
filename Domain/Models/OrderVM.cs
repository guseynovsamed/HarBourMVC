using System;
namespace HarBourBackEnd.ViewModels.Order
{
	public class OrderVM
	{
		public string UserName { get; set; }
		public string UserEmail { get; set; }
		public int Guest { get; set; }
		public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

