using PlayDate.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayDate.Events.Dto
{
    public class GetEventPlayerOutput
    {
        public long Id { get; set; }
        public string Notes { get; set; }
        public PlayerEventTravelType TravelTypeThere { get; set; }
        public PlayerEventTravelType TravelTypeBack { get; set; }
        public PlayerEventStatus Status { get; set; }
    }
}
