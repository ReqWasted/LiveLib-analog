using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LiveLib.Application.Interfaces;
using MediatR;

namespace LiveLib.Application.Features.Auth.Command
{
    public class UserLoginHandler : HandlerBase, IRequestHandler<UserLoginCommand>
    {
        public UserLoginHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
        {
        }

        public async Task Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {

        }
    }
}
