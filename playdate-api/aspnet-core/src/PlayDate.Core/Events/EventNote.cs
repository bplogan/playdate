using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using PlayDate.Players;

namespace PlayDate.Events
{
    [Table("EventNotes")]
    public class EventNote : FullAuditedEntity<Guid>
    {
        [ForeignKey("PlayerId")]
        public long PlayerId { get; set; }
        public Player Player { get; set; }

        [ForeignKey("EventId")]
        public long EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        [StringLength(256)]
        public string Note { get; set; }       
        public Guid? ParentNoteId { get; set; }
        
    }

   
}