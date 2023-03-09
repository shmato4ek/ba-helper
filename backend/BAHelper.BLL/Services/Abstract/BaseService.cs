using AutoMapper;
using BAHelper.DAL.Context;

namespace BAHelper.BLL.Services.Abstract
{
    public class BaseService
    {
        private protected readonly BAHelperDbContext _context;
        private protected readonly IMapper _mapper;
        public BaseService(BAHelperDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}