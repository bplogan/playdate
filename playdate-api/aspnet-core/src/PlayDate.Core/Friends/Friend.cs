using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using PlayDate.Players;

namespace PlayDate.Friends
{
    [Table("Friends")]
    public class Friend : FullAuditedEntity<Guid>
    {        
        public long PlayerOneId { get; set; }          
        public long PlayerTwoId { get; set; }
        public FriendStatus StatusId { get; set; }
    }
}