using System;
using Domain.Common;
using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class ComplaintRepository : BaseRepository<ComplaintSuggest>, IComplaintRepository
    {
        public ComplaintRepository(AppDbContext context) : base(context)
        {

        }
    }
}

