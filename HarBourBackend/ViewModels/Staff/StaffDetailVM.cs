using System;

namespace HarBourBackEnd.ViewModels.Staff
{
	public class StaffDetailVM
	{
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Biography { get; set; }
        public string Education { get; set; }
        public string Awards { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }

        internal IEnumerable<Domain.Models.Staff> ToList()
        {
            throw new NotImplementedException();
        }
    }
}

