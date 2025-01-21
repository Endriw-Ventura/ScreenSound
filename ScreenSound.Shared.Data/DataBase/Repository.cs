using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.DataBase
{
    public class Repository<T> where T : class
    {
        protected readonly ScreenSoundContext _context;
        
        public Repository(ScreenSoundContext context)
        {
            this._context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> GetAll(Func<T, bool> condition)
        {
            return _context.Set<T>().Where(condition).ToList();
        }

        public T? Get(Func<T, bool> condition)
        {
            return _context.Set<T>().FirstOrDefault(condition);
        }

        public void Add(T obj)
        {
            _context.Set<T>().Add(obj);
            _context.SaveChanges();
        }

        public void Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
            _context.SaveChanges();
        }

        public void Delete(Func<T, bool> condition)
        {
            var generic = _context.Set<T>().FirstOrDefault(condition);
            
            if (generic is not null) {
                _context.Set<T>().Remove(generic);
                _context.SaveChanges();
            }
        }

        public void DeleteAll(T obj)
        {
            _context.Set<T>().RemoveRange(_context.Set<T>());
            _context.SaveChanges();
        }

        public void Update(T obj)
        {
            _context.Set<T>().Update(obj);
            _context.SaveChanges();
        }
    }
}
