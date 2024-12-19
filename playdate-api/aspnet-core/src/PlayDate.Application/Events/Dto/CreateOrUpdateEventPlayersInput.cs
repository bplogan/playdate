using PlayDate.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayDate.Events.Dto
{
    public class CreateOrUpdateEventPlayersInput
    {
        public long EventId { get; set; }
        public List<GetEventPlayerOutput> Players { get; set; }
       
    }
}
