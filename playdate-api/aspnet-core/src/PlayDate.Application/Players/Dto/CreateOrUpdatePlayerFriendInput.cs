using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayDate.Players.Dto
{
    public class CreateOrUpdatePlayerFriendInput
    {
        public Guid? Id { get; set; }
        public long FriendIdOne { get; set; } 
        public long FriendIdTwo { get; set; }
        public PlayerFriendStatus StatusId { get; set; }
    }
}
