using Microsoft.EntityFrameworkCore;
using SchoolAttendance.Infrastructure.Repositories.Queries;
using SchoolAttendance.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SchoolAttendance.Domain.Repositories.Query;

namespace SchoolAttendance.Infrastructure.Repositories.Queries.Base
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        protected readonly SchoolAttendanceContext _context;
        private DbSet<T> _entities;

        public QueryRepository(SchoolAttendanceContext context)
        {
            this._context = context;
            this._entities = context.Set<T>();
        }

        public async Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return await _entities.ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<T> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _entities.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id, cancellationToken: cancellationToken);
            return result;
        }

        public async Task<IQueryable<T>> Query(Expression<Func<T, bool>> expression)
        {
            return _entities.Where(expression);
        }
    }
}
