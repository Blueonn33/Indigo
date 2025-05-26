using Indigo.Data;
using Indigo.Models;
using Indigo.Repositories.Interfaces;
using Indigo.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Indigo.Repositories
{
    public class JournalRepository : Repository<Journal>, IJournalRepository
    {
        private readonly ApplicationDbContext _context;
        public JournalRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task AddJournalAsync(Journal journal)
        {
            await _context.Journals.AddAsync(journal);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteJournalAsync(int id)
        {
            var Journal = await _context.Journals.FindAsync(id);

            if (Journal == null)
            {
                throw new KeyNotFoundException();
            }

            _context.Journals.Remove(Journal);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Journal>> GetAllJournalsAsync()
        {
            return await _context.Journals
                .Include(j => j.Tomes)
                .ToListAsync();
        }

        public async Task<Journal> GetJournalByIdAsync(int id)
        {
            var Journal = await _context.Journals.FindAsync(id);

            if (Journal == null)
            {
                throw new KeyNotFoundException();
            }

            return await _context.Journals
                .Include(j => j.Tomes)
                .FirstOrDefaultAsync(j => j.Id == id)
                ?? throw new KeyNotFoundException();
        }

        public Task<IEnumerable<Journal>> GetJournalByUserIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateJournalAsync(Journal journal)
        {
            _context.Journals.Update(journal);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Journal>> GetAllJournalsByTitle(string title)
        {
            if(title == null)
            {
                throw new Exception("Не е въведено заглавие");
            }

            var journals = await _context.Journals
                .Where(j => j.Title.Contains(title))
                .ToListAsync();

            return journals;
        }

        //public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        //{
        //    return await _context.Categories
        //        .Select(c => new CategoryViewModel()
        //        {
        //            Id = c.Id,
        //            Name = c.Name
        //        })
        //        .ToListAsync();
        //}
    }
}
