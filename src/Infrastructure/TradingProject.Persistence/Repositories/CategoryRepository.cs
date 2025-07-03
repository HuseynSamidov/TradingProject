using TradingProject.Application.Abstracts.Repositories;
using TradingProject.Domain.Entities;
using TradingProject.Persistence.Contexts;

namespace TradingProject.Persistence.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly TradingDbCotext _context;

        public CategoryRepository(TradingDbCotext context) : base(context)
        {
            _context = context;
        }

    }
}
