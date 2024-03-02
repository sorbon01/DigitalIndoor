using AutoMapper;
using DigitalIndoorAPI.DTOs;
using DigitalIndoorAPI.DTOs.Response;
using DigitalIndoorAPI.Exceptions;
using DigitalIndoorAPI.Models;
using DigitalIndoorAPI.Models.DB;
using DigitalIndoorAPI.DTOs.Params;
using DigitalIndoorAPI.DTOs.Request;
using Microsoft.EntityFrameworkCore;

namespace DigitalIndoorAPI.Services.Implementations
{
    public class TerminalService : ITerminalService
    {
        readonly IUserService userService;
        readonly Context context;
        readonly IMapper mapper;

        public TerminalService(IUserService userService, Context context, IMapper mapper)
        {
            this.userService = userService;
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<PagedList<Terminal, TerminalViewDto>> SearchAsync(TerminalParam param)
        {
            var query = context.Terminals 
                .Include(x => x.User)
                .Include(x => x.PlayList)
                .Where(x => !x.IsDeleted &&
                (string.IsNullOrWhiteSpace(param.Name) || x.Name.Contains(param.Name)))
                .OrderBy(x => x.Id).AsQueryable();

            var count = query.CountAsync();
            var items = query.Skip((param.Page - 1) * param.Size).Take(param.Size).ToListAsync();

            return new PagedList<Terminal, TerminalViewDto>(await items, await count, param.Page, param.Size, mapper);
        }
        public async Task<TerminalViewDto> AddAsync(TerminalCreateDto create)
        {
            if (await context.Terminals.AnyAsync(c => !c.IsDeleted && c.Name == create.Name))
                throw new ToException(ToErrors.ENTITY_WITH_THIS_NAME_ALREADY_EXIST);

            var terminal = mapper.Map<Terminal>(create);
            var user = await userService.GetCurrentAsync();
            terminal.UserId = user.Id;

            await context.Terminals.AddAsync(terminal);
            await context.SaveChangesAsync();

            terminal.User = user;
            await context.Entry(terminal).Reference(t=>t.PlayList).LoadAsync();

            return mapper.Map<TerminalViewDto>(terminal);
        }

        public async Task<TerminalViewDto> UpdateAsync(TerminalUpdateDto update)
        {
            var oldObj = await context.Terminals
                .AsNoTracking()
                .Include(v => v.User)
                .FirstOrDefaultAsync(x => x.Id == update.Id);
            if (oldObj is null)
                throw new ToException(ToErrors.ENTITY_NOT_FOUND);

            if (await context.Terminals.AnyAsync(c => !c.IsDeleted && c.Id != update.Id && c.Name == update.Name))
                throw new ToException(ToErrors.ENTITY_WITH_THIS_NAME_ALREADY_EXIST);

            var terminal = mapper.Map<Terminal>(update);

            terminal.UserId = oldObj.UserId;
            terminal.RecordDate = oldObj.RecordDate;

            context.Terminals.Update(terminal);
            await context.SaveChangesAsync();

            terminal.User = oldObj.User;
            await context.Entry(terminal).Reference(t => t.PlayList).LoadAsync();

            return mapper.Map<TerminalViewDto>(terminal);
        }

        public async Task<TerminalViewDto> DeleteAsync(int id)
        {
            var terminal = await context.Terminals.Include(x => x.User).FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id);
            if (terminal is null)
                throw new ToException(ToErrors.ENTITY_NOT_FOUND);

            terminal.IsDeleted = true;
            await context.SaveChangesAsync();

            return mapper.Map<TerminalViewDto>(terminal);
        }
    }
}
