using AutoMapper;
namespace DocumentAccessApprovalSystemAPI.Profiles
{
    public class AccessRequestProfile : Profile
    {
        public AccessRequestProfile()
        {
            CreateMap<Models.AccessRequestForCreationDto, Entities.AccessRequest>();
            CreateMap<Entities.AccessRequest, Models.AccessRequestDto>();
        }
    }
}
