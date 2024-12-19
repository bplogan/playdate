using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayDate.Events.Dto
{
    public class GetEventsInput
    {
        public long? UserId { get; set; }
        public long? PlayerId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Title { get; set; } = "";         
    }
}
