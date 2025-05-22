using LiveLib.Application.Commom.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Books.CreateBook
{
	public class CreateBookCommand : IRequest<Result<int>>;
}
