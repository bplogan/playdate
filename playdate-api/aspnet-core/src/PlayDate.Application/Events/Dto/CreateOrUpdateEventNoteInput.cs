using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayDate.Events.Dto
{
    public class CreateOrUpdateEventNoteInput
    {
        public Guid? Id { get; set; }
        public long EventId { get; set; }
        public string Note { get; set; }
        public bool IsReply { get; set; }
      
    }
}
