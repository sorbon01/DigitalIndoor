using DigitalIndoor.DTOs;
using DigitalIndoor.DTOs.Params;
using DigitalIndoor.DTOs.Request;
using DigitalIndoor.DTOs.Response;
using DigitalIndoor.Models.Common;

namespace DigitalIndoor.Services
{
    public interface IRoleService
    {
        Task<PagedList<Role, RoleViewDto>> SearchAsync(NameAndPagedParam param);
        Task<RoleViewDto> AddAsync(RoleCreateDto create);
        Task<RoleViewDto> UpdateAsync(RoleUpdateDto update);
        Task<RoleViewDto> DeleteAsync(int id);
        IList<string> GetAllFunctionals();
    }
}
