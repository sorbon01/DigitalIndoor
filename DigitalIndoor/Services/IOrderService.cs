using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Models;

namespace DigitalIndoorAPI.Services
{
    public interface IOrderService:ICrudService<Order, OrderViewDto, OrderParam, OrderCreateDto, OrderUpdateDto>
    {

    }
}
