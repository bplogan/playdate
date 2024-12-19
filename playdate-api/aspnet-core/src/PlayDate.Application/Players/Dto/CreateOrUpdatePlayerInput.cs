using System;
using System.Collections.Generic;

namespace PlayDate.Players.Dto
{
    public class CreateOrUpdatePlayerInput
    {
        public long Id { get; set; } = 0;
        public string FullName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int Age { get; set; } = 0;
        public bool IsSwimmer { get; set; } = false;
        public bool IsPetAllergy { get; set; } = false;
        public bool IsFoodRestricted { get; set; } = false;
        public bool IsFoodAllergy { get; set; } = false;
        public bool IsOtherAllergy { get; set; } = false;
        public bool IsOtherRestricted { get; set; } = false;
        public bool IsSpecialInstructions { get; set; } = false;
        public bool HasEpiPen { get; set; } = false;
        public bool IsNutAllergy { get; set; } = false;
        public bool IsEggAllergy { get; set; } = false;
        public bool IsDairyAllergy { get; set; } = false;
        public bool IsCatAllergy { get; set; } = false;
        public bool IsDogAllergy { get; set; } = false;
        public bool IsVegetarian { get; set; } = false;
        public bool IsVegan { get; set; } = false;
        public DateTime? DOB { get; set; } = DateTime.Now;
    }
}
