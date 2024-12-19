using Abp.Application.Services;
using PlayDate.Common;
using PlayDate.Events.Dto;
using PlayDate.Players.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayDate.Players
{
    public interface IPlayerAppService : IApplicationService
    {
        Task<GetStandardOutput> CreateOrUpdatePlayer(CreateOrUpdatePlayerInput input);
        Task<List<GetPlayerOutput>> GetPlayers(GetPlayersInput input);
        Task<GetPlayerOutput> GetPlayerDetails(GetPlayerDetailsInput input);
        Task<GetStandardOutput> CreateOrUpdatePlayerAllergy(CreateOrUpdatePlayerAllergyInput input);
        Task<GetStandardOutput> DeletePlayerAllergy(DeletePlayerAllergyInput input);
        Task<List<GetPlayerAllergyOutput>> GetPlayerAllergies(GetPlayerAllergiesInput input);
        Task<GetStandardOutput> CreateOrUpdatePlayerInstruction(CreateOrUpdatePlayerInstructionInput input);
        Task<GetStandardOutput> DeletePlayerInstruction(DeletePlayerInstructionInput input);
        Task<List<GetPlayerInstructionOutput>> GetPlayerInstructions(GetPlayerInstructionsInput input);
        Task<GetStandardOutput> CreateOrUpdatePlayerRestriction(CreateOrUpdatePlayerRestrictionInput input);
        Task<GetStandardOutput> DeletePlayerRestriction(DeletePlayerRestrictionInput input);
        Task<List<GetPlayerRestrictionOutput>> GetPlayerRestrictions(GetPlayerRestrictionsInput input);
        Task<GetStandardOutput> CreateOrUpdatePlayerFriend(CreateOrUpdatePlayerFriendInput input);
        Task<List<GetPlayerOutput>> GetPlayerFriends(GetPlayerFriendsInput input);
    }
}
