using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PlayDate.Players.Dto;
using System;
using System.Collections.Generic;

namespace PlayDate.Events.Dto
{    
    public class GetEventNoteOutput 
    {
        public Guid Id { get; set; }
        public string Note { get; set; }
        public long PlayerId { get; set; }
        public string PlayerName { get; set; }
        public long EventId { get; set; }
        public string EventName { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid? ReplyToNoteId { get; set; }
        public string ReplyToNote { get; set; }

    }
}
