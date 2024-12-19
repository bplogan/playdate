using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using PlayDate.Players;

namespace PlayDate.Players
{
    [Table("Friends")]
    public class PlayerFriend : FullAuditedEntity<Guid>
    {        
        public long PlayerOneId { get; set; }          
        public long PlayerTwoId { get; set; }
        public PlayerFriendStatus StatusId { get; set; }
    }
}