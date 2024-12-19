using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using PlayDate.Players;

namespace PlayDate.Events
{
    [Table("Events")]
    public class Event : FullAuditedEntity<long>
    {       
        [Required]
        [StringLength(256)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string MapsLocation { get; set; }
        public int MaxCapacity { get; set; }
        public bool IsResidense { get; set; }
        public bool IsPublicPlace { get; set; }
        public bool IsPublicPlaceSupervised { get; set; }
        public bool IsIndoors { get; set; }
        public bool IsResidenseSupervisedIndoors { get; set; }
        public bool IsOutdoors { get; set; }
        public bool IsSupervisedOutdoors { get; set; }
        public bool IsFood { get; set; }
        public bool IsPets { get; set; }
        public bool IsSwimming { get; set;}
        public bool IsSwimmingSupervised { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public EventStatus Status { get; set; }
        public long HostPlayerId { get; set; }
        
    }

    public enum EventStatus : int
    {
        NA = 0,
        Editing = 1,
        Saved = 2,
        Scheduled = 3,
        Pending = 4,
        Started = 5,
        Ended = 6,
        Canceled = 99,
    }
}