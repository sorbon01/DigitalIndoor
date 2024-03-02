using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.DTOs;
using DigitalIndoorAPI.Models;

namespace DigitalIndoorAPI.Services
{
    public interface ICrudService<TModel, TView, TParam, TCreate, TUpdate>
    {
        Task<PagedList<TModel, TView>> SearchAsync(TParam param);
        Task<TView> AddAsync(TCreate create);
        Task<TView> UpdateAsync(TUpdate update);
        Task<TView> DeleteAsync(int id);
    }
}
