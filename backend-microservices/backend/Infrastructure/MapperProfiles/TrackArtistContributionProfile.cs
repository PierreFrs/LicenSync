using AutoMapper;
using Core.DTOs.TrackArtistContributionDto;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MapperProfiles
{
    public class TrackArtistContributionProfile : Profile
    {
        public TrackArtistContributionProfile() 
        { 
            CreateMap<TrackArtistContribution,  TrackArtistContributionDto>().ReverseMap();
        }
    }
}
