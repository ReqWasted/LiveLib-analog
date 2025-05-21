using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LiveLib.Application.Interfaces;

namespace LiveLib.Application.Features
{
    public class HandlerBase
    {
        protected readonly IMapper _mapper;
        protected readonly IDatabaseContext _context;

        public HandlerBase(IMapper mapper, IDatabaseContext context)
        {
            _mapper = mapper;
            _context = context;
        }
    }
}
