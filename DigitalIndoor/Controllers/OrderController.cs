using DigitalIndoorAPI.Controllers.Base;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Models;
using DigitalIndoorAPI.Services;

namespace DigitalIndoorAPI.Controllers
{
    public class OrderController : CrudBaseController<IOrderService, Order, OrderViewDto, OrderParam, OrderCreateDto, OrderUpdateDto>
    {
        public OrderController(IOrderService service) : base(service) { }
    }
}
