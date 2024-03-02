using DigitalIndoorAPI.Models.Abstraction;
using DigitalIndoorAPI.Models.Common;

namespace DigitalIndoorAPI.Models
{
    public class Terminal:DbRecord
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public int Pincode { get; set; }
        public int? PlayListId { get; set; }
        public virtual PlayList PlayList { get; set; }
        public bool IsActive { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
