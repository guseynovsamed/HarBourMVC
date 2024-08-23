using System;
using Domain.Common;
using Domain.Helpers.Enums;

namespace Domain.Models
{
    public class Reservation : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int YachtId { get; set; }
        public Yacht Yacht { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int Guests { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}

