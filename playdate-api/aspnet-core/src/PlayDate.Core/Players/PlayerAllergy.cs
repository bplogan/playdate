using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using PlayDate.Authorization.Users;

namespace PlayDate.Players
{
    [Table("PlayerAllergies")]
    public class PlayerAllergy : Entity<long>
    {
        [ForeignKey("PlayerId")]
        public long PlayerId { get; set; }
        public Player Player { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
       
        public AllergyType AllergyType { get; set; }                
    }

    public enum AllergyType : int
    {
        Other = 0,
        Food = 1,
        Medication = 2,
        Cats = 3,
        Dogs = 4,
        Birds = 5
    }
}