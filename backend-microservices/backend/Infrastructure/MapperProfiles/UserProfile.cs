// UserProfile.cs -
// ======================================================================0
// Crée par : pfraisse
// Fichier Crée le : 23/07/2024
// Fichier Modifié le : 23/07/2024
// Code développé pour le projet : backend

using AutoMapper;
using Core.DTOs.IdentityDtos;
using Core.Entities;

namespace Infrastructure.MapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AppUser, AppUserDto>().ReverseMap();
        CreateMap<UserInfo, UserInfoDto>().ReverseMap();
    }
}
