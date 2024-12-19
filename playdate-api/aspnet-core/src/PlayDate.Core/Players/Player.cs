using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using PlayDate.Authorization.Users;

namespace PlayDate.Players
{
    [Table("Players")]
    public class Player : FullAuditedEntity<long>
    {
        [Required]
        [StringLength(150)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string LastName { get; set; }

        [StringLength(150)]
        public string PhoneNumber { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(256)]
        public string Address { get; set; }
        [StringLength(256)]
        public string Address2 { get; set; }
        [StringLength(256)]
        public string City { get; set; }
        [StringLength(256)]
        public string Province { get; set; }
        [StringLength(25)]
        public string PostalCode { get; set; }
        [StringLength(25)]
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
       


        [MaxLength(256)]
        public string PlayerCode { get; set; }

        [ForeignKey("UserId")]
        public long UserId { get; set; }
        public User User { get; set; }
    }
}