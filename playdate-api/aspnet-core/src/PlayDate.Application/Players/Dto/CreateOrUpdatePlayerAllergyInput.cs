using PlayDate.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayDate.Players.Dto
{
    public class CreateOrUpdatePlayerAllergyInput
    {
        public long Id { get; set; }
        public long PlayerId { get; set; }
        public string Description { get; set; }
        public AllergyType AllergyType { get; set; }
    }
}
