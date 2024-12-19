using Abp.Application.Services;
using PlayDate.Common;
using PlayDate.Events.Dto;
using PlayDate.Players.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayDate.Events
{
    public interface IEventAppService: IApplicationService
    {
        Task<List<GetEventsOutput>> GetEvents(GetEventsInput input);
        Task<List<GetPlayerOutput>> GetEventPlayers(EventIdInput input);
        Task<GetStandardOutput> DeleteEvent(EventIdInput input);
        Task<GetStandardOutput> CreateOrUpdateEvent(CreateOrUpdateEventInput input);
        Task<GetStandardOutput> CreateOrUpdateEventPlayers(CreateOrUpdateEventPlayersInput input);
        Task<GetStandardOutput> CreateOrUpdateEventNote(CreateOrUpdateEventNoteInput input);
        Task<GetStandardOutput> DeleteEventNote(DeleteEventNoteInput input);
        Task<List<GetEventNoteOutput>> GetEventNotes(EventIdInput input);

    }
}
