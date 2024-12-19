using System.Collections.Generic;

namespace PlayDate.Players.Dto
{
    public class GetPlayerAllergyOutput
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
        public string Description { get; set; }
        public AllergyType AllergyType { get; set; }
    }
}
