using DigitalIndoorAPI.DTOs;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Models.Common;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.Services;

namespace DigitalIndoorAPI.Services
{
    public interface IRoleService:ICrudService<Role, RoleViewDto, RoleParam, RoleCreateDto, RoleUpdateDto>
    {
        IList<string> GetAllFunctionals();
    }
}
