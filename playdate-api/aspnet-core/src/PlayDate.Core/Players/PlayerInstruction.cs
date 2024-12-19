using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using PlayDate.Authorization.Users;

namespace PlayDate.Players
{
    [Table("PlayerInstructions")]
    public class PlayerInstruction : Entity<long>
    {
        [ForeignKey("PlayerId")]
        public long PlayerId { get; set; }
        public Player Player { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
    }
}