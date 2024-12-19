using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using PlayDate.Authorization.Users;

namespace PlayDate.Players
{
    [Table("PlayerRestrictions")]
    public class PlayerRestriction : Entity<long>
    {
        [ForeignKey("PlayerId")]
        public long PlayerId { get; set; }
        public Player Player { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
       
        public RestrictionType RestrictionType { get; set; }                
    }

    public enum RestrictionType : int
    {
        Other = 0,
        Food = 1       
    }
}