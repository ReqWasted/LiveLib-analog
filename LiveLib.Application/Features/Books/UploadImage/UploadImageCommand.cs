using LiveLib.Application.Commom.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Books.UploadImage
{
	public record UploadImageCommand(Guid BookId, IFormFile Image) :IRequest<Result<string>> ;
}
