using DigitalIndoorAPI.Models.Abstraction;
using DigitalIndoorAPI.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalIndoorAPI.Models
{
    public class PlayList : DbRecord
    {
        public string Name { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public virtual List<Order> Orders { get; set; } = [];
    }
}
