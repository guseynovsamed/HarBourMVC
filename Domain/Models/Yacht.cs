using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;
using HarBourBackEnd.ViewModels.Order;
using MailKit.Search;

namespace Domain.Models
{
	public class Yacht : BaseEntity
	{
        public string Name { get; set; }
        public int Length { get; set; }
        public int Guest { get; set; }
        public decimal Price { get; set; }
        public string Build { get; set; }
        public string Information { get; set; }
        public string Description { get; set; }
        public ICollection<YachtImage> YachtImages { get; set; }
        public int YachtCategoryId { get; set; }
        public YachtCategory YachtCategory { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        [NotMapped]
        public OrderVM OrderVM { get; set; }
    }
}

