using DigitalIndoor.Models.Abstraction;
using DigitalIndoor.Models.Common;

namespace DigitalIndoor.Models
{
    public class Video:DbRecord
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string FileName { get; set; }
        public int? UserId {  get; set; }
        public virtual User User { get; set; }
    }
}
