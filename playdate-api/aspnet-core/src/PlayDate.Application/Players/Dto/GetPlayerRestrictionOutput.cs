using System.Collections.Generic;

namespace PlayDate.Players.Dto
{
    public class GetPlayerRestrictionOutput
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
        public string Description { get; set; }
        public RestrictionType RestrictionType { get; set; }
    }
}
