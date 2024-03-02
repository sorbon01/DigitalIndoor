using AutoMapper;

namespace DigitalIndoorAPI.DTOs
{
    public class PagedList<I, O>
    {
        public IList<O> Items { get; set; }
        public int Page { get;  set; }
        public int TotalCount { get; set; }
        public int Size { get; set; }

        public PagedList(IList<I> items, int totalCount, int page, int size, IMapper mapper)
        {
            Page = page;
            TotalCount = totalCount;
            Size = size;
            Items = mapper.Map<List<O>>(items);
        }
    }
}
