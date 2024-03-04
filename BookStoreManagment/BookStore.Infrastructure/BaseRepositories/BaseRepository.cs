using BookStore.Application.Abstractions;       // DbSet ishlashi uchun
using BookStore.Infrastructure.Persistance;     // BookStoreDbContext ishlashi uchun
using Microsoft.EntityFrameworkCore;            // DbSet ishlashi uchun
using System.Linq.Expressions;                  // Expression ishlashi uchun

namespace BookStore.Infrastructure.BaseRepositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly BookStoreDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(BookStoreDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public string Create(T model)
        {
            _dbSet.AddAsync(model).GetAwaiter().GetResult();
            _context.SaveChangesAsync().GetAwaiter().GetResult();

            return "Create qilindi!";
        }

        public string Delete(Expression<Func<T, bool>> expression)
        {
            T model = _dbSet.FirstOrDefaultAsync(expression).GetAwaiter().GetResult()!;
            if (model == null)
            {
                return "Ma'lumot topilmadi.\nShuning uchun hechnima ham ochirilmadi)";
            }
            _dbSet.Remove(model);
            _context.SaveChangesAsync().GetAwaiter().GetResult();
            return "Malumot ochirildi!";
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToListAsync().GetAwaiter().GetResult();
        }

        public T GetByAny(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression)!;
        }

        public string Update(T model)
        {
            _dbSet.Update(model);
            _context.SaveChangesAsync().GetAwaiter().GetResult();
            return "Update muvaffaqiyat topdi!";
        }
    }
}
