using DigitalIndoorAPI.Models.Abstraction;
using DigitalIndoorAPI.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalIndoorAPI.Models
{
    public class Order:DbRecord
    {
        public string ClientName { get; set; }
        public int? VideoId { get; set; }
        public virtual Video Video { get; set; }
        public int? UserId { get; set; } 
        public virtual User User { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public virtual List<PlayList> PlayLists { get; set; } = [];
    }
}
