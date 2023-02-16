﻿using System;
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
        private IGenericRepository<Listing> _Listing;

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

        public IGenericRepository<Listing> Listing
        {
            get
            {
                if (_Listing == null)
                {
                    _Listing = new GenericRepository<Listing>(_dbContext);
                }

                return _Listing;
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

