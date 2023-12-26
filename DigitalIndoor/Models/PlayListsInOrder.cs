using DigitalIndoor.Models.Abstraction;

namespace DigitalIndoor.Models
{
    public class PlayListsInOrder:DbRecord
    {
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }    
        public int? PlayListId { get; set; }
        public virtual PlayList PlayList { get; set; }  
    }
}
