using AutoMapper;
using DigitalIndoorAPI.DTOs;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Exceptions;
using DigitalIndoorAPI.Models;
using DigitalIndoorAPI.Models.DB;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace DigitalIndoorAPI.Services.Implementations
{
    public class OrderService : IOrderService
    {
        readonly IMapper mapper;
        readonly Context context;
        readonly IUserService userService;
        public OrderService(IMapper mapper, Context context, IUserService userService)
        {
            this.mapper = mapper;
            this.userService = userService;
            this.context = context;
        }
        public async Task<PagedList<Order, OrderViewDto>> SearchAsync(OrderParam param)
        {
            var query = context.Orders
                .Include(x => x.User)
                .Include(x => x.Video)
                .Include(x => x.PlayLists)
                .Where(x => !x.IsDeleted &&
                (string.IsNullOrWhiteSpace(param.ClientName) || x.ClientName.Contains(param.ClientName)))
                .OrderBy(x => x.Id).AsQueryable();
            
            var count = query.CountAsync();
            var items = query.Skip((param.Page - 1) * param.Size).Take(param.Size).ToListAsync();

            return new PagedList<Order, OrderViewDto>(await items, await count, param.Page, param.Size, mapper);
        }
        public async Task<OrderViewDto> AddAsync(OrderCreateDto create)
        {
            var order = mapper.Map<Order>(create);
            var user = await userService.GetCurrentAsync();
            order.PlayLists = await context.PlayLists.Where(x=> !x.IsDeleted && create.PlayListIds.Contains(x.Id)).ToListAsync();

            order.UserId = user.Id;

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            order.User = user;
            await context.Entry(order).Reference(x=>x.Video).LoadAsync();
            return mapper.Map<OrderViewDto>(order);
        }

        public async Task<OrderViewDto> UpdateAsync(OrderUpdateDto update)
        {
            var oldOrder = await context.Orders
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == update.Id);
            if (oldOrder is null)
                throw new ToException(ToErrors.ENTITY_NOT_FOUND); 

            var order = mapper.Map<Order>(update);
            order.PlayLists = await context.PlayLists.Where(x => !x.IsDeleted && update.PlayListIds.Contains(x.Id)).ToListAsync();

            order.UserId = oldOrder.Id;
            order.RecordDate = oldOrder.RecordDate;

            context.Orders.Update(order);
            await context.SaveChangesAsync();

            order.User = oldOrder.User;
            await context.Entry(order).Reference(x => x.Video).LoadAsync();
            return mapper.Map<OrderViewDto>(order);
        }

        public async Task<OrderViewDto> DeleteAsync(int id)
        {
            var order = await context.Orders
                .Include(x => x.User)
                .Include(x=> x.Video)
                .Include(x => x.PlayLists)
                .FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id);
            if (order is null)
                throw new ToException(ToErrors.ENTITY_NOT_FOUND);

            foreach(var playlist in order.PlayLists)
                playlist.IsDeleted = true;

            order.IsDeleted = true;
            await context.SaveChangesAsync();

            return mapper.Map<OrderViewDto>(order);
        }
    }
}
