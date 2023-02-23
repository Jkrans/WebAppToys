using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
	public interface IUnitOfWork
	{
        public IGenericRepository<Category> Category { get; }
        public IGenericRepository<Listing> Listing { get; }
        public IGenericRepository<ApplicationUser> ApplicationUser { get; }

        // save changes to the data source
        int Commit();

        Task<int> CommitAsync();
    }
}

