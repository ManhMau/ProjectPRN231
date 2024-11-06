using AutoMapper;
using BusinessObject.DTO;
using BussinessObject.DTOS;
using BussinessObject.Models;


namespace BussinessObject.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {

            CreateMap<BussinessObject.Models.Type, DocTypeMapper>().ReverseMap();

            CreateMap<Document, DocumentDTO>()
             .ForMember(dest => dest.TypeName, otp => otp.MapFrom(src => src.Type.TypeName))
             .ReverseMap();

            CreateMap<DocumentAddDTO, Document>().ReverseMap();

            // Mapping User -> UserDTO
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.GroupMember != null ? src.GroupMember.NameGroup : null))
                .ReverseMap();

            // Mapping User -> UserDTOUpdate
            CreateMap<User, UserDTOUpdate>().ReverseMap();


            // Ánh xạ giữa GroupMember và GroupMemberDTO
            CreateMap<GroupMember, GroupMemberDTO>()
            .ForMember(dest => dest.Users, otp => otp.MapFrom(src => src.Users))

       .ReverseMap();

        }
    }
}
