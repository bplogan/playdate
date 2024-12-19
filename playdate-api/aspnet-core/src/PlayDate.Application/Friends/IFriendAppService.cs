using Abp.Application.Services;
using PlayDate.Common;
using PlayDate.Events.Dto;
using PlayDate.Friends.Dto;
using PlayDate.Players.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayDate.Friends
{
    public interface IFriendAppService : IApplicationService
    {
        Task<GetStandardOutput> CreateOrUpdateFriend(CreateOrUpdateFriendInput input);
        Task<List<GetPlayerOutput>> GetPlayerFriends(GetPlayerFriendsInput input);
    }
}
