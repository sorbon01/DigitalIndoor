using DigitalIndoorAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using DigitalIndoorAPI.Services;

namespace DigitalIndoorAPI.Controllers.Base
{
    public class CrudBaseController<TInterface, TModel, TView, TParam, TCreate, TUpdate> : BaseController 
        where TInterface : ICrudService<TModel, TView, TParam, TCreate, TUpdate>
    {
        protected readonly TInterface service;
        public CrudBaseController(TInterface service)
            =>this.service = service;

        [HttpGet]
        public async Task<ActionResult<PagedList<TModel, TView>>> GetAsync([FromQuery] TParam param)
            => Ok(await service.SearchAsync(param));

        [HttpPost]
        public async Task<ActionResult<TView>> AddAsync([FromBody] TCreate create)
            => Ok(await service.AddAsync(create));

        [HttpPut]
        public async Task<ActionResult<TView>> UpdateAsync([FromBody] TUpdate update)
            => Ok(await service.UpdateAsync(update));

        [HttpDelete("{id}")]
        public async Task<ActionResult<TView>> DeleteAsync([FromRoute] int id)
             => Ok(await service.DeleteAsync(id));
    }
}
