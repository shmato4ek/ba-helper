using AutoMapper;
using BAHelper.Common.DTOs.Document;
using BAHelper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
