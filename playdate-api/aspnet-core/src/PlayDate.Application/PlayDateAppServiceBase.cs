using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using PlayDate.Authorization.Users;
using PlayDate.MultiTenancy;
using PlayDate.Players.Dto;
using PlayDate.Players;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PlayDate;
/// <summary>
/// Derive your application services from this class.
/// </summary>
public abstract class PlayDateAppServiceBase : ApplicationService
{
    public TenantManager TenantManager { get; set; }

    public UserManager UserManager { get; set; }

    protected PlayDateAppServiceBase()
    {
        LocalizationSourceName = PlayDateConsts.LocalizationSourceName;
    }

    protected virtual async Task<User> GetCurrentUserAsync()
    {
        var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
        if (user == null)
        {
            throw new Exception("There is no current user!");
        }

        return user;
    }

    protected virtual Task<Tenant> GetCurrentTenantAsync()
    {
        return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
    }

    protected virtual void CheckErrors(IdentityResult identityResult)
    {
        identityResult.CheckErrors(LocalizationManager);
    }

    public virtual async Task<GetPlayerOutput> FillPlayerInfo(Player player)
    {
        var result = new GetPlayerOutput();

        result.Address = player.Address;
        result.Address2 = player.Address2;
        result.Age = player.Age;
        result.City = player.City;
        result.Country = player.Country;
        result.EmailAddress = string.Empty;
        result.Friends = new List<GetPlayerOutput>();
        result.FullName = string.IsNullOrEmpty(player.LastName) ? player.FirstName : player.FirstName + " " + player.LastName;
        result.HasEpiPen = player.HasEpiPen;
        result.Id = player.Id;
        result.IsFoodAllergy = player.IsFoodAllergy;
        result.IsFoodRestricted = player.IsFoodRestricted;
        result.IsOtherAllergy = player.IsOtherAllergy;
        result.IsOtherRestricted = player.IsOtherRestricted;
        result.IsPetAllergy = player.IsPetAllergy;
        result.IsSpecialInstructions = player.IsSpecialInstructions;
        result.IsSwimmer = player.IsSwimmer;
        result.PostalCode = player.PostalCode;
        result.Province = player.Province;
        result.UserName = string.IsNullOrEmpty(player.LastName) ? player.FirstName : player.FirstName + " " + player.LastName;
        result.IsNutAllergy = player.IsNutAllergy;
        result.IsEggAllergy = player.IsEggAllergy;
        result.IsDairyAllergy = player.IsDairyAllergy;
        result.IsDogAllergy = player.IsDogAllergy;
        result.IsCatAllergy = player.IsCatAllergy;
        result.IsVegetarian = player.IsVegetarian;
        result.IsVegan = player.IsVegan;


        return result;
    }
}