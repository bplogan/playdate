using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using PlayDate.Players;

namespace PlayDate.Events
{
    public enum PlayerEventTravelType : int
    {
        ParentOrGuardian = 1,
        Bike = 2,
        Walk = 3,
        Carpool = 4,
        Pickup = 5
    }
}