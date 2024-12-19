using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using PlayDate.Players;

namespace PlayDate.Friends
{
    public enum FriendStatus : int
    {
        Invited = 1,
        Accepted = 2,
        Rejected = 3
    }
}