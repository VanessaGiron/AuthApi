using AuthApi.Entidades;
using AuthApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthApi.Repositorios
{
    public class CategoriaDvRepository : ICategoriaDvRepository
    {
        private readonly DbContext _context;

        public CategoriaDvRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<List<categoriaDV>> GetAllAsync()
        {
            return await _context.Set<categoriaDV>().ToListAsync();
        }

        public async Task<categoriaDV> GetByIdAsync(int id)
        {
            return await _context.Set<categoriaDV>().FindAsync(id);
        }

        public async Task<categoriaDV> AddAsync(categoriaDV categoria)
        {
            _context.Set<categoriaDV>().Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<categoriaDV> UpdateAsync(categoriaDV categoria)
        {
            _context.Set<categoriaDV>().Update(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoria = await GetByIdAsync(id);
            if (categoria == null) return false;

            _context.Set<categoriaDV>().Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
