using AutoMapper;
using BussinessObject.DTOS;
using BussinessObject.Models;


namespace BussinessObject.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
          
            CreateMap<BussinessObject.Models.Type,DocTypeMapper>().ReverseMap();

            CreateMap<Document, DocumentDTO>()
             .ForMember(dest => dest.TypeName, otp => otp.MapFrom(src => src.Type.TypeName))
             .ReverseMap();
            
            CreateMap<DocumentAddDTO, Document>()
       
       .ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();


            CreateMap<GroupMember, GroupMemberDTO>()
            .ForMember(dest => dest.Users, otp => otp.MapFrom(src => src.Users))

       .ReverseMap();

        }
    }
}
