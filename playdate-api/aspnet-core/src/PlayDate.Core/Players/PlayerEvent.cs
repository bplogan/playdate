using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using PlayDate.Authorization.Users;
using PlayDate.Events;

namespace PlayDate.Players
{
    [Table("PlayerEvents")]
    public class PlayerEvent : Entity<long>
    {
        [ForeignKey("PlayerId")]
        public long PlayerId { get; set; }
        public Player Player { get; set; }

        [ForeignKey("EventId")]
        public long EventId { get; set; }
        public Event Event { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        public PlayerEventTravelType TravelTypeThere { get; set; } 
        public PlayerEventTravelType TravelTypeBack { get; set; }
        public PlayerEventStatus Status { get; set; }
    }

    public enum PlayerEventStatus : int
    {
        Invited = 0,
        Accepted = 1,
        Confirmed = 2,
        Ended = 3,
        Emergency = 4,
        Rejected = 98,
        Canceled = 99
    }
}