using System;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace Infrastructure.Data
{
	public class UnitOfWork : IUnitOfWork
	{
        private readonly ApplicationDbContext _dbContext;

		public UnitOfWork(ApplicationDbContext dbContext)
		{
            _dbContext = dbContext;
		}

        private IGenericRepository<Category> _Category;
        private IGenericRepository<Toy> _Toy;

        public IGenericRepository<Category> Category
        {
            get
            {
                if (_Category == null)
                {
                    _Category = new GenericRepository<Category>(_dbContext);
                }

                return _Category;
            }
        }

        public IGenericRepository<Toy> Toy
        {
            get
            {
                if (_Toy == null)
                {
                    _Toy = new GenericRepository<Toy>(_dbContext);
                }

                return _Toy;
            }
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

