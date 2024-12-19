using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using PlayDate.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using PlayDate.Common;
using PlayDate.Players;
using PlayDate.Players.Dto;
using PlayDate.Friends.Dto;

namespace PlayDate.Friends
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class FriendAppService : PlayDateAppServiceBase, IFriendAppService
    {
        private readonly IRepository<Friend, Guid> _friendRepository;
        private readonly IRepository<Player, long> _playerRepository;

        public FriendAppService(
            IRepository<Friend, Guid> friendRepository,
            IRepository<Player, long> playerRepository
            )
        {
            _friendRepository = friendRepository;
            _playerRepository = playerRepository;
        }
        
        public async Task<GetStandardOutput> CreateOrUpdateFriend(CreateOrUpdateFriendInput input)
        {            
            var result = new GetStandardOutput();
            try
            {
                if (input.Id != null && input.Id != Guid.Empty)
                {
                    var friends = _friendRepository.FirstOrDefault(q => q.Id == input.Id);
                    if (friends != null)
                    {
                        friends.StatusId = input.StatusId;
                        friends.LastModificationTime = DateTime.UtcNow;
                        friends.LastModifierUserId = AbpSession.GetUserId();
                    }
                }
                else
                {
                    var friends = _friendRepository.FirstOrDefault(
                        q => (q.PlayerOneId == input.FriendIdOne && q.PlayerTwoId == input.FriendIdTwo) ||
                        (q.PlayerOneId == input.FriendIdTwo && q.PlayerTwoId == input.FriendIdOne) &&
                        q.IsDeleted == false);
                    if (friends == null)
                    {
                        _friendRepository.Insert(new Friend()
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
                           join f in _friendRepository.GetAll() on p.Id equals f.PlayerOneId
                           where f.IsDeleted == false && f.StatusId == FriendStatus.Accepted
                           select p).ToList();
            friends.AddRange((from p in _playerRepository.GetAll()
                              join f in _friendRepository.GetAll() on p.Id equals f.PlayerTwoId
                              where f.IsDeleted == false && f.StatusId == FriendStatus.Accepted
                              select p).ToList());
            friends.ForEach(friend =>
            {
                if (results.Where(q => q.Id == friend.Id).Count() == 0)
                {
                    results.Add(new GetPlayerOutput()
                    {
                        Address = friend.Address,
                        Address2 = friend.Address2,
                        Age = friend.Age,
                        City = friend.City,
                        Country = friend.Country,
                        EmailAddress = string.Empty,
                        FullName = string.IsNullOrEmpty(friend.LastName) ? friend.FirstName : friend.FirstName + " " + friend.LastName,
                        HasEpiPen = friend.HasEpiPen,
                        IsFoodAllergy = friend.IsFoodAllergy,
                        IsFoodRestricted = friend.IsFoodRestricted,
                        IsOtherAllergy = friend.IsOtherAllergy,
                        IsOtherRestricted = friend.IsOtherRestricted,
                        IsPetAllergy = friend.IsPetAllergy,
                        IsSpecialInstructions = friend.IsSpecialInstructions,
                        IsSwimmer = friend.IsSwimmer,
                        PostalCode = friend.PostalCode,
                        Province = friend.Province
                    });
                }
            });
            return await Task.FromResult(results);
        }
        
    }
}
