using System.Collections.Generic;

namespace PlayDate.Players.Dto
{
    public class GetPlayerInstructionOutput
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
        public string Description { get; set; }
    }
}
