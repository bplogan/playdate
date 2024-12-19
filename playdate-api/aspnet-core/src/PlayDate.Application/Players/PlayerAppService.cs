using Microsoft.AspNetCore.Identity;
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
using PlayDate.Events;
using PlayDate.Players.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;
using System.Net;
using PlayDate.Events.Dto;


namespace PlayDate.Players
{
    [AbpAuthorize(PermissionNames.Pages)]
    public class PlayerAppService : PlayDateAppServiceBase, IPlayerAppService
    {
        private readonly IRepository<PlayerInstruction, long> _playerInstructionRepository;
        private readonly IRepository<PlayerRestriction, long> _playerRestrictionRepository;
        private readonly IRepository<PlayerAllergy, long> _playerAllergyRepository;
        private readonly IRepository<PlayerFriend, Guid> _playerFriendsRepository;
        private readonly IRepository<Player, long> _playerRepository;

        public PlayerAppService(
            IRepository<PlayerInstruction, long> playerInstructionRepository,
            IRepository<PlayerRestriction, long> playerRestrictionRepository,
            IRepository<PlayerAllergy, long> playerAllergyRepository,
            IRepository<PlayerFriend, Guid> playerFriendsRepository,
            IRepository<Player, long> playerRepository
            )
        {
            _playerInstructionRepository = playerInstructionRepository;
            _playerRestrictionRepository = playerRestrictionRepository;
            _playerAllergyRepository = playerAllergyRepository;
            _playerFriendsRepository = playerFriendsRepository;
            _playerRepository = playerRepository;
        }

