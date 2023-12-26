using DigitalIndoor.Models.Abstraction;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalIndoor.Models
{
    public class PlayList : DbRecord
    {
        public string Name { get; set; }

        [NotMapped]
        public virtual List<Order> Orders { get; set; }
    }
}
