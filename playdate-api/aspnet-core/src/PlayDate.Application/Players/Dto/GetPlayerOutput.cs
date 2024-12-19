using System.Collections.Generic;

namespace PlayDate.Players.Dto
{
    public class GetPlayerOutput
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }        
        public string Address2 { get; set; }   
        public string City { get; set; }    
        public string Province { get; set; }       
        public string PostalCode { get; set; }      
        public string Country { get; set; }
        public int Age { get; set; }
        public bool IsSwimmer { get; set; }
        public bool IsPetAllergy { get; set; }
        public bool IsFoodRestricted { get; set; }
        public bool IsFoodAllergy { get; set; }
        public bool IsOtherAllergy { get; set; }
        public bool IsOtherRestricted { get; set; }
        public bool IsSpecialInstructions { get; set; }
        public bool HasEpiPen { get; set; }
        public bool IsNutAllergy { get; set; } 
        public bool IsEggAllergy { get; set; } 
        public bool IsDairyAllergy { get; set; }
        public bool IsCatAllergy { get; set; } 
        public bool IsDogAllergy { get; set; } 
        public bool IsVegetarian { get; set; }
        public bool IsVegan { get; set; }
        public List<GetPlayerOutput> Friends { get; set; } = new List<GetPlayerOutput>();
        public List<GetPlayerAllergyOutput> Allergies { get; set; } = new List<GetPlayerAllergyOutput>();
        public List<GetPlayerInstructionOutput> Instructions { get; set; } = new List<GetPlayerInstructionOutput>();
        public List<GetPlayerRestrictionOutput> Restrictions { get; set; } = new List<GetPlayerRestrictionOutput>();
    }
}
