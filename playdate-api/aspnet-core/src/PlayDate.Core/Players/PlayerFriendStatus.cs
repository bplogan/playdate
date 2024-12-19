using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using PlayDate.Players;

namespace PlayDate.Players
{
    public enum PlayerFriendStatus : int
    {
        Invited = 1,
        Accepted = 2,
        Rejected = 3
    }
}