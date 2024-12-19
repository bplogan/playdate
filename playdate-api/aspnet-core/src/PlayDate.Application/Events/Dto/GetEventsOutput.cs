using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PlayDate.Players.Dto;
using System;
using System.Collections.Generic;

namespace PlayDate.Events.Dto
{
    [AutoMapFrom(typeof(Event))]
    public class GetEventsOutput : EntityDto
    {
        public long Id { get; set; }
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
        public bool IsSwimming { get; set; }
        public bool IsSwimmingSupervised { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public EventStatus Status { get; set; }
        public List<GetPlayerOutput> Players { get; set; }

    }
}
