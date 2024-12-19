using Microsoft.AspNetCore.Identity;
using PlayDate.Events.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization;
using PlayDate.Authorization;


using System.Text.RegularExpressions;

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using PlayDate.Authorization.Accounts;
using PlayDate.Authorization.Roles;
using PlayDate.Authorization.Users;
using PlayDate.Roles.Dto;
using PlayDate.Users.Dto;
using System.Data.SqlTypes;
using PlayDate.Common;
using PlayDate.Players;
using Microsoft.AspNetCore.Connections;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Logging;
using PlayDate.Players.Dto;

namespace PlayDate.Events
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class EventAppService : PlayDateAppServiceBase, IEventAppService
    {
        private readonly IRepository<Event, long> _eventRepository;
        private readonly IRepository<PlayerEvent, long> _playerEventRepository;
        private readonly IRepository<EventNote, Guid> _eventNoteRepository;
        private readonly IRepository<Player, long> _playerRepository;
        private readonly IPlayerAppService _playerAppService;

        public EventAppService(
            IRepository<Event, long> eventRepository,
            IRepository<PlayerEvent, long> playerEventRepository,
            IRepository<EventNote, Guid> eventNoteRepository,
            IRepository<Player, long> playerRepository,
            IPlayerAppService playerAppService
            )
        {
            _eventRepository = eventRepository;
            _playerEventRepository = playerEventRepository;
            _eventNoteRepository = eventNoteRepository;
            _playerRepository = playerRepository;
            _playerAppService = playerAppService;
        }
        
        public async Task<List<GetEventsOutput>> GetEvents(GetEventsInput input)
        {
            var results = new List<GetEventsOutput>();
            input.UserId = input.UserId == null ? 0 : input.UserId;
            input.PlayerId = input.PlayerId == null ? 0 : input.PlayerId;
            input.DateFrom = input.DateFrom == null ? (DateTime)SqlDateTime.MinValue : input.DateFrom;
            input.DateTo = input.DateTo == null ? DateTime.MaxValue : input.DateTo;

            _eventRepository.GetAll().Where(
                q => q.IsDeleted == false &&
                (q.CreatorUserId == input.UserId || input.UserId <= 0) &&
                (q.HostPlayerId == input.PlayerId || input.PlayerId <= 0) &&
                (q.StartDate >= input.DateFrom && q.EndDate <= input.DateTo) &&
                (
                    input.Title == string.Empty ||
                    q.Title.StartsWith(input.Title) ||
                    q.Title.EndsWith(input.Title)
                ))
                .OrderByDescending(o => o.StartDate)
                .OrderBy(o => o.Title)
                .ToList()
                .ForEach(ev =>
                {
                    results.Add(new GetEventsOutput()
                    {
                        Title = ev.Title,
                        Description = ev.Description,
                        EndDate = ev.EndDate,
                        Id = ev.Id,
                        Instructions = ev.Instructions,
                        IsFood = ev.IsFood,
                        IsIndoors = ev.IsIndoors,
                        IsOutdoors = ev.IsOutdoors,
                        IsPets = ev.IsPets,
                        IsPublicPlace = ev.IsPublicPlace,
                        IsPublicPlaceSupervised = ev.IsPublicPlaceSupervised,
                        IsResidense = ev.IsResidense,
                        IsResidenseSupervisedIndoors = ev.IsResidenseSupervisedIndoors,
                        IsSupervisedOutdoors = ev.IsSupervisedOutdoors,
                        IsSwimming = ev.IsSwimming,
                        IsSwimmingSupervised = ev.IsSwimmingSupervised,
                        MapsLocation = ev.MapsLocation,
                        MaxCapacity = ev.MaxCapacity,
                        StartDate = ev.StartDate,
                        Status = ev.Status
                    });
                });

            return await Task.FromResult(results);
        }
        public async Task<List<GetPlayerOutput>> GetEventPlayers(EventIdInput input)
        {
            var results = new List<GetPlayerOutput>();
            var players = (from ep in _playerEventRepository.GetAll()
                           join p in _playerRepository.GetAll() on ep.PlayerId equals p.Id
                           where ep.EventId == input.Id
                           select p).Distinct().ToList();
            foreach(var player in players)
            {
                var p = await FillPlayerInfo(player);
                results.Add(p);
            }
            return await Task.FromResult(results);
                
        }
        public async Task<GetStandardOutput> DeleteEvent(EventIdInput input)
        {
            var result = new GetStandardOutput();
            var ev = _eventRepository.FirstOrDefault(q => q.Id == input.Id && q.IsDeleted == false);
            if(ev != null)
            {
                if(ev.Status != EventStatus.Started)
                {
                    ev.IsDeleted = true;
                    ev.DeleterUserId = AbpSession.GetUserId();
                    ev.DeletionTime = DateTime.UtcNow;
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
                else
                {
                    result.Errors.Add("Please Cancel this event before deleting.");
                }
            }
            return result;
        }
        public async Task<GetStandardOutput> CreateOrUpdateEvent(CreateOrUpdateEventInput input)
        {
            var result = new GetStandardOutput();
            var ev = new Event();
            if (input.Id <= 0)
            {
                ev = new Event();
            }
            else
            {
                ev = _eventRepository.FirstOrDefault(q => q.Id == input.Id && q.IsDeleted == false);
            }
            if (ev != null)
            {
                ev.StartDate = input.StartDate;
                ev.EndDate = input.EndDate;
                ev.Description = input.Description;
                ev.Title = input.Title;
                ev.Instructions = input.Title;
                ev.MapsLocation = input.MapsLocation;
                ev.MaxCapacity = input.MaxCapacity;
                ev.IsResidense = input.IsResidense;
                ev.IsPublicPlace = input.IsPublicPlace;
                ev.IsPublicPlaceSupervised = input.IsPublicPlaceSupervised;
                ev.IsIndoors = input.IsIndoors;
                ev.IsSupervisedOutdoors = input.IsSupervisedOutdoors;
                ev.IsFood = input.IsFood;
                ev.IsPets = input.IsPets;
                ev.IsSwimming = input.IsSwimming;
                ev.IsSwimmingSupervised = input.IsSwimmingSupervised;
                ev.LastModifierUserId = AbpSession.GetUserId();
                ev.LastModificationTime = DateTime.UtcNow;

                await CurrentUnitOfWork.SaveChangesAsync();
            }
          
            return result;
        }
        public async Task<GetStandardOutput> CreateOrUpdateEventPlayers(CreateOrUpdateEventPlayersInput input)
        {
            var result = new GetStandardOutput();
            foreach (var player in input.Players)
            {

                var ev = _playerEventRepository.FirstOrDefault(
                    q => q.PlayerId == player.Id && 
                    q.EventId == input.EventId);

                if (ev != null)
                {
                   
                    var evplayer = _playerEventRepository.FirstOrDefault(
                        q => q.PlayerId == player.Id &&
                        q.EventId == input.EventId);

                    if (evplayer != null)
                    {
                        evplayer.Notes = player.Notes;
                        evplayer.TravelTypeThere = player.TravelTypeThere;
                        evplayer.TravelTypeBack = player.TravelTypeBack;
                        evplayer.Status = player.Status;
                    }
                    else
                    {
                        _playerEventRepository.Insert(new PlayerEvent()
                        {
                            EventId = input.EventId,
                            PlayerId = player.Id,
                            Notes = player.Notes,
                            TravelTypeThere = player.TravelTypeThere,
                            TravelTypeBack = player.TravelTypeBack,
                            Status = player.Status
                        });
                    }
                   
                    await CurrentUnitOfWork.SaveChangesAsync();

                }
            }
            return result;
        }
        public async Task<GetStandardOutput> CreateOrUpdateEventNote(CreateOrUpdateEventNoteInput input)
        {
            var result = new GetStandardOutput();
            if(input.Id != null)
            {
                var evnote = _eventNoteRepository.FirstOrDefault(q => q.Id == input.Id);
                if (evnote != null) {

                    if (!input.IsReply)
                    {
                        await _eventNoteRepository.InsertAsync(new EventNote()
                        {
                            Id = Guid.NewGuid(),
                            CreationTime = DateTime.UtcNow,
                            CreatorUserId = AbpSession.GetUserId(),
                            DeleterUserId = null,
                            DeletionTime = null,
                            EventId = input.EventId,
                            IsDeleted = false,
                            Note = input.Note,
                            ParentNoteId = input.Id,
                            PlayerId = AbpSession.GetUserId()
                        });
                    }
                    else
                    {
                        evnote.Note = input.Note;
                        evnote.LastModificationTime = DateTime.UtcNow;
                        evnote.LastModifierUserId = AbpSession.GetUserId();                           
                    }
                }
                
            }
            else if(!string.IsNullOrEmpty(input.Note))
            {
                await _eventNoteRepository.InsertAsync(new EventNote()
                {
                    Id = Guid.NewGuid(),
                    CreationTime = DateTime.UtcNow,
                    CreatorUserId = AbpSession.GetUserId(),
                    DeleterUserId = null,
                    DeletionTime = null,
                    EventId = input.EventId,
                    IsDeleted = false,
                    Note = input.Note,
                    ParentNoteId = null,
                    PlayerId = AbpSession.GetUserId()
                });
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            return await Task.FromResult(result);
        }
        public async Task<GetStandardOutput> DeleteEventNote(DeleteEventNoteInput input)
        {
            var result = new GetStandardOutput();
            var ev = _eventNoteRepository.FirstOrDefault(q => q.Id == input.Id && q.IsDeleted == false);
            if (ev != null && ev.CreatorUserId == AbpSession.GetUserId())
            {                
                ev.IsDeleted = true;
                ev.DeleterUserId = AbpSession.GetUserId();
                ev.DeletionTime = DateTime.UtcNow;
                await CurrentUnitOfWork.SaveChangesAsync();               
            }
            return await Task.FromResult(result);
        }
        public async Task<List<GetEventNoteOutput>> GetEventNotes(EventIdInput input)
        {
            var results = new List<GetEventNoteOutput>();
            var ev = _eventRepository.FirstOrDefault(q => q.Id == input.Id);
            if (ev != null) {

                results = (from note in _eventNoteRepository.GetAll()
                            join player in _playerRepository.GetAll() on note.PlayerId equals player.Id
                            where note.EventId == ev.Id && note.IsDeleted == false
                            select new GetEventNoteOutput()
                            {
                                Id = note.Id,
                                EventId = note.EventId,
                                CreationTime = note.CreationTime,
                                EventName = ev.Title,
                                Note = note.Note,
                                PlayerId = note.PlayerId,
                                PlayerName = player.FirstName + ' ' + player.LastName,
                                ReplyToNoteId = note.ParentNoteId
                            }).OrderByDescending(o => o.CreationTime).ToList();
            }
            return await Task.FromResult(results);
                
        }
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
