using DigitalIndoor.Models.Abstraction;

namespace DigitalIndoor.Models
{
    public class Terminal:DbRecord
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public int Pincode { get; set; }
        public int? PlayListId { get; set; }
        public virtual PlayList PlayList { get; set; } 
        public bool IsActive { get; set; }
    }
}