        public async Task<GetStandardOutput> CreateOrUpdatePlayer(CreateOrUpdatePlayerInput input)
        {
            var result = new GetStandardOutput();
            try
            {           
                if (input.Id > 0)
                {
                    var player = _playerRepository.FirstOrDefault(q => q.Id == input.Id);
                    if (player != null)
                    {
                    
                        player.Address = input.Address;
                        player.LastModificationTime = DateTime.UtcNow;
                        player.LastModifierUserId = AbpSession.GetUserId();
                        player.PhoneNumber = "";
                        player.Address2 = input.Address2;
                        player.Age = input.Age;
                        player.City = input.City;
                        player.Country = input.Country;
                        player.DOB = input.DOB;
                        player.FirstName = input.FullName;
                        player.HasEpiPen = input.HasEpiPen;
                        player.IsFoodAllergy = input.IsFoodAllergy;
                        player.IsFoodRestricted = input.IsFoodRestricted;
                        player.IsOtherAllergy = input.IsOtherAllergy;
                        player.IsOtherRestricted = input.IsOtherRestricted;
                        player.IsPetAllergy = input.IsPetAllergy;
                        player.IsSpecialInstructions = input.IsSpecialInstructions;
                        player.IsSwimmer = input.IsSwimmer;
                        player.PostalCode = input.PostalCode;
                        player.Province = input.Province;
                        player.IsNutAllergy = input.IsNutAllergy;
                        player.IsEggAllergy = input.IsEggAllergy;
                        player.IsDairyAllergy = input.IsDairyAllergy;
                        player.IsDogAllergy = input.IsDogAllergy;
                        player.IsCatAllergy = input.IsCatAllergy;
                        player.IsVegetarian = input.IsVegetarian;
                        player.IsVegan = input.IsVegan;
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                }
                else
                {
                    var newCode = GetPlayerCode();
                    _playerRepository.Insert(
                        new Player()
                        {
                            Address = input.Address,
                            CreatorUserId = AbpSession.GetUserId(),
                            PhoneNumber = "",
                            Address2 = input.Address2,
                            Age = input.Age,
                            City = input.City,
                            Country = input.Country,
                            CreationTime = DateTime.UtcNow,
                            DOB = input.DOB,
                            FirstName = input.FullName,
                            HasEpiPen = input.HasEpiPen,
                            Id = input.Id,
                            IsDeleted = false,
                            IsFoodAllergy = input.IsFoodAllergy,
                            IsFoodRestricted = input.IsFoodRestricted,
                            IsOtherAllergy = input.IsOtherAllergy,
                            IsOtherRestricted = input.IsOtherRestricted,
                            IsPetAllergy = input.IsPetAllergy,
                            IsSpecialInstructions = input.IsSpecialInstructions,
                            IsSwimmer = input.IsSwimmer,
                            PlayerCode = newCode,
                            PostalCode = input.PostalCode,
                            Province = input.Province,
                            UserId = AbpSession.GetUserId(),
                            IsVegan = input.IsVegan,
                            IsVegetarian = input.IsVegetarian,
                            IsDogAllergy = input.IsDogAllergy,
                            IsCatAllergy = input.IsCatAllergy,
                            IsDairyAllergy = input.IsDairyAllergy,
                            IsEggAllergy = input.IsEggAllergy,
                            IsNutAllergy = input.IsNutAllergy
                        });
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message + e.InnerException != null ? e.InnerException.Message : "");                
            }
            return await Task.FromResult(result);
        }
        public async Task<List<GetPlayerOutput>> GetPlayers(GetPlayersInput input)
        {
            try
            {
                var players = new List<GetPlayerOutput>();
                input.UserId = input.UserId == null ? 0 : input.UserId;
                input.PlayerId = input.PlayerId == null ? 0 : input.PlayerId;
                if (input.IncludeDetails == null || input.IncludeDetails == false)
                {
                    players = (from p in _playerRepository.GetAll()
                               where (
                               (input.UserId <= 0 || p.UserId == input.UserId) &&
                               (input.PlayerId <= 0 || p.Id == input.PlayerId)) &&
                               p.IsDeleted == false
                               select new GetPlayerOutput()
                               {
                                   FullName = string.IsNullOrEmpty(p.LastName) ? p.FirstName : p.FirstName + " " + p.LastName,
                                   Address = p.Address,
                                   Address2 = p.Address2,
                                   Age = p.Age,
                                   City = p.City,
                                   Country = p.Country,
                                   EmailAddress = "",
                                   Friends = new List<GetPlayerOutput>(),
                                   HasEpiPen = p.HasEpiPen,
                                   Id = p.Id,
                                   IsFoodAllergy = p.IsFoodAllergy,
                                   IsFoodRestricted = p.IsFoodRestricted,
                                   IsOtherAllergy = p.IsOtherAllergy,
                                   IsOtherRestricted = p.IsOtherRestricted,
                                   IsPetAllergy = p.IsPetAllergy,
                                   IsSpecialInstructions = p.IsSpecialInstructions,
                                   IsSwimmer = p.IsSwimmer,
                                   PostalCode = p.PostalCode,
                                   Province = p.Province,
                                   UserName = string.IsNullOrEmpty(p.LastName) ? p.FirstName : p.FirstName + " " + p.LastName

                               }).OrderBy(o => o.FullName).ToList();
                }
                else
                {
                    var playerIds = (from p in _playerRepository.GetAll()
                                     where ((input.UserId <= 0 || p.UserId == input.UserId) ||
                                     (input.PlayerId <= 0 || p.Id == input.PlayerId)) &&
                                     p.IsDeleted == false
                                     select p.Id).ToList();
                    foreach (var id in playerIds)
                    {
                        var details = await GetPlayerDetails(new GetPlayerDetailsInput() { Id = id });
                        players.Add(details);
                    }
                }
                return await Task.FromResult(players);
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public async Task<GetPlayerOutput> GetPlayerDetails(GetPlayerDetailsInput input)
        {
            try
            {
                var player = new GetPlayerOutput();
                var dbPlayer = _playerRepository.FirstOrDefault(q => q.Id == input.Id && q.IsDeleted == false);
                if (dbPlayer != null)
                {
                    player = await FillPlayerInfo(dbPlayer);
                    player.Allergies = await GetPlayerAllergies(new GetPlayerAllergiesInput() { PlayerId = input.Id });
                    player.Instructions = await GetPlayerInstructions(new GetPlayerInstructionsInput() { PlayerId = input.Id });
                    player.Restrictions = await GetPlayerRestrictions(new GetPlayerRestrictionsInput() { PlayerId = input.Id });
                    player.Friends = await GetPlayerFriends(new GetPlayerFriendsInput() { PlayerId = input.Id });
                }
                return await Task.FromResult(player);
            }
            catch (Exception e)
            {

                throw e;
            }
           
        }
        public async Task<GetStandardOutput> CreateOrUpdatePlayerAllergy(CreateOrUpdatePlayerAllergyInput input)
        {
            var result = new GetStandardOutput();
            try
            {
                if (input.Id > 0)
                {
                    var allergy = _playerAllergyRepository.FirstOrDefault(q => q.Id == input.Id);
                    if (allergy != null)
                    {
                        allergy.Description = input.Description;
                        allergy.AllergyType = input.AllergyType;
                    }
                }
                else
                {
                    _playerAllergyRepository.Insert(new PlayerAllergy()
                    {
                        AllergyType = input.AllergyType,
                        Description = input.Description,
                        PlayerId = input.PlayerId
                    });
                }
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                if (e.InnerException != null)
                {
                    result.Errors.Add(e.InnerException.Message);
                }
            }
            return result;
        }
        public async Task<GetStandardOutput> DeletePlayerAllergy(DeletePlayerAllergyInput input)
        {
            var result = new GetStandardOutput();
            try
            {
                var allergy = _playerAllergyRepository.FirstOrDefault(q => q.Id == input.Id);
                if (allergy != null)
                {
                    await _playerAllergyRepository.DeleteAsync(allergy.Id);
                }
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                if (e.InnerException != null)
                {
                    result.Errors.Add(e.InnerException.Message);
                }
            }
            return result;
        }
        public async Task<List<GetPlayerAllergyOutput>> GetPlayerAllergies(GetPlayerAllergiesInput input)
        {
            var results = new List<GetPlayerAllergyOutput>();
            try
            {
                results = (from a in _playerAllergyRepository.GetAll()
                           where a.PlayerId == input.PlayerId
                           select new GetPlayerAllergyOutput()
                           {
                               PlayerId = a.PlayerId,
                               AllergyType = a.AllergyType,
                               Description = a.Description,
                               Id = a.Id
                           }).OrderBy(o => o.Description).ThenBy(o => o.AllergyType)
                           .ToList();

                return await Task.FromResult(results);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<GetStandardOutput> CreateOrUpdatePlayerInstruction(CreateOrUpdatePlayerInstructionInput input)
        {
            var result = new GetStandardOutput();
            try
            {
                if (input.Id > 0)
                {
                    var allergy = _playerInstructionRepository.FirstOrDefault(q => q.Id == input.Id);
                    if (allergy != null)
                    {
                        allergy.Description = input.Description;
                    }
                }
                else
                {
                    _playerInstructionRepository.Insert(new PlayerInstruction()
                    {
                        Description = input.Description,
                        PlayerId = input.PlayerId
                    });
                }
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                if (e.InnerException != null)
                {
                    result.Errors.Add(e.InnerException.Message);
                }
            }
            return result;
        }
        public async Task<GetStandardOutput> DeletePlayerInstruction(DeletePlayerInstructionInput input)
        {
            var result = new GetStandardOutput();
            try
            {
                var instruction = _playerInstructionRepository.FirstOrDefault(q => q.Id == input.Id);
                if (instruction != null)
                {
                    await _playerInstructionRepository.DeleteAsync(instruction.Id);
                }
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                if (e.InnerException != null)
                {
                    result.Errors.Add(e.InnerException.Message);
                }
            }
            return result;
        }
        public async Task<List<GetPlayerInstructionOutput>> GetPlayerInstructions(GetPlayerInstructionsInput input)
        {
            var results = new List<GetPlayerInstructionOutput>();
            try
            {
                results = (from a in _playerInstructionRepository.GetAll()
                           where a.PlayerId == input.PlayerId
                           select new GetPlayerInstructionOutput()
                           {
                               PlayerId = a.PlayerId,
                               Description = a.Description,
                               Id = a.Id
                           }).OrderBy(o => o.Description)
                           .ToList();

                return await Task.FromResult(results);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<GetStandardOutput> CreateOrUpdatePlayerRestriction(CreateOrUpdatePlayerRestrictionInput input)
        {
            var result = new GetStandardOutput();
            try
            {
                if (input.Id > 0)
                {
                    var restriction = _playerRestrictionRepository.FirstOrDefault(q => q.Id == input.Id);
                    if (restriction != null)
                    {
                        restriction.Description = input.Description;
                        restriction.RestrictionType = input.RestrictionType;
                    }
                }
                else
                {
                    _playerRestrictionRepository.Insert(new PlayerRestriction()
                    {
                        Description = input.Description,
                        PlayerId = input.PlayerId,
                        RestrictionType = input.RestrictionType
                    });
                }
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                if (e.InnerException != null)
                {
                    result.Errors.Add(e.InnerException.Message);
                }
            }
            return result;
        }
        public async Task<GetStandardOutput> DeletePlayerRestriction(DeletePlayerRestrictionInput input)
        {
            var result = new GetStandardOutput();
            try
            {
                var restriction = _playerRestrictionRepository.FirstOrDefault(q => q.Id == input.Id);
                if (restriction != null)
                {
                    await _playerRestrictionRepository.DeleteAsync(restriction.Id);
                }
                await CurrentUnitOfWork.SaveChangesAsync();
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message);
                if (e.InnerException != null)
                {
                    result.Errors.Add(e.InnerException.Message);
                }
            }
            return result;
        }
        public async Task<List<GetPlayerRestrictionOutput>> GetPlayerRestrictions(GetPlayerRestrictionsInput input)
        {
            var results = new List<GetPlayerRestrictionOutput>();
            try
            {
                results = (from a in _playerRestrictionRepository.GetAll()
                           where a.PlayerId == input.PlayerId
                           select new GetPlayerRestrictionOutput()
                           {
                               PlayerId = a.PlayerId,
                               Description = a.Description,
                               Id = a.Id,
                               RestrictionType = a.RestrictionType
                           }).OrderBy(o => o.Description)
                           .ToList();

                return await Task.FromResult(results);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<GetStandardOutput> CreateOrUpdatePlayerFriend(CreateOrUpdatePlayerFriendInput input)
        {
            var result = new GetStandardOutput();
            try
            {
                if (input.Id != null && input.Id != Guid.Empty)
                {
                    var friends = _playerFriendsRepository.FirstOrDefault(q => q.Id == input.Id);
                    if (friends != null)
                    {
                        friends.StatusId = input.StatusId;
                        friends.LastModificationTime = DateTime.UtcNow;
                        friends.LastModifierUserId = AbpSession.GetUserId();
                    }
                }
                else
                {
                    var friends = _playerFriendsRepository.FirstOrDefault(
                        q => (q.PlayerOneId == input.FriendIdOne && q.PlayerTwoId == input.FriendIdTwo) ||
                        (q.PlayerOneId == input.FriendIdTwo && q.PlayerTwoId == input.FriendIdOne) &&
                        q.IsDeleted == false);
                    if (friends == null)
                    {
                        _playerFriendsRepository.Insert(new PlayerFriend()
                        {
                            StatusId = input.StatusId,
                            CreationTime = DateTime.UtcNow,
                            CreatorUserId = AbpSession.GetUserId(),
                            Id = Guid.NewGuid(),
                            PlayerOneId = input.FriendIdOne,
                            PlayerTwoId = input.FriendIdTwo,
                            IsDeleted = false
                        });
                    }
                    else
                    {
                        friends.StatusId = input.StatusId;
                    }
                }
            }
            catch (Exception e)
            {
                result.Errors.Add(e.Message + e.InnerException != null ? ". " + e.InnerException.Message : string.Empty);
            }

            return await Task.FromResult(result);
        }
        public async Task<List<GetPlayerOutput>> GetPlayerFriends(GetPlayerFriendsInput input)
        {
            var results = new List<GetPlayerOutput>();
            var friends = (from p in _playerRepository.GetAll()
                           join f in _playerFriendsRepository.GetAll() on p.Id equals f.PlayerOneId
                           where f.IsDeleted == false && f.StatusId == PlayerFriendStatus.Accepted
                           select p).ToList();
            friends.AddRange((from p in _playerRepository.GetAll()
                              join f in _playerFriendsRepository.GetAll() on p.Id equals f.PlayerTwoId
                              where f.IsDeleted == false && f.StatusId == PlayerFriendStatus.Accepted
                              select p).ToList());
            foreach (var friend in friends)
            {
                if (results.Where(q => q.Id == friend.Id).Count() == 0)
                {
                    var p = await FillPlayerInfo(friend);
                    results.Add(p);
                }
            }
            return await Task.FromResult(results);
        }

        private string GetPlayerCode()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789";
            var code1 = string.Empty;
            var code2 = string.Empty;
            var code = string.Empty;
            var valid = false;
            while (valid == false)
            {
                code1 = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());

                code2 = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());

                code = $"{code1}-{code2}";

                valid = _playerRepository.GetAll().Where(q => q.PlayerCode == code).Count() == 0;
            }
            return code;          
        }

    }
       
}
