using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayDate.Friends.Dto
{
    public class CreateOrUpdateFriendInput
    {
        public Guid? Id { get; set; }
        public long FriendIdOne { get; set; } 
        public long FriendIdTwo { get; set; }
        public FriendStatus StatusId { get; set; }
    }
}
