using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using LiveLib.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Books.CreateBook
{
	public class CreateBookHandler : HandlerBase, IRequestHandler<CreateBookCommand, Result<int>>
	{
		public CreateBookHandler(IMapper mapper, IDatabaseContext context) : base(mapper, context)
		{

		}

		public async Task<Result<int>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
		{
			var book = _mapper.Map<Book>(request);

			if (book == null)
			{
				return Result<int>.Failure("W");
			}

			throw new NotImplementedException();
		}
	}
}
