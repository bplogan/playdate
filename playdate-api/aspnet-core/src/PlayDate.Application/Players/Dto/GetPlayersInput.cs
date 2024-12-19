using System.Collections.Generic;

namespace PlayDate.Players.Dto
{
    public class GetPlayersInput
    {
        public long? UserId { get; set; }
        public long? PlayerId { get; set; }
        public bool? IncludeDetails { get; set; } = false;
    }
}
