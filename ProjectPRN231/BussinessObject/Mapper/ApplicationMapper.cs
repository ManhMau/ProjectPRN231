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
          
            CreateMap<BussinessObject.Models.Type,DocTypeMapper>().ReverseMap();

            CreateMap<Document, DocumentDTO>()
             .ForMember(dest => dest.TypeName, otp => otp.MapFrom(src => src.Type.TypeName))
             .ReverseMap();
            
            CreateMap<DocumentAddDTO, Document>()
       
       .ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();


            // Ánh xạ giữa GroupMember và GroupMemberDTO
            CreateMap<GroupMember, GroupMemberDTO>()
/*        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))  // Lấy UserId từ GroupMember
*/        .ReverseMap();

        }
    }
}
