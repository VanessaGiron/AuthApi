using AuthApi.DTOs.CategoriaDvDTOs;

namespace AuthApi.Interfaces
{
    public interface ICategoriaDvService
    {
        Task<List<CategoriaDvDTO>> GetAllAsync();
        Task<CategoriaDvDTO> GetByIdAsync(int id);
        Task<CategoriaDvDTO> CreateAsync(CategoriaDvDTO dto);
        Task<CategoriaDvDTO> UpdateAsync(int id, CategoriaDvUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
