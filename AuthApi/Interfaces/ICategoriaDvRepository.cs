using AuthApi.Entidades;

namespace AuthApi.Interfaces
{
    public interface ICategoriaDvRepository
    {
        Task<List<categoriaDV>> GetAllAsync();
        Task<categoriaDV> GetByIdAsync(int id);
        Task<categoriaDV> AddAsync(categoriaDV categoria);
        Task<categoriaDV> UpdateAsync(categoriaDV categoria);
        Task<bool> DeleteAsync(int id);
    }
}
