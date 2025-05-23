using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LiveLib.Application.Commom.Mapping;
using LiveLib.Application.Commom.Result;
using LiveLib.Domain.Models;
using MediatR;

namespace LiveLib.Application.Features.Users.CreateUser
{
    public class CreateUserCommand : IRequest<Result<User>>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
