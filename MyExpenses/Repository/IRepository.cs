using System;
using System.Collections.Generic;
using MyExpenses.Models;

namespace MyExpenses.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        TEntity Single(object key);
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> Paged(string orderBy, int page, int rows, string direction = "ASC");
        int Count();
        bool Exists(object key);
        void Save(TEntity item);
        void Delete(object key);
    }
}
