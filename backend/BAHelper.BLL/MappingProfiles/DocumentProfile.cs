using AutoMapper;
using BAHelper.Common.DTOs.Document;
using BAHelper.DAL.Entities;

namespace BAHelper.BLL.MappingProfiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<NewDocumentDto, Document>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<DocumentDTO, Document>();
        }
    }
}
