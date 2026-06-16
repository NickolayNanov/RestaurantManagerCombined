using AutoMapper;
using RestaurantManager.Application.Handlers.UserDetails.Create;
using RestaurantManager.Application.Handlers.UserDetails.GetByUserId;
using RestaurantManager.Application.Handlers.UserDetails.Update;
using RestaurantManager.Domain.Entities;

namespace RestaurantManager.Application.Mappings
{
    public class ProfileDetailsApplicationProfile : Profile
    {
        public ProfileDetailsApplicationProfile()
        {
            this.CreateMap<ProfileDetails, GetProfileDetailsByUserIdResponse>();

            this.CreateMap<CreateProfileDetailsCommand, ProfileDetails>();
            this.CreateMap<ProfileDetails, CreateProfileDetailsResponse>();

            this.CreateMap<UpdateProfileDetailsCommand, ProfileDetails>();
            this.CreateMap<ProfileDetails, UpdateProfileDetailsResponse>();
        }
    }
}
