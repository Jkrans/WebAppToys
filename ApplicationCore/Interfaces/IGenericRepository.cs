using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        // Get a single object by it's key id
        T GetByID(int id);

        //used to get (SELECT/WHERE)
        //A fucn<T,bool> represents a function that takes an object of type T and returns a bool.
        //It's commonly referred to as a "predicate",
        //It is used to verify a condition on an object.
        //Expression<Func<T>> is a description of a function as an expression tree.
        //It can be compiled at run time that generates a Func<T>
        //It can also be translated to other languages e.g. SQL in LINQ to SQL.
        //NoTracking is ReadOnly Results, and Includes is Join of other objects.
        T Get(Expression<Func<T, bool>> predicate, bool asNoTracking = false, string includes = null);

        // Same as above but Asynchronous action
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, string includes = null);

        //Returns and Enumerable list of results to iterate thought
        IEnumerable<T> List();

        //Returns an Enumerable list of results to iterate through. Expression is the query, optional Order By. Optional includes (Join) other objects
        IEnumerable<T> List(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy = null, string includes = null);

        //Same as above but Asynchronous action
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, int>> orderBy = null, string includes = null);

        //Add/Insert a new record instance.
        void Add(T entity);

        //Delete/Remove a single record instance.
        void Delete(T entity);

        //Delete/Remove multiple record instances.
        void Delete(IEnumerable<T> entities);

        //Update all changes in an object
        void Update(T entity);

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );
    }
}

