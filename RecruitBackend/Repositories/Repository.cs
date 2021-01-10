using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecruitBackend.Models;

namespace RecruitBackend.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _entities;
        protected readonly DatabaseContext Context;

        protected Repository(DatabaseContext context)
        {
            this.Context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public T GetById(Guid id)
        {
            return _entities.SingleOrDefault(s => s.Id == id);
        }

        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _entities.Add(entity);
            Context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = _entities.SingleOrDefault(s => s.Id == id);
            _entities.Remove(entity ?? throw new InvalidOperationException("Retrieved value to delete was null."));
            Context.SaveChanges();
        }
    }
}