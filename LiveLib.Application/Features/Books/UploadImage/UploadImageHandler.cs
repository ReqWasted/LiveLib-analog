using AutoMapper;
using LiveLib.Application.Commom.Result;
using LiveLib.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveLib.Application.Features.Books.UploadImage
{
	public class UploadImageHandler : HandlerBase, IRequestHandler<UploadImageCommand, Result<string>>

	{
		private readonly ICacheProvider _cache;

		public UploadImageHandler(IMapper mapper, IDatabaseContext context, ICacheProvider cacheProvider) : base(mapper, context)
		{
			_cache = cacheProvider;
		}

		public async Task<Result<string>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
		{
			if (request.Image == null || request.Image.Length == 0)
			{
				return Result<string>.BadRequest("Image not found");
			}

			byte[] imageBytes;
			using (var ms = new MemoryStream())
			{
				await request.Image.CopyToAsync(ms, cancellationToken);
				imageBytes = ms.ToArray();
			}

			var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
			await _cache.BytesSetAsync($"image:{fileName}", imageBytes);

			var book = await _context.Books
			 .FirstOrDefaultAsync(g => g.Id == request.BookId, cancellationToken);

			if (book == null)
			{
				return Result<string>.NotFound($"Book {request.BookId} not found");
			}

			book.CoverId = fileName;
			await _context.SaveChangesAsync(cancellationToken);

			return Result<string>.Success(book.CoverId);
		}
	}
}
