using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Theater_ticket_booking.Models;

namespace Theater_ticket_booking.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected readonly TheaterContext _db;
        public BaseRepository(TheaterContext db)
        {
            _db = db;
        }

        public virtual async Task<bool> Add(T model)
        {
            try
            {
                _db.Add(model);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual async Task<bool> Update(T model)
        {
            try
            {
                _db.Update(model);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual async Task<bool> Remove(T model)
        {
            try
            {
                _db.Remove(model);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public virtual async Task<bool> Remove(int id)
        {
            var model = await _db.Set<T>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
            if (model != null)
                return await Remove(model);
            return false;
        }

        public virtual async Task<bool> RemoveRange(IEnumerable<T> models)
        {
            try
            {
                foreach (var model in models)
                {
                    model.IsDelete = true;
                }
                _db.UpdateRange(models);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual IQueryable<T> GetList()
        {
            return _db.Set<T>().AsNoTracking().Where(p => !p.IsDelete).AsQueryable();
        }

        public virtual IQueryable<T> GetListWithDeleted()
        {
            return _db.Set<T>().AsNoTracking().AsQueryable();
        }

        public virtual async Task<T> Find(int id)
        {
            return await GetList().FirstOrDefaultAsync(p => p.Id == id);
        }

        public virtual async Task<bool> Any(Expression<Func<T, bool>> func)
        {
            return await GetList().AnyAsync(func);
        }

        public virtual async Task<T> FirstOrDefault(Expression<Func<T, bool>> func)
        {
            return await GetList().FirstOrDefaultAsync(func);
        }
    }
}
